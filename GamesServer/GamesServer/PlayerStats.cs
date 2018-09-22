using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace GamesServer
{
    [DataContract]
    public class PlayerStats
    {
        [DataMember]
        public int Wins { get; set; }
        [DataMember]
        public int Ties { get; set; }
        [DataMember]
        public int Loses { get; set; }
        [DataMember]
        public int TotalGames { get; set; }
    }
}
