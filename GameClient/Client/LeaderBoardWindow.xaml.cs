using Client.ServiceReference;
using GamesServer;
using System;
using System.Collections.Generic;
using System.Data;
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
using System.Windows.Shapes;

namespace Client
{
    /// <summary>
    /// Interaction logic for LeaderBoardWindow.xaml
    /// </summary>
    public partial class LeaderBoardWindow : Window
    {
        public GameServiceClient Client {get;set;}

        public LeaderBoardWindow()
        {
            InitializeComponent();
        }

        private void FillLeaderboard()
        {
            DataTable dataTable = new DataTable();
            DataColumn rank = new DataColumn("Rank", typeof(int));
            DataColumn userName = new DataColumn("User Name",typeof(string));
            DataColumn winnings = new DataColumn("Winnings",typeof(int));
            DataColumn ties = new DataColumn("Ties",typeof(int));
            DataColumn loses = new DataColumn("Losses",typeof(int));
            DataColumn totalGames = new DataColumn("Total Games", typeof(int));
            DataColumn winRate = new DataColumn("Winnings Rate",typeof(string));
            dataTable.Columns.Add(rank);
            dataTable.Columns.Add(userName);
            dataTable.Columns.Add(winnings);
            dataTable.Columns.Add(ties);
            dataTable.Columns.Add(loses);
            dataTable.Columns.Add(totalGames);
            dataTable.Columns.Add(winRate);

            List<PlayerDTO> players = Client.GetAllPlayers();
            players.Sort(
                delegate (PlayerDTO first, PlayerDTO second)
                 {
                     return second.GamesWon.CompareTo(first.GamesWon);
                 }
                );
            int rankCount = 1;
            foreach(PlayerDTO player in players)
            {
                DataRow row = dataTable.NewRow();
                row[0] = rankCount++;
                row[1] = player.UserName;
                row[2] = player.GamesWon;
                row[3] = player.GamesTie;
                row[4] = player.GamesLost;
                int total = player.GamesWon + player.GamesTie + player.GamesLost;
                row[5] = total;
                if (total != 0)
                {
                    row[6] = (Convert.ToDouble(player.GamesWon) / Convert.ToDouble(total) * 100d) + "%";
                }
                else
                {
                    row[6] = 0;
                }
                dataTable.Rows.Add(row);
            }
            table.ItemsSource = dataTable.DefaultView;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.Icon = new BitmapImage(new Uri(System.AppDomain.CurrentDomain.BaseDirectory + "/Resources/app-icon2.png"));
            rightCrown.Source = new BitmapImage(new Uri(System.AppDomain.CurrentDomain.BaseDirectory + "/Resources/black-crown.png"));
            leftCrown.Source = new BitmapImage(new Uri(System.AppDomain.CurrentDomain.BaseDirectory + "/Resources/black-crown.png"));
            FillLeaderboard();
        }
    }
}
