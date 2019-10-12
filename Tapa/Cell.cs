﻿using System.Collections.Generic;

namespace Tapa
{
    public class Cell
    {
        public CellState State;
        public List<int> Clues;

        public Cell(List<int> clues = null)
        {
            if(clues == null)
            {
                clues = new List<int>();
            }

            State = CellState.Kitty;
            Clues = clues;
        }

        public bool IsClue()
        {
            return Clues.Count > 0;
        }

        public bool IsPathable()
        {
            if (IsClue() || State == CellState.White)
            {
                return false;
            }
            return true;
        }

        public bool IsPath()
        {
            if (State == CellState.Black)
            {
                return true;
            }
            return false;
        }

        public bool IsBrokenClue(List<Cell> neighbors)
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
            return true;
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
        Kitty,
        Black,
        White
    }
}
