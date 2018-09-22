using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace GamesServer
{
    [DataContract]
    public class GameParams
    {
        [DataMember]
        public int rows { get; set; }
        [DataMember]
        public int cols { get; set; }
        [DataMember]
        public int mines { get; set; }
        public GameParams(int rows, int cols, int mines)
        {
            this.rows = rows;
            this.cols = cols;
            this.mines = mines;
        }
    }
}
