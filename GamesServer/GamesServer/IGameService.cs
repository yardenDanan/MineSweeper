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
        [FaultContract(typeof(UserFaultException))]
        void RegisterClient(string username, string password);

        [OperationContract]
        [FaultContract(typeof(UserExistsFault))]
        PlayerDTO GetPlayerDetailes(string username);

        [OperationContract]
        List<GameDTO> GetAllGamesPlayed();

        [OperationContract]
        List<PlayerDTO> GetAllPlayers();

        [OperationContract(IsOneWay = true)]
        void SendMessage(string message, string fromClient, string toClient);

        [OperationContract]
        [FaultContract(typeof(UserFaultException))]
        void ChangeClientPassword(string userName, string oldPassword, string newPassword);

    }

    public interface IGameServiceCallback {

        [OperationContract(IsOneWay = true)]
        void UpdateClientsList(IEnumerable<string> clients);

        [OperationContract(IsOneWay = true)]
        void SendMessageToClient(string message, string fromClient);
    }
}
