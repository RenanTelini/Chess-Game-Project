using ChessConsole.Entities.Chessboard;

namespace ChessConsole.Entities
{
    class Screen
    {
        public static void DisplayBoard(Board board)
        {
            for (int i = 0; i < board.Lines; i++)
            {
                for (int j = 0; j < board.Columns; j++)
                {
                    if (board.GetPieces(i, j) == null)
                        Console.Write("- ");
                    else
                        Console.Write(board.GetPieces(i, j) + " ");
                }

                Console.WriteLine();
            }
        }
    }
}