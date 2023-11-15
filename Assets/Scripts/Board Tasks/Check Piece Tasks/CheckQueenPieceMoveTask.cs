using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Chess.Scripts.GridSlot;
using Chess.Scripts.DataStructs;
using Chess.Scripts.Enums;

namespace Chess.Scripts.BoardTasks.CheckPieceTasks
{
    public class CheckQueenPieceMoveTask : BaseCheckPieceMoveTask
    {
        public CheckQueenPieceMoveTask(GridCellManager gridCellManager)
        {
            _gridCellManager = gridCellManager;
        }

        public override void Check(Vector3 position, out List<AvailableCellData> availablePosition)
        {
            _availablePositions.Clear();

            int x = (int)position.x;
            int z = (int)position.z;

            Vector3 checkPosition;
            IGridCell queenCell = _gridCellManager.Get(position);

            checkPosition = new Vector3(x, 0, z);
            while(IsPositionInBound(checkPosition)) // Check right
            {
                checkPosition = checkPosition + Vector3.right;
                if (CheckPieceMoveable(queenCell, checkPosition))
                    _availablePositions.Add(new AvailableCellData
                    {
                        Position = checkPosition,
                        MoveType = AvailableMoveEnum.NormalMove
                    });

                else if (CheckOccupiable(queenCell, checkPosition))
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
                if (CheckPieceMoveable(queenCell, checkPosition))
                    _availablePositions.Add(new AvailableCellData
                    {
                        Position = checkPosition,
                        MoveType = AvailableMoveEnum.NormalMove
                    });

                else if (CheckOccupiable(queenCell, checkPosition))
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
            while (IsPositionInBound(checkPosition)) // Check up
            {
                checkPosition = checkPosition + Vector3.forward;
                if (CheckPieceMoveable(queenCell, checkPosition))
                    _availablePositions.Add(new AvailableCellData
                    {
                        Position = checkPosition,
                        MoveType = AvailableMoveEnum.NormalMove
                    });

                else if (CheckOccupiable(queenCell, checkPosition))
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
                if (CheckPieceMoveable(queenCell, checkPosition))
                    _availablePositions.Add(new AvailableCellData
                    {
                        Position = checkPosition,
                        MoveType = AvailableMoveEnum.NormalMove
                    });

                else if (CheckOccupiable(queenCell, checkPosition))
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
                checkPosition = checkPosition + Vector3.down;
                if (CheckPieceMoveable(queenCell, checkPosition))
                    _availablePositions.Add(new AvailableCellData
                    {
                        Position = checkPosition,
                        MoveType = AvailableMoveEnum.NormalMove
                    });

                else if (CheckOccupiable(queenCell, checkPosition))
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
            while (IsPositionInBound(checkPosition)) // Check up right
            {
                checkPosition = checkPosition + new Vector3(1, 0, 1);
                if (CheckPieceMoveable(queenCell, checkPosition))
                    _availablePositions.Add(new AvailableCellData
                    {
                        Position = checkPosition,
                        MoveType = AvailableMoveEnum.NormalMove
                    });

                else if (CheckOccupiable(queenCell, checkPosition))
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
                if (CheckPieceMoveable(queenCell, checkPosition))
                    _availablePositions.Add(new AvailableCellData
                    {
                        Position = checkPosition,
                        MoveType = AvailableMoveEnum.NormalMove
                    });

                else if (CheckOccupiable(queenCell, checkPosition))
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
                if (CheckPieceMoveable(queenCell, checkPosition))
                    _availablePositions.Add(new AvailableCellData
                    {
                        Position = checkPosition,
                        MoveType = AvailableMoveEnum.NormalMove
                    });

                else if (CheckOccupiable(queenCell, checkPosition))
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
                if (CheckPieceMoveable(queenCell, checkPosition))
                    _availablePositions.Add(new AvailableCellData
                    {
                        Position = checkPosition,
                        MoveType = AvailableMoveEnum.NormalMove
                    });

                else if (CheckOccupiable(queenCell, checkPosition))
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
