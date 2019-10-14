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

            MeasurePerformance(10, 1);

            Board tapa = GetPuzzle(1);

            Stopwatch stopwatch = Stopwatch.StartNew();
            Solver solver = new Solver();
            tapa = solver.SolveBoard(tapa);
            stopwatch.Stop();
            Console.WriteLine(stopwatch.Elapsed);

            MainForm form = new MainForm(tapa);
            Application.Run(form);
        }

        static Board GetPuzzle(int choice)
        {
            Board puzzle = null;
            if(choice == 0)
            {
                puzzle = new Board(5);
                puzzle.At(1, 1).SetClues(2, 2);
                puzzle.At(2, 1).SetClues(3, 1, 1);
                puzzle.At(2, 4).SetClues(3);
            }
            else if(choice == 1)
            {
                puzzle = new Board(10);
                puzzle.At(3, 0).SetClues(1, 1);
                puzzle.At(0, 1).SetClues(1);
                puzzle.At(6, 1).SetClues(2, 2);
                puzzle.At(9, 1).SetClues(2);
                puzzle.At(3, 3).SetClues(1);
                puzzle.At(2, 4).SetClues(1, 1);
                puzzle.At(5, 4).SetClues(2);
                puzzle.At(7, 4).SetClues(2, 2);
                puzzle.At(2, 5).SetClues(4);
                puzzle.At(4, 5).SetClues(4);
                puzzle.At(7, 5).SetClues(3);
                puzzle.At(6, 6).SetClues(3);
                puzzle.At(0, 8).SetClues(4);
                puzzle.At(3, 8).SetClues(4);
                puzzle.At(9, 8).SetClues(3);
                puzzle.At(6, 9).SetClues(3);
            }
            else if(choice == 2)
            {
                puzzle = new Board(13);
                int[] x = {3, 1, 7, 4, 9, 2, 11, 0, 5, 10, 12, 5, 8, 3, 5, 9};
                int[] y = {0, 1, 1, 2, 3, 4, 4, 5, 5, 6, 7, 8, 10, 11, 11, 12};
                for(int i = 0; i < x.Length; i++)
                {
                    puzzle.At(x[i], y[i]).SetClues(4);
                }
                x = new int[] {9, 11, 7, 2, 7, 1, 10, 3, 1, 11};
                y = new int[] {1, 1, 4, 6, 7, 8, 8, 9, 11, 11};
                for (int i = 0; i < x.Length; i++)
                {
                    puzzle.At(x[i], y[i]).SetClues(7);
                }
            }
            else if (choice == 3)
            {
                puzzle = new Board(16);
                int[] x = { 8, 15, 0, 10, 12, 9, 5, 15, 14, 0, 4, 7};
                int[] y = { 0, 0, 2, 4, 4, 8, 9, 13, 14, 15, 15, 15};
                for(int i = 0; i < x.Length; i++)
                {
                    puzzle.At(x[i], y[i]).SetClues(2);
                }
                x = new int[] { 11, 1, 4, 13, 5, 2, 7, 15, 10, 2, 4, 6, 11, 13, 0, 8, 13, 3, 5, 10, 2, 11 };
                y = new int[] { 0, 1, 2, 2, 4, 5, 5, 5, 6, 7, 7, 7, 8, 8, 10, 10, 10, 11, 11, 11, 13, 13 };
                for (int i = 0; i < x.Length; i++)
                {
                    puzzle.At(x[i], y[i]).SetClues(2, 2);
                }
            }
            return puzzle;
        }

        static void MeasurePerformance(int reps, int puzzle)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            Solver solver = new Solver();
            for (int i = 0; i < reps; i++)
            {
                Board tapa = GetPuzzle(puzzle);
                tapa = solver.SolveBoard(tapa);
            }
            stopwatch.Stop();
            Console.WriteLine("Total: " + stopwatch.Elapsed);
            Console.WriteLine("Average: " + TimeSpan.FromTicks(stopwatch.Elapsed.Ticks / reps));
        }
    }
}
