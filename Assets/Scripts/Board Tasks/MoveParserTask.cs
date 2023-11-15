using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Chess.Scripts.GridSlot;
using MessageBrokers;
using Chess.Scripts.Messages;
using Cysharp.Threading.Tasks;
using System.Threading;
using Chess.Scripts.Enums;

namespace Chess.Scripts.BoardTasks
{
    public class MoveParserTask : IDisposable
    {
        private readonly GridCellManager _gridCellManager;
        private readonly PieceExecutingTask _pieceExecutingTask;

        private string[] _bestMove;
        private CancellationToken _cancellationToken;
        private CancellationTokenSource _tokenSource;

        public MoveParserTask(GridCellManager gridCellManager, PieceExecutingTask pieceExecutingTask)
        {
            _tokenSource = new();
            _cancellationToken = _tokenSource.Token;

            _gridCellManager = gridCellManager;
            _pieceExecutingTask = pieceExecutingTask;

            MessageBroker.Default.Subscribe<BestMoveMessage>(value =>
            {
                SetMove(value.BestMove).Forget();
            });
        }

        public async UniTask SetMove(string bestMoveMessage)
        {
            char promotionSign;
            _bestMove = bestMoveMessage.Split(' '); // Get best move string for chess
            var (pos1, pos2) = GetCellsFromSign(_bestMove[1], out promotionSign);

            IGridCell cell1 = _gridCellManager.Get(pos1);
            IGridCell cell2 = _gridCellManager.Get(pos2);

            _pieceExecutingTask.TakePieceCell(cell1, false);
            
            await UniTask.Delay(TimeSpan.FromSeconds(0.2f), cancellationToken: _cancellationToken);
            if (_cancellationToken.IsCancellationRequested)
                return;

            _pieceExecutingTask.TakePieceCell(cell2, false, GetPieceNameFromSign(promotionSign));
        }

        private (Vector3, Vector3) GetCellsFromSign(string move, out char promotion)
        {
            char[] cellElement = move.ToCharArray();
            Vector3 pos1 =  GetPositionFromSign(cellElement[0], cellElement[1]);
            Vector3 pos2 = GetPositionFromSign(cellElement[2], cellElement[3]);

            if (cellElement.Length == 5)
                promotion = cellElement[4];
            else
                promotion = default;

            return (pos1, pos2);
        }

        private Vector3 GetPositionFromSign(char column, char row)
        {
            int x = column - 97;
            int z = int.Parse(new ReadOnlySpan<char>(new char[] { row }));
            return new Vector3(x, 0, z - 1);
        }

        private PromotionEnum GetPieceNameFromSign(char c)
        {
            PromotionEnum piece;
            switch (c)
            {
                case 'q':
                    piece = PromotionEnum.Queen;
                    break;
                case 'r':
                    piece = PromotionEnum.Rook;
                    break;
                case 'b':
                    piece = PromotionEnum.Bishop;
                    break;
                case 'n':
                    piece = PromotionEnum.Knight;
                    break;
                default:
                    piece = PromotionEnum.None;
                    break;
            }

            return piece;
        }

        public void Dispose()
        {
            _tokenSource.Cancel();
            _tokenSource.Dispose();
        }
    }
}
