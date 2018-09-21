using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace GamesServer
{
    [DataContract]
    public enum MinesweeperItemType
    {
        [EnumMember]
        MinesweeperItemType_None = 0,
        [EnumMember]
        MinesweeperItem_Empty = 1,
        [EnumMember]
        MinesweeperItem_MineWarning = 2,
        [EnumMember]
        MinesweeperItem_Mine = 3
    }
    [DataContract]
    public class MinesweeperItem
    {
        [DataMember]
        public Object tag { get; set; }
        [DataMember]
        public MinesweeperItemType type { get; set; }
        [DataMember]
        public Object value { get; set; }
        [DataMember]
        public MinesweeperItemCellDefinition cell { get; protected set; }

        public MinesweeperItem(MinesweeperItemCellDefinition cell)
        {
            this.cell = cell;
        }
    }
}
