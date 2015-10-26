using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectEuler
{
    public class Problem12 : IEulerProblem
    {
        public string Answer()
        {
            long num = 0;
            int divisorCount = 0;
            int x = 1;
            for (; divisorCount < 500; x++)
            {
                num += x;
                divisorCount = Helpers.DivisorCount(num);
            }
            return string.Format("Triangle #{0} whose value is {1} has {2} divisors", x, num, divisorCount);

        }
    }
}
