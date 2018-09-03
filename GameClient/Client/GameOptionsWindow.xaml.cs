using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Client.ServiceReference;
using GameServer;

namespace Client {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class GameOptionsWindow : Window {
        public GameOptionsWindow() {
            InitializeComponent();
        }

        public GameServiceClient Client { get;  set; }
        public GameCallback CallBack { get;  set; }
        public string Username { get;  set; }

        private void Window_Loaded(object sender, RoutedEventArgs e) {
            this.Icon = new BitmapImage(new Uri(System.AppDomain.CurrentDomain.BaseDirectory + "/Resources/app-icon2.png"));
            BackButton.Source = new BitmapImage(new Uri(System.AppDomain.CurrentDomain.BaseDirectory + "/Resources/back-arrow4.png"));
            CallBack.updateConnectedClients += UpdateClients;
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
            invitationWindow.ShowDialog();
        }

        private void PlatAloneButton_Click(object sender, RoutedEventArgs e)
        {
            PlayAloneConfigWindow configWindow = new PlayAloneConfigWindow();
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
            winsPercentage.Content = "Wins: " + winsAsPercentage + "%";
            tiesPercentage.Content = "Ties: " + tiesAsPercentage + "%";
            losesPercentage.Content = "Lost: " + losesAsPercentage + "%";
        }

        private void Leaderboard_Click(object sender, RoutedEventArgs e)
        {
            LeaderBoardWindow leadrboardWindow = new LeaderBoardWindow();
            leadrboardWindow.Client = Client;
            leadrboardWindow.ShowDialog();
        }
    }
}
