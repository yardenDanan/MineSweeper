using Client.ServiceReference;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GamesServer;
using System.ServiceModel;

namespace Client {
    public class GameCallback : ServiceReference.IGameServiceCallback
    {

        public delegate void UpdateConnectedClientListDelegate(List<string> clients);
        public event UpdateConnectedClientListDelegate updateConnectedClients;
        public void UpdateClientsList(List<string> clients) {
            updateConnectedClients(clients);
        }

        public delegate void DisplaymessageDelegate(string message, string fromClient);
        public event DisplaymessageDelegate displayMessage;
        public void SendMessageToClient(string message, string fromClient)
        {
            displayMessage(message, fromClient);
        }

        public delegate void ShowSentInvitationDelegate(string senderName, GameParams parameters, PlayerStats playerStats);
        public event ShowSentInvitationDelegate showInvitation;
        public void ShowSentInvitation(string senderName, GameParams parameters, PlayerStats playerStats)
        {
            showInvitation(senderName, parameters, playerStats);
        }
    }
}
