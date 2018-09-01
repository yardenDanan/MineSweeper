using Client.ServiceReference;
using System;
using System.ServiceModel;
using System.Windows;


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
            MessageBox.Show("You need to enter user name and password!");
            return false;
        }

        private void Login(string userName, string password) {
            GameCallback callback = new GameCallback();
            try {
                GameServiceClient client = new GameServiceClient(new InstanceContext(callback));
                client.ClientConnected(userName, password);
                PlayOptionsWindow optionWindow = new PlayOptionsWindow();
                optionWindow.Client = client;
                optionWindow.CallBack = callback;
                optionWindow.Username = userName;
                optionWindow.Title = userName;
                this.Close();
                optionWindow.Show();
            } 
            catch (FaultException<UserExistsFault> ex) { MessageBox.Show(ex.Detail.Message); }
            catch (FaultException ex) { MessageBox.Show(ex.Message); }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void RegisterClick(object sender, RoutedEventArgs e) {
            if (CheckInput()) {
                Register(UserNameTextBox.Text, PasswordTextBox.Password);
            }
        }

        private void Register(string userName, string password) {
            GameCallback callback = new GameCallback();
            try {
                GameServiceClient client = new GameServiceClient(new InstanceContext(callback));
                client.RegisterClient(userName, password);
                PlayOptionsWindow optionWindow = new PlayOptionsWindow();
                optionWindow.Client = client;
                optionWindow.CallBack = callback;
                optionWindow.Username = userName;
                optionWindow.Title = userName;
                this.Close();
                optionWindow.Show();
            } 
            catch (FaultException<UserExistsFault> ex) { MessageBox.Show(ex.Detail.Message); }
            catch (FaultException ex) { MessageBox.Show(ex.Message); }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
    }
}
