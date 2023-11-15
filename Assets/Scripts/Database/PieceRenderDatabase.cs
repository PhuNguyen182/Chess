using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Chess.Scripts.DataStructs;

namespace Chess.Scripts.Database
{
    [CreateAssetMenu(fileName = "Piece Renderer Database", menuName = "Scriptable Objects/Databases/Piece Renderer Database", order = 1)]
    public class PieceRenderDatabase : ScriptableObject
    {
        [SerializeField] private TeamRenderData white;
        [SerializeField] private TeamRenderData black;

        public TeamRenderData White => white;
        public TeamRenderData Black => black;
    }
}
