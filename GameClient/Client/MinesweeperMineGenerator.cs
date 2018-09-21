using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    class MinesweeperMineGenerator
    {
        private Random random;
        private int cells = 0;

        public MinesweeperMineGenerator(int cells)
        {
            this.random = new Random(DateTime.Now.Second);
            this.cells = cells;
        }
        public int make()
        {
            return random.Next(1, this.cells);
        }
    }
}
