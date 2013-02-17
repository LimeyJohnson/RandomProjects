using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FantasyLeague.FEPLObjects;
namespace FantasyLeague.Comparators
{
    class FormComparator: IComparer<Player>
    {
        public int Compare(Player a, Player b)
        {
            double formDiff = a.Form - b.Form;
            if (formDiff > 0) return -1;
            if (formDiff == 0) return 0;
            else return 1;
        }
    }
}
