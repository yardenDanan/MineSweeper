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

        public string UserName { get; set; }
        public GameServiceClient Client { get; set; }

        public DeleteAccountWindow()
        {
            InitializeComponent();
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(PasswordTextBox.Password) &&
                !string.IsNullOrEmpty(ConfirmPasswordTextBox.Password))
            {
                if (PasswordTextBox.Password.Equals(ConfirmPasswordTextBox.Password))
                {
                    MessageBoxResult userChoice = MessageBox.Show("Are you sure you want to delete your account?",
                    "No way back", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (userChoice.Equals(MessageBoxResult.Yes))
                    {
                        Client.DeleteAccount(UserName,PasswordTextBox.Password);
                    }
                    else
                    {
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("You need to enter your password and confirm it.", "Notice",
                    MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
            }

        }
    }
}
