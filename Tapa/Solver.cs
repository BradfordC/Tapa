using System;
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
            while (!foundSolution && i >= 0)
            {
                Cell cell = puzzle.Cells[i];
                if (cell.State == CellState.Wall)
                {
                    cell.State = CellState.Empty;
                    do
                    {
                        i--;
                    } while (i >= 0 && puzzle.Cells[i].IsClue());
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
                    else if(validator.BoardSolvable())
                    {
                        do
                        {
                            i++;
                        } while (i < puzzle.Cells.Count && puzzle.Cells[i].IsClue());
                        //Rewind here, in case the last few cells are clues
                        if(i == puzzle.Cells.Count)
                        {
                            while (i >= 0 && puzzle.Cells[i].IsClue())
                            {
                                i--;
                            }
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
    }
}
