using ChessConsole.Entities.Enums;
using ChessConsole.Entities.Chessboard;

namespace ChessConsole.Entities.Chess
{
    class Queen : Piece
    {
        public Queen(Board board, Color color) : base(color, board)
        {
        }

        public override string ToString()
        {
            return "Q";
        }

        private bool CanMove(Position pos)
        {
            Piece p = Board.GetPieces(pos);
            return p == null || p.Color != Color;
        }

        public override bool[,] PossibleMoves()
        {

            bool[,] mat = new bool[Board.Lines, Board.Columns];

            Position pos = new Position(0, 0);

            //west
            pos.SetValues(Position.Line, Position.Column - 1);
            while (Board.CheckPosition(pos) && CanMove(pos))
            {
                mat[pos.Line, pos.Column] = true;
                if (Board.GetPieces(pos) != null && Board.GetPieces(pos).Color != Color)
                    break;

                pos.Column = pos.Column - 1;
            }

            //east
            pos.SetValues(Position.Line, Position.Column + 1);
            while (Board.CheckPosition(pos) && CanMove(pos))
            {
                mat[pos.Line, pos.Column] = true;
                if (Board.GetPieces(pos) != null && Board.GetPieces(pos).Color != Color)
                    break;

                pos.Column = pos.Column + 1;
            }

            //north
            pos.SetValues(Position.Line - 1, Position.Column);
            while (Board.CheckPosition(pos) && CanMove(pos))
            {
                mat[pos.Line, pos.Column] = true;
                if (Board.GetPieces(pos) != null && Board.GetPieces(pos).Color != Color)
                    break;

                pos.Line = pos.Line - 1;
            }

            //south
            pos.SetValues(Position.Line + 1, Position.Column);
            while (Board.CheckPosition(pos) && CanMove(pos))
            {
                mat[pos.Line, pos.Column] = true;
                if (Board.GetPieces(pos) != null && Board.GetPieces(pos).Color != Color)
                    break;

                pos.Line = pos.Line + 1;
            }

            //northwest
            pos.SetValues(Position.Line - 1, Position.Column + 1);
            while (Board.CheckPosition(pos) && CanMove(pos))
            {
                mat[pos.Line, pos.Column] = true;
                if (Board.GetPieces(pos) != null && Board.GetPieces(pos).Color != Color)
                    break;

                pos.SetValues(pos.Line - 1, pos.Column + 1);
            }

            //northeast
            pos.SetValues(Position.Line - 1, Position.Column - 1);
            while (Board.CheckPosition(pos) && CanMove(pos))
            {
                mat[pos.Line, pos.Column] = true;
                if (Board.GetPieces(pos) != null && Board.GetPieces(pos).Color != Color)
                    break;

                pos.SetValues(pos.Line - 1, pos.Column - 1);
            }

            //southeast
            pos.SetValues(Position.Line + 1, Position.Column + 1);
            while (Board.CheckPosition(pos) && CanMove(pos))
            {
                mat[pos.Line, pos.Column] = true;
                if (Board.GetPieces(pos) != null && Board.GetPieces(pos).Color != Color)
                    break;

                pos.SetValues(pos.Line + 1, pos.Column + 1);
            }

            //southwest
            pos.SetValues(Position.Line + 1, Position.Column - 1);
            while (Board.CheckPosition(pos) && CanMove(pos))
            {
                mat[pos.Line, pos.Column] = true;
                if (Board.GetPieces(pos) != null && Board.GetPieces(pos).Color != Color)
                    break;

                pos.SetValues(pos.Line + 1, pos.Column - 1);
            }

            return mat;
        }

    }
}