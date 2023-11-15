using Chess.Scripts.DataStructs;
using Chess.Scripts.Enums;
using Chess.Scripts.GridSlot;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chess.Scripts.BoardTasks.CheckPieceTasks
{
    public class CheckRookPieceMoveTask : BaseCheckPieceMoveTask
    {
        public CheckRookPieceMoveTask(GridCellManager gridCellManager)
        {
            _gridCellManager = gridCellManager;
        }

        public override void Check(Vector3 position, out List<AvailableCellData> availablePosition)
        {
            _availablePositions.Clear();

            int x = (int)position.x;
            int z = (int)position.z;

            Vector3 checkPosition;
            IGridCell rookCell = _gridCellManager.Get(position);

            checkPosition = new Vector3(x, 0, z);
            while (IsPositionInBound(checkPosition)) // Check right
            {
                checkPosition = checkPosition + Vector3.right;
                if (CheckPieceMoveable(rookCell, checkPosition))
                    _availablePositions.Add(new AvailableCellData
                    {
                        Position = checkPosition,
                        MoveType = AvailableMoveEnum.NormalMove
                    });

                else if (CheckOccupiable(rookCell, checkPosition))
                {
                    _availablePositions.Add(new AvailableCellData
                    {
                        Position = checkPosition,
                        MoveType = AvailableMoveEnum.OccupyMove
                    });
                    break;
                }

                else break;
            }

            checkPosition = new Vector3(x, 0, z);
            while (IsPositionInBound(checkPosition)) // Check left
            {
                checkPosition = checkPosition + Vector3.left;
                if (CheckPieceMoveable(rookCell, checkPosition))
                    _availablePositions.Add(new AvailableCellData
                    {
                        Position = checkPosition,
                        MoveType = AvailableMoveEnum.NormalMove
                    });
                
                else if (CheckOccupiable(rookCell, checkPosition))
                {
                    _availablePositions.Add(new AvailableCellData
                    {
                        Position = checkPosition,
                        MoveType = AvailableMoveEnum.OccupyMove
                    });
                }
                
                else break;
            }

            checkPosition = new Vector3(x, 0, z);
            while (IsPositionInBound(checkPosition)) // Check up
            {
                checkPosition = checkPosition + Vector3.forward;
                if (CheckPieceMoveable(rookCell, checkPosition))
                    _availablePositions.Add(new AvailableCellData
                    {
                        Position = checkPosition,
                        MoveType = AvailableMoveEnum.NormalMove
                    });

                else if (CheckOccupiable(rookCell, checkPosition))
                {
                    _availablePositions.Add(new AvailableCellData
                    {
                        Position = checkPosition,
                        MoveType = AvailableMoveEnum.OccupyMove
                    });
                    break;
                }
                else break;
            }

            checkPosition = new Vector3(x, 0, z);
            while (IsPositionInBound(checkPosition)) // Check down
            {
                checkPosition = checkPosition + Vector3.back;
                if (CheckPieceMoveable(rookCell, checkPosition))
                    _availablePositions.Add(new AvailableCellData
                    {
                        Position = checkPosition,
                        MoveType = AvailableMoveEnum.NormalMove
                    });

                else if (CheckOccupiable(rookCell, checkPosition))
                {
                    _availablePositions.Add(new AvailableCellData
                    {
                        Position = checkPosition,
                        MoveType = AvailableMoveEnum.OccupyMove
                    });
                    break;
                }

                else break;
            }

            availablePosition = _availablePositions;
        }
    }
}
