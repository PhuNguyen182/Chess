using System;
using UnityEngine;

namespace Chess.Scripts.DataStructs
{
    [Serializable]
    public struct PieceRenderData
    {
        public Mesh PieceMesh;
        public Material PieceMaterial;
    }

    [Serializable]
    public struct TeamRenderData
    {
        public PieceRenderData King;
        public PieceRenderData Queen;
        public PieceRenderData Rook;
        public PieceRenderData Bishop;
        public PieceRenderData Knight;
        public PieceRenderData Pawn;
    }
}
