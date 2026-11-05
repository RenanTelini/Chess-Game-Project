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
                    try
                    {

                        Console.Clear();
                        Screen.DisplayBoard(game.ChessBoard);
                        Console.WriteLine();
                        Console.WriteLine("Turn: " + game.Turn);
                        Console.WriteLine("Waiting for move: " + game.CurrentPlayer);

                        Console.WriteLine();
                        Console.Write("Origin: ");
                        Position origin = Screen.ReadChessPosition().ToPosition();
                        game.ValidateOriginPosition(origin);

                        bool[,] possiblePositions = game.ChessBoard.GetPieces(origin).PossibleMoves();

                        Console.Clear();
                        Screen.DisplayBoard(game.ChessBoard, possiblePositions);

                        Console.WriteLine();
                        Console.Write("Destination: ");
                        Position destination = Screen.ReadChessPosition().ToPosition();
                        game.ValidateDestinationPosition(origin, destination);

                        game.MakeMove(origin, destination);
                    }
                    catch (BoardException e)
                    {
                        Console.WriteLine(e.Message);
                        Console.ReadLine();
                    }
                }
            }
            catch (BoardException e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}