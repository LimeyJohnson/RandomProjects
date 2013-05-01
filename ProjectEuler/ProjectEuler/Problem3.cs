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
            long maxPrime = startNum >0?startNum / 2:0; // facetor has to be less than 1/2 the original value so start looking there
            int x = 2;
            
            while(true)
            {
                while (!isPrime(x)) x++;
                long numToTry = startNum / x;
                if (startNum % numToTry == 0 && isPrime(numToTry))
                {
                    result = x;
                    return;
                }
                x++;
            }

        }
        public override string ToString()
        {
            return result.ToString();
        }
        public bool isPrime(long num)
        {
            int x = 2;
            while(x<=(num/x))
            {
                if (num % x == 0) return false;
                x++;
            }
            return true;
        }
    }
}
