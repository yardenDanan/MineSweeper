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
    /// Interaction logic for GameInvitationWindow.xaml
    /// </summary>
    public partial class GameInvitationWindow : Window
    {
        public String InviteReciverName { get; set; }

        public GameInvitationWindow()
        {
            InitializeComponent();
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e) {

        }

        private void CheckBox_Checked_1(object sender, RoutedEventArgs e) {

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.Icon = new BitmapImage(new Uri(System.AppDomain.CurrentDomain.BaseDirectory + "/Resources/app-icon2.png"));
            competitorLabel.Content += InviteReciverName; 
        }
    }
}
