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
    /// Interaction logic for LiveGamesWindow.xaml
    /// </summary>
    public partial class LiveGamesWindow : Window
    {
        private DataTable dataTable;
        public GameServiceClient Client { get; set; }

        public LiveGamesWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.Icon = new BitmapImage(new Uri(System.AppDomain.CurrentDomain.BaseDirectory + "/Resources/app-icon2.png"));
            rightLive.Source = new BitmapImage(new Uri(System.AppDomain.CurrentDomain.BaseDirectory + "/Resources/right-live.png"));
            leftLive.Source = new BitmapImage(new Uri(System.AppDomain.CurrentDomain.BaseDirectory + "/Resources/_live_video_camera-512 (1).png"));
            FillLiveGames();
        }

        private void FillLiveGames()
        {
            dataTable = new DataTable();
            DataColumn serialNumber = new DataColumn("No", typeof(int));
            DataColumn homePlayer = new DataColumn("Home Player", typeof(string));
            DataColumn awayPlayer = new DataColumn("Away Player", typeof(string));
            dataTable.Columns.Add(serialNumber);
            dataTable.Columns.Add(homePlayer);
            dataTable.Columns.Add(awayPlayer);

            List<GameDTO> games = Client.GetLiveGames();
            if (games == null) return;
            int rankCount = 1;
            foreach (GameDTO game in games)
            {
                DataRow row = dataTable.NewRow();
                row[0] = rankCount++;
                row[1] = game.UserName1;
                row[2] = game.UserName2;
                dataTable.Rows.Add(row);
            }
            table.ItemsSource = dataTable.DefaultView;
        }
    }
}
