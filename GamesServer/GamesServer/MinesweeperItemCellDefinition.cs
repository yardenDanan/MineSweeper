using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace GamesServer
{
    [DataContract]
   public class MinesweeperItemCellDefinition
    {
        [DataMember]
        public int row { get; protected set; }
        [DataMember]
        public int col { get; protected set; }

        public MinesweeperItemCellDefinition(int row, int col)
        {
            this.row = row;
            this.col = col;
        }   
    }
}
