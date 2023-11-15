using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Chess.Scripts.Enums;

namespace Chess.Scripts.BoardTasks
{
    public static class GameBoardManager
    {
        private static TeamEnum _currentTeam = TeamEnum.White;

        public static bool WhiteKingMoved;
        public static bool BlackKingMoved;

        public static bool LeftWhiteRookMove;
        public static bool RightWhiteRookMoved;

        public static bool LeftBlackRookMove;
        public static bool RightBlackRookMoved;

        public static TeamEnum CurrentTeam => _currentTeam;

        public static void SwitchTeam()
        {
            _currentTeam = _currentTeam == TeamEnum.White ? TeamEnum.Black 
                                           : TeamEnum.White;
        }
    }
}
