using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FantasyLeague.Comparators;
using System.IO;

namespace FantasyLeague.FEPLObjects
{
    public class PlayerTeam
    {
        private const int TOTALNEEDED = 15, TEAMPRICE = 99;
        private const double AVGPLAYERCOST = 4.5;
        private List<Player> m_Players;
        private double m_form, m_price, m_points;
        private int[] m_PositionCounts = { 0, 0, 0, 0 }, m_PositionMax = { 2, 5, 5, 3 }, m_PositionMin = { 1, 3, 0, 1 };

        public PlayerTeam()
        {
            m_Players = new List<Player>();

        }

        public Boolean AddPlayer(Player player)
        {
            if (m_price + player.Price <= TEAMPRICE)
            {
                
                if (IsValidFormation(player.Position))
                {
                    m_form += player.Form;
                    m_price += player.Price;
                    m_points += player.Points;
                    m_PositionCounts[(int)player.Position]++;
                    m_Players.Add(player);
                    return true;
                }
                return false;
            }
            return false;

        }
        public int TotalAllowed
        {
            get { return TOTALNEEDED; }
        }
        public double Price
        {
            get
            {
                return m_price;
            }
        }
        public double Form
        {
            get
            {
                return m_form;
            }
        }
        public double Points
        {
            get
            {
                return m_points;
            }
        }
        public Boolean IsValidFormation(Player.Positions position)
        {
            int positionIndex = (int) position;
            if (m_PositionCounts[positionIndex] + 1 <= m_PositionMax[positionIndex])
            {
                int playersNeededNotInThisPosition = 0;
                for (int x = 0; x < 4; x++)
                {
                    if (x != positionIndex) playersNeededNotInThisPosition += Math.Max(m_PositionMin[x] - m_PositionCounts[x],0);
                   
                }
                return (PlayersNeeded - 1) >= playersNeededNotInThisPosition;
            }
            else return false;
        }
       
        public void WriteToFile(String FileName)
        {
           lock(this)
           {
            String path = Path.Combine("C:\\Development\\EPL","EPLResults\\"+ FileName +".csv");
            StreamWriter sw = new StreamWriter(path);
            int x = 1;
            foreach (Player p in m_Players)
            {
                sw.WriteLine(x + ", " + p.PlayerName + ", " + p.Team + ", " + p.Position + ", " + p.Price + ", " + p.Form+", "+p.Points);
                x++;
            }
            sw.WriteLine("Total Points, " + this.m_points + ",, Total Form, " + this.Form + ",, Total Price: " + this.Price);
            sw.Close();
           }
        }
        public Boolean IsTeamFull
        {
            get { return this.m_Players.Count == TotalAllowed; }
        }
        public void RemovePlayer(Player p)
        {
            m_Players.Remove(p);
            m_form -= p.Form;
            m_price -= p.Price;
            m_points -= p.Points;
            m_PositionCounts[(int)p.Position]--;
        }
        public int PlayersNeeded
        {
            get { return TotalAllowed - m_Players.Count; }
        }
        public double MoneyNeeded
        {
            get { return PlayersNeeded * AVGPLAYERCOST; }
        }
        public double MoneyRemaining
        {
            get { return TEAMPRICE - Price; }
        }

    }
}
