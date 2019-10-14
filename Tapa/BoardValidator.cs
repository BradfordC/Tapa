using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tapa
{
    class BoardValidator
    {
        public Board board;

        private List<Point> ClueLocs;

        public BoardValidator(Board board)
        {
            this.board = board;
            ClueLocs = null;
        }

        public void FindClues()
        {
            ClueLocs = new List<Point>();
            for(int x = 0; x < board.Width; x++)
            {
                for(int y = 0; y < board.Height; y++)
                {
                    if (board.At(x, y).IsClue())
                    {
                        ClueLocs.Add(new Point(x, y));
                    }

                }
            }
        }

        public List<Point> GetClues()
        {
            if(ClueLocs == null)
            {
                FindClues();
            }
            return new List<Point>(ClueLocs);
        }

        public bool BoardSolved()
        {
            return ValidateClues() && ValidateSquares() && ValidateConnection(false);
        }

        //Sees if the board is still solvable.
        //If a cell is given, it is assumed that the board was solvable before that cell was changed.
        public bool BoardSolvable(Point? changedCell = null)
        {
            return !ImpossibleClues(changedCell) && ValidateSquares(changedCell) && ValidateConnection(true, changedCell);
        }

        public bool ValidateClues()
        {
            if(ClueLocs == null)
            {
                FindClues();
            }
            foreach(Point clueLoc in ClueLocs)
            {
                int x = clueLoc.X;
                int y = clueLoc.Y;

                if(!board.At(x, y).IsFulfilledClue(board.GetNeighbors(x, y)))
                {
                    return false;
                }
            }
            return true;
        }

        //Searches through the clues to see if any of them are impossible to fulfill.
        //If a cell is given, only the clues neighboring it are checked- the rest are assumed to be fulfilled.
        public bool ImpossibleClues(Point? changedCell = null)
        {
            if (ClueLocs == null)
            {
                FindClues();
            }
            foreach (Point clueLoc in ClueLocs)
            {
                int x = clueLoc.X;
                int y = clueLoc.Y;
                //Only check the clue if it's beside the cell that changed
                if(changedCell != null && !board.AreNeighbors(changedCell.Value, clueLoc))
                {
                    continue;
                }

                if (board.At(x, y).RemainingClueConfigs(board.GetNeighbors(x, y)) == 0)
                {
                    return true;
                }
            }
            return false;
        }

        //Make sure the path doesn't form a 2x2 square anywhere on the board
        public bool ValidateSquares(Point? changedCell = null)
        {
            //If the changed cell is not a path now, there's no way it's part in a square
            if(changedCell != null && !board.At(changedCell.Value).IsPath())
            {
                return true;
            }

            //For each cell, check the 2x2 square with it in the top left.
            //We don't need to check the far bottom or far right cells
            int[] dx = {0, 0, 1, 1 };
            int[] dy = {0, 1, 0, 1 };

            for(int x = 0; x < board.Width - 1; x++)
            {
                for(int y = 0; y < board.Height - 1; y++)
                {
                    for(int i = 0; i < 4; i++)
                    {
                        if(!board.At(x + dx[i], y + dy[i]).IsPath())
                        {
                            break;
                        }
                        if(i == 3)
                        {
                            return false;
                        }
                    }
                }
            }

            return true;
        }

        //See if all path tiles are connected (ignoring diagonals)
        //If a cell is given, only paths that could be affected by the cell changing are checked.
        //I.e. it is assumed that, before that cell was changed, all paths were connected
        public bool ValidateConnection(bool countEmpty, Point? changedCell = null)
        {
            //Defaults to false, so no need to initialize each element
            bool[,] cellVisited = new bool[board.Width,board.Height];

            Stack<Point> cellsToVisit = new Stack<Point>();
            //Find the first path cell to start the process
            if(changedCell != null)
            {
                Point changed = changedCell.Value;

                //If empty cells can be considered for connectivity, there's no way for an empty cell to break connectivity
                if (countEmpty && board.At(changed).State == CellState.Empty)
                {
                    return true;
                }

                //If we're focusing on a changed cell, start with the cells beside it.
                foreach (Point neighbor in board.GetNeighborLocations(changed.X, changed.Y, true, false))
                {
                    //All other paths at this point are assumed to be connected, so if this cell is a path and it sees a path, it'll be connected as well.
                    if (board.At(changed).IsPath() && board.At(neighbor).IsPath())
                    {
                        return true;
                    }
                    //If this cell breaks connections, see if there's a path potentially going through it
                    //An empty cell at this point breaks connections, since if it were empty and counted as a path, it would've been handled already
                    bool connectionBreakingState = board.At(changed).State == CellState.Wall || board.At(changed).State == CellState.Empty;
                    if (connectionBreakingState && board.At(neighbor).IsPath())
                    {
                        cellsToVisit.Push(neighbor);
                        break;
                    }
                }
                if (board.At(changed).IsPath())
                {
                    cellsToVisit.Push(changed);
                }
            }
            //If we don't have a specific cell to start at, find any black cell
            if (changedCell == null || cellsToVisit.Count == 0)
            {
                for (int x = 0; x < board.Width && cellsToVisit.Count == 0; x++)
                {
                    for (int y = 0; y < board.Height && cellsToVisit.Count == 0; y++)
                    {
                        if (board.At(x, y).IsPath())
                        {
                            foreach (Point neighbor in board.GetNeighborLocations(x, y, true, false))
                            {
                                if (!cellVisited[neighbor.X, neighbor.Y])
                                {
                                    cellsToVisit.Push(neighbor);
                                }
                            }
                            cellVisited[x, y] = true;
                        }
                        else if (!countEmpty)
                        {
                            cellVisited[x, y] = true;
                        }
                    }
                }
            }


            //Perform DFS to identify all path cells that are connected (or potentially connected)
            while(cellsToVisit.Count > 0)
            {
                Point cell = cellsToVisit.Pop();
                if(board.At(cell).IsPath() || (countEmpty && board.At(cell).IsPathable()))
                {
                    foreach (Point neighbor in board.GetNeighborLocations(cell.X, cell.Y, true, false))
                    {
                        if (!cellVisited[neighbor.X, neighbor.Y])
                        {
                            cellsToVisit.Push(neighbor);
                        }
                    }
                }
                cellVisited[cell.X, cell.Y] = true;
            }

            //Search through the board again to identify cells that are paths and are not connected to the group identified earlier
            for (int x = 0; x < board.Width && cellsToVisit.Count == 0; x++)
            {
                for (int y = 0; y < board.Height && cellsToVisit.Count == 0; y++)
                {
                    if (!cellVisited[x, y] && board.At(x, y).IsPath())
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}