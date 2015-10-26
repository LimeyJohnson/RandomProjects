using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectEuler
{
    public class Problem6 : IEulerProblem
    {
        double result;
        public Problem6()
        {
            double sumOfSquare = 0, sum =0;
            for (int x = 0; x <= 100; x++)
            {
                sumOfSquare += Math.Pow(x, 2);
                sum += x;
            }
            result = Math.Pow(sum, 2) - sumOfSquare;    

        }
        public string Answer()
        {
            return result.ToString();
        }
    }
}
