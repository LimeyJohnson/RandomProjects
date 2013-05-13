using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectEuler
{
    public static class Helpers
    {
        public static bool isPrime(long num)
        {
            int x = 2;
            while (x <= (num / x))
            {
                if (num % x == 0) return false;
                x++;
            }
            return true;
        }
    }
}
