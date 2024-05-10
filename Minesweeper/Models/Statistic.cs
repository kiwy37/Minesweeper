using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper.Models;

public class Statistic
{
    public GameState Result { get; set; }
    public DateTime GameFinished { get; set; }
    public DifficultyLevel Difficulty { get; set; }
    public String Time { get; set; }
}