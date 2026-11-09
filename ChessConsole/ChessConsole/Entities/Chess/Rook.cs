using ChessConsole.Entities.Chessboard;
using ChessConsole.Entities.Enums;

namespace ChessConsole.Entities.Chess
{
    class Rook : Piece
    {
        public Rook(Board board, Color color) : base(color, board)
        {
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

            //east
            pos.SetValues(Position.Line, Position.Column + 1);
            while (Board.CheckPosition(pos) && CanMove(pos))
            {
                mat[pos.Line, pos.Column] = true;
                if (Board.GetPieces(pos) != null && Board.GetPieces(pos).Color != Color)
                    break;

                pos.Column = pos.Column + 1;
            }

            //west
            pos.SetValues(Position.Line, Position.Column - 1);
            while (Board.CheckPosition(pos) && CanMove(pos))
            {
                mat[pos.Line, pos.Column] = true;
                if (Board.GetPieces(pos) != null && Board.GetPieces(pos).Color != Color)
                    break;

                pos.Column = pos.Column - 1;
            }

            return mat;
        }

        public override string ToString()
        {
            return "R";
        }
    }
}