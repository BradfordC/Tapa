using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tapa
{
    public partial class MainForm : Form
    {
        private Board tapaBoard;
        private BoardValidator validator;


        public MainForm(Board board)
        {
            tapaBoard = board;
            validator = new BoardValidator(board);
            validator.FindClues();

            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);

            InitializeComponent();
        }

        private void DrawingPanel_Paint(object sender, PaintEventArgs e)
        {
            Panel panel = (Panel)sender;

            //Draw the boxes for the game
            int boardSize = Math.Min(panel.Width, panel.Height);
            int tileSize = boardSize / Math.Min(tapaBoard.Width, tapaBoard.Height);
            int cellBezel = 5;
            Size cellSize = new Size(tileSize - cellBezel * 2, tileSize - cellBezel * 2);
            for(int x = 0; x < tapaBoard.Width; x++)
            {
                for(int y = 0; y < tapaBoard.Height; y++)
                {
                    Cell cell = tapaBoard.At(x, y);
                    //Figure out what to print
                    Color color = Color.White;
                    string cellText = null;
                    if(cell.IsClue())
                    {
                        if(cell.IsFulfilledClue(tapaBoard.GetNeighbors(x, y)))
                        {
                            color = Color.Green;
                        }
                        else
                        {
                            color = Color.Red;
                        }
                        cellText = cell.GetClueString();
                    }
                    else
                    {
                        if(cell.State == CellState.Path)
                        {
                            color = Color.Black;
                        }
                        else if(cell.State == CellState.Wall)
                        {
                            color = Color.Cyan;
                        }
                        else
                        {
                            color = Color.White;
                        }
                    }
                    Point cellCorner = new Point(x * tileSize + cellBezel, y * tileSize + cellBezel);
                    e.Graphics.FillRectangle(new SolidBrush(color), new Rectangle(cellCorner, cellSize));
                    if(cellText != null)
                    {
                        Font font = new Font("Arial", 20);
                        SolidBrush brush = new SolidBrush(Color.Black);
                        StringFormat format = new StringFormat();
                        format.Alignment = StringAlignment.Center;
                        format.LineAlignment = StringAlignment.Center;

                        e.Graphics.DrawString(cellText, font, brush, Point.Add(cellCorner, new Size(cellSize.Width / 2, cellSize.Height / 2)), format);
                    }
                }
            }
        }

        private void DrawingPanel_MouseClick(object sender, MouseEventArgs e)
        {
            Panel panel = (Panel)sender;
            //Figure out which tile was clicked
            int boardSize = Math.Min(panel.Width, panel.Height);
            int tileSize = boardSize / Math.Min(tapaBoard.Width, tapaBoard.Height);
            Point mousePoint = panel.PointToClient(MousePosition);
            int x = mousePoint.X / tileSize;
            int y = mousePoint.Y / tileSize;
            //Switch the color of the clicked tile
            Cell cell = tapaBoard.At(x, y);
            
            if(!cell.IsClue())
            {
                CellState chosenState = (e.Button == MouseButtons.Left) ? CellState.Path : CellState.Wall;
                if(cell.State == chosenState)
                {
                    cell.State = CellState.Empty;
                }
                else
                {
                    cell.State = chosenState;
                }
            }

            this.CluePanel.BackColor = validator.ValidateClues() ? Color.Green : Color.Red;
            this.SquarePanel.BackColor = validator.ValidateSquares() ? Color.Green : Color.Red;
            this.ConnectionPanel.BackColor = validator.ValidateConnection(false) ? Color.Green : Color.Red;

            this.Refresh();
        }
    }
}
