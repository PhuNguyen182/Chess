using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chess.Scripts.GridSlot
{
    public class GridSlotCell : MonoBehaviour, IGridSlotCell
    {
        [SerializeField] private MeshRenderer gridRenderer;
        
        [Header("Color")]
        [SerializeField] private Color moveableSign;
        [SerializeField] private Color occupiableSign;
        [SerializeField] private Color glowSign;
        [SerializeField] private Color checkmateSign;
        [SerializeField] private Color specialMoveSign;

        private int _colorKey;
        private Vector3 _gridPosition;
        private MaterialPropertyBlock _materialPropertyBlock;

        public Vector3 GridPosition => _gridPosition;

        private void Awake()
        {
            int x = (int)transform.position.x;
            int z = (int)transform.position.z;
            _gridPosition = new Vector3(x, 0, z);
        }

        private void Start()
        {
            _colorKey = Shader.PropertyToID("_Color");
            _materialPropertyBlock = new MaterialPropertyBlock();
        }

        public void ShowNormal()
        {
            gridRenderer.gameObject.SetActive(false);
        }

        public void GlowCurrentCell()
        {
            gridRenderer.gameObject.SetActive(true);
            _materialPropertyBlock.SetColor(_colorKey, glowSign);
            gridRenderer.SetPropertyBlock(_materialPropertyBlock);
        }

        public void ShowAvailableMove()
        {
            gridRenderer.gameObject.SetActive(true);
            _materialPropertyBlock.SetColor(_colorKey, moveableSign);
            gridRenderer.SetPropertyBlock(_materialPropertyBlock);
        }

        public void ShowOccupiableCell()
        {
            gridRenderer.gameObject.SetActive(true);
            _materialPropertyBlock.SetColor(_colorKey, occupiableSign);
            gridRenderer.SetPropertyBlock(_materialPropertyBlock);
        }

        public void ShowCheckmate()
        {
            gridRenderer.gameObject.SetActive(true);
            _materialPropertyBlock.SetColor(_colorKey, checkmateSign);
            gridRenderer.SetPropertyBlock(_materialPropertyBlock);
        }

        public void ShowSpecialMove()
        {
            gridRenderer.gameObject.SetActive(true);
            _materialPropertyBlock.SetColor(_colorKey, specialMoveSign);
            gridRenderer.SetPropertyBlock(_materialPropertyBlock);
        }
    }
}
