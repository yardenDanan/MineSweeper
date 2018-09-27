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
        List<GameDTO> GetLiveGames();

        [OperationContract]
        List<PlayerDTO> GetAllPlayers();

        [OperationContract(IsOneWay = true)]
        void SendMessage(string message, string fromClient, string toClient);

        [OperationContract]
        [FaultContract(typeof(UserFaultException))]
        void ChangeClientPassword(string userName, string oldPassword, string newPassword);

        [OperationContract]
        [FaultContract(typeof(UserFaultException))]
        void DeleteAccount(string userName, string password);

        [OperationContract]
        LiveMatch GetRandomGrid(int rows, int columns, int mines);

        [OperationContract]
        [FaultContract(typeof(UserFaultException))]
        void SendInvitation(string senderName, string reciverName, GameParams parameters,PlayerStats playerStats);

        [OperationContract]
        [FaultContract(typeof(UserFaultException))]
        void CancelInvitation(string senderName);

        [OperationContract]
        [FaultContract(typeof(UserFaultException))]
        void AcceptInvitation(string senderName,string reciverName, GameParams gameParams);

        [OperationContract]
        [FaultContract(typeof(UserFaultException))]
        LiveMatch GetSameGridAsOpponent(string senderName, string reciverName);

        [OperationContract]
        [FaultContract(typeof(UserFaultException))]
        Boolean WhosTurn(string homePlayer,string awayPlayer);

        [OperationContract]
        [FaultContract(typeof(UserFaultException))]
        void FinishTurn(MinesweeperItemCellDefinition cell, string homePlayer, string awayPlayer, string playerName);

        [OperationContract]
        [FaultContract(typeof(UserFaultException))]
        void PlayerLose(string homePlayer, string awayPlayer, string player);

        [OperationContract]
        [FaultContract(typeof(UserFaultException))]
        void GameFinishInTie(string homePlayer, string awayPlayer);
    }

    public interface IGameServiceCallback {

        [OperationContract(IsOneWay = true)]
        void UpdateClientsList(IEnumerable<string> clients);

        [OperationContract(IsOneWay = true)]
        void SendMessageToClient(string message, string fromClient);

        [OperationContract(IsOneWay = true)]
        void ShowSentInvitation(string sender, GameParams parameters, PlayerStats playerStats);

        [OperationContract(IsOneWay = true)]
        void CancelSenderInvitation();

        [OperationContract(IsOneWay = true)]
        void AcceptSenderInvitation(LiveMatch match);

        [OperationContract(IsOneWay = true)]
        void UpdateOpponentBoard(MinesweeperItemCellDefinition cell);

        [OperationContract(IsOneWay = true)]
        void NotifyWinner();

        [OperationContract(IsOneWay = true)]
        void NotifyTie();
    }
}
