using Client.ServiceReference;
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
using System.Windows.Shapes;

namespace Client
{
    /// <summary>
    /// Interaction logic for DeleteAccountWindow.xaml
    /// </summary>
    public partial class DeleteAccountWindow : Window
    {
        public DeleteAccountWindow()
        {
            InitializeComponent();
        }

        public delegate void DeleteDelegate(string password);
        public event DeleteDelegate deleteAccount;

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(PasswordTextBox.Password)
                && !string.IsNullOrEmpty(ConfirmPasswordTextBox.Password))
            {
                if (PasswordTextBox.Password.Equals(ConfirmPasswordTextBox.Password))
                {
                   deleteAccount(PasswordTextBox.Password);
                   this.Close();
                }
                else
                {
                    MessageBox.Show("Password not confirmed", "Notice",
                               MessageBoxButton.OK, MessageBoxImage.Information);
                }
                this.Close();
            }
            else
            {
                MessageBox.Show("You need to enter your password and confirm it.", "Notice",
                                MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.Icon = new BitmapImage(new Uri(System.AppDomain.CurrentDomain.BaseDirectory + "/Resources/app-icon2.png"));
        }
    }
}
