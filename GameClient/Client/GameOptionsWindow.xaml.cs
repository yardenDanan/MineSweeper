using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using Client.ServiceReference;
using System.ServiceModel;
using GamesServer;
using System.Threading;

namespace Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class GameOptionsWindow : Window {
        public GameOptionsWindow() {
            InitializeComponent();
        }

        public GameServiceClient Client { get;  set; }
        public GameCallback CallBack { get;  set; }
        public PlayerStats PlayerStats { get; set; }
        public string Username { get;  set; }

        private void Window_Loaded(object sender, RoutedEventArgs e) {
            this.Icon = new BitmapImage(new Uri(System.AppDomain.CurrentDomain.BaseDirectory + "/Resources/app-icon2.png"));
            BackButton.Source = new BitmapImage(new Uri(System.AppDomain.CurrentDomain.BaseDirectory + "/Resources/back-arrow4.png"));
            CallBack.updateConnectedClients += UpdateClients;
            CallBack.displayMessage += DisplayMessage;
            CallBack.showInvitation += ShowReciveInviation;
            Client.SendMessage("Welcome to MineSweeper!", "Server", Username);
            Thread setPlayerStats = new Thread(() => SetPlayerStats());
            setPlayerStats.Start();
        }

        private void SetPlayerStats()
        {
            PlayerDTO player = Client.GetPlayerDetailes(Username);
            PlayerStats = new PlayerStats();
            int totalGames = player.GamesLost + player.GamesTie + player.GamesWon;
            PlayerStats.TotalGames = totalGames;
            PlayerStats.Wins = player.GamesWon;
            PlayerStats.Ties = player.GamesTie;
            PlayerStats.Loses = player.GamesLost;
        }

        private void ShowReciveInviation(string senderName, GameParams parameters, PlayerStats senderStats)
        {
            IncomingInvitationWindow incomingInvitationWindow = new IncomingInvitationWindow();
            incomingInvitationWindow.Client = Client;
            incomingInvitationWindow.CallBack = CallBack;
            incomingInvitationWindow.SenderName = senderName;
            incomingInvitationWindow.GameParams = parameters;
            incomingInvitationWindow.Username = Username;
            incomingInvitationWindow.SenderStats = senderStats;
            incomingInvitationWindow.ReciverStats = PlayerStats;
            incomingInvitationWindow.Show();
        }

        private void SendMessageButton_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(tbMassage.Text) && lbUsers.SelectedItem != null)
            {
                if(!Username.Equals(lbUsers.SelectedItem as string))
                {
                 Client.SendMessage(tbMassage.Text, Username, lbUsers.SelectedItem as string);
                 DisplayMessage(tbMassage.Text,"Me to " + lbUsers.SelectedItem as string);
                 tbMassage.Text = string.Empty;
                }
                else
                {
                    MessageBox.Show("You cant send message to yourslef!", "Notice", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }

        private void DeleteAccount(string password)
        {
            MessageBoxResult userChoice = MessageBox.Show("Are you sure you want to delete your account?",
            "No way back", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (userChoice.Equals(MessageBoxResult.Yes))
             {
                try
                {
                  Client.DeleteAccount(Username, password);
                    MessageBox.Show("Account has been deleted successfully.", "Notice", MessageBoxButton.OK, MessageBoxImage.Information);
                    this.Close();
                    new LoginWindow().Show();
                }
                catch (FaultException<UserFaultException> ex)
                {
                    MessageBox.Show(ex.Message, "Oops!", MessageBoxButton.OK, MessageBoxImage.Error);
                }

             }
             else
             {
               return;
             }
            
        }

        private void DisplayMessage(string message, string fromClient)
        {
            tbConversation.Text += GetCurrentTime() + " " + fromClient + ": " + message + "\n";
        }

        private string GetCurrentTime()
        {
            return DateTime.Now.ToString("HH:mm");
        }

        private void UpdateClients(List<string> clients) {
            lbUsers.ItemsSource = clients;
        }

        private void Window_Closed(object sender, EventArgs e) {
            Client.ClientDisconnected(Username);
        }

        private void BackButton_MouseDown(object sender, MouseButtonEventArgs e)
        {
             new LoginWindow().Show();
             this.Close();
        }

        private void btInvitePlayer_Click(object sender, RoutedEventArgs e)
        {
            if (lbUsers.SelectedIndex == -1)
            {
                MessageBox.Show("In order to invite player you need to choose player first.",
                    "Error occurred!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            String selectedUser = lbUsers.SelectedItem.ToString();
            if (selectedUser.Equals(Username))
            {
                MessageBox.Show("You cant invite yourself, choose other player.",
                    "Error occurred!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            GameInvitationWindow invitationWindow = new GameInvitationWindow();
            invitationWindow.InviteReciverName = selectedUser;
            invitationWindow.Client = Client;
            invitationWindow.Username = Username;
            invitationWindow.PlayerStats = PlayerStats;
            invitationWindow.CallBack = CallBack;
            invitationWindow.ShowDialog();
        }

        private void ChangePassword_Click(object sender, RoutedEventArgs e)
        {
            ChangePasswordWindow changePasswordWindow = new ChangePasswordWindow();
            changePasswordWindow.Show();
            changePasswordWindow.passwordChange += ChangePassword;
        }

        private void ChangePassword(string oldPassword, string newPassword)
        {
            try
            {
                Client.ChangeClientPassword(Username, oldPassword, newPassword);
                MessageBox.Show("Password changed successfuly","Notice", MessageBoxButton.OK,MessageBoxImage.Information);
            }
            catch (FaultException<UserFaultException> ex)
            {
                MessageBox.Show(ex.Detail.Message,"Error!",MessageBoxButton.OK,MessageBoxImage.Error);
            }
        }

        private void PlatAloneButton_Click(object sender, RoutedEventArgs e)
        {
            PlayAloneConfigWindow configWindow = new PlayAloneConfigWindow();
            configWindow.Client = Client;
            configWindow.CallBack = CallBack;
            configWindow.ShowDialog();
        }

        private void lbUsers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lbUsers.SelectedIndex == -1) return;
            string username = lbUsers.SelectedItem.ToString();
            PlayerDTO player = Client.GetPlayerDetailes(username);
            int totalGames = player.GamesLost + player.GamesTie + player.GamesWon;
            winsNumber.Content = "Wins: " + player.GamesWon;
            tiesNumber.Content = "Ties: " + player.GamesTie;
            losesNumber.Content = "Loses: " + player.GamesLost;
            totalNumber.Content = "Total Games: " + totalGames;
            double winsAsPercentage = 0, tiesAsPercentage = 0, losesAsPercentage = 0;
            if(totalGames != 0)
            {
             winsAsPercentage = Convert.ToDouble(player.GamesWon) / Convert.ToDouble(totalGames) * 100d;
             tiesAsPercentage = Convert.ToDouble(player.GamesTie) / Convert.ToDouble(totalGames) * 100d;
             losesAsPercentage = Convert.ToDouble(player.GamesLost) / Convert.ToDouble(totalGames) * 100d;
            }
            winsPercentage.Content = "Wins: " + System.Math.Round(winsAsPercentage, 2) + "%";
            tiesPercentage.Content = "Ties: " + System.Math.Round(tiesAsPercentage, 2) + "%";
            losesPercentage.Content = "Lost: " + System.Math.Round(losesAsPercentage, 2) + "%";
        }

        private void Leaderboard_Click(object sender, RoutedEventArgs e)
        {
            LeaderBoardWindow leadrboardWindow = new LeaderBoardWindow();
            leadrboardWindow.Client = Client;
            leadrboardWindow.ShowDialog();
        }

        private void DeleteAccount_Click(object sender, RoutedEventArgs e)
        {
            DeleteAccountWindow deleteAccountWindow = new DeleteAccountWindow();
            deleteAccountWindow.deleteAccount += DeleteAccount;
            deleteAccountWindow.Show();
        }
    }
}
