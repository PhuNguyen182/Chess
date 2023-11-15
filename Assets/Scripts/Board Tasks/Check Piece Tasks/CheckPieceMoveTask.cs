using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Chess.Scripts.GridSlot;
using Chess.Scripts.Enums;
using Chess.Scripts.DataStructs;

namespace Chess.Scripts.BoardTasks.CheckPieceTasks
{
    public class CheckPieceMoveTask : IDisposable
    {
        private readonly GridCellManager _gridCellManager;
        private readonly CheckKingPieceMoveTask _checkKingPieceMoveTask;
        private readonly CheckQueenPieceMoveTask _checkQueenPieceMoveTask;
        private readonly CheckRookPieceMoveTask _checkRookPieceMoveTask;
        private readonly CheckBishopPieceMoveTask _checkBishopPieceMoveTask;
        private readonly CheckKnightPieceMoveTask _checkKnightPieceMoveTask;
        private readonly CheckPawnPieceMoveTask _checkPawnPieceMoveTask;

        public CheckPieceMoveTask(GridCellManager gridCellManager)
        {
            _gridCellManager = gridCellManager;
            _checkKingPieceMoveTask = new CheckKingPieceMoveTask(_gridCellManager);
            _checkQueenPieceMoveTask = new CheckQueenPieceMoveTask(_gridCellManager);
            _checkRookPieceMoveTask = new CheckRookPieceMoveTask(_gridCellManager);
            _checkBishopPieceMoveTask = new CheckBishopPieceMoveTask(_gridCellManager);
            _checkKnightPieceMoveTask = new CheckKnightPieceMoveTask(_gridCellManager);
            _checkPawnPieceMoveTask = new CheckPawnPieceMoveTask(_gridCellManager);
        }

        public void CheckMoveable(Vector3 position, out List<AvailableCellData> moveablePositions)
        {
            List<AvailableCellData> positions = new List<AvailableCellData>();
            IGridCell pieceCell = _gridCellManager.Get(position);

            switch (pieceCell.Piece.PieceName)
            {
                case PieceEnums.King:
                    _checkKingPieceMoveTask.Check(position, out positions);
                    break;
                case PieceEnums.Queen:
                    _checkQueenPieceMoveTask.Check(position, out positions);
                    break;
                case PieceEnums.Rook:
                    _checkRookPieceMoveTask.Check(position, out positions);
                    break;
                case PieceEnums.Bishop:
                    _checkBishopPieceMoveTask.Check(position, out positions);
                    break;
                case PieceEnums.Knight:
                    _checkKnightPieceMoveTask.Check(position, out positions);
                    break;
                case PieceEnums.Pawn:
                    _checkPawnPieceMoveTask.Check(position, out positions);
                    break;
            }

            moveablePositions = positions;
        }

        public void Dispose()
        {
            _checkKingPieceMoveTask.Dispose();
            _checkQueenPieceMoveTask.Dispose();
            _checkRookPieceMoveTask.Dispose();
            _checkBishopPieceMoveTask.Dispose();
            _checkKnightPieceMoveTask.Dispose();
            _checkPawnPieceMoveTask.Dispose();
        }
    }
}
