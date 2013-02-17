using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FantasyLeague.FEPLObjects
{
    public class Player
    {
        private String m_PlayerName, m_TeamName, m_FirstName, m_PhotoURL;
        private int m_PlayerID, m_Played, m_Total, m_TeamID, m_FLID;
        private double m_Price, /*temporary until they fix the site*/ m_Points;
        private Positions m_Position;
        private bool m_Active;
        public enum Positions
        {
            Defender = 1,
            Midfielder = 2,
            Forward = 3,
            Goalkeeper = 0,
            Undefined
        }

        public Player()
        {

        }
        public static Player GetOrCreatePlayer(string playerName, string playerTeam)
        {
            DataAccess DA = DataAccess.Instance;
            Player p = DA.GetPlayer(playerName, playerTeam);
            if (p == null)
            {
                DA.InsertPlayer(playerName, playerTeam);
                p = DA.GetPlayer(playerName, playerTeam);
            }
            return p;
        }
        public bool Update()
        {
            return DataAccess.Instance.UpdatePlayer(this);
        }
        public static Positions GetPositionFromString(string position)
        {
            if (!String.IsNullOrEmpty(position))
            {
                return (Positions)Enum.Parse(typeof(Positions), position);
            }
            else return Positions.Undefined;
        }

        #region Parameters
        public string PhotoURL
        {
            get { return m_PhotoURL; }
            set { m_PhotoURL = value; }
        }
        public string PlayerName
        {
            get { return m_PlayerName; }
            set { m_PlayerName = value; }
        }
        public string FirstName
        {
            get { return m_FirstName; }
            set { m_FirstName = value; }
        }
        public int FLID
        {
            get { return m_FLID; }
            set { m_FLID = value; }
        }
        public int ID
        {
            get { return m_PlayerID; }
            set { m_PlayerID = value; }
        }
        public int Played
        {
            get { return m_Played; }
            set { m_Played = value; }
        }
        public int Total
        {
            get { return m_Total; }
            set { m_Total = value; }
        }
        public double Price
        {
            get { return m_Price; }
            set { m_Price = value; }
        }
        public int PlayerID
        {
            get { return m_PlayerID; }
        }
        public double Form
        {
            get { return (double)Total / (double)Played; }
        }

        public Positions Position
        {
            get { return this.m_Position; }
            set { this.m_Position = value; }
        }
        public bool Active
        {
            get { return m_Active; }
            set { m_Active = value; }
        }
        public int TeamID
        {
            get { return m_TeamID; }
            set { m_TeamID = value; }
        }
        public string Team
        {
            get { return m_TeamName; }
            set { m_TeamName = value; }
        }
        public double Points
        {
            get { return m_Points; }
            set { m_Points = value; }
        }
#endregion

    }
}
