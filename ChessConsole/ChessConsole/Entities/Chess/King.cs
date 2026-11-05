using ChessConsole.Entities.Chessboard;
using ChessConsole.Entities.Enums;

namespace ChessConsole.Entities.Chess
{
    class King : Piece
    {
        public King(Board board, Color color) : base(color, board)
        {
        }

        public override string ToString()
        {
            return "K";
        }
    }
}