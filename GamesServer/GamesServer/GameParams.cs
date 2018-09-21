using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamesServer
{
    class GameParams
    {
        public int rows { get; set; }
        public int cols { get; set; }
        public int mines { get; set; }
        public GameParams(int rows, int cols, int mines)
        {
            this.rows = rows;
            this.cols = cols;
            this.mines = mines;
        }
    }
}
