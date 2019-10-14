using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tapa
{
    class Solver
    {
        public Board SolveBoard(Board puzzle)
        {
            BoardValidator validator = new BoardValidator(puzzle);
            int i = 0;
            bool foundSolution = false;
            List<Point> cells = GenerateCellPriority(validator);

            while (!foundSolution && i >= 0)
            {
                Cell cell = puzzle.At(cells[i]);
                if (cell.State == CellState.Wall)
                {
                    cell.State = CellState.Empty;
                    i--;
                }
                else
                {
                    if (cell.State == CellState.Empty)
                    {
                        cell.State = CellState.Path;
                    }
                    else if (cell.State == CellState.Path)
                    {
                        cell.State = CellState.Wall;
                    }

                    if(validator.BoardSolved())
                    {
                        foundSolution = true;
                    }
                    else if(validator.BoardSolvable(cells[i]))
                    {
                        i++;
                        //Rewind here, in case the last few cells are clues
                        if(i == cells.Count)
                        {
                            i--;
                        }
                    }
                }
            }
            if(i < 0)
            {
                return null;
            }

            return puzzle;
        }

        private List<Point> GenerateCellPriority(BoardValidator validator)
        {
            List<Point> cells = new List<Point>();

            Board puzzle = validator.board;
            bool[,] cellAdded = new bool[puzzle.Width, puzzle.Height];
            foreach (Point clue in validator.GetClues())
            {
                foreach (Point clueNeighbor in puzzle.GetNeighborLocations(clue.X, clue.Y, true, true))
                {
                    if(!cellAdded[clueNeighbor.X, clueNeighbor.Y] && !puzzle.At(clueNeighbor).IsClue())
                    {
                        cellAdded[clueNeighbor.X, clueNeighbor.Y] = true;
                        cells.Add(clueNeighbor);
                    }
                }
            }

            for (int x = 0; x < puzzle.Width; x++)
            {
                for(int y = 0; y < puzzle.Height; y++)
                {
                    if(!cellAdded[x, y] && !puzzle.At(x, y).IsClue())
                    {
                        cells.Add(new Point(x, y));
                    }
                }
            }
            return cells;
        }
    }
}
