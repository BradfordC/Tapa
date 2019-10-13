using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace Tapa
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Board tapa = new Board(10);
            tapa.At(3, 0).SetClues(1, 1);
            tapa.At(0, 1).SetClues(1);
            tapa.At(6, 1).SetClues(2, 2);
            tapa.At(9, 1).SetClues(2);
            tapa.At(3, 3).SetClues(1);
            tapa.At(2, 4).SetClues(1, 1);
            tapa.At(5, 4).SetClues(2);
            tapa.At(7, 4).SetClues(2, 2);
            tapa.At(2, 5).SetClues(4);
            tapa.At(4, 5).SetClues(4);
            tapa.At(7, 5).SetClues(3);
            tapa.At(6, 6).SetClues(3);
            tapa.At(0, 8).SetClues(4);
            tapa.At(3, 8).SetClues(4);
            tapa.At(9, 8).SetClues(3);
            tapa.At(6, 9).SetClues(3);
            
            Stopwatch stopwatch = Stopwatch.StartNew();
            Solver solver = new Solver();
            tapa = solver.SolveBoard(tapa);
            stopwatch.Stop();
            Console.WriteLine(stopwatch.Elapsed);

            MainForm form = new MainForm(tapa);
            Application.Run(form);
        }
    }
}
