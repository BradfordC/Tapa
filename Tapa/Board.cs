using System.Collections.Generic;
using System.Drawing;

namespace Tapa
{
    public class Board
    {
        public int Width;
        public int Height;
        public List<Cell> Cells;

        public Board(int size)
        {
            Width = size;
            Height = size;
            Cells = new List<Cell>();
            for (int i = 0; i < Width * Height; i++)
            {
                Cells.Add(new Cell());
            }
        }

        //Returns the cell at the given location, or a wall cell if it's off the board
        public Cell At(int x, int y)
        {
            if (x >= 0 && x < Width && y >= 0 && y < Height)
            {
                return Cells[x + y * Width];
            }
            else
            {
                Cell wallCell = new Cell();
                wallCell.State = CellState.Wall;
                return wallCell;
            }
        }

        //Returns the cell at the given location, or a wall cell if it's off the board
        public Cell At(Point location)
        {
            return At(location.X, location.Y);
        }

        public List<Cell> GetNeighbors(int x, int y, bool countDiagonals = true)
        {
            List<Cell> neighbors = new List<Cell>();
            foreach(Point neighborLoc in GetNeighborLocations(x, y, false, countDiagonals))
            {
                neighbors.Add(At(neighborLoc));
            }
            return neighbors;
        }

        public List<Point> GetNeighborLocations(int x, int y, bool stayInbounds = false, bool countDiagonals = true)
        {
            List<Point> neighborLocs = new List<Point>();
            //Neighbors should be added in the following order
            //  123
            //  8x4
            //  765
            int[] dx = { -1, 0, 1, 1, 1, 0, -1, -1 };
            int[] dy = { -1, -1, -1, 0, 1, 1, 1, 0 };

            for (int i = 0; i < dx.Length; i++)
            {
                //Don't add the diagonals if indicated
                if (!countDiagonals && dx[i] != 0 && dy[i] != 0)
                {
                    continue;
                }
                if(stayInbounds && (x + dx[i] >= Width || x + dx[i] < 0 || y + dy[i] >= Height || y + dy[i] < 0))
                {
                    continue;
                }
                neighborLocs.Add(new Point(x + dx[i], y + dy[i]));
            }
            return neighborLocs;
        }
    }
}
