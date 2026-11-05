using ChessConsole.Entities.Chessboard;
using ChessConsole.Entities.Enums;

namespace ChessConsole.Entities.Chess
{
    class Rook : Piece
    {
        public Rook(Board board, Color color) : base(color, board)
        {
        }

        public override string ToString()
        {
            return "R";
        }
    }
}