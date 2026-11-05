using ChessConsole.Entities.Chessboard;
using ChessConsole.Exceptions;
using ChessConsole.Entities.Enums;

namespace ChessConsole.Entities.Chess
{
    class ChessMatch
    {
        public Board ChessBoard { get; private set; }
        public int Turn { get; private set; }
        public Color CurrentPlayer { get; private set; }
        public bool Fineshed { get; private set; }

        public ChessMatch()
        {
            ChessBoard = new Board(8, 8);
            Turn = 1;
            CurrentPlayer = Color.White;
            Fineshed = false;
            SetPieces();
        }

        public void RunMovement(Position origin, Position destination)
        {
            Piece p = ChessBoard.RemovePiece(origin);
            p.IncrementQtyMovements();
            Piece capturedPiece = ChessBoard.RemovePiece(destination);
            ChessBoard.SetPiecePosition(p, destination);
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

        private void SetPieces()
        {
            ChessBoard.SetPiecePosition(new Rook(ChessBoard, Color.White), new ChessPosition('c', 1).ToPosition());
            ChessBoard.SetPiecePosition(new Rook(ChessBoard, Color.White), new ChessPosition('c', 2).ToPosition());
            ChessBoard.SetPiecePosition(new Rook(ChessBoard, Color.White), new ChessPosition('d', 2).ToPosition());
            ChessBoard.SetPiecePosition(new Rook(ChessBoard, Color.White), new ChessPosition('e', 2).ToPosition());
            ChessBoard.SetPiecePosition(new Rook(ChessBoard, Color.White), new ChessPosition('e', 1).ToPosition());
            ChessBoard.SetPiecePosition(new King(ChessBoard, Color.White), new ChessPosition('d', 1).ToPosition());

            ChessBoard.SetPiecePosition(new Rook(ChessBoard, Color.Black), new ChessPosition('c', 7).ToPosition());
            ChessBoard.SetPiecePosition(new Rook(ChessBoard, Color.Black), new ChessPosition('c', 8).ToPosition());
            ChessBoard.SetPiecePosition(new Rook(ChessBoard, Color.Black), new ChessPosition('d', 7).ToPosition());
            ChessBoard.SetPiecePosition(new Rook(ChessBoard, Color.Black), new ChessPosition('e', 7).ToPosition());
            ChessBoard.SetPiecePosition(new Rook(ChessBoard, Color.Black), new ChessPosition('e', 8).ToPosition());
            ChessBoard.SetPiecePosition(new King(ChessBoard, Color.Black), new ChessPosition('d', 8).ToPosition());
        }
    }
}