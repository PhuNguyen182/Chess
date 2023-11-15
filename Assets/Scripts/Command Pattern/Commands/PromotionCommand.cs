using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Chess.Scripts.Enums;
using Chess.Scripts.Pieces;
using Chess.Scripts.Messages;
using Chess.Scripts.GridSlot;
using Chess.Scripts.BoardTasks;
using Cysharp.Threading.Tasks;
using MessageBrokers;
using System;
using System.Threading;

namespace CommandPattern
{
    public class PromotionCommand : BaseCommand
    {
        private readonly IGridCell _senderCell;
        private readonly IGridCell _targetCell;
        private readonly PieceEnums _targetName;

        private IPiece executedPiece; // first cell piece
        private IPiece occupiedPiece; // second cell piece

        private readonly PieceEnums _currentName;
        private readonly RolePositionEnum _currentRole;

        private string _startPositionSign;
        private string _endPositionSign;
        private string _moveSign;

        private CancellationToken _token;
        private CancellationTokenSource _tokenSource;

        public PromotionCommand(IGridCell senderCell, IGridCell targetCell, PromotionEnum targetName)
        {
            _tokenSource = new();
            _token = _tokenSource.Token;

            _senderCell = senderCell;
            _targetCell = targetCell;

            switch (targetName)
            {
                case PromotionEnum.None:
                    _targetName = PieceEnums.None;
                    break;
                case PromotionEnum.Queen:
                    _targetName = PieceEnums.Queen;
                    break;
                case PromotionEnum.Rook:
                    _targetName = PieceEnums.Rook;
                    break;
                case PromotionEnum.Bishop:
                    _targetName = PieceEnums.Bishop;
                    break;
                case PromotionEnum.Knight:
                    _targetName = PieceEnums.Knight;
                    break;
                default:
                    _targetName = PieceEnums.None;
                    break;
            }

            _currentName = _senderCell.Piece.PieceName;
            _currentRole = _senderCell.Piece.StartPosition;
        }

        public override void Dispose()
        {
            _tokenSource.Cancel();
            _tokenSource.Dispose();
        }

        public override void Execute()
        {
            ExecutePromotion().Forget();
        }

        public override void Redo()
        {
            
        }

        public override void Undo()
        {
            ExecuteUndo().Forget();
        }

        private char GetPromotionSign()
        {
            char p = default;
            switch (_targetName)
            {
                case PieceEnums.Queen:
                    p = 'q';
                    break;
                case PieceEnums.Rook:
                    p = 'r';
                    break;
                case PieceEnums.Bishop:
                    p = 'b';
                    break;
                case PieceEnums.Knight:
                    p = 'n';
                    break;
            }

            return p;
        }

        private async UniTask ExecutePromotion()
        {
            if (_targetCell == null || _senderCell == null)
                return;

            executedPiece = _senderCell.Piece;
            _startPositionSign = executedPiece.PositionSign;

            _senderCell.SetPiece(null);
            await executedPiece.Move(_targetCell.Position);
            occupiedPiece = _targetCell.Piece;

            if (_targetCell.HasPiece)
                _targetCell.SetOccupyState(false); // false means the piece is deleted

            _targetCell.SetPiece(executedPiece);

            char p = GetPromotionSign();
            _endPositionSign = _targetCell.Piece.PositionSign;
            _moveSign = $"{_startPositionSign}{_endPositionSign}{p}";

            await UniTask.Delay(TimeSpan.FromSeconds(0.2f), cancellationToken: _token);
            if (_token.IsCancellationRequested)
                return;

            MessageBroker.Default.Publish(new ChangePieceMessage
            {
                Role = RolePositionEnum.AllBoard,
                Sender = _senderCell.Piece,
                TargetName = _targetName
            });

            int depth = executedPiece.Team == TeamEnum.White ? 10 : 20;
            ChessEngineCommunicator.GetNextMove(_moveSign, depth);
        }

        private async UniTask ExecuteUndo()
        {
            if (_targetCell == null || _senderCell == null)
                return;

            if (occupiedPiece != null)
                occupiedPiece.SetOccupyState(true);

            _targetCell.SetPiece(occupiedPiece);
            await executedPiece.Move(_senderCell.Position);
            _senderCell.SetPiece(executedPiece);

            MessageBroker.Default.Publish(new ChangePieceMessage
            {
                Role = _currentRole,
                Sender = _senderCell.Piece,
                TargetName = _currentName
            });
        }
    }
}
