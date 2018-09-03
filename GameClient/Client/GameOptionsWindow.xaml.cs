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
    }
}
