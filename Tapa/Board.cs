using System.Collections.Generic;

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

        //Returns the cell at the given location, or a white cell if it's off the board
        public Cell At(int x, int y)
        {
            if (x >= 0 && x < Width && y >= 0 && y < Height)
            {
                return Cells[x + y * Width];
            }
            else
            {
                Cell whiteCell = new Cell();
                whiteCell.State = CellState.White;
                return whiteCell;
            }
        }

        public List<Cell> GetNeighbors(int x, int y, bool countDiagonals = true)
        {
            List<Cell> neighbors = new List<Cell>();
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
                neighbors.Add(At(x + dx[i], y + dy[i]));
            }
            return neighbors;
        }
    }
}
