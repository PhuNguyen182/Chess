using System;
using System.Threading;
using System.Collections;
using Chess.Scripts.Enums;
using Chess.Scripts.BoardTasks;
using Chess.Scripts.GridSlot;
using Chess.Scripts.Pieces;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace CommandPattern
{
    public class ChessMoveCommand : BaseCommand
    {
        private readonly IGridCell _firstCell;
        private readonly IGridCell _secondCell;

        private IPiece executedPiece; // first cell piece
        private IPiece occupiedPiece; // second cell piece

        private string _startPositionSign;
        private string _endPositionSign;
        private string _moveSign;

        private CancellationToken _token;
        private CancellationTokenSource _tokenSource;

        public ChessMoveCommand(IGridCell firstCell, IGridCell secondCell)
        {
            _tokenSource = new();
            _token = _tokenSource.Token;

            _firstCell = firstCell;
            _secondCell = secondCell;
        }

        public override void Execute()
        {
            ExecutePiece().Forget();
        }

        public override void Undo()
        {
            ExecuteUndo().Forget();
        }

        public override void Redo()
        {
            ExecutePiece().Forget();
        }

        private async UniTask ExecutePiece()
        {
            if (_secondCell == null || _firstCell == null)
                return;

            executedPiece = _firstCell.Piece;
            _startPositionSign = executedPiece.PositionSign;

            _firstCell.SetPiece(null);
            await executedPiece.Move(_secondCell.Position);
            occupiedPiece = _secondCell.Piece;

            if (_secondCell.HasPiece)
                _secondCell.SetOccupyState(false); // false means the piece is deleted

            _secondCell.SetPiece(executedPiece);

            _endPositionSign = _secondCell.Piece.PositionSign;
            _moveSign = $"{_startPositionSign}{_endPositionSign}";

            await UniTask.Delay(TimeSpan.FromSeconds(0.2f), cancellationToken: _token);
            if (_token.IsCancellationRequested)
                return;

            int depth = executedPiece.Team == TeamEnum.White ? 10 : 20;
            ChessEngineCommunicator.GetNextMove(_moveSign, depth);
        }

        private async UniTask ExecuteUndo()
        {
            if (_secondCell == null || _firstCell == null)
                return;

            if (occupiedPiece != null)
                occupiedPiece.SetOccupyState(true);

            _secondCell.SetPiece(occupiedPiece);
            await executedPiece.Move(_firstCell.Position);
            _firstCell.SetPiece(executedPiece);
        }

        public override void Dispose()
        {
            _tokenSource.Cancel();
            _tokenSource.Dispose();
        }
    }
}
