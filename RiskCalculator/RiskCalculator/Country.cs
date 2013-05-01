using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiskCalculator
{
    class Country
    {
        public List<Country> Countries { get; set; }
        public int ArmyCount;
        public Country(int armyCount)
        {
            Countries = new List<Country>();
            Countries.Add(this);
            this.ArmyCount = armyCount;
        }
        public static Country operator >(Country a, Country b)
        {
            a.Countries.AddRange(b.Countries);
            return a;
        }
        public static Country operator <(Country a, Country b)
        {
            throw new NotImplementedException("Don't support this operator");
        }
    }
    
}
