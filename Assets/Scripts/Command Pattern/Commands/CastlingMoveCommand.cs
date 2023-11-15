using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Chess.Scripts.GridSlot;
using Cysharp.Threading.Tasks;
using Chess.Scripts.Pieces;
using Chess.Scripts.Enums;
using Chess.Scripts.BoardTasks;
using System;
using System.Threading;

namespace CommandPattern
{
    public class CastlingMoveCommand : BaseCommand
    {
        private readonly IGridCell _originKingCell;
        private readonly IGridCell _originRookCell;
        private readonly IGridCell _toKingCell;
        private readonly IGridCell _toRookCell;

        private readonly Vector3 _originKingPos;
        private readonly Vector3 _originRookPos;
        private readonly Vector3 _toKingPos;
        private readonly Vector3 _toRookPos;

        private readonly IPiece _kingPiece;
        private readonly IPiece _rookPiece;

        private string _moveSign;
        private CancellationToken _token;
        private CancellationTokenSource _tokenSource;

        public CastlingMoveCommand(IGridCell kingCell, IGridCell toKingCell, IGridCell rookCell, IGridCell toRookCell)
        {
            _tokenSource = new();
            _token = _tokenSource.Token;

            _originKingCell = kingCell;
            _toKingCell = toKingCell;
            _originRookCell = rookCell;
            _toRookCell = toRookCell;

            _kingPiece = _originKingCell.Piece;
            _rookPiece = _originRookCell.Piece;

            _originKingPos = _originKingCell.Position;
            _originRookPos = _originRookCell.Position;

            _toKingPos = _toKingCell.Position;
            _toRookPos = _toRookCell.Position;
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
            if (_originKingCell == null || _originRookCell == null)
                return;

            int depth;
            if (_originKingCell.Piece.Team == TeamEnum.White)
            {
                depth = 10;
                _moveSign = _rookPiece.StartPosition == RolePositionEnum.Right ? "e1g1" : "e1c1";
            }
            else
            {
                depth = 20;
                _moveSign = _rookPiece.StartPosition == RolePositionEnum.Right ? "e8g8" : "e8c8";
            }

            _originKingCell.SetPiece(null);
            _originRookCell.SetPiece(null);

            _kingPiece.Move(_toKingPos).Forget();
            await _rookPiece.Move(_toRookPos);

            _toKingCell.SetPiece(_kingPiece);
            _toRookCell.SetPiece(_rookPiece);

            await UniTask.Delay(TimeSpan.FromSeconds(0.2f), cancellationToken: _token);
            if (_token.IsCancellationRequested)
                return;

            ChessEngineCommunicator.GetNextMove(_moveSign, depth);
        }

        private async UniTask ExecuteUndo()
        {
            _originKingCell.SetPiece(_kingPiece);
            _originRookCell.SetPiece(_rookPiece);

            _kingPiece.Move(_originKingPos).Forget();
            await _rookPiece.Move(_originRookPos);

            _toKingCell.SetPiece(null);
            _toRookCell.SetPiece(null);
        }

        public override void Dispose()
        {
            _tokenSource.Cancel();
            _tokenSource.Dispose();
        }
    }
}
