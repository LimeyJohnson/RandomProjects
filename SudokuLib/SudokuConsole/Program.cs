using SudokuLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Board b = new Board();
            b[0, 5].Value = 1;
            b[0, 6].Value = 7;
            b[0, 7].Value = 3;
            b[0, 8].Value = 8;
            b[1, 4].Value = 3;
            b[1, 5].Value = 7;
            b[1, 7].Value = 9;
            b[2, 1].Value = 7;
            b[2, 2].Value = 3;
            b[2, 3].Value = 5;
            b[2, 6].Value = 4;
            b[2, 7].Value = 2;
            b[2, 8].Value = 1;
            b[3, 1].Value = 2;
            b[3, 3].Value = 3;
            b[3, 4].Value = 6;
            b[3, 5].Value = 4;
            b[3, 6].Value = 8;
            b[4, 0].Value = 3;

            b[4, 4].Value = 7;
            b[4, 7].Value = 4;
            b[4, 8].Value = 2;
            b[5, 0].Value = 4;
            b[5, 1].Value = 8;
            b[5, 2].Value = 7;
            b[5, 3].Value = 1;
            b[5, 4].Value = 2;
            b[5, 5].Value = 5;
            b[5, 6].Value = 9;
            b[5, 7].Value = 6;
            b[5, 8].Value = 3;
            b[6, 0].Value = 7;
            b[6, 2].Value = 4;
            b[6, 5].Value = 2;
            b[6, 6].Value = 3;
            b[7, 1].Value = 5;
            b[7, 3].Value = 7;
            b[7, 5].Value = 3;
            b[8, 0].Value = 8;
            b[8, 1].Value = 3;
            b[8, 2].Value = 1;
            b[8, 6].Value = 2;

            b[4, 6].Value = 5;
            b[8, 5].Value = 9;
            bool rows = true, columns = true, grid = true;
            while (rows || columns || grid)
            {
                rows = b.TestRowsForSingleAvailible();
                columns = b.TestColumnForSingleAvailible();
                grid = b.TestGridForSingleAvailible();
            }
            Console.Out.Write("The End;");



        }
    }
}
