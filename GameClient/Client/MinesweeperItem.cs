using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    public enum MinesweeperItemType
    {
        MinesweeperItemType_None = 0,
        MinesweeperItem_Empty = 1,
        MinesweeperItem_MineWarning = 2,
        MinesweeperItem_Mine = 3
    }
    class MinesweeperItem
    {
        public Object tag { get; set; }
        public MinesweeperItemType type { get; set; }
        public Object value { get; set; }
        public MinesweeperItemCellDefinition cell { get; protected set; }

        public MinesweeperItem(MinesweeperItemCellDefinition cell)
        {
            this.cell = cell;
        }
    }
}
