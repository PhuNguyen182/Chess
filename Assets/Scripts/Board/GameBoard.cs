using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Chess.Scripts.DataStructs;
using Chess.Scripts.BoardTasks;
using Chess.Scripts.GridSlot;
using Chess.Scripts.Database;
using Chess.Scripts.BoardTasks.CheckPieceTasks;
using MessageBrokers;
using CommandPattern;

namespace Chess.Scripts.Board
{
    public class GameBoard : MonoBehaviour
    {
        [SerializeField] private bool check;
        [SerializeField] private BoardInput boardInput;
        [SerializeField] private PieceKeyValuePair[] kings;
        [SerializeField] private PieceKeyValuePair[] queens;
        [SerializeField] private PieceKeyValuePair[] rooks;
        [SerializeField] private PieceKeyValuePair[] bishops;
        [SerializeField] private PieceKeyValuePair[] knights;
        [SerializeField] private PieceKeyValuePair[] pawns;
        [SerializeField] private GridSlotCell gridSlotCell;
        [SerializeField] private Transform gridContainer;
        [SerializeField] private PieceRenderDatabase pieceRenderDatabase;

        private GridCellManager _gridCellManager;
        private CheckPieceMoveTask _checkPieceMove;
        private PieceExecutingTask _pieceExecutingTask;
        private BoardInputTask _boardInputTask;
        private MoveParserTask _moveParserTask;
        private PieceRendererManager _pieceRendererManager;

        private void Awake()
        {
            Initialize();
            BuildChessBoard();
        }

        private void Initialize()
        {
            _gridCellManager = new GridCellManager();
            _checkPieceMove = new CheckPieceMoveTask(_gridCellManager);
            _pieceExecutingTask = new PieceExecutingTask(_gridCellManager, _checkPieceMove);
            _boardInputTask = new BoardInputTask(_gridCellManager, boardInput, _pieceExecutingTask);

            _pieceExecutingTask.SetBoardInput(_boardInputTask);
            _boardInputTask.IsLocked = false;

            _moveParserTask = new MoveParserTask(_gridCellManager, _pieceExecutingTask);
            _pieceRendererManager = new PieceRendererManager(pieceRenderDatabase);
        }

        private void BuildChessBoard()
        {
            _gridCellManager.AddRange(kings);
            _gridCellManager.AddRange(queens);
            _gridCellManager.AddRange(rooks);
            _gridCellManager.AddRange(bishops);
            _gridCellManager.AddRange(knights);
            _gridCellManager.AddRange(pawns);

            GridCell gridCell;
            for (int i = 0; i <= 7; i++)
            {
                for (int j = 2; j <= 5; j++)
                {
                    var slotCell = Instantiate(gridSlotCell, new Vector3(i, 0, j), Quaternion.identity, gridContainer);
                    gridCell = new GridCell(new Vector3(i, 0, j), null);
                    gridCell.SetGridSlotCell(slotCell);
                    _gridCellManager.Add(gridCell.Position, gridCell);
                }
            }
        }

        private void SetValue(PieceKeyValuePair[] pieces)
        {
            for (int i = 0; i < pieces.Length; i++)
            {
                Vector3 position = pieces[i].Piece.transform.position;
                position.y = 0;
                pieces[i].GridSlotCell = Instantiate(gridSlotCell, position, Quaternion.identity, gridContainer);
                pieces[i].Position = position;
            }
        }

        public void Dispose()
        {
            ChessEngineCommunicator.Stop();
            ChessEngineCommunicator.Quit();

            _gridCellManager.Dispose();
            _checkPieceMove.Dispose();
            _pieceExecutingTask.Dispose();
            _moveParserTask.Dispose();

            MessageBroker.Default.Dispose();
            CommandManager.Dispose();
        }

        private void OnDestroy()
        {
            Dispose();
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            if (check)
            {
                check = false;

                SetValue(kings);
                SetValue(queens);
                SetValue(rooks);
                SetValue(bishops);
                SetValue(knights);
                SetValue(pawns);
            }
        }
#endif
    }
}
