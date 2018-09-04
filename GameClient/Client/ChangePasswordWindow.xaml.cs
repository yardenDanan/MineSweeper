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
    /// Interaction logic for ChangePasswordWindow.xaml
    /// </summary>
    public partial class ChangePasswordWindow : Window
    {
        public ChangePasswordWindow()
        {
            InitializeComponent();
        }

        public delegate void ChangePassDelegate(string oldPassword, string newPassword);
        public event ChangePassDelegate passwordChange;

        private void Change_Click(object sender, RoutedEventArgs e)
        {
            if(!string.IsNullOrEmpty(OldPasswordTextBox.Password) &&
                !string.IsNullOrEmpty(PasswordTextBox.Password) &&
                !string.IsNullOrEmpty(ConfirmPasswordTextBox.Password))
            {
                if (PasswordTextBox.Password.Equals(ConfirmPasswordTextBox.Password))
                {
                    passwordChange(OldPasswordTextBox.Password, PasswordTextBox.Password);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("New password does not confirm properly",
                        "Oops!", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            else
            {
                MessageBox.Show("You need to enter old password, new password and confirm it.",
                                       "Oops!", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.Icon = new BitmapImage(new Uri(System.AppDomain.CurrentDomain.BaseDirectory + "/Resources/app-icon2.png"));
        }
    }
}
