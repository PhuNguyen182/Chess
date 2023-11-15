using Chess.Scripts.DataStructs;
using Chess.Scripts.Enums;
using Chess.Scripts.GridSlot;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chess.Scripts.BoardTasks.CheckPieceTasks
{
    public class CheckBishopPieceMoveTask : BaseCheckPieceMoveTask
    {
        public CheckBishopPieceMoveTask(GridCellManager gridCellManager)
        {
            _gridCellManager = gridCellManager;
        }

        public override void Check(Vector3 position, out List<AvailableCellData> availablePosition)
        {
            _availablePositions.Clear();

            int x = (int)position.x;
            int z = (int)position.z;

            Vector3 checkPosition;
            IGridCell bishopCell = _gridCellManager.Get(position);

            checkPosition = new Vector3(x, 0, z);
            while (IsPositionInBound(checkPosition)) // Check up right
            {
                checkPosition = checkPosition + new Vector3(1, 0, 1);
                if (CheckPieceMoveable(bishopCell, checkPosition))
                    _availablePositions.Add(new AvailableCellData
                    {
                        Position = checkPosition,
                        MoveType = AvailableMoveEnum.NormalMove
                    });

                else if (CheckOccupiable(bishopCell, checkPosition))
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
            while (IsPositionInBound(checkPosition)) // Check up left
            {
                checkPosition = checkPosition + new Vector3(-1, 0, 1);
                if (CheckPieceMoveable(bishopCell, checkPosition))
                    _availablePositions.Add(new AvailableCellData
                    {
                        Position = checkPosition,
                        MoveType = AvailableMoveEnum.NormalMove
                    });

                else if (CheckOccupiable(bishopCell, checkPosition))
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
            while (IsPositionInBound(checkPosition)) // Check down right
            {
                checkPosition = checkPosition + new Vector3(1, 0, -1);
                if (CheckPieceMoveable(bishopCell, checkPosition))
                    _availablePositions.Add(new AvailableCellData
                    {
                        Position = checkPosition,
                        MoveType = AvailableMoveEnum.NormalMove
                    });

                else if (CheckOccupiable(bishopCell, checkPosition))
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
            while (IsPositionInBound(checkPosition)) // Check down left
            {
                checkPosition = checkPosition + new Vector3(-1, 0, -1);
                if (CheckPieceMoveable(bishopCell, checkPosition))
                    _availablePositions.Add(new AvailableCellData
                    {
                        Position = checkPosition,
                        MoveType = AvailableMoveEnum.NormalMove
                    });

                else if (CheckOccupiable(bishopCell, checkPosition))
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
