using ChessConsole.Entities;
using ChessConsole.Entities.Chessboard;

namespace ChessConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Board board = new Board(8, 8);

            Screen.DisplayBoard(board);
        }
    }
}