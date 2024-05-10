using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Minesweeper.Models;
using Minesweeper.Interfaces;

namespace MinesweeperTests
{
    [TestClass]
    public class DifficultiesTests
    {
        [TestMethod]
        public void ProduceEasyGame()
        {
            IGame game = IGame.Produce(DifficultyLevel.Easy);
            Assert.AreEqual(game.Board.Height, 8, "The height for the easy level board should be 8");
            Assert.AreEqual(game.Board.Width, 10, "The width for the easy level board should be 10");
            Assert.AreEqual(game.FlagsAvailable, 10, "The number of mines in the easy level board should be 10");
            Assert.AreEqual(game.DifficultyLevel, DifficultyLevel.Easy, "The resulting game should be easy");
            
        }



        [TestMethod]
        public void ProduceMediumGame()
        {
            IGame game = IGame.Produce(DifficultyLevel.Medium);
            Assert.AreEqual(game.Board.Height, 14, "The height for the easy level board should be 14");
            Assert.AreEqual(game.Board.Width, 18, "The width for the easy level board should be 18");
            Assert.AreEqual(game.FlagsAvailable, 40, "The number of mines in the easy level board should be 40");
            Assert.AreEqual(game.DifficultyLevel, DifficultyLevel.Medium, "The resulting game should be medium");

        }



        [TestMethod]
        public void ProduceHardGame()
        {
            IGame game = IGame.Produce(DifficultyLevel.Hard);
            Assert.AreEqual(game.Board.Height, 22, "The height for the easy level board should be 22");
            Assert.AreEqual(game.Board.Width, 24, "The width for the easy level board should be 24");
            Assert.AreEqual(game.FlagsAvailable, 99, "The number of mines in the easy level board should be 99");
            Assert.AreEqual(game.DifficultyLevel, DifficultyLevel.Hard, "The resulting game should be hard");

        }

    }
}
