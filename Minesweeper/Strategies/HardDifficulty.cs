using Minesweeper.Interfaces;
using Minesweeper.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper.Strategies
{
    internal class HardDifficulty : IDifficultyStrategy
    {
        public void SetDifficulty(IGame game)
        {
            game.Board = new Board(99, 22, 24);
            game.FlagsAvailable = 99;
        }
    }
}
