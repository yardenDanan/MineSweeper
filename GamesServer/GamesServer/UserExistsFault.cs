using System.Runtime.Serialization;

namespace GamesServer {
    [DataContract]
     class UserExistsFault {
        [DataMember]
        public string Message { get; set; }
    }
}