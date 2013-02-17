using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FantasyLeague.FEPLObjects
{
    public class Game
    {
        public int gameID, gameWeek;
        public Game(int gameID, int gameWeek)
        {
            this.gameID = gameID;
            this.gameWeek = gameWeek;
        }

    }  
}
