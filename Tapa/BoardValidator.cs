using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tapa
{
    class BoardValidator
    {
        private List<int[]> ClueLocs;

        private Board board;

        public BoardValidator(Board board)
        {
            this.board = board;
            ClueLocs = null;
        }

        public void FindClues()
        {
            ClueLocs = new List<int[]>();
            for(int x = 0; x < board.Width; x++)
            {
                for(int y = 0; y < board.Height; y++)
                {
                    if (board.At(x, y).IsClue())
                    {
                        ClueLocs.Add(new int[] { x, y });
                    }

                }
            }
        }

        public bool ValidateClues()
        {
            if(ClueLocs == null)
            {
                FindClues();
            }
            foreach(int[] clueLoc in ClueLocs)
            {
                int x = clueLoc[0];
                int y = clueLoc[1];

                if(!board.At(x, y).IsFulfilledClue(board.GetNeighbors(x, y)))
                {
                    return false;
                }
            }
            return true;
        }
    }
}