using System.Runtime.Serialization;

namespace GamesServer {
    [DataContract]
    public class GameDTO {
        public GameDTO(Game game) {
            GameID = game.GameID;
            Date = game.Date;
            Winner = game.Winner;
            UserName1 = game.Player1_UserName;
            UserName2 = game.Player2_UserName;
        }
        [DataMember]
        public int GameID { get; set; }
        [DataMember]
        public System.DateTime Date { get; set; }
        [DataMember]
        public string Winner { get; set; }
        [DataMember]
        public string UserName1 { get; set; }
        [DataMember]
        public string UserName2 { get; set; }
    }
}