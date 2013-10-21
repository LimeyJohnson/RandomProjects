using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FantasyLeague.FEPLObjects
{
    public class Team
    {
        public String teamName;
        public int teamID;


        public Team(String _teamName)
        {
            teamName = _teamName;
            if (_teamName.Length > 3) teamID = DataAccess.Instance.GetTeamIDByName(teamName);
            else teamID = DataAccess.Instance.GetTeamIDByInitials(teamName);
        }
       
    }
}
