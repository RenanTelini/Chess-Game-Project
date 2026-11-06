using ChessConsole.Entities.Chessboard;
using ChessConsole.Entities.Enums;
using ChessConsole.Exceptions;
using System.Text.RegularExpressions;

namespace ChessConsole.Entities.Chess
{
    class ChessMatch
    {
        public Board ChessBoard { get; private set; }
        public int Turn { get; private set; }
        public Color CurrentPlayer { get; private set; }
        public bool Fineshed { get; private set; }
        private HashSet<Piece> _pieces;
        private HashSet<Piece> _captured;

        public ChessMatch()
        {
            ChessBoard = new Board(8, 8);
            Turn = 1;
            CurrentPlayer = Color.White;
            Fineshed = false;
            _pieces = new HashSet<Piece>();
            _captured = new HashSet<Piece>();
            SetPieces();
        }

        public void RunMovement(Position origin, Position destination)
        {
            Piece p = ChessBoard.RemovePiece(origin);
            p.IncrementQtyMovements();
            Piece capturedPiece = ChessBoard.RemovePiece(destination);
            ChessBoard.SetPiecePosition(p, destination);

            if (capturedPiece != null)
                _captured.Add(capturedPiece);
        }

        public void MakeMove(Position origin, Position destination)
        {
            RunMovement(origin, destination);
            Turn++;
            ChangePlayer();
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
            if (!ChessBoard.GetPieces(origin).CanMoveTo(destination))
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

        public void SetNewPiece(char column, int line, Piece piece)
        {
            ChessBoard.SetPiecePosition(piece, new ChessPosition(column, line).ToPosition());
            _pieces.Add(piece);
        }

        private void SetPieces()
        {
            SetNewPiece('c', 1, new Rook(ChessBoard, Color.White));
            SetNewPiece('c', 2, new Rook(ChessBoard, Color.White));
            SetNewPiece('d', 2, new Rook(ChessBoard, Color.White));
            SetNewPiece('e', 2, new Rook(ChessBoard, Color.White));
            SetNewPiece('e', 1, new Rook(ChessBoard, Color.White));
            SetNewPiece('d', 1, new King(ChessBoard, Color.White));

            SetNewPiece('c', 7, new Rook(ChessBoard, Color.Black));
            SetNewPiece('c', 8, new Rook(ChessBoard, Color.Black));
            SetNewPiece('d', 7, new Rook(ChessBoard, Color.Black));
            SetNewPiece('e', 7, new Rook(ChessBoard, Color.Black));
            SetNewPiece('e', 8, new Rook(ChessBoard, Color.Black));
            SetNewPiece('d', 8, new King(ChessBoard, Color.Black));
        }
    }
}