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
        public event MinesweeperItemAdded itemAdded;
        public delegate void MinesweeperItemAdded(MinesweeperItem item);

        //this event will be raised when a "mine" item will be added (randomly, see MinesweeperMineGenerator)
        public event MinesweeperItemMineAdded itemMineAdded;
        public delegate void MinesweeperItemMineAdded(MinesweeperItem item);

        //this event will be raised when the grid game will be completely loaded
        public event MinesweeperLoadingCompleted loadingCompleted;
        public delegate void MinesweeperLoadingCompleted(List<MinesweeperItem> items);

        //this event will be raised when a new item will be open as empty cell or mine cell
        public event MinesweeperCellOpeningCompleted cellOpeningCompleted;
        public delegate void MinesweeperCellOpeningCompleted(MinesweeperItem item);

        //this event will be raised when item is a mine
        public event MinesweeperGameOver gameOver;
        public delegate void MinesweeperGameOver(MinesweeperItem item);

        //this event will be raised when an error occurs
        public event MinesweeperError errorOccurred;
        public delegate void MinesweeperError(Exception ex);

        //returns the number of grid rows (this attribute will be set on the constructor)
        [DataMember]
        public int rows { get; protected set; }

        //returns the number of grid cols (this attribute will be set on the constructor)
        [DataMember]
        public int cols { get; protected set; }
        //returns then number of max mines for the games (this attribute will be set on the constructor)
        [DataMember]
        public int maxMines { get; protected set; }
        //returns a list of MinesweeperItem: it represents all items (cells) of the game grid
        [DataMember]
        public List<MinesweeperItem> items { get; protected set; }

        //class constructors
        public MinesweeperGrid(int rows, int cols) : this(rows, cols, 0) { }
        public MinesweeperGrid(int rows, int cols, int maxMines)
        {
            try
            {
                //here will set and initialize some class attributes

                this.rows = rows;
                this.cols = cols;
                this.maxMines = maxMines;
                //if the constructor maxMines param is not valid or zero,
                //maxMines will be set equal row-1 (arbitrary value)
                //however in a more complex solution we can suppose, 
                //to implement a calculation algorithm for this value, for instance 
                //based on the number of the cell (matrix rows*cols)
                if (this.maxMines <= 0 || maxMines > (rows * cols))
                {
                    this.maxMines = this.rows - 1;
                }
                this.items = new List<MinesweeperItem>();
            }
            catch (Exception e)
            {
                hanldeError(e);
            }
        }

        public void makeGrid()
        {
            //it makes the game grid by adding and item for each cell (rox,cols)
            //and at the end of this process defines randmly the mine positions (mine cells)
            try
            {
                for (int r = 0; r < this.rows; r++)
                {
                    for (int c = 0; c < this.cols; c++)
                    {
                        MinesweeperItem item = new MinesweeperItem(new MinesweeperItemCellDefinition(r, c));
                        this.items.Add(item);
                        if (itemAdded != null)
                        {
                            itemAdded(item);
                        }
                    }
                }
                //it places mines on the grid
                placeMines();

                //raise event
                if (loadingCompleted != null)
                {
                    loadingCompleted(this.items);
                }
            }
            catch (Exception e)
            {
                hanldeError(e);
            }
        }
        private void placeMines()
        {

            try
            {
                int mineCounter = 0;
                int itemIndex = 0;
                //MinesweeperMineGenerator is an helper class that, randomly, choose a cell in a matrix of rows*cols
                MinesweeperMineGenerator mineGenerator = new MinesweeperMineGenerator(this.rows * this.cols);

                //performs a loop untli the number of mines placed on the grid will be equal the
                //maxMines chosen for the game
                while (mineCounter < this.maxMines)
                {
                    //get randomly a number (cell index) of a matrix rows*cols
                    itemIndex = mineGenerator.make();
                    if (this.items[itemIndex].type != MinesweeperItemType.MinesweeperItem_Mine)
                    {
                        //marks item as mine
                        this.items[itemIndex].type = MinesweeperItemType.MinesweeperItem_Mine;
                        if (itemMineAdded != null)
                        {
                            itemMineAdded(this.items[itemIndex]);
                        }
                        mineCounter++;
                    }
                }
            }
            catch (Exception e)
            {
                hanldeError(e);
            }

        }
        public void openAllCells()
        {
            //this method is useful when we are in a "game over" mode
            //it will "open" all cells with a proper value (empty, mine number warning, mine)
            foreach (MinesweeperItem item in this.items)
            {
                setAdjacentCells(item);
            }
        }
        public void evaluateItem(MinesweeperItem item)
        {
            //with this method you can implement all the game behavior
            //because if the item will be a mine, then will be raised a game over event
            //and all cells will be opened
            //otherwise it will find all the item's adjacent cells
            try
            {
                if (item.type == MinesweeperItemType.MinesweeperItem_Mine)
                {
                    openAllCells();
                    //raise event game over
                    if (gameOver != null)
                    {
                        gameOver(item);
                    }
                }
                else
                {
                    setAdjacentCells(item);
                }
            }
            catch (Exception ex)
            {
                hanldeError(ex);
            }


        }
        private List<MinesweeperItem> getAdjacentCells(int row, int col)
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
                adjacentItem = findItemAt(adjacentCell);
                if (adjacentItem != null) ret.Add(adjacentItem);
                //X, Y - 1
                adjacentCell = new MinesweeperItemCellDefinition(row, col - 1);
                adjacentItem = findItemAt(adjacentCell);
                if (adjacentItem != null) ret.Add(adjacentItem);
                //X + 1, Y - 1
                adjacentCell = new MinesweeperItemCellDefinition(row + 1, col - 1);
                adjacentItem = findItemAt(adjacentCell);
                if (adjacentItem != null) ret.Add(adjacentItem);
                //X - 1, Y 
                adjacentCell = new MinesweeperItemCellDefinition(row - 1, col);
                adjacentItem = findItemAt(adjacentCell);
                if (adjacentItem != null) ret.Add(adjacentItem);
                //X + 1, Y 
                adjacentCell = new MinesweeperItemCellDefinition(row + 1, col);
                adjacentItem = findItemAt(adjacentCell);
                if (adjacentItem != null) ret.Add(adjacentItem);
                //X - 1, Y + 1
                adjacentCell = new MinesweeperItemCellDefinition(row - 1, col + 1);
                adjacentItem = findItemAt(adjacentCell);
                if (adjacentItem != null) ret.Add(adjacentItem);
                //X , Y + 1
                adjacentCell = new MinesweeperItemCellDefinition(row, col + 1);
                adjacentItem = findItemAt(adjacentCell);
                if (adjacentItem != null) ret.Add(adjacentItem);
                //X + 1, Y + 1
                adjacentCell = new MinesweeperItemCellDefinition(row + 1, col + 1);
                adjacentItem = findItemAt(adjacentCell);
                if (adjacentItem != null) ret.Add(adjacentItem);


                return ret;
            }
            catch (Exception ex)
            {
                hanldeError(ex);
                return null;
            }
        }

        public void setAdjacentCells(MinesweeperItem item)
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
                    List<MinesweeperItem> adjacentCells = getAdjacentCells(item.cell.row, item.cell.col);
                    //loop thorugh all adjacent cells to calculate adjacent mines.
                    //adjacentMines could be zero or greater than zero
                    foreach (MinesweeperItem adjacentCell in adjacentCells)
                    {
                        if (cellIsTypeOf(adjacentCell.cell.row, adjacentCell.cell.col, MinesweeperItemType.MinesweeperItem_Mine))
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
                            cellIsTypeOf(adjacentCell.cell.row, adjacentCell.cell.col, MinesweeperItemType.MinesweeperItemType_None, out adjacentItem);
                            //here we implement a recursive behaviour to find all empty blocks
                            if (adjacentItem != null) setAdjacentCells(adjacentItem);
                        }
                    }

                }
                //raise event
                if (cellOpeningCompleted != null)
                {
                    cellOpeningCompleted(item);
                }

            }
            catch (Exception e)
            {
                hanldeError(e);

            }
        }


        public MinesweeperItem findItemAt(MinesweeperItemCellDefinition cell)
        {
            try
            {
                //find an item by coordinate (row,col)
                return this.items.Find(x => (x.cell.col == cell.col && x.cell.row == cell.row));

                //if you prefer a Linq select on the items collection to find the item you can uss this:
                /*
                return (from item in this.items
                                        where (item.cell.col==cell.col && item.cell.row==cell.row)
                                        select item).FirstOrDefault();
                */
            }
            catch (Exception e)
            {
                hanldeError(e);
                return null;
            }


        }

        private bool cellIsTypeOf(int row, int col, MinesweeperItemType type)
        {
            MinesweeperItem item = null;
            return cellIsTypeOf(row, col, type, out item);
        }

        private bool cellIsTypeOf(int row, int col, MinesweeperItemType type, out MinesweeperItem item)
        {
            try
            {
                //find an item and compare the input type
                //return true if item is not null and the input type is the same of the found item type
                //return fals if the item is null or the input type is not equal to the found item type
                MinesweeperItemCellDefinition adjacentCell = null;
                MinesweeperItem adjacentItem = null;
                adjacentCell = new MinesweeperItemCellDefinition(row, col);
                adjacentItem = findItemAt(adjacentCell);
                item = adjacentItem;
                if (adjacentItem != null)
                {
                    return adjacentItem.type == type;
                }
                return false;
            }
            catch (Exception e)
            {
                hanldeError(e);
                item = null;
                return false;
            }
        }

        private void hanldeError(Exception ex)
        {
            if (errorOccurred != null)
            {
                errorOccurred(ex);
            }
        }
    }
}
