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
            for (int dx = -1; dx <= 1; dx++)
            {
                for (int dy = -1; dy <= 1; dy++)
                {
                    //Don't add the cell at the center
                    if (dx == 0 && dy == 0)
                    {
                        continue;
                    }
                    //Don't add the diagonals if indicated
                    if (countDiagonals && dx != 0 && dy != 0)
                    {
                        continue;
                    }
                    neighbors.Add(At(x + dx, y + dy));
                }
            }
            return neighbors;
        }
    }
}
