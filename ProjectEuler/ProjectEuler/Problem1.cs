using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectEuler
{
    public class Problem1:IEulerProblem
    {
        int result = 0;
        public Problem1(int upperBound)
        {
            for (int x = 0; x < upperBound; x++)
            {
                if (x % 5 == 0 || x % 3 == 0)
                {
                    result += x;
                }
            }
        }

        public string Answer()
        {
            return result.ToString();
        }
    }
}
