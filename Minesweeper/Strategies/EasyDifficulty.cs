using Minesweeper.Interfaces;
using Minesweeper.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper.Strategies
{
    internal class EasyDifficulty : IDifficultyStrategy
    {
        public void SetDifficulty(IGame game)
        {
            game.Board = new Board(10, 8, 10);
            game.FlagsAvailable = 10;
        }
    }
}
