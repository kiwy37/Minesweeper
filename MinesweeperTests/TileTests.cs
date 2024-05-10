using Microsoft.VisualStudio.TestTools.UnitTesting;
using Minesweeper.Models;

namespace MinesweeperTests
{
    [TestClass]
    public class TileTests
    {
        [TestMethod]
        public void TestTileInitialization()
        {
            // Arrange
            Tile tile = new Tile();

            // Assert
            Assert.IsFalse(tile.IsFlagged, "IsFlagged should be initialized to false.");
            Assert.IsFalse(tile.IsRevealed, "IsRevealed should be initialized to false.");
            Assert.IsFalse(tile.IsMine, "IsMine should be initialized to false.");
            Assert.AreEqual(0, tile.NumberOfNeighbouringMines, "NumberOfNeighbouringMines should be initialized to 0.");

        }

        [TestMethod]
        public void TestFlaggedTile()
        {
            Tile tile = new Tile();
            tile.IsFlagged = true;
            Assert.IsFalse(tile.IsRevealed, "A flagged mine cannot be revealed");
        }

        [TestMethod]
        public void TestRevealedTile()
        {
            Tile tile = new Tile();
            tile.IsRevealed=true;
            Assert.IsFalse(tile.IsFlagged, "A revealed tile cannot be marked as mine");
        }
    }
}
