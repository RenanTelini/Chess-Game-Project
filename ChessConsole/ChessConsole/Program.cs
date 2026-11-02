using ChessConsole.Entities;
using ChessConsole.Entities.Chessboard;
using ChessConsole.Entities.ChessPieces;

namespace ChessConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Board board = new Board(8, 8);

            board.SetPiecePosition(new Rook(board, Entities.Enums.Color.Black), new Position(0, 0));
            board.SetPiecePosition(new Rook(board, Entities.Enums.Color.Black), new Position(1, 3));
            board.SetPiecePosition(new King(board, Entities.Enums.Color.Black), new Position(2, 4));

            Screen.DisplayBoard(board);
        }
    }
}