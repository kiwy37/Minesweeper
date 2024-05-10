using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper.Models
{
    [Serializable]
    public class Statistics
    {
        public string Result { get; set; }
        public DateTime EndTime { get; set; }
        public string Difficulty { get; set; }
        public string ElapsedTime { get; set; }
    }
}
