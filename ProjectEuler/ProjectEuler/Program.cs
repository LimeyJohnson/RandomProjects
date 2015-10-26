using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectEuler
{
    class Program
    {
        static void Main(string[] args)
        {
            IEulerProblem problem = new Problem15();
            Console.WriteLine(problem.Answer());
            Console.Read();



        }
    }
}
