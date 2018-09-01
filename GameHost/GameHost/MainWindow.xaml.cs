using System.Windows;
using GamesServer;
using System.ServiceModel;
using System.ServiceModel.Description;
using System;

namespace GameHost {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();
        }
        ServiceHost host = null;
        private void Window_Loaded(object sender, RoutedEventArgs e) {
            try {
                host = new ServiceHost(typeof(GameService));
                host.Description.Behaviors.Add(
                    new ServiceMetadataBehavior { HttpGetEnabled = true });
                host.Open();
                lbload.Content = "Server Is Running";
            } catch (InvalidOperationException ex) {
                MessageBox.Show(ex.Message);
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e) {
            host.Close();
        }
    }
}
