﻿using System;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Client.ServiceReference;

namespace Client {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class PlayerVsPlayerWindow : Window {
        public PlayerVsPlayerWindow() {
            InitializeComponent();
        }

        public GameServiceClient Client { get; internal set; }
        public GameCallback CallBack { get; internal set; }
        public string Username { get; internal set; }
    }
}
