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

            ChessPosition pos = new ChessPosition('c', 7);

            Console.WriteLine(pos);

            Console.WriteLine(pos.ToPosition());
        }
    }
}