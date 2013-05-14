using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectEuler
{
    class Problem7
    {
        long result = 0;
        public Problem7()
        {
            int foundCount = 1, x = 3;
            while (foundCount != 10001)
            {
                if (Helpers.IsPrime(x))
                {
                    result = x;
                    foundCount++;
                }
                x += 2;
            }
        }
        public override string ToString()
        {
            return result.ToString();
        }
    }
}
