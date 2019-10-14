using System;
using System.Collections.Generic;
using System.Linq;

namespace Tapa
{
    public class Cell
    {
        public CellState State;

        private List<int> Clues;

        public Cell(List<int> clues = null, CellState state = CellState.Empty)
        {
            if(clues == null)
            {
                clues = new List<int>();
            }

            State = state;
            Clues = clues;
        }

        //Create a deep copy of the given Cell
        public Cell(Cell other) : this(new List<int>(other.Clues), other.State)
        {
        }

        public void AddClues(params int[] clues)
        {
            if(Clues == null)
            {
                Clues = new List<int>();
            }

            foreach(int clue in clues)
            {
                Clues.Add(clue);
            }
        }

        public void ClearClues()
        {
            Clues = new List<int>();
        }

        public void SetClues(params int[] clues)
        {
            ClearClues();
            AddClues(clues);
        }

        public bool IsClue()
        {
            return Clues.Count > 0;
        }

        public bool IsPathable()
        {
            if (IsClue() || State == CellState.Wall)
            {
                return false;
            }
            return true;
        }

        public bool IsPath()
        {
            if (State == CellState.Path)
            {
                return true;
            }
            return false;
        }

        public bool IsFulfilledClue(List<Cell> neighbors)
        {
            if (!IsClue())
            {
                return false;
            }
            //Check neighbors for line segments
            List<int> segments = new List<int>();
            int currentSegment = 0;
            foreach (Cell neighbor in neighbors)
            {
                if (neighbor.IsPath())
                {
                    currentSegment += 1;
                }
                else if (currentSegment > 0)
                {
                    segments.Add(currentSegment);
                    currentSegment = 0;
                }
            }
            //If it ended on a line segment, check to see if it's connected to the starting segment, since the neighbors form a loop
            if (currentSegment > 0)
            {
                if (neighbors[0].IsPath() && segments.Count > 0)
                {
                    segments[0] += currentSegment;
                }
                else
                {
                    segments.Add(currentSegment);
                }
            }

            //Compare current line segments to clue
            List<int> goal = new List<int>(Clues);
            List<int> actual = new List<int>(segments);
            goal.Sort();
            goal.Reverse();
            actual.Sort();
            actual.Reverse();

            if (goal.Count != actual.Count)
            {
                return false;
            }
            for (int i = 0; i < goal.Count; i++)
            {
                //If any segments are larger than they should be, the clue has been broken
                if(actual[i] != goal[i])
                {
                    return false;
                }
            }
            return true;
        }

        public int RemainingClueConfigs(List<Cell> neighbors)
        {
            if(!IsClue())
            {
                return 0;
            }
            //Deep clone here to prevent affecting the actual cells
            List<Cell> neighborClones = new List<Cell>();
            neighbors.ForEach(x => neighborClones.Add(new Cell(x)));

            //Figure out which neighboring cells are still under consideration (i.e. aren't paths, walls, or clues)
            //We'll be backtracking, so we don't want to worry about the assigned cells
            List<Cell> unresolvedCells = new List<Cell>();
            for(int i = 0; i < neighborClones.Count; i++)
            {
                if(neighborClones[i].IsPathable() && !neighborClones[i].IsPath())
                {
                    unresolvedCells.Add(neighborClones[i]);
                }
            }

            //Go through all possible configurations, counting the valid ones
            int validConfigurations = 0;
            for(int i = 0; i < 1 << unresolvedCells.Count; i++)
            {
                //Set each cell's value based on a bit in a number
                for (int j = 0; j < unresolvedCells.Count; j++)
                {
                    Cell neighbor = unresolvedCells[j];
                    neighbor.State = ((i & (1 << j)) != 0) ? CellState.Path : CellState.Wall;
                }

                //See if it's a valid configuration
                if (IsFulfilledClue(neighborClones))
                {
                    validConfigurations++;
                }
            }

            return validConfigurations;
        }

        public string GetClueString()
        {
            if(!IsClue())
            {
                return null;
            }

            string clueString = "";
            foreach(int clue in Clues)
            {
                clueString += clue + " ";
            }
            clueString.Remove(clueString.Length - 1);
            return clueString;
        }
    }

    public enum CellState
    {
        Empty,
        Path,
        Wall
    }
}
