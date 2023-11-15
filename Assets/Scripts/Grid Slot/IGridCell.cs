using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Chess.Scripts.Pieces;
using Chess.Scripts.GridSlot;

namespace Chess.Scripts.GridSlot
{
    public interface IGridCell
    {
        public bool HasPiece { get; }
        public Vector3 Position { get; }
        public IPiece Piece { get; }
        public IGridSlotCell GridSlotCell { get; }

        public void SetOccupyState(bool active);
        public void SetPiece(IPiece piece);
        public void SetGridSlotCell(IGridSlotCell gridSlotCell);
    }
}
