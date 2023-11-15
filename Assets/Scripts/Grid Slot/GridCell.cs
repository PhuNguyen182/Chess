using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Chess.Scripts.Pieces;

namespace Chess.Scripts.GridSlot
{
    public class GridCell : IGridCell
    {
        private IGridSlotCell _gridSlotCell;
        private Vector3 _position;
        private IPiece _piece;

        public bool HasPiece => Piece != null;
        public Vector3 Position => _position;
        public IPiece Piece => _piece;
        public IGridSlotCell GridSlotCell => _gridSlotCell;

        public GridCell(Vector3 position, IPiece piece)
        {
            _position = position;
            SetPiece(piece);
        }

        public void SetOccupyState(bool active)
        {
            _piece.SetOccupyState(active);
        }

        public void SetPiece(IPiece piece)
        {
            _piece = piece;
            if(piece != null)
            {
                piece.Position = Position;
            }
        }

        public void SetGridSlotCell(IGridSlotCell gridSlotCell)
        {
            _gridSlotCell = gridSlotCell;
        }
    }
}
