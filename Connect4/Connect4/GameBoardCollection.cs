using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connect4
{
    internal class GameBoardCollection : List<GameBoard>
    {
        static GameBoardComparator comparer= new GameBoardComparator();
        public bool MyContains(GameBoard b)
        {
            return this.Contains(b, comparer);
        }
    }
    public class GameBoardComparator : IEqualityComparer<GameBoard>
    {
        public bool Equals(GameBoard x, GameBoard y)
        {
            return x.Equals(y);
        }

        public int GetHashCode(GameBoard obj)
        {
            return obj.HashCode.Value;
        }
    }
    
   

}
