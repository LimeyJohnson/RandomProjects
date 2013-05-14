using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectEuler
{
    public class Problem10
    {
        public override string ToString()
        {
            int sum = 0;
            for (int x = 0; x < 2000000; x++)
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
