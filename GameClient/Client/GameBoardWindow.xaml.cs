using Client.ServiceReference;
using GamesServer;
using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Client
{
    /// <summary>
    /// Interaction logic for GameBoardWindow.xaml
    /// </summary>
    public enum PlayerType
    {
        Home = 0,
        Away = 1
    }

    public partial class GameBoardWindow : Window
    {
        public GameMode.Mode Mode { get; set; }
        public PlayerType Type { get; set; }
        public GameServiceClient Client { get; set; }
        public GameCallback CallBack { get; set; }
        public GameParams GameParams { get; set; }
        private const int MAX_ITEMS_NUMBER = 100;
        private const int CELL_SIDE = 35;
        public LiveMatch Match { get; set; }
        public string UserName { get; set; }
        private Boolean OpponentLost = false;

        public GameBoardWindow()
        {
            InitializeComponent();

        }

        private void StartGame()
        {
            try
            {
                if(Mode == GameMode.Mode.Alone)
                {
                Match = Client.GetRandomGrid(GameParams.rows, GameParams.cols, GameParams.mines);
                }
                //sets MinesweeperGrid event hanlder
                Match.Board.ItemAdded += GameGrid_itemAdded;
                Match.Board.ItemMineAdded += GameGrid_itemMineAdded;
                Match.Board.LoadingCompleted += GameGrid_loadingCompleted;
                Match.Board.ErrorOccurred += GameGrid_errorOccurred;
                Match.Board.CellOpeningCompleted += GameGrid_cellOpeningCompleted;
                Match.Board.GameOver += GameGrid_gameOver;
                //makes game grid: it will raise a gameGrid_loadingCompleted() event
                Match.Board.MakeGrid();
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }


        }

        
        private void SetFormLayout()
        {
            //main layout will be displayed as a grid of buttons
            //this procedure will resize main form anduniform grid, considering that
            // on the grid will be rows*cols button with a height=CELL_SIDE and width=CELL_SIDE
            //Form Width=cols*CELL_SIDE
            //Form Height=rows*CELL_SIDE
            //We have laso to consider, on the height, size of controls
            try
            {

                //arbitrary value
                int sizeMargin = 100;

                this.Width = (Match.Board.Cols * CELL_SIDE) + sizeMargin;
                this.Height = (Match.Board.Rows * CELL_SIDE) + sizeMargin;

                gamePanel.Width = this.Width - sizeMargin;
                gamePanel.Height = this.Height - sizeMargin;

            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }


        private void MakeButtonsGrid()
        {
            //makeButtonsGrid will be invoked from gameGrid_loadingCompleted() event

            try
            {
                //set layout
                SetFormLayout();

                //sets cols and rows from the UniformGrid container
                gamePanel.Columns = Match.Board.Cols;
                gamePanel.Rows = Match.Board.Rows;
                //clear all items from the UniformGrid container
                gamePanel.Children.Clear();
                //for each grid item add a button on panel form 
                List<MinesweeperItem> items = Match.Board.Items;
                foreach (MinesweeperItem item in items)
                {
                    gamePanel.Children.Add(GetGridButton(item));
                }
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }

        }
        private Button GetGridButton(MinesweeperItem item)
        {
            try
            {
                //creates a button
                Button button = new Button();
                //stores the button on the item tag
                item.tag = button;
                //stores the item on the tag button 
                button.Tag = item;
                button.Content = ".";
                button.Width = CELL_SIDE;
                button.Height = CELL_SIDE;
                button.FontSize = 16;
                button.FontWeight = FontWeights.Bold;
                button.Click += GridButton_Click;
                return button;
            }
            catch (Exception ex)
            {
                HandleException(ex);
                return null;
            }

        }

        private void GridButton_Click(object sender, RoutedEventArgs e)
        {
            OpponentLost = false;
            if (Mode == GameMode.Mode.Online)
            {
                if (!IsMyTurn())
                {
                    return;
                }
            }
            try
            {
                Button button = (Button)sender;
                MinesweeperItem item = (MinesweeperItem)button.Tag;
                Thread notifyServer = new Thread(() => Client.FinishTurn(item.cell, Match.HomePlayer, Match.AwayPlayer, UserName));
                notifyServer.Start();
                Match.Board.EvaluateItem(item);
                if (IsAllCellsOpened())
                {
                    Thread notifyServerAboutTie = new Thread(() => Client.GameFinishInTie(Match.HomePlayer,Match.AwayPlayer));
                    notifyServerAboutTie.Start();
                }
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }

        private void GameGrid_gameOver(MinesweeperItem item)
        {
            if (!OpponentLost)
            {
                Thread notifyServer = new Thread(() => Client.PlayerLose(Match.HomePlayer, Match.AwayPlayer, UserName));
                notifyServer.Start();
                MessageBox.Show("You lost the game :(", "Sorry", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();

            }
        }

        private Boolean IsMyTurn()
        {
            Boolean whosTurn = false;
            Thread turnCheck = new Thread(() => whosTurn = Client.WhosTurn(Match.HomePlayer, Match.AwayPlayer));
            turnCheck.Start();
            Thread.Sleep(250);
            if (Type == PlayerType.Home && whosTurn == false)
            {
                return false;
            }
            if (Type == PlayerType.Away && whosTurn == true)
            {
                return false;
            }
            return true;
        }

        private void GameGrid_cellOpeningCompleted(MinesweeperItem item)
        {
            //this event will be raised after calling setAdjacentCells() method
            try
            {
                //gets the button from the item tag (see getGridButton method)
                Button button = (Button)item.tag;

                switch (item.type)
                {
                    case MinesweeperItemType.MinesweeperItem_Empty:
                        button.Content = "";
                        button.Background = Brushes.SeaGreen;
                        break;
                    case MinesweeperItemType.MinesweeperItem_Mine:
                        button.Content = "*";
                        button.Background = Brushes.Red;
                        break;
                    case MinesweeperItemType.MinesweeperItem_MineWarning:
                        button.Content = item.value.ToString();
                        button.Background = Brushes.Orange;
                        break;
                    case MinesweeperItemType.MinesweeperItemType_None:
                        break;
                }

                //if items is opened, remove the click event handler from the button
                if (item.type != MinesweeperItemType.MinesweeperItemType_None)
                {
                    button.Click -= GridButton_Click;
                }
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }

        }

        private void GameGrid_errorOccurred(Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
        private void GameGrid_loadingCompleted(List<MinesweeperItem> items)
        {
            //this event will be raised from the makeGrid method
            MakeButtonsGrid();
        }

        private void GameGrid_itemMineAdded(MinesweeperItem item)
        {
            //TODO
        }

        private void GameGrid_itemAdded(MinesweeperItem item)
        {
            //TODO
        }
        private void HandleException(Exception ex)
        {
            MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void PerformOpponentMove(MinesweeperItemCellDefinition cell)
        {
            OpponentLost = true;
            MinesweeperItem item = Match.Board.FindItemAt(cell);
            Match.Board.EvaluateItem(item);
        }

        private void NotifyWinner()
        {
            MessageBox.Show("You are the Winner :)", "Congratulations", MessageBoxButton.OK, MessageBoxImage.Information);
            this.Close();
        }

        private void NotifyTie()
        {
            MessageBox.Show("Game finish in a Tie -)", "Stretching Finish", MessageBoxButton.OK, MessageBoxImage.Information);
            this.Close();
        }

        private Boolean IsAllCellsOpened()
        {
            int closeCells = 0;
            foreach(MinesweeperItem item in Match.Board.Items)
            {
                Button button = (Button)item.tag;
                if (button.Content.Equals(".")) closeCells++;
            }
            return closeCells == Match.Board.MaxMines;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.Icon = new BitmapImage(new Uri(System.AppDomain.CurrentDomain.BaseDirectory + "/Resources/app-icon2.png"));
            if (CallBack.IsGameBoardEventsAreNull())
            {
                CallBack.updateOpponentBoard += PerformOpponentMove;
                CallBack.notifyWinner += NotifyWinner;
                CallBack.notifyTie += NotifyTie;
            }
            StartGame();
        }
    }
}

