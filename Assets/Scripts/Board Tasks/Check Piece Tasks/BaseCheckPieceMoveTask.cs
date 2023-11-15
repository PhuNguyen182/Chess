using Chess.Scripts.DataStructs;
using Chess.Scripts.GridSlot;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chess.Scripts.BoardTasks.CheckPieceTasks
{
    public abstract class BaseCheckPieceMoveTask : IDisposable
    {
        protected GridCellManager _gridCellManager;

        protected List<AvailableCellData> _availablePositions = new List<AvailableCellData>();

        public abstract void Check(Vector3 position, out List<AvailableCellData> availablePosition);

        protected bool CheckPieceMoveable(IGridCell pieceCell, Vector3 checkPosition)
        {
            if (!IsPositionInBound(checkPosition))
                return false;

            if (IsPositionOvelayed(pieceCell.Position, checkPosition))
                return false;

            IGridCell checkCell = _gridCellManager.Get(checkPosition);

            if (checkCell == null)
                return false;

            if (checkCell.HasPiece)
                return false;

            return true;
        }

        public bool CheckOccupiable(IGridCell gridCell, Vector3 position)
        {
            if (gridCell == null)
                return false;

            if (!IsPositionInBound(position))
                return false;

            IGridCell cell = _gridCellManager.Get(position);
            if (cell == null)
                return false;

            if (cell.HasPiece)
                return cell.Piece.Team != gridCell.Piece.Team;

            return false;
        }

        protected bool IsPositionInBound(Vector3 position)
        {
            return position.x >= 0 && position.x <= 7 
                   && position.z >= 0 && position.z <= 7;
        }

        protected bool IsPositionOvelayed(Vector3 currentPosition, Vector3 checkPosition)
        {
            return currentPosition == checkPosition;
        }

        public void Dispose()
        {
            _availablePositions.Clear();
        }
    }
}
