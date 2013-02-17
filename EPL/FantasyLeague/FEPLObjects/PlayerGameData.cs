using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace FantasyLeague.FEPLObjects
{
    public class PlayerGameData
    {
        public int goals, played, assist, conceded, pensave, penmiss, yellow, red, saves, bonus, ownGoal, total, team, cleansheets, EAsports, transfer, player, gameweek, game;
        public double value;
        public PlayerGameData() // initializes to null
        {
            goals = played = assist = conceded = pensave = penmiss = yellow = red = saves = bonus = ownGoal = total = team = 0;
        }
        public PlayerGameData(int _goals, int _played, int _assist, int _conceded, int _pensave, int _penmiss, int _yellow, int _red, int _saves, int _bonus, int _ownGoal, int _total, int _team)
        {

            goals = _goals;
            played = _played;
            assist = _assist;
            conceded = _conceded;
            pensave = _pensave;
            penmiss = _penmiss;
            yellow = _yellow;
            red = _red;
            saves = _saves;
            bonus = _bonus;
            ownGoal = _ownGoal;
            total = _total;
            team = _team;
        }


        public bool Insert()
        {
            if (PlayerID != 0)
            {

                return DataAccess.Instance.InsertPlayerGameData(this, PlayerID);

            }
            else return false;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public static void InsertGameData(XmlNode node, Player player)
        {
            if (node.ChildNodes.Count != 19) throw new ArgumentException("Invalid number of childred in node. Maybe json has been updated");
            PlayerGameData gameData = new PlayerGameData();
            gameData.player = player.ID;
            gameData.gameweek = int.Parse(node.ChildNodes[1].InnerText);
            int otherTeam = new Team(node.ChildNodes[2].InnerText.Substring(0, 3)).teamID;

            if ((gameData.game = DataAccess.Instance.GetGameIDByTeams(otherTeam, Form1.SEASON, gameData.gameweek, out gameData.team)) == 0)
            {
                throw new ArgumentOutOfRangeException("Game could not be found");
            }
            gameData.played = int.Parse(node.ChildNodes[3].InnerText);
            gameData.goals = int.Parse(node.ChildNodes[4].InnerText);
            gameData.assist = int.Parse(node.ChildNodes[5].InnerText);
            gameData.cleansheets = int.Parse(node.ChildNodes[6].InnerText);
            gameData.conceded = int.Parse(node.ChildNodes[7].InnerText);
            gameData.ownGoal = int.Parse(node.ChildNodes[8].InnerText);
            gameData.pensave = int.Parse(node.ChildNodes[9].InnerText);
            gameData.penmiss = int.Parse(node.ChildNodes[10].InnerText);
            gameData.yellow = int.Parse(node.ChildNodes[11].InnerText);
            gameData.red = int.Parse(node.ChildNodes[12].InnerText);
            gameData.saves = int.Parse(node.ChildNodes[13].InnerText);
            gameData.bonus = int.Parse(node.ChildNodes[14].InnerText);
            gameData.EAsports = int.Parse(node.ChildNodes[15].InnerText);
            gameData.transfer = int.Parse(node.ChildNodes[16].InnerText);
            gameData.value = double.Parse(node.ChildNodes[17].InnerText);
            gameData.total = int.Parse(node.ChildNodes[18].InnerText);

            if(gameData.played>0)gameData.Insert();
        }

        #region Parameters
        public int PlayerID
        {
            get { return this.player; }
            set { this.player = value; }
        }
        public int GameWeek
        {
            get { return this.gameweek; }
            set { this.gameweek = value; }
        }
        #endregion
    }
}
