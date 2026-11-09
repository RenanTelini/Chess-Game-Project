using ChessConsole.Entities.Chessboard;
using ChessConsole.Entities.Enums;

namespace ChessConsole.Entities.Chess
{
    class King : Piece
    {
        private ChessMatch _game;

        public King(Board board, Color color, ChessMatch game) : base(color, board)
        {
            _game = game;
        }

        private bool CanMove(Position pos)
        {
            Piece p = Board.GetPieces(pos);
            return p == null || p.Color != Color;
        }

        private bool CanCastleWithRook(Position pos)
        {
            Piece p = Board.GetPieces(pos);
            return p != null
                && p is Rook
                && p.Color == Color
                && p.QtyMovements == 0;
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

            //east
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

            //west
            pos.SetValues(Position.Line, Position.Column - 1);
            if (Board.CheckPosition(pos) && CanMove(pos))
                mat[pos.Line, pos.Column] = true;

            //northwest
            pos.SetValues(Position.Line - 1, Position.Column - 1);
            if (Board.CheckPosition(pos) && CanMove(pos))
                mat[pos.Line, pos.Column] = true;


            // #Special move: Castling

            if (QtyMovements == 0 & !_game.Check)
            {
                //# specialmove: Kingside Castling
                Position posR1 = new Position(Position.Line, Position.Column + 3);
                if (CanCastleWithRook(posR1))
                {
                    Position p1 = new Position(Position.Line, Position.Column + 1);
                    Position p2 = new Position(Position.Line, Position.Column + 2);

                    if (Board.GetPieces(p1) == null
                        && Board.GetPieces(p2) == null)
                    {
                        mat[Position.Line, Position.Column + 2] = true;
                    }
                }

                //# specialmove: Queenside Castling
                Position posR2 = new Position(Position.Line, Position.Column - 4);
                if (CanCastleWithRook(posR2))
                {
                    Position p1 = new Position(Position.Line, Position.Column - 1);
                    Position p2 = new Position(Position.Line, Position.Column - 2);
                    Position p3 = new Position(Position.Line, Position.Column - 3);

                    if (Board.GetPieces(p1) == null 
                        && Board.GetPieces(p2) == null 
                        && Board.GetPieces(p3) == null)
                    {
                        mat[Position.Line, Position.Column - 2] = true;
                    }
                }
            }

            return mat;
        }

        public override string ToString()
        {
            return "K";
        }
    }
}