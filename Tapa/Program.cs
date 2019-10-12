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

            Board tapa = new Board(4);
            tapa.At(0, 1).SetClues(1, 2);
            tapa.At(2, 1).SetClues(1, 3);

            MainForm form = new MainForm(tapa);
            Application.Run(form);
        }
    }
}
