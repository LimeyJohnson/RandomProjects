using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectEuler
{
    public class Problem2 : IEulerProblem
    {
        int result = 0;
        public Problem2()
        {
            int last1 = 2, last2 = 1;
            while (last1 < 4000000)
            {
                if(last1%2 ==0)result += last1;
                int lower = last1;
                last1 = last2 + last1;
                last2 = lower;
                
            }
        }
        public string Answer()
        {
            return result.ToString();
        }
    }
}
