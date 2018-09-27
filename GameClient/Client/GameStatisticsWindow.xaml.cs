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
    /// Interaction logic for GameStatisticsWindow.xaml
    /// </summary>
    public partial class GameStatisticsWindow : Window
    {
        public GameServiceClient Client { get; set; }

        public GameStatisticsWindow()
        {
            InitializeComponent();
        }

        private void FillGameStatistics()
        {
            DataTable dataTable = new DataTable();
            DataColumn serialNumber = new DataColumn("No", typeof(string));
            DataColumn homePlayer = new DataColumn("Home Player", typeof(string));
            DataColumn awayPlayer = new DataColumn("Away Player", typeof(string));
            DataColumn finishTime = new DataColumn("Finish Time", typeof(DateTime));
            DataColumn result = new DataColumn("Result", typeof(string));
            dataTable.Columns.Add(serialNumber);
            dataTable.Columns.Add(homePlayer);
            dataTable.Columns.Add(awayPlayer);
            dataTable.Columns.Add(finishTime);
            dataTable.Columns.Add(result);

            List<GameDTO> games = Client.GetAllGamesPlayed();
            int rankCount = 1;
            foreach (GameDTO game in games)
            {
                DataRow row = dataTable.NewRow();
                row[0] = rankCount++;
                row[1] = game.UserName1;
                row[2] = game.UserName2;
                row[3] = game.Date;
                if (game.Winner.Equals("Tie"))
                {
                    row[4] = game.Winner;
                }
                else
                {
                    row[4] = game.Winner + " Won";
                }
                dataTable.Rows.Add(row);
            }
            table.ItemsSource = dataTable.DefaultView;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.Icon = new BitmapImage(new Uri(System.AppDomain.CurrentDomain.BaseDirectory + "/Resources/app-icon2.png"));
            rightBars.Source = new BitmapImage(new Uri(System.AppDomain.CurrentDomain.BaseDirectory + "/Resources/right-side-stats.png"));
            leftBars.Source = new BitmapImage(new Uri(System.AppDomain.CurrentDomain.BaseDirectory + "/Resources/stats.png"));
            FillGameStatistics();
        }
    }
}
