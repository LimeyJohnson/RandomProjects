using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectEuler
{
    public class Problem14
    {
        Dictionary<int, int> KnownValues = new Dictionary<int, int>();
        string result;
        int max = 0, maxNum;
        public Problem14()
        {
            for (int x = 1; x < 1000000; x++)
            {
                int y = Collatz(x);
                if (y > max)
                {
                    max = y;
                    maxNum = x;
                }

               
            }
            result = string.Format("{0} has a sequence of {1}", maxNum, max);
        }
        public override string ToString()
        {
            return result;
        }
        public int Collatz(int num)
        {
            if (KnownValues.Keys.Contains(num))
            {
                return KnownValues[num];
            }
            if (num > 1)
            {
                int value;
                if (num % 2 == 0)
                {
                    // num is even
                    value = num / 2;
                }
                else
                {
                    value = 3 * num + 1;
                }
                
                int result = Collatz(value) + 1;
                KnownValues.Add(num, result);
                return result;

            }
            else return 1;
        }
    }
}
