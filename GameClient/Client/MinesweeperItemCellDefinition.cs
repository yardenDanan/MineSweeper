using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    class MinesweeperItemCellDefinition
    {
        public int row { get; protected set; }
        public int col { get; protected set; }

        public MinesweeperItemCellDefinition(int row, int col)
        {
            this.row = row;
            this.col = col;
        }
    }
}
