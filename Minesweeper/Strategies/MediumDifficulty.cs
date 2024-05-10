using Minesweeper.Interfaces;
using Minesweeper.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper.Strategies
{
    internal class MediumDifficulty : IDifficultyStrategy
    {
        public void SetDifficulty(IGame game)
        {
            game.Board = new Board(40, 14, 18);
            game.FlagsAvailable = 40;
        }
    }
}
