using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Chess.Scripts.GridSlot;
using Chess.Scripts.DataStructs;

namespace Chess.Scripts.BoardTasks
{
    public class GridCellManager : IDisposable
    {
        private Dictionary<Vector3, IGridCell> _kvp;

        public GridCellManager()
        {
            _kvp = new Dictionary<Vector3, IGridCell>();
        }

        public void Add(Vector3 key, IGridCell value)
        {
            if (!_kvp.ContainsKey(key))
            {
                _kvp.Add(key, value);
            }
        }

        public void AddRange(PieceKeyValuePair[] pieces)
        {
            GridCell gridCell;
            for (int i = 0; i < pieces.Length; i++)
            {
                gridCell = new GridCell(pieces[i].Position, pieces[i].Piece);
                gridCell.SetGridSlotCell(pieces[i].GridSlotCell);
                Add(pieces[i].Position, gridCell);
            }
        }

        public IGridCell Get(Vector3 position)
        {
            if (_kvp.ContainsKey(position))
                return _kvp[position];

            return null;
        }

        public void Dispose()
        {
            _kvp.Clear();   
        }
    }
}
