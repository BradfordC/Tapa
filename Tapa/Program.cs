using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tapa
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Board tapa = new Board(3);
            tapa.At(1, 0).State = CellState.Black;
            tapa.At(1, 1).Clues.Add(1);
            tapa.At(1, 1).Clues.Add(2);

            MainForm form = new MainForm(tapa);
            Application.Run(form);
        }
    }
}
