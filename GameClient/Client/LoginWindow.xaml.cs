using Client.ServiceReference;
using System;
using System.ServiceModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace Client {
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window {
        public LoginWindow() {
            InitializeComponent();
        }

        private void LoginClick(object sender, RoutedEventArgs e) {
            if (CheckInput()) {
                Login(UserNameTextBox.Text, PasswordTextBox.Password);
            }
        }

        private bool CheckInput() {
            if (!string.IsNullOrEmpty(UserNameTextBox.Text) && !string.IsNullOrEmpty(PasswordTextBox.Password)) {
                return true;
            }
            MessageBox.Show("You need to enter user name and password!",
                "Error occurred!", MessageBoxButton.OK, MessageBoxImage.Error);
            return false;
        }

        private void Login(string userName, string password) {
            GameCallback callback = new GameCallback();
            try {
                GameServiceClient client = new GameServiceClient(new InstanceContext(callback));
                client.ClientConnected(userName, password);
                GameOptionsWindow optionWindow = new GameOptionsWindow();
                optionWindow.Client = client;
                optionWindow.CallBack = callback;
                optionWindow.Username = userName;
                optionWindow.Title = userName;
                this.Close();
                optionWindow.Show();
            } 
            catch (FaultException<UserExistsFault> ex) { MessageBox.Show(ex.Detail.Message, "Oops!", MessageBoxButton.OK, MessageBoxImage.Information); }
            catch (FaultException ex) { MessageBox.Show(ex.Message, "Error occurred!", MessageBoxButton.OK, MessageBoxImage.Error); }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Error occurred!", MessageBoxButton.OK, MessageBoxImage.Error); }
        }

        private void RegisterClick(object sender, RoutedEventArgs e) {
            RegisterWindow registerWindow = new RegisterWindow();
            this.Close();
            registerWindow.Show();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.Icon = new BitmapImage(new Uri(System.AppDomain.CurrentDomain.BaseDirectory + "/Resources/app-icon2.png"));
        }
    }
}
