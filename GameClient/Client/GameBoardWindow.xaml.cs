using Client.ServiceReference;
using GamesServer;
using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Client
{
    /// <summary>
    /// Interaction logic for GameBoardWindow.xaml
    /// </summary>
    public partial class GameBoardWindow : Window
    {
        public GameServiceClient Client { get; set; }
        public GameParams GameParams { get; set; }
        private const int MAX_ITEMS_NUMBER = 100;
        private const int CELL_SIDE = 35;
        private MinesweeperGrid gameGrid;

        public GameBoardWindow()
        {
            InitializeComponent();

        }

        private void StartGame()
        {
            try
            {
                //creates an intance of MinesweeperGrid by using a GameParams instance (see gameParamsAreOk)
                gameGrid = Client.GetRandomGrid(GameParams.rows, GameParams.cols, GameParams.mines);
                //sets MinesweeperGrid event hanlder
                gameGrid.itemAdded += gameGrid_itemAdded;
                gameGrid.itemMineAdded += gameGrid_itemMineAdded;
                gameGrid.loadingCompleted += gameGrid_loadingCompleted;
                gameGrid.errorOccurred += gameGrid_errorOccurred;
                gameGrid.cellOpeningCompleted += gameGrid_cellOpeningCompleted;
                gameGrid.gameOver += gameGrid_gameOver;
                //makes game grid: it will raise a gameGrid_loadingCompleted() event
                gameGrid.makeGrid();
            }
            catch (Exception ex)
            {
                handleException(ex);
            }


        }

        
        private void setFormLayout()
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

                this.Width = (gameGrid.cols * CELL_SIDE) + sizeMargin;
                this.Height = (gameGrid.rows * CELL_SIDE) + sizeMargin;

                gamePanel.Width = this.Width - sizeMargin;
                gamePanel.Height = this.Height - sizeMargin;

            }
            catch (Exception ex)
            {
                handleException(ex);
            }
        }


        private void makeButtonsGrid()
        {
            //makeButtonsGrid will be invoked from gameGrid_loadingCompleted() event

            try
            {
                //set layout
                setFormLayout();

                //sets cols and rows from the UniformGrid container
                gamePanel.Columns = gameGrid.cols;
                gamePanel.Rows = gameGrid.rows;
                //clear all items from the UniformGrid container
                gamePanel.Children.Clear();
                //for each grid item add a button on panel form 
                List<MinesweeperItem> items = gameGrid.items;
                foreach (MinesweeperItem item in items)
                {
                    gamePanel.Children.Add(getGridButton(item));
                }
            }
            catch (Exception ex)
            {
                handleException(ex);
            }

        }
        private Button getGridButton(MinesweeperItem item)
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
                button.Click += gridButton_Click;
                return button;
            }
            catch (Exception ex)
            {
                handleException(ex);
                return null;
            }

        }

        private void gridButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Button button = (Button)sender;
                MinesweeperItem item = (MinesweeperItem)button.Tag;
                gameGrid.evaluateItem(item);
            }
            catch (Exception ex)
            {
                handleException(ex);
            }
        }
        private void gameGrid_gameOver(MinesweeperItem item)
        {
            MessageBox.Show("Oh no!\nI'm a mine :(", "GAME OVER", MessageBoxButton.OK, MessageBoxImage.Warning);
        }
        private void gameGrid_cellOpeningCompleted(MinesweeperItem item)
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
                    button.Click -= gridButton_Click;
                }
            }
            catch (Exception ex)
            {
                handleException(ex);
            }

        }

        private void gameGrid_errorOccurred(Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
        private void gameGrid_loadingCompleted(List<MinesweeperItem> items)
        {
            //this event will be raised from the makeGrid method
            makeButtonsGrid();
        }

        private void gameGrid_itemMineAdded(MinesweeperItem item)
        {
            //TODO
        }

        private void gameGrid_itemAdded(MinesweeperItem item)
        {
            //TODO
        }
        private void handleException(Exception ex)
        {
            MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            StartGame();
        }
    }
}

