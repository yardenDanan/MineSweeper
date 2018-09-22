﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
    /// Interaction logic for WaitForOpponentWindow.xaml
    /// </summary>
    public partial class WaitForOpponentWindow : Window
    {
        public string OpponentName { get; set; }
        public GameCallback CallBack { get; set; }

        public WaitForOpponentWindow()
        {
            InitializeComponent();
        }

        private void RequestRejected()
        {
            MessageBox.Show(OpponentName + " has rejected your game request.", "Notice!", MessageBoxButton.OK, MessageBoxImage.Information);
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.Icon = new BitmapImage(new Uri(System.AppDomain.CurrentDomain.BaseDirectory + "/Resources/app-icon2.png"));
            GifImage.Source = new Uri(System.AppDomain.CurrentDomain.BaseDirectory + "/Resources/Spin-1.5s-80px.gif");
            titleText.Content = "Waiting for " + OpponentName + " response";
            CallBack.cancelInvitation += RequestRejected;
        }
    }
}
