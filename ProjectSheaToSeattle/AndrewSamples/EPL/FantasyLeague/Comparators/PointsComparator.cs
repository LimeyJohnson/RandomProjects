using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FantasyLeague.FEPLObjects;
namespace FantasyLeague.Comparators
{
    class PointsComparator: IComparer<Player>
    {
        public int Compare(Player a, Player b)
        {
            double pointsDiff = a.Points - b.Points;
            if (pointsDiff > 0) return -1;
            if (pointsDiff == 0) return 0;
            else return 1;
        }
    }
}
