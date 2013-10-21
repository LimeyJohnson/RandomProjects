using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FantasyLeague.FEPLObjects;
namespace FantasyLeague.Comparators
{
    class PriceComparator: IComparer<Player>
    {
        public int Compare(Player a, Player b)
        {
            double formDiff = a.Price - b.Price;
            if (formDiff > 0) return 1;
            if (formDiff == 0) return 0;
            else return -1;
        }
    }
}
