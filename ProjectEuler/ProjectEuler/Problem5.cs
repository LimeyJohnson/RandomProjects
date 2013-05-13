using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectEuler
{
    public class Problem5
    {
        static int[] divisibles = { 20, 19, 18, 17, 16, 15, 14, 13, 12, 11 };
        int result = 0;
        public Problem5()
        {
            int x = 0;
            while (result == 0)
            {
                if (checkDivision(x)) result = x ;
                x++;
            }


        }

        public override string ToString()
        {
            return result.ToString();

        }
        public bool checkDivision(int num)
        {
            for (int x = 0; x < divisibles.Length; x++)
            {
                if (num % divisibles[x] != 0) return false; 
            }
            return true;
        }
    }
}
