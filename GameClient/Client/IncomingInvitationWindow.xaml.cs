using Client.ServiceReference;
using GamesServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Client
{
    /// <summary>
    /// Interaction logic for IncomingInvitationWindow.xaml
    /// </summary>
    public partial class IncomingInvitationWindow : Window
    {
        public GameServiceClient Client { get; set; }
        public GameParams GameParams { get; set; }
        public PlayerStats SenderStats { get; set; }
        public PlayerStats ReciverStats { get; set; }
        public string SenderName { get; set; }
        public string Username { get; set; }

        public IncomingInvitationWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.Icon = new BitmapImage(new Uri(System.AppDomain.CurrentDomain.BaseDirectory + "/Resources/app-icon2.png"));
            SenderNameText.Content += SenderName;
            BoardSize.Content += GameParams.rows + " X " + GameParams.cols;
            BombsNumber.Content += GameParams.mines.ToString();

            SenderWinsNumber.Content = "Wins: " + SenderStats.Wins;
            SenderTiesNumber.Content = "Ties: " + SenderStats.Ties;
            SenderLosesNumber.Content = "Loses: " + SenderStats.Loses;
            SenderTotalNumber.Content = "Total Games: " + SenderStats.TotalGames;

            ReciverWinsNumber.Content = "Wins: " + ReciverStats.Wins;
            ReciverTiesNumber.Content = "Ties: " + ReciverStats.Ties;
            ReciverLosesNumber.Content = "Loses: " + ReciverStats.Loses;
            ReciverTotalNumber.Content = "Total Games: " + ReciverStats.TotalGames;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
            Client.CancelInvitation(SenderName);
            }
            catch (FaultException<UserFaultException> ex)
            {
                MessageBox.Show(ex.Detail.Message, "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            this.Close();
        }

        private void AcceptButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Client.AcceptInvitation(SenderName,Username,GameParams);
            }
            catch (FaultException<UserFaultException> ex)
            {
                MessageBox.Show(ex.Detail.Message, "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            ServiceReference.LiveMatch match = Client.GetSameGridAsOpponent(SenderName, Username);
            GameBoardWindow gameBoardWindow = new GameBoardWindow();
            gameBoardWindow.Match = match;
            gameBoardWindow.GameParams = match.GameParams;
            gameBoardWindow.Client = Client;
            this.Close();
            gameBoardWindow.Show();
        }
    }
}
