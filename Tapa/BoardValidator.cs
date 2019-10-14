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

        public bool BoardSolvable()
        {
            return !ImpossibleClues() && ValidateSquares() && ValidateConnection(true);
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

        public bool ImpossibleClues()
        {
            if (ClueLocs == null)
            {
                FindClues();
            }
            foreach (Point clueLoc in ClueLocs)
            {
                int x = clueLoc.X;
                int y = clueLoc.Y;

                if (board.At(x, y).RemainingClueConfigs(board.GetNeighbors(x, y)) == 0)
                {
                    return true;
                }
            }
            return false;
        }

        //Make sure the path doesn't form a 2x2 square anywhere on the board
        public bool ValidateSquares()
        {
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
        public bool ValidateConnection(bool countEmpty)
        {
            //Defaults to false, so no need to initialize each element
            bool[,] cellVisited = new bool[board.Width,board.Height];

            Stack<Point> cellsToVisit = new Stack<Point>();
            //Find the first path cell to start the process
            for(int x = 0; x < board.Width && cellsToVisit.Count == 0; x++)
            {
                for(int y = 0; y < board.Height && cellsToVisit.Count == 0; y++)
                {
                    if(board.At(x, y).IsPath())
                    {
                        foreach(Point neighbor in board.GetNeighborLocations(x, y, true, false))
                        {
                            if(!cellVisited[neighbor.X,neighbor.Y])
                            {
                                cellsToVisit.Push(neighbor);
                            }
                        }
                        cellVisited[x, y] = true;
                    }
                    else if(!countEmpty)
                    {
                        cellVisited[x, y] = true;
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