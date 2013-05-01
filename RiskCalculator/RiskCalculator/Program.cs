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

            RunPlan(45, new Country(2) > new Country(2) >new Country(6) > new Country(20));
            RunBattle(2, 1);
            RunBattle(3, 1);
            RunBattle(4, 1);
            RunBattle(6, 1);
            RunBattle(7, 1);
            RunBattle(8, 1);
            
            RunBattle(2, 2);
            RunBattle(3, 2);
            RunBattle(4, 2);
            RunBattle(6, 3);
            RunBattle(5, 7);
            RunBattle(7,6);

            RunBattle(11,2);

            RunBattle(16,5 );
            RunBattle(10,5);
        }
        public static double RunBattle(int AArmy, int DArmy, int ADice = 3, int DDice =2, bool write= true)
        {
            RiskCalc calc = new RiskCalc();
            int aWins = 0, dWins = 0;
            double AArmySum = 0;
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
                AArmySum += calc.AArmyRemaining;
            }
            if(write)Console.WriteLine("Battle {0}:{1} vs {2}:{3} result: {4} - {5} ({6}) left: {7}", AArmy, ADice, DArmy, DDice, aWins, dWins,100*(((double) aWins)/RunCount), AArmySum / RunCount);
            return AArmySum / RunCount;
        }
        public static void RunPlan(int armyCount, Country d)
        {

            foreach (Country c in d.Countries)
            {

                armyCount = (int)Math.Round(RunBattle(armyCount, c.ArmyCount, 3, 2, false), 0) - 1;
                Console.WriteLine("Plan after Attacking {0} arms remaining {1}", c.ArmyCount, armyCount);

                if (armyCount <= 0)
                {
                    break;
                }
            }
        }
    }
}
