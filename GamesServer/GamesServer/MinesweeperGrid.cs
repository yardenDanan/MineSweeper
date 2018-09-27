using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace GamesServer
{
    [DataContract]
    public class MinesweeperGrid
    {
        //this event will be raised when a new item (cell) will be added
        public event MinesweeperItemAdded ItemAdded;
        public delegate void MinesweeperItemAdded(MinesweeperItem item);

        //this event will be raised when a "mine" item will be added (randomly, see MinesweeperMineGenerator)
        public event MinesweeperItemMineAdded ItemMineAdded;
        public delegate void MinesweeperItemMineAdded(MinesweeperItem item);

        //this event will be raised when the grid game will be completely loaded
        public event MinesweeperLoadingCompleted LoadingCompleted;
        public delegate void MinesweeperLoadingCompleted(List<MinesweeperItem> items);

        //this event will be raised when a new item will be open as empty cell or mine cell
        public event MinesweeperCellOpeningCompleted CellOpeningCompleted;
        public delegate void MinesweeperCellOpeningCompleted(MinesweeperItem item);

        //this event will be raised when item is a mine
        public event MinesweeperGameOver GameOver;
        public delegate void MinesweeperGameOver(MinesweeperItem item);

        //this event will be raised when an error occurs
        public event MinesweeperError ErrorOccurred;
        public delegate void MinesweeperError(Exception ex);

        //returns the number of grid rows (this attribute will be set on the constructor)
        [DataMember]
        public int Rows { get; protected set; }

        //returns the number of grid cols (this attribute will be set on the constructor)
        [DataMember]
        public int Cols { get; protected set; }
        //returns then number of max mines for the games (this attribute will be set on the constructor)
        [DataMember]
        public int MaxMines { get; protected set; }
        //returns a list of MinesweeperItem: it represents all items (cells) of the game grid
        [DataMember]
        public List<MinesweeperItem> Items { get; protected set; }

        //class constructors
        public MinesweeperGrid(int rows, int cols) : this(rows, cols, 0) { }
        public MinesweeperGrid(int rows, int cols, int maxMines)
        {
            try
            {
                //here will set and initialize some class attributes

                this.Rows = rows;
                this.Cols = cols;
                this.MaxMines = maxMines;
                //if the constructor maxMines param is not valid or zero,
                //maxMines will be set equal row-1 (arbitrary value)
                //however in a more complex solution we can suppose, 
                //to implement a calculation algorithm for this value, for instance 
                //based on the number of the cell (matrix rows*cols)
                if (this.MaxMines <= 0 || maxMines > (rows * cols))
                {
                    this.MaxMines = this.Rows - 1;
                }
                this.Items = new List<MinesweeperItem>();
            }
            catch (Exception e)
            {
                HanldeError(e);
            }
        }

        public void MakeGrid()
        {
            //it makes the game grid by adding and item for each cell (rox,cols)
            //and at the end of this process defines randmly the mine positions (mine cells)
            try
            {
                for (int r = 0; r < this.Rows; r++)
                {
                    for (int c = 0; c < this.Cols; c++)
                    {
                        MinesweeperItem item = new MinesweeperItem(new MinesweeperItemCellDefinition(r, c));
                        this.Items.Add(item);
                        if (ItemAdded != null)
                        {
                            ItemAdded(item);
                        }
                    }
                }
                //it places mines on the grid
                PlaceMines();

                //raise event
                if (LoadingCompleted != null)
                {
                    LoadingCompleted(this.Items);
                }
            }
            catch (Exception e)
            {
                HanldeError(e);
            }
        }
        private void PlaceMines()
        {

            try
            {
                int mineCounter = 0;
                int itemIndex = 0;
                //MinesweeperMineGenerator is an helper class that, randomly, choose a cell in a matrix of rows*cols
                MinesweeperMineGenerator mineGenerator = new MinesweeperMineGenerator(this.Rows * this.Cols);

                //performs a loop untli the number of mines placed on the grid will be equal the
                //maxMines chosen for the game
                while (mineCounter < this.MaxMines)
                {
                    //get randomly a number (cell index) of a matrix rows*cols
                    itemIndex = mineGenerator.make();
                    if (this.Items[itemIndex].type != MinesweeperItemType.MinesweeperItem_Mine)
                    {
                        //marks item as mine
                        this.Items[itemIndex].type = MinesweeperItemType.MinesweeperItem_Mine;
                        if (ItemMineAdded != null)
                        {
                            ItemMineAdded(this.Items[itemIndex]);
                        }
                        mineCounter++;
                    }
                }
            }
            catch (Exception e)
            {
                HanldeError(e);
            }

        }
        public void OpenAllCells()
        {
            //this method is useful when we are in a "game over" mode
            //it will "open" all cells with a proper value (empty, mine number warning, mine)
            foreach (MinesweeperItem item in this.Items)
            {
                SetAdjacentCells(item);
            }
        }
        public void EvaluateItem(MinesweeperItem item)
        {
            //with this method you can implement all the game behavior
            //because if the item will be a mine, then will be raised a game over event
            //and all cells will be opened
            //otherwise it will find all the item's adjacent cells
            try
            {
                if (item.type == MinesweeperItemType.MinesweeperItem_Mine)
                {
                    OpenAllCells();
                    //raise event game over
                    if (GameOver != null)
                    {
                        GameOver(item);
                    }
                }
                else
                {
                    SetAdjacentCells(item);
                }
            }
            catch (Exception ex)
            {
                HanldeError(ex);
            }


        }
        private List<MinesweeperItem> GetAdjacentCells(int row, int col)
        {

            //this method returns all the adjacent items (cells) around the (X,Y)item
            //each item,at most, it will be surrounded by eight items (cells): see the schema below

            // -------X
            // |
            // |
            // |Y
            //------------------------------------------------
            //          |           |           |           |
            //------------------------------------------------
            //  X-1,Y-1 | X,Y-1     | X+1,Y-1   |           |
            //------------------------------------------------
            // X-1,Y    |  X,Y      | X+1,Y     |           |
            //------------------------------------------------
            // X-1,Y+1  | X,Y+1     | X+1,Y+1   |           |
            //------------------------------------------------

            try
            {
                //steps below work as follow
                //1) defines cell item coordinate (MinesweeperItemCellDefinition)
                //2) try to find the item on the grid (findItemAt)
                //3) if the item exists it will be added on the return list object

                List<MinesweeperItem> ret = new List<MinesweeperItem>();
                MinesweeperItemCellDefinition adjacentCell = null;
                MinesweeperItem adjacentItem = null;
                //X - 1, Y - 1
                adjacentCell = new MinesweeperItemCellDefinition(row - 1, col - 1);
                adjacentItem = FindItemAt(adjacentCell);
                if (adjacentItem != null) ret.Add(adjacentItem);
                //X, Y - 1
                adjacentCell = new MinesweeperItemCellDefinition(row, col - 1);
                adjacentItem = FindItemAt(adjacentCell);
                if (adjacentItem != null) ret.Add(adjacentItem);
                //X + 1, Y - 1
                adjacentCell = new MinesweeperItemCellDefinition(row + 1, col - 1);
                adjacentItem = FindItemAt(adjacentCell);
                if (adjacentItem != null) ret.Add(adjacentItem);
                //X - 1, Y 
                adjacentCell = new MinesweeperItemCellDefinition(row - 1, col);
                adjacentItem = FindItemAt(adjacentCell);
                if (adjacentItem != null) ret.Add(adjacentItem);
                //X + 1, Y 
                adjacentCell = new MinesweeperItemCellDefinition(row + 1, col);
                adjacentItem = FindItemAt(adjacentCell);
                if (adjacentItem != null) ret.Add(adjacentItem);
                //X - 1, Y + 1
                adjacentCell = new MinesweeperItemCellDefinition(row - 1, col + 1);
                adjacentItem = FindItemAt(adjacentCell);
                if (adjacentItem != null) ret.Add(adjacentItem);
                //X , Y + 1
                adjacentCell = new MinesweeperItemCellDefinition(row, col + 1);
                adjacentItem = FindItemAt(adjacentCell);
                if (adjacentItem != null) ret.Add(adjacentItem);
                //X + 1, Y + 1
                adjacentCell = new MinesweeperItemCellDefinition(row + 1, col + 1);
                adjacentItem = FindItemAt(adjacentCell);
                if (adjacentItem != null) ret.Add(adjacentItem);


                return ret;
            }
            catch (Exception ex)
            {
                HanldeError(ex);
                return null;
            }
        }

        public void SetAdjacentCells(MinesweeperItem item)
        {

            try
            {

                int adjacentMines = 0;
                if (item.type == MinesweeperItemType.MinesweeperItemType_None)
                {
                    //if the item is not "opened" it could be
                    //1) An Item with adjacent mines:wil be raised a cellOpeningCompleted event and on the "client-side" you can perform the presentation of the adjacent mines by using the value property of the item (see MinesweeperItem)
                    //2) An empty item: recursively will be raised a cellOpeningCompleted event until an item will have an empty adjacent item

                    //gets all adjacent cells for te input item
                    List<MinesweeperItem> adjacentCells = GetAdjacentCells(item.cell.row, item.cell.col);
                    //loop thorugh all adjacent cells to calculate adjacent mines.
                    //adjacentMines could be zero or greater than zero
                    foreach (MinesweeperItem adjacentCell in adjacentCells)
                    {
                        if (CellIsTypeOf(adjacentCell.cell.row, adjacentCell.cell.col, MinesweeperItemType.MinesweeperItem_Mine))
                        {
                            adjacentMines++;
                        }
                    }

                    if (adjacentMines != 0)
                    {
                        //item has adjacentMines, so its type will be set as MinesweeperItem_MineWarning
                        item.type = MinesweeperItemType.MinesweeperItem_MineWarning;
                        item.value = adjacentMines;
                    }
                    else if (adjacentMines == 0)
                    {
                        //item does not have adjacentMines, so we have to inspect all empty cells
                        item.type = MinesweeperItemType.MinesweeperItem_Empty;
                        foreach (MinesweeperItem adjacentCell in adjacentCells)
                        {
                            MinesweeperItem adjacentItem = null;
                            CellIsTypeOf(adjacentCell.cell.row, adjacentCell.cell.col, MinesweeperItemType.MinesweeperItemType_None, out adjacentItem);
                            //here we implement a recursive behaviour to find all empty blocks
                            if (adjacentItem != null) SetAdjacentCells(adjacentItem);
                        }
                    }

                }
                //raise event
                if (CellOpeningCompleted != null)
                {
                    CellOpeningCompleted(item);
                }

            }
            catch (Exception e)
            {
                HanldeError(e);

            }
        }


        public MinesweeperItem FindItemAt(MinesweeperItemCellDefinition cell)
        {
            try
            {
                //find an item by coordinate (row,col)
                return this.Items.Find(x => (x.cell.col == cell.col && x.cell.row == cell.row));

                //if you prefer a Linq select on the items collection to find the item you can uss this:
                /*
                return (from item in this.items
                                        where (item.cell.col==cell.col && item.cell.row==cell.row)
                                        select item).FirstOrDefault();
                */
            }
            catch (Exception e)
            {
                HanldeError(e);
                return null;
            }


        }

        private bool CellIsTypeOf(int row, int col, MinesweeperItemType type)
        {
            MinesweeperItem item = null;
            return CellIsTypeOf(row, col, type, out item);
        }

        private bool CellIsTypeOf(int row, int col, MinesweeperItemType type, out MinesweeperItem item)
        {
            try
            {
                //find an item and compare the input type
                //return true if item is not null and the input type is the same of the found item type
                //return fals if the item is null or the input type is not equal to the found item type
                MinesweeperItemCellDefinition adjacentCell = null;
                MinesweeperItem adjacentItem = null;
                adjacentCell = new MinesweeperItemCellDefinition(row, col);
                adjacentItem = FindItemAt(adjacentCell);
                item = adjacentItem;
                if (adjacentItem != null)
                {
                    return adjacentItem.type == type;
                }
                return false;
            }
            catch (Exception e)
            {
                HanldeError(e);
                item = null;
                return false;
            }
        }

        private void HanldeError(Exception ex)
        {
            if (ErrorOccurred != null)
            {
                ErrorOccurred(ex);
            }
        }
    }
}
