using Minesweeper.Models;
using Minesweeper.Strategies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper.Interfaces
{
    internal interface IDifficultyStrategy
    {
        void SetDifficulty(IGame game);

        static IDifficultyStrategy CreateDifficultyStrategy(DifficultyLevel difficultyLevel)
        {
            switch (difficultyLevel)
            {
                case DifficultyLevel.Easy:
                    return new EasyDifficulty();
                case DifficultyLevel.Medium:
                    return new MediumDifficulty();
                case DifficultyLevel.Hard:
                    return new HardDifficulty();
                default:
                    return new EasyDifficulty();
            }
        }
    }
}
