using Client.ServiceReference;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
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
    /// Interaction logic for RegisterWindow.xaml
    /// </summary>
    public partial class RegisterWindow : Window
    {
        public RegisterWindow()
        {
            InitializeComponent();
        }

        private void BackButton_MouseDown(object sender, MouseButtonEventArgs e) {
            LoginWindow loginWindow = new LoginWindow();
            this.Close();
            loginWindow.Show();
        }

        private bool CheckInput() {
            if (!string.IsNullOrEmpty(UserNameTextBox.Text) && !string.IsNullOrEmpty(PasswordTextBox.Password)) {
                return true;
            }
            MessageBox.Show("You need to enter user name and password!", "Oops!", MessageBoxButton.OK, MessageBoxImage.Information);
            return false;
        }

        private void Register(string userName, string password) {
            GameCallback callback = new GameCallback();
            try {
                GameServiceClient client = new GameServiceClient(new InstanceContext(callback));
                client.RegisterClient(userName, password);
                GameOptionsWindow optionWindow = new GameOptionsWindow();
                optionWindow.Client = client;
                optionWindow.CallBack = callback;
                optionWindow.Username = userName;
                optionWindow.Title = userName;
                this.Close();
                optionWindow.Show();
            } catch (FaultException<UserExistsFault> ex) { MessageBox.Show(ex.Detail.Message, "Oops!", MessageBoxButton.OK, MessageBoxImage.Information); } catch (FaultException ex) { MessageBox.Show(ex.Message, "Error occurred", MessageBoxButton.OK, MessageBoxImage.Error); } catch (Exception ex) { MessageBox.Show(ex.Message, "Error occurred", MessageBoxButton.OK, MessageBoxImage.Error); }
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e) {
            if (CheckInput()) {
                Register(UserNameTextBox.Text, PasswordTextBox.Password);
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e) {
            BackButton.Source = new BitmapImage(new Uri(System.AppDomain.CurrentDomain.BaseDirectory + "/Resources/back-arrow4.png"));
            this.Icon = new BitmapImage(new Uri(System.AppDomain.CurrentDomain.BaseDirectory + "/Resources/app-icon2.png"));
        }
    }
}
