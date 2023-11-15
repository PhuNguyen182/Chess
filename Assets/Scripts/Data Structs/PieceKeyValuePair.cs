using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Chess.Scripts.Pieces;
using Chess.Scripts.GridSlot;

namespace Chess.Scripts.DataStructs
{
    [System.Serializable]
    public struct PieceKeyValuePair
    {
        public Vector3 Position;
        public GridSlotCell GridSlotCell;
        public Piece Piece;
    }
}
