using ChessConsole.Entities.Chessboard;
using ChessConsole.Entities.Enums;
using ChessConsole.Exceptions;
using System.Runtime.ConstrainedExecution;

namespace ChessConsole.Entities.Chess
{
    class ChessMatch
    {
        public Board ChessBoard { get; private set; }
        public int Turn { get; private set; }
        public Color CurrentPlayer { get; private set; }
        public bool Fineshed { get; private set; }
        public Piece VulnerableEnPassant { get; private set; }
        private HashSet<Piece> _pieces;
        private HashSet<Piece> _captured;
        public bool Check { get; private set; }

        public ChessMatch()
        {
            ChessBoard = new Board(8, 8);
            Turn = 1;
            CurrentPlayer = Color.White;
            Fineshed = false;
            Check = false;
            VulnerableEnPassant = null;
            _pieces = new HashSet<Piece>();
            _captured = new HashSet<Piece>();
            SetPieces();
        }

        public Piece RunMovement(Position origin, Position destination)
        {
            Piece p = ChessBoard.RemovePiece(origin);
            p.IncrementQtyMovements();
            Piece capturedPiece = ChessBoard.RemovePiece(destination);
            ChessBoard.SetPiecePosition(p, destination);

            if (capturedPiece != null)
                _captured.Add(capturedPiece);

            //# specialmove: Kingside Castling
            if (p is King && destination.Column == origin.Column + 2)
            {
                Position originRook = new Position(origin.Line, origin.Column + 3);
                Position destinationRook = new Position(origin.Line, origin.Column + 1);
                Piece R = ChessBoard.RemovePiece(originRook);
                R.IncrementQtyMovements();
                ChessBoard.SetPiecePosition(R, destinationRook);
            }

            //# specialmove: Queenside Castling
            if (p is King && destination.Column == origin.Column - 2)
            {
                Position originRook = new Position(origin.Line, origin.Column - 4);
                Position destinationRook = new Position(origin.Line, origin.Column - 1);
                Piece R = ChessBoard.RemovePiece(originRook);
                R.IncrementQtyMovements();
                ChessBoard.SetPiecePosition(R, destinationRook);
            }

            //# Specialmove: En Passant
            if (p is Pawn)
            {
                if (origin.Column != destination.Column && capturedPiece == null)
                {
                    Position posP;
                    if (p.Color == Color.White)
                        posP = new Position(destination.Line + 1, destination.Column);
                    else
                        posP = new Position(destination.Line - 1, destination.Column);

                    capturedPiece = ChessBoard.RemovePiece(posP);
                    _captured.Add(capturedPiece);
                }
            }

            return capturedPiece;
        }

        public void UndoMove(Position origin, Position destination, Piece capturedPiece)
        {
            Piece p = ChessBoard.RemovePiece(destination);
            p.DecrementQtyMovements();
            if (capturedPiece != null)
            {
                ChessBoard.SetPiecePosition(capturedPiece, destination);
                _captured.Remove(capturedPiece);
            }
            ChessBoard.SetPiecePosition(p, origin);

            //# specialmove: Kingside Castling
            if (p is King && destination.Column == origin.Column + 2)
            {
                Position originRook = new Position(origin.Line, origin.Column + 3);
                Position destinationRook = new Position(origin.Line, origin.Column + 1);
                Piece R = ChessBoard.RemovePiece(destinationRook);
                R.DecrementQtyMovements();
                ChessBoard.SetPiecePosition(R, originRook);
            }

            //# specialmove: Queenside Castling
            if (p is King && destination.Column == origin.Column - 2)
            {
                Position originRook = new Position(origin.Line, origin.Column - 4);
                Position destinationRook = new Position(origin.Line, origin.Column - 1);
                Piece R = ChessBoard.RemovePiece(destinationRook);
                R.DecrementQtyMovements();
                ChessBoard.SetPiecePosition(R, originRook);
            }

            //# Specialmove: En Passant
            if (p is Pawn)
            {
                if (origin.Column != destination.Column && capturedPiece == VulnerableEnPassant)
                {
                    Piece pawn = ChessBoard.RemovePiece(destination);
                    Position posP;
                    if (p.Color == Color.White)
                        posP = new Position(3, destination.Column);
                    else
                        posP = new Position(4, destination.Column);

                    ChessBoard.SetPiecePosition(pawn, posP);
                }
            }
        }

        public void MakeMove(Position origin, Position destination)
        {
            Piece capturedPiece = RunMovement(origin, destination);

            if (IsKingInCheck(CurrentPlayer))
            {
                UndoMove(origin, destination, capturedPiece);
                throw new BoardException("You cannot put yourself in check!");
            }

            Piece p = ChessBoard.GetPieces(destination);

            //# Specialmove: Promotion
            if (p is Pawn)
            {
                if ((p.Color == Color.White && destination.Line == 0) 
                    || p.Color == Color.Black && destination.Line == 7)
                {
                    p = ChessBoard.RemovePiece(destination);
                    _pieces.Remove(p);
                    Piece queen = new Queen(ChessBoard, p.Color);
                    ChessBoard.SetPiecePosition(queen, destination);
                    _pieces.Add(queen);
                }
            }

            if (IsKingInCheck(OpposingPiece(CurrentPlayer)))
                Check = true;
            else
                Check = false;

            if (TestCheckMate(OpposingPiece(CurrentPlayer)))
                Fineshed = true;
            else
            {
                Turn++;
                ChangePlayer();
            }

            //# Specialmove: En Passant
            if (p is Pawn && (destination.Line == origin.Line - 2 || destination.Line == origin.Line + 2))
            {
                VulnerableEnPassant = p;
            }
            else
            {
                VulnerableEnPassant = null;
            }
        }

        public void ValidateOriginPosition(Position pos)
        {
            if (ChessBoard.GetPieces(pos) == null)
                throw new BoardException("There's no piece at the chosen starting position!");

            if (CurrentPlayer != ChessBoard.GetPieces(pos).Color)
                throw new BoardException("The chess piece you selected as the starting piece is not yours!");

            if (!ChessBoard.GetPieces(pos).ExistPossibleMovements())
                throw new BoardException("There are no possible moves for the chosen piece!");
        }

        public void ValidateDestinationPosition(Position origin, Position destination)
        {
            if (!ChessBoard.GetPieces(origin).PossibleMove(destination))
                throw new BoardException("Invalid destination position!");
        }

        private void ChangePlayer()
        {
            if (CurrentPlayer == Color.White)
                CurrentPlayer = Color.Black;
            else
                CurrentPlayer = Color.White;
        }

        public HashSet<Piece> PiecesCaptured(Color color)
        {
            HashSet<Piece> aux = new HashSet<Piece>();
            foreach (Piece x in _captured)
            {
                if (x.Color == color)
                    aux.Add(x);
            }
            return aux;
        }

        public HashSet<Piece> PiecesInPlay(Color color)
        {
            HashSet<Piece> aux = new HashSet<Piece>();
            foreach (Piece x in _pieces)
            {
                if (x.Color == color)
                    aux.Add(x);
            }
            aux.ExceptWith(PiecesCaptured(color));
            return aux;
        }

        private Color OpposingPiece(Color color)
        {
            if (color == Color.White)
                return Color.Black;
            else
                return Color.White;
        }

        private Piece KingColor(Color color)
        {
            foreach (Piece x in PiecesInPlay(color))
            {
                if (x is King)
                    return x;
            }
            return null;
        }

        public bool IsKingInCheck(Color color)
        {
            Piece king = KingColor(color);
            if (king == null)
                throw new BoardException("There is no " + color + " king on the board!");

            foreach (Piece x in PiecesInPlay(OpposingPiece(color)))
            {
                bool[,] mat = x.PossibleMoves();
                if (mat[king.Position.Line, king.Position.Column])
                    return true;
            }
            return false;
        }

        public bool TestCheckMate(Color color)
        {
            if (!IsKingInCheck(color))
                return false;

            foreach (Piece x in PiecesInPlay(color))
            {
                bool[,] mat = x.PossibleMoves();
                for (int i = 0; i < ChessBoard.Lines; i++)
                {
                    for (int j = 0; j < ChessBoard.Columns; j++)
                    {
                        if (mat[i, j])
                        {
                            Position origin = x.Position;
                            Position destination = new Position(i, j);
                            Piece capturedPiece = RunMovement(origin, destination);
                            bool testCheck = IsKingInCheck(color);
                            UndoMove(origin, destination, capturedPiece);
                            if (!testCheck)
                                return false;
                        }
                    }
                }
            }
            return true;
        }
        
        public void SetNewPiece(char column, int line, Piece piece)
        {
            ChessBoard.SetPiecePosition(piece, new ChessPosition(column, line).ToPosition());
            _pieces.Add(piece);
        }

        private void SetPieces()
        {
            SetNewPiece('a', 1, new Rook(ChessBoard, Color.White));
            SetNewPiece('b', 1, new Knight(ChessBoard, Color.White));
            SetNewPiece('c', 1, new Bishop(ChessBoard, Color.White));
            SetNewPiece('d', 1, new Queen(ChessBoard, Color.White));
            SetNewPiece('e', 1, new King(ChessBoard, Color.White, this));
            SetNewPiece('f', 1, new Bishop(ChessBoard, Color.White));
            SetNewPiece('g', 1, new Knight(ChessBoard, Color.White));
            SetNewPiece('h', 1, new Rook(ChessBoard, Color.White));
            SetNewPiece('a', 2, new Pawn(ChessBoard, Color.White, this));
            SetNewPiece('b', 2, new Pawn(ChessBoard, Color.White, this));
            SetNewPiece('c', 2, new Pawn(ChessBoard, Color.White, this));
            SetNewPiece('d', 2, new Pawn(ChessBoard, Color.White, this));
            SetNewPiece('e', 2, new Pawn(ChessBoard, Color.White, this));
            SetNewPiece('f', 2, new Pawn(ChessBoard, Color.White, this));
            SetNewPiece('g', 2, new Pawn(ChessBoard, Color.White, this));
            SetNewPiece('h', 2, new Pawn(ChessBoard, Color.White, this));
            
            SetNewPiece('a', 8, new Rook(ChessBoard, Color.Black));
            SetNewPiece('b', 8, new Knight(ChessBoard, Color.Black));
            SetNewPiece('c', 8, new Bishop(ChessBoard, Color.Black));
            SetNewPiece('d', 8, new Queen(ChessBoard, Color.Black));
            SetNewPiece('e', 8, new King(ChessBoard, Color.Black, this));
            SetNewPiece('f', 8, new Bishop(ChessBoard, Color.Black));
            SetNewPiece('g', 8, new Knight(ChessBoard, Color.Black));
            SetNewPiece('h', 8, new Rook(ChessBoard, Color.Black));
            SetNewPiece('a', 7, new Pawn(ChessBoard, Color.Black, this));
            SetNewPiece('b', 7, new Pawn(ChessBoard, Color.Black, this));
            SetNewPiece('c', 7, new Pawn(ChessBoard, Color.Black, this));
            SetNewPiece('d', 7, new Pawn(ChessBoard, Color.Black, this));
            SetNewPiece('e', 7, new Pawn(ChessBoard, Color.Black, this));
            SetNewPiece('f', 7, new Pawn(ChessBoard, Color.Black, this));
            SetNewPiece('g', 7, new Pawn(ChessBoard, Color.Black, this));
            SetNewPiece('h', 7, new Pawn(ChessBoard, Color.Black, this));
        }
    }
}