using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using Client.ServiceReference;

namespace Client
{
    /// <summary>
    /// Interaction logic for PlayOptionsWindow.xaml
    /// </summary>
    public partial class PlayOptionsWindow : Window
    {
        public PlayOptionsWindow()
        {
            InitializeComponent();
        }

        public GameServiceClient Client { get;  set; }
        public string Username { get;  set; }
        public GameCallback CallBack { get; set; }

        private void Window_Loaded(object sender, RoutedEventArgs e) {
            BackButton.Source = new BitmapImage(new Uri(System.AppDomain.CurrentDomain.BaseDirectory + "/Resources/back-arrow.png"));
        }

        private void PlayOnlineClick(object sender, RoutedEventArgs e) {
            PlayerVsPlayerWindow playOnlineWindow = new PlayerVsPlayerWindow();
            playOnlineWindow.Client = Client;
            playOnlineWindow.CallBack = CallBack;
            playOnlineWindow.Username = Username;
            this.Close();
            playOnlineWindow.Show();
        }

        private void BackButton_MouseDown(object sender, MouseButtonEventArgs e) {
            LoginWindow loginWindow = new LoginWindow();
            this.Close();
            loginWindow.Show();
        }

        private void PlayAloneClick(object sender, RoutedEventArgs e) {
            PlayAloneConfigWindow configWindow = new PlayAloneConfigWindow();
            this.Close();
            configWindow.Show();
        }

        private void Window_Closed(object sender, EventArgs e) {
            MessageBox.Show("window closing event is fired");
            Client.ClientDisconnected(Username);
        }
    }
}
