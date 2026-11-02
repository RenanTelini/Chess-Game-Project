using ChessConsole.Entities.Chessboard;

namespace ChessConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Position p = new Position(3, 4);
            
            Console.WriteLine("Position: {0}", p);
        }
    }
}