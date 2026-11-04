using ChessConsole.Entities;
using ChessConsole.Exceptions;
using ChessConsole.Entities.Chessboard;
using ChessConsole.Entities.ChessPieces;

namespace ChessConsole
{
    class Program
    {
        static void Main(string[] args)
        {

            try
            {

                Board board = new Board(8, 8);

                board.SetPiecePosition(new Rook(board, Entities.Enums.Color.Black), new Position(0, 0));
                board.SetPiecePosition(new Rook(board, Entities.Enums.Color.Black), new Position(1, 3));
                board.SetPiecePosition(new King(board, Entities.Enums.Color.Black), new Position(0, 2));

                board.SetPiecePosition(new Rook(board, Entities.Enums.Color.White), new Position(3, 5));

                Screen.DisplayBoard(board);
            }
            catch (BoardException e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}