using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace GamesServer {
    [ServiceContract(CallbackContract = typeof(IGameServiceCallback))]
    public interface IGameService {
        [OperationContract]
        [FaultContract(typeof(UserExistsFault))]
        void ClientConnected(string username, string password);
        [OperationContract]
        void ClientDisconnected(string username);
        [OperationContract]
        [FaultContract(typeof(UserExistsFault))]
        void RegisterClient(string username, string password);
        [OperationContract]
        [FaultContract(typeof(UserExistsFault))]
        PlayerDTO GetPlayerDetailes(string username);
        [OperationContract]
        List<GameDTO> GetAllGamesPlayed();
    }

    public interface IGameServiceCallback {
        [OperationContract(IsOneWay = true)]
        void UpdateClientsList(IEnumerable<string> clients);
    }
}
