using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectEuler  
{
    public class Problem9 : IEulerProblem
    {
        public string Answer()
        {
            for (int a = 1; a < 1000; a++)
            {
                for (int b = a; b < 1000; b++)
                {
                    double c = Math.Sqrt(Math.Pow(a, 2) + Math.Pow(b, 2));
                    if (Math.Truncate(c) == c && a + b + c == 1000)
                    {
                        return string.Format("a = {0}, b = {1}, c = {2} product = {3}", a, b, c, a*b*c);
                    }
                }
            }
            return "unknown";
        }
    }
}
