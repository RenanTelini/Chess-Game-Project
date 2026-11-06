using ChessConsole.Entities.Chessboard;
using ChessConsole.Entities.Chess;
using ChessConsole.Entities.Enums;

namespace ChessConsole.Entities
{
    class Screen
    {
        public static void DisplayGame(ChessMatch game)
        {
            DisplayBoard(game.ChessBoard);
            Console.WriteLine();
            DisplayCapturedPieces(game);
            Console.WriteLine();
            Console.WriteLine("Turn: " + game.Turn);
            Console.WriteLine("Waiting for move: " + game.CurrentPlayer);
        }

        public static void DisplayCapturedPieces(ChessMatch game)
        {
            Console.WriteLine("Captured pieces:");
            Console.Write("White: ");
            DisplaySet(game.PiecesCaptured(Enums.Color.White));
            Console.WriteLine();

            Console.Write("Black: ");

            ConsoleColor aux = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Yellow;

            DisplaySet(game.PiecesCaptured(Enums.Color.Black));

            Console.ForegroundColor = aux;
            Console.WriteLine();
        }

        public static void DisplaySet(HashSet<Piece> set)
        {
            Console.Write("[");
            foreach (Piece x in set)
            {
                Console.Write(x + " ");
            }
            Console.Write("]");
        }


        public static void DisplayBoard(Board board)
        {
            for (int i = 0; i < board.Lines; i++)
            {
                Console.Write(8 - i + " ");
                for (int j = 0; j < board.Columns; j++)
                {
                    PrintPiece(board.GetPieces(i, j));
                }
                Console.WriteLine();
            }

            Console.WriteLine("  a b c d e f g h");
        }

        public static void DisplayBoard(Board board, bool[,] possiblePositions)
        {

            ConsoleColor originalBackground = Console.BackgroundColor;
            ConsoleColor changedBackground = ConsoleColor.DarkGray;

            for (int i = 0; i < board.Lines; i++)
            {
                Console.Write(8 - i + " ");
                for (int j = 0; j < board.Columns; j++)
                {
                    if (possiblePositions[i, j])
                        Console.BackgroundColor = changedBackground; //ConsoleColor.DarkGray;
                    else
                        Console.BackgroundColor = originalBackground;

                    PrintPiece(board.GetPieces(i, j));

                    Console.BackgroundColor = originalBackground;
                }
                Console.WriteLine();
            }

            Console.WriteLine("  a b c d e f g h");
            Console.BackgroundColor = originalBackground;
        }

        public static ChessPosition ReadChessPosition()
        {
            string s = Console.ReadLine();
            char column = s[0];
            int line = int.Parse(s[1] + "");
            return new ChessPosition(column, line);
        }

        public static void PrintPiece(Piece piece)
        {
            if (piece == null)
                Console.Write("- ");
            else
            {
                if (piece.Color == Enums.Color.White)
                    Console.Write(piece);
                else
                {
                    ConsoleColor aux = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write(piece);
                    Console.ForegroundColor = aux;
                }
                Console.Write(" ");
            }
        }
    }
}