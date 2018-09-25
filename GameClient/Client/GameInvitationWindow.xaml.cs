using Client.ServiceReference;
using GamesServer;
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
        public string Username { get; set; }
        public String InviteReciverName { get; set; }
        public GameServiceClient Client { get; set; }
        public GameCallback CallBack { get; set; }
        public PlayerStats PlayerStats { get;  set; }

        public GameInvitationWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.Icon = new BitmapImage(new Uri(System.AppDomain.CurrentDomain.BaseDirectory + "/Resources/app-icon2.png"));
            competitorLabel.Content += InviteReciverName; 
        }

        private void BegginerCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            if (IntermediateCheckBox.IsChecked == true)
            {
                IntermediateCheckBox.IsChecked = false;
            }
            if (ExpertCheckBox.IsChecked == true)
            {
                ExpertCheckBox.IsChecked = false;
            }
            if (CustomCheckBox.IsChecked == true)
            {
                CustomCheckBox.IsChecked = false;
            }

        }

        private void IntermediateCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            if (BegginerCheckBox.IsChecked == true)
            {
                BegginerCheckBox.IsChecked = false;
            }
            if (ExpertCheckBox.IsChecked == true)
            {
                ExpertCheckBox.IsChecked = false;
            }
            if (CustomCheckBox.IsChecked == true)
            {
                CustomCheckBox.IsChecked = false;
            }
        }

        private void ExpertCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            if (IntermediateCheckBox.IsChecked == true)
            {
                IntermediateCheckBox.IsChecked = false;
            }
            if (BegginerCheckBox.IsChecked == true)
            {
                BegginerCheckBox.IsChecked = false;
            }
            if (CustomCheckBox.IsChecked == true)
            {
                CustomCheckBox.IsChecked = false;
            }
        }

        private void CustomCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            if (IntermediateCheckBox.IsChecked == true)
            {
                IntermediateCheckBox.IsChecked = false;
            }
            if (ExpertCheckBox.IsChecked == true)
            {
                ExpertCheckBox.IsChecked = false;
            }
            if (BegginerCheckBox.IsChecked == true)
            {
                BegginerCheckBox.IsChecked = false;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            GameParams gameParams = null;
            if (BegginerCheckBox.IsChecked == true)
            {
                gameParams = new GameParams(8, 8, 10);
            }
            else if (IntermediateCheckBox.IsChecked == true)
            {
                gameParams = new GameParams(16, 16, 40);
            }
            else if (ExpertCheckBox.IsChecked == true)
            {
                gameParams = new GameParams(16, 30, 99);
            }

            else if (CustomCheckBox.IsChecked == true)
            {
                int height = 0, width = 0, bombs = 0;
                try
                {
                    int.TryParse(CustomHeight.Text, out height);
                    int.TryParse(CustomWidth.Text, out width);
                    int.TryParse(CustomBombs.Text, out bombs);

                    if (height < 1 || height > 100 || width < 1 || width > 100 ||
                        (height * width) / 3 < bombs)
                    {
                        MessageBox.Show("Input is not valid",
                        "Error occurred!", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }

                    gameParams = new GameParams(height, width, bombs);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Oops!", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }

            else
            {
                return;
            }
            try
            {
            Client.SendInvitation(Username, InviteReciverName, gameParams,PlayerStats);             
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Oops!", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            WaitForOpponentWindow waitForOpponentWindow = new WaitForOpponentWindow();
            waitForOpponentWindow.OpponentName = InviteReciverName;
            waitForOpponentWindow.CallBack = CallBack;
            waitForOpponentWindow.Client = Client;
            waitForOpponentWindow.UserName = Username;
            waitForOpponentWindow.CallBack = CallBack;
            waitForOpponentWindow.Show();
            this.Close();
        }
    }
}
