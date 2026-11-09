using ChessConsole.Entities.Enums;
using ChessConsole.Entities.Chessboard;

namespace ChessConsole.Entities.Chess
{
    class Pawn : Piece
    {
        public Pawn(Board board, Color color) : base(color, board)
        {
        }

        public override string ToString()
        {
            return "P";
        }

        private bool HasEnemy(Position pos)
        {
            Piece p = Board.GetPieces(pos);
            return p != null && p.Color != Color;
        }
        
        private bool IsSquareEmpty(Position pos)
        {
            return Board.GetPieces(pos) == null;
        }

        public override bool[,] PossibleMoves()
        {
            bool[,] mat = new bool[Board.Lines, Board.Columns];

            Position pos = new Position(0, 0);

            if (Color == Color.White)
            {
                pos.SetValues(Position.Line - 1, Position.Column);
                if (Board.CheckPosition(pos) && IsSquareEmpty(pos))
                    mat[pos.Line, pos.Column] = true;

                pos.SetValues(Position.Line - 2, Position.Column);
                Position p2 = new Position(pos.Line - 1, pos.Column);
                if (Board.CheckPosition(p2) && IsSquareEmpty(p2) 
                    && Board.CheckPosition(pos) && IsSquareEmpty(pos)
                    && QtyMovements == 0)
                {
                    mat[pos.Line, pos.Column] = true;
                }

                pos.SetValues(Position.Line - 1, Position.Column - 1);
                if (Board.CheckPosition(pos) && HasEnemy(pos))
                    mat[pos.Line, pos.Column] = true;

                pos.SetValues(Position.Line - 1, Position.Column + 1);
                if (Board.CheckPosition(pos) && HasEnemy(pos))
                    mat[pos.Line, pos.Column] = true;
            }
            else
            {
                pos.SetValues(Position.Line + 1, Position.Column);
                if (Board.CheckPosition(pos) && IsSquareEmpty(pos))
                    mat[pos.Line, pos.Column] = true;

                pos.SetValues(Position.Line + 2, Position.Column);
                Position p2 = new Position(pos.Line + 1, pos.Column);
                if (Board.CheckPosition(p2) && IsSquareEmpty(p2)
                    && Board.CheckPosition(pos) && IsSquareEmpty(pos)
                    && QtyMovements == 0)
                {
                    mat[pos.Line, pos.Column] = true;
                }

                pos.SetValues(Position.Line + 1, Position.Column - 1);
                if (Board.CheckPosition(pos) && HasEnemy(pos))
                    mat[pos.Line, pos.Column] = true;

                pos.SetValues(Position.Line + 1, Position.Column + 1);
                if (Board.CheckPosition(pos) && HasEnemy(pos))
                    mat[pos.Line, pos.Column] = true;
            }

            return mat;
        }
    }
}