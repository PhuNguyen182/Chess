using Chess.Scripts.Enums;
using Chess.Scripts.DataStructs;
using Chess.Scripts.GridSlot;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chess.Scripts.BoardTasks.CheckPieceTasks
{
    public class CheckKnightPieceMoveTask : BaseCheckPieceMoveTask
    {
        private Vector3[] checkPositions = new Vector3[8];

        public CheckKnightPieceMoveTask(GridCellManager gridCellManager)
        {
            _gridCellManager = gridCellManager;
        }

        public override void Check(Vector3 position, out List<AvailableCellData> availablePosition)
        {
            _availablePositions.Clear();

            int x = (int)position.x;
            int z = (int)position.z;

            Vector3 checkPosition = new Vector3(x, 0, z);
            IGridCell knightCell = _gridCellManager.Get(checkPosition);

            checkPositions[0] = checkPosition + new Vector3(1, 0, 2);
            checkPositions[1] = checkPosition + new Vector3(-1, 0, 2);
            checkPositions[2] = checkPosition + new Vector3(-2, 0, 1);
            checkPositions[3] = checkPosition + new Vector3(-2, 0, -1);
            checkPositions[4] = checkPosition + new Vector3(-1, 0, -2);
            checkPositions[5] = checkPosition + new Vector3(1, 0, -2);
            checkPositions[6] = checkPosition + new Vector3(2, 0, -1);
            checkPositions[7] = checkPosition + new Vector3(2, 0, 1);

            for (int i = 0; i < 8; i++)
            {
                if (!IsPositionInBound(checkPositions[i]))
                    continue;

                if (CheckPieceMoveable(knightCell, checkPositions[i]))
                    _availablePositions.Add(new AvailableCellData
                    {
                        Position = checkPositions[i],
                        MoveType = AvailableMoveEnum.NormalMove
                    });

                else if (CheckOccupiable(knightCell, checkPositions[i]))
                    _availablePositions.Add(new AvailableCellData
                    {
                        Position = checkPositions[i],
                        MoveType = AvailableMoveEnum.OccupyMove
                    });
            }

            availablePosition = _availablePositions;
        }
    }
}
