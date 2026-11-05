using ChessConsole.Entities;
using ChessConsole.Exceptions;
using ChessConsole.Entities.Chess;
using ChessConsole.Entities.Chessboard;

namespace ChessConsole
{
    class Program
    {
        static void Main(string[] args)
        {

            try
            {
                ChessMatch game = new ChessMatch();

                while (!game.Fineshed)
                {
                    Console.Clear();
                    Screen.DisplayBoard(game.ChessBoard);

                    Console.WriteLine();
                    Console.Write("Origin: ");
                    Position origin = Screen.ReadChessPosition().ToPosition();

                    Console.Write("Destination: ");
                    Position destination = Screen.ReadChessPosition().ToPosition();

                    game.RunMovement(origin, destination);
                }


            }
            catch (BoardException e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}