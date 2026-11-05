using ChessConsole.Entities.Chessboard;
using ChessConsole.Entities.Enums;

namespace ChessConsole.Entities.Chess
{
    class ChessMatch
    {
        public Board ChessBoard { get; private set; }
        private int _turn;
        private Color _currentPlayer;
        public bool Fineshed { get; private set; }

        public ChessMatch()
        {
            ChessBoard = new Board(8, 8);
            _turn = 1;
            _currentPlayer = Color.White;
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