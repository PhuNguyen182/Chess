using Chess.Scripts.Pieces;
using Chess.Scripts.Enums;

namespace Chess.Scripts.Messages
{
    public struct ChangePieceMessage
    {
        public RolePositionEnum Role;
        public PieceEnums TargetName;
        public IPiece Sender;
    }
}
