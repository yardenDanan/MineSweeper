using System.Runtime.Serialization;

namespace GamesServer {
    [DataContract]
    public class PlayerDTO {
        public PlayerDTO(Player player) {
            UserName = player.UserName;
            GamesWon = player.GamesWon;
            GamesLost = player.GamesLost;
            GamesTie = player.GamesTie;
        }
        [DataMember]
        public string UserName { get; set; }
        [DataMember]
        public int GamesWon { get; set; }
        [DataMember]
        public int GamesLost { get; set; }
        [DataMember]
        public int GamesTie { get; set; }
    }
}