using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Chess.Scripts.GridSlot;
using Chess.Scripts.DataStructs;
using Chess.Scripts.Enums;

namespace Chess.Scripts.BoardTasks.CheckPieceTasks
{
    public class CheckKingPieceMoveTask : BaseCheckPieceMoveTask
    {
        private int x, y;
        private int left, right, up, down;
        IGridCell kingCell;

        public CheckKingPieceMoveTask(GridCellManager gridCellManager)
        {
            _gridCellManager = gridCellManager;
        }

        public override void Check(Vector3 position, out List<AvailableCellData> availablePosition)
        {
            _availablePositions.Clear();
            x = (int)position.x;
            y = (int)position.z;

            kingCell = _gridCellManager.Get(position);

            right = x + 1;
            left = x - 1;
            up = y + 1;
            down = y - 1;

            CheckNormalMove();
            CheckOccupiable();
            CheckCastleMove();

            availablePosition = _availablePositions;
        }

        private void CheckNormalMove()
        {
            if (CheckPieceMoveable(kingCell, new Vector3(x, 0, up)))
                _availablePositions.Add(new AvailableCellData
                {
                    Position = new Vector3(x, 0, up),
                    MoveType = AvailableMoveEnum.NormalMove
                });

            if (CheckPieceMoveable(kingCell, new Vector3(x, 0, down)))
                _availablePositions.Add(new AvailableCellData
                {
                    Position = new Vector3(x, 0, down),
                    MoveType = AvailableMoveEnum.NormalMove
                });

            if (CheckPieceMoveable(kingCell, new Vector3(right, 0, y)))
                _availablePositions.Add(new AvailableCellData
                {
                    Position = new Vector3(right, 0, y),
                    MoveType = AvailableMoveEnum.NormalMove
                });

            if (CheckPieceMoveable(kingCell, new Vector3(left, 0, y)))
                _availablePositions.Add(new AvailableCellData
                {
                    Position = new Vector3(left, 0, y),
                    MoveType = AvailableMoveEnum.NormalMove
                });

            if (CheckPieceMoveable(kingCell, new Vector3(right, 0, up)))
                _availablePositions.Add(new AvailableCellData
                {
                    Position = new Vector3(right, 0, up),
                    MoveType = AvailableMoveEnum.NormalMove
                });

            if (CheckPieceMoveable(kingCell, new Vector3(left, 0, up)))
                _availablePositions.Add(new AvailableCellData
                {
                    Position = new Vector3(left, 0, up),
                    MoveType = AvailableMoveEnum.NormalMove
                });

            if (CheckPieceMoveable(kingCell, new Vector3(right, 0, down)))
                _availablePositions.Add(new AvailableCellData
                {
                    Position = new Vector3(right, 0, down),
                    MoveType = AvailableMoveEnum.NormalMove
                });

            if (CheckPieceMoveable(kingCell, new Vector3(left, 0, down)))
                _availablePositions.Add(new AvailableCellData
                {
                    Position = new Vector3(left, 0, down),
                    MoveType = AvailableMoveEnum.NormalMove
                });
        }

        private void CheckOccupiable()
        {
            if (CheckOccupiable(kingCell, new Vector3(x, 0, up)))
                _availablePositions.Add(new AvailableCellData
                {
                    Position = new Vector3(x, 0, up),
                    MoveType = AvailableMoveEnum.OccupyMove
                });

            if (CheckOccupiable(kingCell, new Vector3(x, 0, down)))
                _availablePositions.Add(new AvailableCellData
                {
                    Position = new Vector3(x, 0, down),
                    MoveType = AvailableMoveEnum.OccupyMove
                });

            if (CheckOccupiable(kingCell, new Vector3(right, 0, y)))
                _availablePositions.Add(new AvailableCellData
                {
                    Position = new Vector3(right, 0, y),
                    MoveType = AvailableMoveEnum.OccupyMove
                });

            if (CheckOccupiable(kingCell, new Vector3(left, 0, y)))
                _availablePositions.Add(new AvailableCellData
                {
                    Position = new Vector3(left, 0, y),
                    MoveType = AvailableMoveEnum.OccupyMove
                });

            if (CheckOccupiable(kingCell, new Vector3(right, 0, up)))
                _availablePositions.Add(new AvailableCellData
                {
                    Position = new Vector3(right, 0, up),
                    MoveType = AvailableMoveEnum.OccupyMove
                });

            if (CheckOccupiable(kingCell, new Vector3(left, 0, up)))
                _availablePositions.Add(new AvailableCellData
                {
                    Position = new Vector3(left, 0, up),
                    MoveType = AvailableMoveEnum.OccupyMove
                });

            if (CheckOccupiable(kingCell, new Vector3(right, 0, down)))
                _availablePositions.Add(new AvailableCellData
                {
                    Position = new Vector3(right, 0, down),
                    MoveType = AvailableMoveEnum.OccupyMove
                });

            if (CheckOccupiable(kingCell, new Vector3(left, 0, down)))
                _availablePositions.Add(new AvailableCellData
                {
                    Position = new Vector3(left, 0, down),
                    MoveType = AvailableMoveEnum.OccupyMove
                });
        }

        private void CheckCastleMove()
        {
            bool kingMoved = kingCell.Piece.Team == TeamEnum.White
                            ? GameBoardManager.WhiteKingMoved
                            : GameBoardManager.BlackKingMoved;
            if (kingMoved)
                return;

            bool leftRookMoved, rightRookMoved;

            IGridCell queenCheck;
            IGridCell bishopCheck1, bishopCheck2;
            IGridCell knightCheck1, knightCheck2;

            Vector3 checkQueenPos = new Vector3(3, 0, 0);
            Vector3 checkBishopPos1, checkBishopPos2;
            Vector3 checkKnightPos1, checkKnightPos2;

            checkBishopPos1 = kingCell.Piece.Team == TeamEnum.White
                              ? new Vector3(5, 0, 0) : new Vector3(5, 0, 7);

            checkKnightPos1 = kingCell.Piece.Team == TeamEnum.White
                              ? new Vector3(6, 0, 0) : new Vector3(6, 0, 7);

            checkBishopPos2 = kingCell.Piece.Team == TeamEnum.White
                              ? new Vector3(2, 0, 0) : new Vector3(2, 0, 7);

            checkKnightPos2 = kingCell.Piece.Team == TeamEnum.White
                              ? new Vector3(1, 0, 0) : new Vector3(1, 0, 7);

            leftRookMoved = kingCell.Piece.Team == TeamEnum.White
                            ? GameBoardManager.LeftWhiteRookMove
                            : GameBoardManager.LeftBlackRookMove;

            rightRookMoved = kingCell.Piece.Team == TeamEnum.White
                             ? GameBoardManager.RightWhiteRookMoved
                             : GameBoardManager.RightBlackRookMoved;

            if (!rightRookMoved)
            {
                bishopCheck1 = _gridCellManager.Get(checkBishopPos1);
                knightCheck1 = _gridCellManager.Get(checkKnightPos1);

                if (!bishopCheck1.HasPiece && !knightCheck1.HasPiece)
                    _availablePositions.Add(new AvailableCellData
                    {
                        Position = checkKnightPos1,
                        MoveType = AvailableMoveEnum.CastlingMoveRight
                    });
            }

            if (!leftRookMoved)
            {
                queenCheck = _gridCellManager.Get(checkQueenPos);
                bishopCheck2 = _gridCellManager.Get(checkBishopPos2);
                knightCheck2 = _gridCellManager.Get(checkKnightPos2);

                if (!queenCheck.HasPiece && !bishopCheck2.HasPiece && !knightCheck2.HasPiece)
                    _availablePositions.Add(new AvailableCellData
                    {
                        Position = checkBishopPos2,
                        MoveType = AvailableMoveEnum.CastlingMoveLeft
                    });
            }
        }
    }
}
