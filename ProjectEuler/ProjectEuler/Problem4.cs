using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectEuler
{
    public class Problem4 : IEulerProblem
    {
        int result = 0;
        public string Answer()
        {
            for (int a = 999; a > 0; a--)
            {
                for (int b = a; b > 0; b--)
                {
                    int product = b*a;
                    if (product > result && isPalindrome(product))
                    {
                         result = product;
                    }
                }
            }
            return result.ToString();
        }
        public bool isPalindrome(int num)
        {
            string s = num.ToString();
            for (int x = 0; x < s.Length / 2; x++)
            {
                if (s[x] != s[s.Length -1 -x]) return false;
            }
            return true;
        }
    }
}
