using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectEuler
{
    public class Problem14 : IEulerProblem
    {
        //SortedDictionary<int, int> KnownValues = new SortedDictionary<int, int>();
        string result;
        KeyValuePair<int, int> max = new KeyValuePair<int,int>(0,0);
        long maxNum, maxIndex;
        public Problem14()
        {
            for (long x = 1; x < 1000001; x++)
            {
                int y = Collatz(x);
                if (y > maxNum)
                {
                    maxIndex = x;
                    maxNum = y;
                }
            }
            //foreach (KeyValuePair<int, int> pair in KnownValues)
            //{
            //    if (pair.Value > max.Value) max = pair;
            //}
            result = string.Format("{0} has a length of {1}", maxIndex, maxNum);
        }
        public string Answer()
        {
            return result;
        }
        public int Collatz(long num)
        {
            //if (KnownValues.Keys.Contains(num))
            //{
            //    return KnownValues[num];
            //}
            if (num > 1)
            {
                long value;
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
                //KnownValues.Add(num, result);
                return result;

            }
            else return 1;
        }
    }
}
