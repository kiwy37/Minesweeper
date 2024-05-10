using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;
using Minesweeper.Models;

namespace MinesweeperTests
{
    [TestClass]
    public class BoardTests
    {

        [TestMethod]
        public void TestBoardInitialization()
        {
            Board board = new Board(40, 14, 18);
            Assert.AreEqual(board.Height, 14, "The height of the board should be equal to the transmitted parameter");
            Assert.AreEqual(board.Width, 18, "The width of the board should be equal to the transmitted parameter");
            Assert.AreEqual(board.Height, board.Tiles.Count, "The height of the board and of the collection of tiles does not align");
            for (int i = 0; i < board.Height; i++)
                Assert.AreEqual(board.Width, board.Tiles[i].Count, "The width of the board and of the collection of tiles does not align");

        }


        [TestMethod]
        public void TestMinePlacement()
        {
            Board board = new Board(40, 14, 18);

            int noOfMines = 0;
            for(int i=0; i < board.Height; i++)
                for(int j=0; j < board.Width; j++)
                    if(board.Tiles[i][j].IsMine)
                        noOfMines++;
            Assert.AreEqual(40, noOfMines, "The number of placed mines does not align with the parameter");
        }


        [TestMethod]
        public void TestTilesValue()
        {
            Board board = new Board(40, 14, 18);
            for (int i = 0; i < board.Height; i++)
            {
                for (int j = 0; j < board.Width; j++)
                {
                    if (!board.Tiles[i][j].IsMine)
                    {
                        int countMines = 0;

                        for (int k = Math.Max(0, i - 1); k <= Math.Min(board.Height - 1, i + 1); k++)
                        {
                            for (int l = Math.Max(0, j - 1); l <= Math.Min(board.Width - 1, j + 1); l++)
                            {
                                if (board.Tiles[k][l].IsMine)
                                {
                                    countMines++;
                                }
                            }
                        }
                        Assert.AreEqual(countMines, board.Tiles[i][j].NumberOfNeighbouringMines, "The value of the tile is not assigned correctly!!!");
                    }
                    else
                    {
                       Assert.AreEqual(0, board.Tiles[i][j].NumberOfNeighbouringMines, "The number of mines that border a mine is not an essential information");
                    }
                }
            }
        }
    }
}
