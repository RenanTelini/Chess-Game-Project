using ChessConsole.Entities.Chessboard;
using ChessConsole.Entities.Enums;

namespace ChessConsole.Entities.Chess
{
    class King : Piece
    {
        public King(Board board, Color color) : base(color, board)
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
            if (Board.CheckPosition(pos) && CanMove(pos))
                mat[pos.Line, pos.Column] = true;

            //northeast
            pos.SetValues(Position.Line - 1, Position.Column + 1);
            if (Board.CheckPosition(pos) && CanMove(pos))
                mat[pos.Line, pos.Column] = true;

            //right
            pos.SetValues(Position.Line, Position.Column + 1);
            if (Board.CheckPosition(pos) && CanMove(pos))
                mat[pos.Line, pos.Column] = true;

            //southeast
            pos.SetValues(Position.Line + 1, Position.Column + 1);
            if (Board.CheckPosition(pos) && CanMove(pos))
                mat[pos.Line, pos.Column] = true;

            //south
            pos.SetValues(Position.Line + 1, Position.Column);
            if (Board.CheckPosition(pos) && CanMove(pos))
                mat[pos.Line, pos.Column] = true;

            //southwest
            pos.SetValues(Position.Line + 1, Position.Column - 1);
            if (Board.CheckPosition(pos) && CanMove(pos))
                mat[pos.Line, pos.Column] = true;

            //left
            pos.SetValues(Position.Line, Position.Column - 1);
            if (Board.CheckPosition(pos) && CanMove(pos))
                mat[pos.Line, pos.Column] = true;

            //northwest
            pos.SetValues(Position.Line - 1, Position.Column - 1);
            if (Board.CheckPosition(pos) && CanMove(pos))
                mat[pos.Line, pos.Column] = true;

            return mat;
        }

        public override string ToString()
        {
            return "K";
        }
    }
}