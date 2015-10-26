using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectEuler
{
    public class Problem15 : IEulerProblem
    {
        public string Answer()
        {

            const int SIZE = 21;
            var numbers = new double[SIZE, SIZE];
            for (int x = 0; x < SIZE; x++)
            {
                numbers[0, x] = 1;
                numbers[x, 0] = 1;
            }

            for (int x = 1; x < SIZE; x++)
            {
                for (int y = 1; y < SIZE; y++)
                {
                    numbers[x, y] = numbers[x - 1, y] + numbers[x, y - 1];
                }
            }
            var result = numbers[SIZE - 1, SIZE - 1];
            return numbers[SIZE - 1, SIZE - 1].ToString();
        }

    }
}
