using System.Runtime.Serialization;

namespace GamesServer
{
    [DataContract]
    class UserFaultException
    {
        [DataMember]
        public string Message { get; set; }
    }
}