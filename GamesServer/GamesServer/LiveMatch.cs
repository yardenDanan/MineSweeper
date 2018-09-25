using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace GamesServer
{
    [DataContract]
    public class LiveMatch
    {
        [DataMember]
        public MinesweeperGrid Board { get; set; }
        [DataMember]
        public GameParams GameParams { get; set; }
        [DataMember]
        public string HomePlayer { get; set; }
        [DataMember]
        public string AwayPlayer { get; set; }
        //Home turn (sender) = true, Away turn (reciver) = false
        [DataMember]
        public Boolean WhosTurn { get; set; }
    }
}
