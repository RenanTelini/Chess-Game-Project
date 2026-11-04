using ChessConsole.Exceptions;

namespace ChessConsole.Entities.Chessboard
{
    class Board
    {
        public int Lines { get; set; }
        public int Columns { get; set; }
        private Piece[,] Pieces { get; set; }

        public Board(int lines, int columns)
        {
            Lines = lines;
            Columns = columns;
            Pieces = new Piece[lines, columns];
        }

        public Piece GetPieces(int line, int column)
        {
            return Pieces[line, column];
        }

        public Piece GetPieces(Position pos)
        {
            return Pieces[pos.Line, pos.Column];
        }

        public void SetPiecePosition(Piece p, Position pos)
        {
            if (ExistPiece(pos))
                throw new BoardException("There is already a piece in this position!");

            Pieces[pos.Line, pos.Column] = p;
            p.Position = pos;
        }

        public bool ExistPiece(Position pos)
        {
            ValidatePosition(pos);
            return GetPieces(pos) != null;
        }
        
        public bool CheckPosition(Position pos)
        {
            if (pos.Line < 0 || pos.Line >= Lines || 
                pos.Column < 0 || pos.Column >= Columns)
                return false;

            return true;
        }

        public void ValidatePosition(Position pos)
        {
            if (!CheckPosition(pos))
                throw new BoardException("Invalid position!");
        }
    }
}