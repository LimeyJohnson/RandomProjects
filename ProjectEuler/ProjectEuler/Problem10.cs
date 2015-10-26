using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectEuler
{
    public class Problem10 : IEulerProblem
    {
        public string Answer()
        {
            long sum = 2;
            for (int x = 3; x < 2000000; x += 2)
            {
                if (Helpers.IsPrime(x))
                {
                    sum += x;
                }
            }
            return string.Format("sum = {0}", sum);
        }
    }
}
