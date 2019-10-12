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
    }
}