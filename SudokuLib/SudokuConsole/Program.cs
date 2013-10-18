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
            b[0, 5].Value = 7;
            b[0, 6].Value = 4;
            b[0, 7].Value = 9;
            b[0, 8].Value = 1;
            b[1, 0].Value = 3;
            b[1, 4].Value = 1;
            b[1, 5].Value = 6;
            b[1, 7].Value = 7;
            b[2, 1].Value = 9;
            b[2, 6].Value = 3;
            b[3,2].Value = 1;
            b[3,3].Value =7;
            b[4,3].Value =1;
            b[4,4].Value= 3;
            b[4,5].Value = 4;
            b[5,5].Value =9; 
            b[5,6].Value =7;
            b[6,2].Value = 8;
            b[6,7].Value=2;
            b[7,1].Value=2;
            b[7,3].Value =8;
            b[7,4].Value=4;
            b[7,8].Value = 7;
            b[8,0].Value = 6; 
            b[8,1].Value = 7;
            b[8,2].Value =4;
            b[8, 3].Value = 5;
            b[8,4].Value = 9;
           
            b[0, 3].Value = 3;
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
