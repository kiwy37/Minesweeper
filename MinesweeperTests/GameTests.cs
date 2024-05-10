using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using Minesweeper.Models;
using Minesweeper.Interfaces;
using Moq;

namespace MinesweeperTests
{ 

    [TestClass]
    public class GameTests
    {
        [TestMethod]
        public void InitializeGame()
        {
            IGame game = new EasyGame();
            Assert.AreEqual(GameState.InProgress, game.GameState, "A newly produced game should be in progress");
            Assert.AreEqual(0, game.FlagsUsed,  "At the begining of a game, the number of used flags should be 0");
            Assert.AreEqual(0, game.Time,  "At the begining of a game, time should be initialized to 0");          
        }



        [TestMethod]
        public void RevealAllTilesTest()
        {
            IGame game = IGame.Produce(DifficultyLevel.Easy);
            game.RevealAllTiles();
            for (int i = 0; i < game.Board.Height; i++)
                for (int j = 0; j < game.Board.Width; j++)
                    Assert.IsTrue(game.Board.Tiles[i][j].IsRevealed, "All tiles should be revealed");

        }



        [TestMethod]
        public void OnMineClickedTest()
        {
            IGame game = IGame.Produce(DifficultyLevel.Easy);
            var mockMineTile = new Mock<Tile>();
            mockMineTile.Setup(x => x.IsMine).Returns(true);
            game.LeftClickOnTile(mockMineTile.Object);
            Assert.AreEqual(GameState.Lost, game.GameState, "The game is lost on clicking on mine");
        }


        [TestMethod]
        public void AllSafeCellsRevealedTest()
        {
            var mockGame = new Mock<IGame>();
            mockGame.Setup(game => game.AreAllNonMinesRevealed()).Returns(true);

            IGame game = mockGame.Object;

            var mockNonMineTile = new Mock<Tile>();
            mockNonMineTile.Setup(x => x.IsMine).Returns(false);
            mockGame.Setup(x => x.RevealNeighboringTiles(It.IsAny<Tile>()));
            mockGame.Setup(x => x.RevealAllTiles());

            game.LeftClickOnTile(mockNonMineTile.Object);
            Assert.AreEqual(GameState.Won, game.GameState, "The game is won on revealing all the safe cells");
        }


    }
}
