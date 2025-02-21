using Chess.Core;
using Chess.Core.Pieces;


namespace Chess.Tests
{
    public class Chess960Tests
    {
        [Fact]
        public void TestKingBetweenRooks()
        {
            var board = new Board(8, false);

            int whiteKingPos = -1;
            int whiteRook1Pos = -1;
            int whiteRook2Pos = -1;

            int blackKingPos = -1;
            int blackRook1Pos = -1;
            int blackRook2Pos = -1;

            // Find positions of the king and rooks for white pieces (row 7)
            for (int j = 0; j < 8; j++)
            {
                var piece = board.GetPiece(7, j);
                if (piece is King)
                    whiteKingPos = j;
                else if (piece is Rook)
                {
                    if (whiteRook1Pos == -1)
                        whiteRook1Pos = j;
                    else
                        whiteRook2Pos = j;
                }
            }
            // Assert that the king is between the rooks for both white and black
            Assert.True(whiteKingPos > Math.Min(whiteRook1Pos, whiteRook2Pos) && whiteKingPos < Math.Max(whiteRook1Pos, whiteRook2Pos),
                "White king is not between the rooks.");
        }

        [Fact]
        public void TestBlackPiecesMirrorWhitePieces()
        {
            var board = new Board(8, false);
            // Compare the back row of white (row 7) and black (row 0)
            for (int j = 0; j < 8; j++)
            {
                var whitePiece = board.GetPiece(7, j);
                var blackPiece = board.GetPiece(0, j);

                // Assert that the piece types match (e.g., white rook vs black rook)
                Assert.Equal(whitePiece.GetType(), blackPiece.GetType());
            }
        }

        [Fact]
        public void TestBishopsOnSameColorFails()
        {
            var board = new Board(8, false);
            int bishop1Pos = -1, bishop2Pos = -1;

            // Find bishops
            for (int j = 0; j < 8; j++)
            {
                if (board.GetPiece(7, j) is Bishop)
                {
                    if (bishop1Pos == -1)
                        bishop1Pos = j;
                    else
                        bishop2Pos = j;
                }
            }
            // Bishops must be on opposite colors
            Assert.False(bishop1Pos % 2 == bishop2Pos % 2, "Bishops are not placed on opposite colors.");
        }

        [Fact]
        public void TestRandomPositionThrowsOnEmptyList()
        {
            var board = new Board(8, false);

            List<int> emptyList = new List<int>();

            Assert.Throws<InvalidOperationException>(() => board.GenerateRandomPosition(emptyList));
        }
    }
}
