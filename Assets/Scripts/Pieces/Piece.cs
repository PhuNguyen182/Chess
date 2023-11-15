using System.Threading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Chess.Scripts.Enums;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Chess.Scripts.DataStructs;

namespace Chess.Scripts.Pieces
{
    public class Piece : MonoBehaviour, IPiece
    {
        [Header("Piece Configs")]
        [SerializeField] private float moveDuration = 1.5f;
        [SerializeField] private TeamEnum team;
        [SerializeField] private PieceEnums pieceName;
        [SerializeField] private RolePositionEnum position;

        private MeshFilter pieceMesh;
        private MeshRenderer pieceRenderer;

        private bool _hasBeenOccupied;
        private CancellationToken _cancellationToken;

        public string PositionSign 
        {
            get
            {
                char column = (char)((int)Position.x + 97);
                int row = (int)Position.z + 1;
                return $"{column}{row}";
            }
        }

        public Vector3 Position { get; set; }
        public bool HasBeenOccupied => _hasBeenOccupied;
        public TeamEnum Team => team;
        public PieceEnums PieceName => pieceName;
        public RolePositionEnum StartPosition
        {
            get => position;
            set => position = value;
        }

        private void Awake()
        {
            _cancellationToken = this.GetCancellationTokenOnDestroy();
            gameObject.name = $"{team} {pieceName}";

            pieceMesh = GetComponent<MeshFilter>();
            pieceRenderer = GetComponent<MeshRenderer>();
        }

        public async UniTask Move(Vector3 toPosition)
        {
            await transform.DOMove(toPosition, moveDuration)
                           .SetEase(Ease.Linear)
                           .WithCancellation(_cancellationToken);

            if (_cancellationToken.IsCancellationRequested)
                return;

            transform.position = toPosition;
        }

        public void SetOccupyState(bool isOccupied)
        {
            _hasBeenOccupied = isOccupied;
            gameObject.SetActive(isOccupied);
        }

        public void ChangePieceTo(PieceEnums targetPieceName, RolePositionEnum role)
        {
            pieceName = targetPieceName;
            position = role;
        }

        public void ChangePieceAppearance(PieceRenderData renderData)
        {
            pieceMesh.mesh = renderData.PieceMesh;
            pieceRenderer.material = renderData.PieceMaterial;
        }
    }
}
