using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiskCalculator
{
    class Program
    {
        static double RunCount = 1000;
        static void Main(string[] args)
        {
            RunBattle(14, 10);
            RunBattle(30, 20);
        }
        public static void RunBattle(int AArmy, int DArmy, int ADice = 3, int DDice =2)
        {

            RiskCalc calc = new RiskCalc();
            int aWins = 0, dWins = 0, a;
            for(int x = 0; x< RunCount; x++)
            {
                if (calc.Battle(AArmy, DArmy, ADice, DDice))
                {
                    aWins++;
                }
                else
                {
                    dWins++;
                }
            }
            Console.WriteLine("Battle {0}:{1} vs {2}:{3} result: {4} - {5} ({6})", AArmy, ADice, DArmy, DDice, aWins, dWins,100*(((double) aWins)/RunCount));
        }
    }
}
