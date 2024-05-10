﻿using Minesweeper.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper.Models
{
    public class MediumGame : IGame
    {
        public MediumGame()
        {
            DifficultyLevel = DifficultyLevel.Medium;
        }
    }
}
