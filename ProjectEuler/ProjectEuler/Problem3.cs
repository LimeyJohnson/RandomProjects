using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectEuler
{
    public class Problem3
    {
        long result = 0;
        long startNum = 600851475143;
        public Problem3()
        {
            int x=2;

            while (startNum > 1)
            {
                if (Helpers.IsPrime(x) && startNum % x == 0)
                {
                    startNum /= x;
                    result = Math.Max(x, result);
                    x = 1;//should be 2, but will be updated
                    
                }
                x++;
            }

        }
        public override string ToString()
        {
            return result.ToString();
        }
        
    }
}
