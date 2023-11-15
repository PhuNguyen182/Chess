using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Chess.Scripts.Enums;
using Chess.Scripts.Board;
using Chess.Scripts.GridSlot;

namespace Chess.Scripts.BoardTasks
{
    public class BoardInputTask
    {
        private readonly GridCellManager _gridCellManager;
        private readonly PieceExecutingTask _pieceExecutingTask;

        private GridSlotCell _currentSlotCell;
        private BoardInput _boardInput;

        public bool IsLocked { get; set; }

        public BoardInputTask(GridCellManager gridCellManager, BoardInput boardInput, PieceExecutingTask pieceExecutingTask)
        {
            _gridCellManager = gridCellManager;
            _pieceExecutingTask = pieceExecutingTask;

            _boardInput = boardInput;
            _boardInput.OnSlotCell = OnSlotClick;
            _boardInput.OnSlotRelease = OnSlotRelease;
        }

        private void OnSlotClick(GridSlotCell cell)
        {
            if (!IsLocked)
            {
                _currentSlotCell = cell;
            }
        }

        private void OnSlotRelease()
        {
            if (!IsLocked)
            {
                if (_currentSlotCell == null)
                    return;

                IGridCell cell = _gridCellManager.Get(_currentSlotCell.GridPosition);
                _pieceExecutingTask.TakePieceCell(cell, promotionEnum: PromotionEnum.Queen);
            }
        }
    }
}
