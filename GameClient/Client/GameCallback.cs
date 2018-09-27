using Client.ServiceReference;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GamesServer;
using System.ServiceModel;

namespace Client {
    [CallbackBehavior(ConcurrencyMode = ConcurrencyMode.Multiple)]
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

        public delegate void CancelSenderInvitationDelegate();
        public event CancelSenderInvitationDelegate cancelInvitation;
        public void CancelSenderInvitation()
        {
            cancelInvitation();
        }

        public delegate void AcceptSenderInvitationDelegate(LiveMatch match);
        public event AcceptSenderInvitationDelegate acceptInvitation;

        public void AcceptSenderInvitation(LiveMatch match)
        {
            acceptInvitation(match);
        }

        public void ClearWaitForOpponentEvents()
        {
            if (acceptInvitation == null) return;
            foreach(Delegate handler in acceptInvitation.GetInvocationList())
            {
             acceptInvitation -= (AcceptSenderInvitationDelegate)handler;                
            }

            foreach(Delegate handler in cancelInvitation.GetInvocationList())
            {
              cancelInvitation -= (CancelSenderInvitationDelegate)handler;
            }
        }

        public delegate void UpdateOpponentBoardDelegate(MinesweeperItemCellDefinition cell);
        public event UpdateOpponentBoardDelegate updateOpponentBoard;
        public void UpdateOpponentBoard(MinesweeperItemCellDefinition cell)
        {
            updateOpponentBoard(cell);
        }

        public delegate void NotifyWinnerDelegate();
        public event NotifyWinnerDelegate notifyWinner;
        public void NotifyWinner()
        {
            notifyWinner();
        }

        public delegate void NotifyTieDelegate();
        public event NotifyTieDelegate notifyTie;
        public void NotifyTie()
        {
            notifyTie();
        }

        public delegate void NotifyTurnFlipDelegate();
        public event NotifyTurnFlipDelegate notifyTurnFlip;
        public void FlipTurnImageNotify()
        {
            notifyTurnFlip();
        }

        public void ClearGameBoardEvents()
        {
            if (updateOpponentBoard == null) return;
            foreach (Delegate handler in updateOpponentBoard.GetInvocationList())
            {
                updateOpponentBoard -= (UpdateOpponentBoardDelegate)handler;
            }

            foreach (Delegate handler in notifyWinner.GetInvocationList())
            {
                notifyWinner -= (NotifyWinnerDelegate)handler;
            }

            foreach (Delegate handler in notifyTie.GetInvocationList())
            {
                notifyTie -= (NotifyTieDelegate)handler;
            }

            foreach (Delegate handler in notifyTurnFlip.GetInvocationList())
            {
                notifyTurnFlip -= (NotifyTurnFlipDelegate)handler;
            }
        }
    }
}
