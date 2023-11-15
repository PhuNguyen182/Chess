using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Chess.Scripts.Enums;
using Chess.Scripts.DataStructs;
using Cysharp.Threading.Tasks;

namespace Chess.Scripts.Pieces
{
    public interface IPiece
    {
        public string PositionSign { get; }
        public Vector3 Position { get; set; }
        public bool HasBeenOccupied { get; }
        public TeamEnum Team { get; }
        public PieceEnums PieceName { get; }
        public RolePositionEnum StartPosition { get; set; }

        public UniTask Move(Vector3 toPosition);
        public void SetOccupyState(bool isOccupied);
        public void ChangePieceTo(PieceEnums targetPieceName, RolePositionEnum role);
        public void ChangePieceAppearance(PieceRenderData renderData);
    }
}
