using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Chess.Scripts.BoardTasks;

namespace Chess.Scripts.Temps
{
    public class TestChessEngine : MonoBehaviour
    {
        private void Start()
        {
            ChessEngineCommunicator.StartCommunicate();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.M))
            {
                ChessEngineCommunicator.GetFirstMove("");
            }
        }
    }
}
