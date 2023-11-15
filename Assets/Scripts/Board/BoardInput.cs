using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Chess.Scripts.GridSlot;
using CommandPattern;
using Chess.Scripts.BoardTasks;

namespace Chess.Scripts.Board 
{
    public class BoardInput : MonoBehaviour
    {
        [SerializeField] private LayerMask castLayer;

        private Camera _mainCamera;
        private RaycastHit _raycastHit;
        private Ray _mouseRay;

        public Action<GridSlotCell> OnSlotCell;
        public Action OnSlotRelease;

        private void Awake()
        {
            _mainCamera = Camera.main;
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                _mouseRay = _mainCamera.ScreenPointToRay(Input.mousePosition);
                if(Physics.Raycast(_mouseRay, out _raycastHit, 100, castLayer))
                {
                    if (_raycastHit.collider.TryGetComponent<GridSlotCell>(out var cell))
                        OnSlotCell?.Invoke(cell);
                }
            }

            if (Input.GetMouseButtonUp(0))
            {
                OnSlotRelease?.Invoke();
            }

            if (Input.GetKeyDown(KeyCode.Z))
            {
                // Process undo
                CommandManager.Undo();
                GameBoardManager.SwitchTeam();
            }

            if (Input.GetKeyDown(KeyCode.Y))
            {
                // Process redo
                CommandManager.Redo();
                GameBoardManager.SwitchTeam();
            }
        }
    } 
}
