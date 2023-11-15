using System;
using System.Threading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Chess.Scripts.BoardTasks.CheckPieceTasks;
using Cysharp.Threading.Tasks;
using Chess.Scripts.GridSlot;
using Chess.Scripts.Pieces;
using Chess.Scripts.Enums;
using CommandPattern;
using Chess.Scripts.DataStructs;

namespace Chess.Scripts.BoardTasks
{
    public class PieceExecutingTask : IDisposable
    {
        private readonly GridCellManager _gridCellManager;
        private readonly CheckPieceMoveTask _checkPieceMoveTask;
        private BoardInputTask _boardInputTask;

        private IGridCell _pieceCell1;
        private IGridCell _pieceCell2;

        private CancellationToken _token;
        private CancellationTokenSource _tokenSource;
        private List<AvailableCellData> _moveablePositions;

        public PieceExecutingTask(GridCellManager gridCellManager, CheckPieceMoveTask checkPieceMoveTask)
        {
            _tokenSource = new();
            _token = _tokenSource.Token;

            _gridCellManager = gridCellManager;
            _checkPieceMoveTask = checkPieceMoveTask;
        }

        public void SetBoardInput(BoardInputTask boardInputTask)
        {
            _boardInputTask = boardInputTask;
        }

        public void TakePieceCell(IGridCell gridCell, bool isHighlight = true, PromotionEnum promotionEnum = PromotionEnum.None)
        {
            if (gridCell == null)
                return;

            if (_pieceCell1 == null)
            {
                if (!gridCell.HasPiece)
                    return;

                if (gridCell.Piece.Team != GameBoardManager.CurrentTeam)
                    return;

                _pieceCell1 = gridCell;

                if (isHighlight)
                    _pieceCell1.GridSlotCell.GlowCurrentCell();

                _checkPieceMoveTask.CheckMoveable(_pieceCell1.Position, out _moveablePositions);

                if (isHighlight)
                    ShowMoveableCell(true);
            }

            else
            {
                if (isHighlight)
                    _pieceCell1.GridSlotCell.ShowNormal();

                if (_pieceCell1 == gridCell)
                {
                    _pieceCell1 = null;
                    _pieceCell2 = null;
                }

                else if (_pieceCell2 == null)
                {
                    _pieceCell2 = gridCell;
                    Execute(promotionEnum).Forget();
                }

                if (isHighlight)
                    ShowMoveableCell(false);
            }
        }

        private async UniTask Execute(PromotionEnum promotionEnum = PromotionEnum.None)
        {
            if (_pieceCell1 == null)
                return;
                
            if (_moveablePositions == null)
            {
                _pieceCell1 = null;
                _pieceCell2 = null;
                return;
            }
            
            if (_moveablePositions.Count <= 0)
            {
                _pieceCell1 = null;
                _pieceCell2 = null;
                return;
            }

            if (CheckPositionIsContained(_pieceCell2.Position, out var availableCell))
            {
                _boardInputTask.IsLocked = true;
                IPiece piece = _pieceCell1.Piece;

                CheckFirstMove(piece); // Check first move for castling move
                ImplementMove(availableCell.MoveType, promotionEnum);

                await UniTask.Delay(TimeSpan.FromSeconds(0.605f), cancellationToken: _token);
                if (_token.IsCancellationRequested) 
                    return;

                GameBoardManager.SwitchTeam();
            }

            _pieceCell1 = null;
            _pieceCell2 = null;
            _boardInputTask.IsLocked = false;
        }

        private void ImplementMove(AvailableMoveEnum moveEnum, PromotionEnum promotionEnum = PromotionEnum.None)
        {
            switch (moveEnum)
            {
                case AvailableMoveEnum.NormalMove:
                case AvailableMoveEnum.OccupyMove:
                    ChessMoveCommand moveCommand = new ChessMoveCommand(_pieceCell1, _pieceCell2);
                    CommandManager.AddCommand(moveCommand);
                    break;

                case AvailableMoveEnum.CastlingMoveRight:
                    int zR = _pieceCell1.Piece.Team == TeamEnum.White ? 0 : 7;
                    IGridCell rightRookCell = _gridCellManager.Get(new Vector3(7, 0, zR));
                    IGridCell toRightKingCell = _gridCellManager.Get(new Vector3(6, 0, zR));
                    IGridCell toRightRookCell = _gridCellManager.Get(new Vector3(5, 0, zR));

                    CastlingMoveCommand rightCastleCommand = new CastlingMoveCommand
                        (_pieceCell1, toRightKingCell, rightRookCell, toRightRookCell);
                    CommandManager.AddCommand(rightCastleCommand);
                    break;

                case AvailableMoveEnum.CastlingMoveLeft:
                    int zL = _pieceCell1.Piece.Team == TeamEnum.White ? 0 : 7;
                    IGridCell leftRookCell = _gridCellManager.Get(new Vector3(0, 0, zL));
                    IGridCell toLeftKingCell = _gridCellManager.Get(new Vector3(2, 0, zL));
                    IGridCell toLeftRookCell = _gridCellManager.Get(new Vector3(3, 0, zL));

                    CastlingMoveCommand leftCastleCommand = new CastlingMoveCommand
                        (_pieceCell1, toLeftKingCell, leftRookCell, toLeftRookCell);
                    CommandManager.AddCommand(leftCastleCommand);
                    break;

                case AvailableMoveEnum.EnpassantMove:
                    break;

                case AvailableMoveEnum.PromotionMove:
                    PromotionCommand promotionCommand = new PromotionCommand(_pieceCell1, _pieceCell2, promotionEnum);
                    CommandManager.AddCommand(promotionCommand);
                    break;
            }
        }

        private void ShowMoveableCell(bool moveable)
        {
            if (_moveablePositions == null)
                return;

            if (_moveablePositions.Count <= 0)
                return;

            IGridCell cell;

            if (moveable)
            {
                for (int i = 0; i < _moveablePositions.Count; i++)
                {
                    cell = _gridCellManager.Get(_moveablePositions[i].Position);
                    if (cell.HasPiece)
                    {
                        if (cell.Piece.PieceName == PieceEnums.King)
                            cell.GridSlotCell.ShowCheckmate();
                        else
                            cell.GridSlotCell.ShowOccupiableCell();
                    }
                    else
                    {
                        if (_moveablePositions[i].MoveType == AvailableMoveEnum.NormalMove)
                            cell.GridSlotCell.ShowAvailableMove();
                        else cell.GridSlotCell.ShowSpecialMove();
                    }
                }
            }

            else
            {
                for (int i = 0; i < _moveablePositions.Count; i++)
                {
                    cell = _gridCellManager.Get(_moveablePositions[i].Position);
                    cell.GridSlotCell.ShowNormal();
                }
            }
        }

        private void CheckFirstMove(IPiece piece)
        {
            if (piece.PieceName == PieceEnums.King)
            {
                if (piece.Team == TeamEnum.White && !GameBoardManager.WhiteKingMoved)
                    GameBoardManager.WhiteKingMoved = true;

                else if (piece.Team == TeamEnum.Black && !GameBoardManager.BlackKingMoved)
                    GameBoardManager.BlackKingMoved = true;
            }

            else if (piece.PieceName == PieceEnums.Rook)
            {
                if (piece.Team == TeamEnum.White)
                {
                    if (piece.StartPosition == RolePositionEnum.Left && !GameBoardManager.LeftWhiteRookMove)
                        GameBoardManager.LeftWhiteRookMove = true;
                    else if (piece.StartPosition == RolePositionEnum.Right && !GameBoardManager.RightWhiteRookMoved)
                        GameBoardManager.RightWhiteRookMoved = true;
                }

                else
                {
                    if (piece.StartPosition == RolePositionEnum.Left && !GameBoardManager.LeftBlackRookMove)
                        GameBoardManager.LeftBlackRookMove = true;
                    else if (piece.StartPosition == RolePositionEnum.Right && !GameBoardManager.RightBlackRookMoved)
                        GameBoardManager.RightBlackRookMoved = true;
                }
            }

        }

        private bool CheckPositionIsContained(Vector3 position, out AvailableCellData availableCell)
        {
            for (int i = 0; i < _moveablePositions.Count; i++)
            {
                if (position == _moveablePositions[i].Position)
                {
                    availableCell = _moveablePositions[i];
                    return true;
                }
            }

            availableCell = default;
            return false;
        }

        public void Dispose()
        {
            _moveablePositions?.Clear();
            _tokenSource.Cancel();
            _tokenSource.Dispose();
        }
    }
}
