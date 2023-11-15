using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Chess.Scripts.Enums;
using Chess.Scripts.Messages;
using Chess.Scripts.Database;
using MessageBrokers;
using Chess.Scripts.DataStructs;

namespace Chess.Scripts.BoardTasks
{
    public class PieceRendererManager
    {
        private readonly PieceRenderDatabase _pieceRenderDatabase;

        public PieceRendererManager(PieceRenderDatabase pieceRenderDatabase)
        {
            _pieceRenderDatabase = pieceRenderDatabase;
            MessageBroker.Default.Subscribe<ChangePieceMessage>(ChangePieceRender);
        }

        private void ChangePieceRender(ChangePieceMessage message)
        {
            TeamEnum team = message.Sender.Team;
            PieceEnums targetName = message.TargetName;
            RolePositionEnum role = message.Role;
            PieceRenderData pieceRender = default;

            switch (targetName)
            {
                case PieceEnums.King:
                    pieceRender = team == TeamEnum.White 
                                  ? _pieceRenderDatabase.White.King 
                                  : _pieceRenderDatabase.Black.King;
                    break;
                case PieceEnums.Queen:
                    pieceRender = team == TeamEnum.White 
                                  ? _pieceRenderDatabase.White.Queen 
                                  : _pieceRenderDatabase.Black.Queen;
                    break;
                case PieceEnums.Rook:
                    pieceRender = team == TeamEnum.White 
                                  ? _pieceRenderDatabase.White.Rook 
                                  : _pieceRenderDatabase.Black.Rook;
                    break;
                case PieceEnums.Bishop:
                    pieceRender = team == TeamEnum.White 
                                  ? _pieceRenderDatabase.White.Bishop 
                                  : _pieceRenderDatabase.Black.Bishop;
                    break;
                case PieceEnums.Knight:
                    pieceRender = team == TeamEnum.White 
                                  ? _pieceRenderDatabase.White.Knight 
                                  : _pieceRenderDatabase.Black.Knight;
                    break;
                case PieceEnums.Pawn:
                    pieceRender = team == TeamEnum.White 
                                  ? _pieceRenderDatabase.White.Pawn 
                                  : _pieceRenderDatabase.Black.Pawn;
                    break;
            }

            message.Sender.ChangePieceTo(targetName, role);
            message.Sender.ChangePieceAppearance(pieceRender);
        }
    }
}
