using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chess.Scripts.GridSlot
{
    public interface IGridSlotCell
    {
        public Vector3 GridPosition { get; }
        public void ShowNormal();
        public void ShowAvailableMove();
        public void GlowCurrentCell();
        public void ShowOccupiableCell();
        public void ShowCheckmate();
        public void ShowSpecialMove();
    }
}
