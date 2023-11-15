using Chess.Scripts.GridSlot;
using System.Collections;
using System.Collections.Generic;
using Chess.Scripts.Enums;
using UnityEngine;
using Chess.Scripts.DataStructs;

namespace Chess.Scripts.BoardTasks.CheckPieceTasks
{
    public class CheckPawnPieceMoveTask : BaseCheckPieceMoveTask
    {
        private IGridCell pawnCell;
        private Vector3 _checkPosition1, _checkPosition2;
        private Vector3 _checkOccupyPosition1, _checkOccupyPosition2;

        public CheckPawnPieceMoveTask(GridCellManager gridCellManager)
        {
            _gridCellManager = gridCellManager;
        }

        public override void Check(Vector3 position, out List<AvailableCellData> availablePosition)
        {
            _availablePositions.Clear();

            int x = (int)position.x;
            int z = (int)position.z;

            Vector3 checkPosition = new Vector3(x, 0, z);
            pawnCell = _gridCellManager.Get(position);

            if (pawnCell.Piece.Team == TeamEnum.White)
            {
                if (z == 1)
                {
                    _checkPosition1 = checkPosition + Vector3.forward;
                    if (IsPositionInBound(_checkPosition1) && CheckPieceMoveable(pawnCell, _checkPosition1))
                        _availablePositions.Add(new AvailableCellData
                        {
                            Position = _checkPosition1,
                            MoveType = AvailableMoveEnum.NormalMove
                        });

                    _checkPosition2 = checkPosition + Vector3.forward * 2;
                    if (IsPositionInBound(_checkPosition2) && CheckPieceMoveable(pawnCell, _checkPosition2))
                        _availablePositions.Add(new AvailableCellData
                        {
                            Position = _checkPosition2,
                            MoveType = AvailableMoveEnum.NormalMove
                        });

                    _checkOccupyPosition1 = checkPosition + new Vector3(1, 0, 1);
                    if (IsPositionInBound(_checkOccupyPosition1) && CheckOccupiable(pawnCell, _checkOccupyPosition1))
                        _availablePositions.Add(new AvailableCellData
                        {
                            Position = _checkOccupyPosition1,
                            MoveType = AvailableMoveEnum.OccupyMove
                        });

                    _checkOccupyPosition2 = checkPosition + new Vector3(-1, 0, 1);
                    if (IsPositionInBound(_checkOccupyPosition2) && CheckOccupiable(pawnCell, _checkOccupyPosition2))
                        _availablePositions.Add(new AvailableCellData
                        {
                            Position = _checkOccupyPosition2,
                            MoveType = AvailableMoveEnum.OccupyMove
                        });
                }
                else
                {
                    _checkPosition1 = checkPosition + Vector3.forward;
                    if (IsPositionInBound(_checkPosition1))
                    {
                        if (CheckPromotion(_checkPosition1) && !CheckOccupiable(pawnCell, _checkPosition1))
                            _availablePositions.Add(new AvailableCellData
                            {
                                Position = _checkPosition1,
                                MoveType = AvailableMoveEnum.PromotionMove
                            });

                        else if (CheckPieceMoveable(pawnCell, _checkPosition1))
                            _availablePositions.Add(new AvailableCellData
                            {
                                Position = _checkPosition1,
                                MoveType = AvailableMoveEnum.NormalMove
                            });
                    }

                    _checkOccupyPosition1 = checkPosition + new Vector3(1, 0, 1);
                    if (IsPositionInBound(_checkOccupyPosition1) && CheckOccupiable(pawnCell, _checkOccupyPosition1))
                    {
                        if (CheckPromotion(_checkOccupyPosition1))
                            _availablePositions.Add(new AvailableCellData
                            {
                                Position = _checkOccupyPosition1,
                                MoveType = AvailableMoveEnum.PromotionMove
                            });
                        else
                            _availablePositions.Add(new AvailableCellData
                            {
                                Position = _checkOccupyPosition1,
                                MoveType = AvailableMoveEnum.OccupyMove
                            });
                    }

                    _checkOccupyPosition2 = checkPosition + new Vector3(-1, 0, 1);
                    if (IsPositionInBound(_checkOccupyPosition2) && CheckOccupiable(pawnCell, _checkOccupyPosition2))
                    {
                        if (CheckPromotion(_checkOccupyPosition1))
                            _availablePositions.Add(new AvailableCellData
                            {
                                Position = _checkOccupyPosition2,
                                MoveType = AvailableMoveEnum.PromotionMove
                            });
                        else
                            _availablePositions.Add(new AvailableCellData
                            {
                                Position = _checkOccupyPosition2,
                                MoveType = AvailableMoveEnum.OccupyMove
                            });
                    }
                }
            }

            else
            {
                if (z == 6)
                {
                    _checkPosition1 = checkPosition + Vector3.back;
                    if (IsPositionInBound(_checkPosition1) && CheckPieceMoveable(pawnCell, _checkPosition1))
                        _availablePositions.Add(new AvailableCellData
                        {
                            Position = _checkPosition1,
                            MoveType = AvailableMoveEnum.NormalMove
                        });

                    _checkPosition2 = checkPosition + Vector3.back * 2;
                    if (IsPositionInBound(_checkPosition2) && CheckPieceMoveable(pawnCell, _checkPosition2))
                        _availablePositions.Add(new AvailableCellData
                        {
                            Position = _checkPosition2,
                            MoveType = AvailableMoveEnum.NormalMove
                        });

                    _checkOccupyPosition1 = checkPosition + new Vector3(1, 0, -1);
                    if (IsPositionInBound(_checkOccupyPosition1) && CheckOccupiable(pawnCell, _checkOccupyPosition1))
                        _availablePositions.Add(new AvailableCellData
                        {
                            Position = _checkOccupyPosition1,
                            MoveType = AvailableMoveEnum.OccupyMove
                        });

                    _checkOccupyPosition2 = checkPosition + new Vector3(-1, 0, -1);
                    if (IsPositionInBound(_checkOccupyPosition2) && CheckOccupiable(pawnCell, _checkOccupyPosition2))
                        _availablePositions.Add(new AvailableCellData
                        {
                            Position = _checkOccupyPosition2,
                            MoveType = AvailableMoveEnum.OccupyMove
                        });
                }
                else
                {
                    _checkPosition1 = checkPosition + Vector3.back;
                    if (IsPositionInBound(_checkPosition1))
                    {
                        if (CheckPromotion(_checkPosition1) && !CheckOccupiable(pawnCell, _checkPosition1))
                            _availablePositions.Add(new AvailableCellData
                            {
                                Position = _checkPosition1,
                                MoveType = AvailableMoveEnum.PromotionMove
                            });

                        else if (CheckPieceMoveable(pawnCell, _checkPosition1))
                            _availablePositions.Add(new AvailableCellData
                            {
                                Position = _checkPosition1,
                                MoveType = AvailableMoveEnum.NormalMove
                            });
                    }

                    _checkOccupyPosition1 = checkPosition + new Vector3(1, 0, -1);
                    if (IsPositionInBound(_checkOccupyPosition1) && CheckOccupiable(pawnCell, _checkOccupyPosition1))
                    {
                        if (CheckPromotion(_checkOccupyPosition1))
                            _availablePositions.Add(new AvailableCellData
                            {
                                Position = _checkOccupyPosition1,
                                MoveType = AvailableMoveEnum.PromotionMove
                            });
                        else
                            _availablePositions.Add(new AvailableCellData
                            {
                                Position = _checkOccupyPosition1,
                                MoveType = AvailableMoveEnum.OccupyMove
                            });
                    }

                    _checkOccupyPosition2 = checkPosition + new Vector3(-1, 0, -1);
                    if (IsPositionInBound(_checkOccupyPosition2) && CheckOccupiable(pawnCell, _checkOccupyPosition2))
                    {
                        if (CheckPromotion(_checkOccupyPosition1))
                            _availablePositions.Add(new AvailableCellData
                            {
                                Position = _checkOccupyPosition2,
                                MoveType = AvailableMoveEnum.PromotionMove
                            });
                        else
                            _availablePositions.Add(new AvailableCellData
                            {
                                Position = _checkOccupyPosition2,
                                MoveType = AvailableMoveEnum.OccupyMove
                            });
                    }
                }
            }

            availablePosition = _availablePositions;
        }

        private bool CheckPromotion(Vector3 position)
        {
            return pawnCell.Piece.Team == TeamEnum.White ? 
                   position.z == 7 : position.z == 0;
        }

        private bool CheckEnPassant()
        {
            return false;
        }
    }
}
