using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectEuler
{
    public static class Helpers
    {
        public static bool IsPrime(long num)
        {
            int x = 2;
            while (x <= (num / x))
            {
                if (num % x == 0) return false;
                x++;
            }
            return true;
        }
        public static int DivisorCount(long num)
        {
            int sum = 0;
            long limit = (long)Math.Sqrt(num);
            for (int x = 1; x < limit; x++)
            {
                if (num % x == 0)
                {
                    sum++;
                }
            }
            return sum;
            }
    }
}
