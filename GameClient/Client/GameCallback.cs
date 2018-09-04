using Client.ServiceReference;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client {
    public class GameCallback : IGameServiceCallback {

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
    }
}
