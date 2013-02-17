using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FantasyLeague.FEPLObjects;
using System.Threading;
namespace FantasyLeague
{
    class ProcessThread
    {
        private List<Player> m_Deck;
        private MyMax m_Max;
        private int m_StartIndex;
        public void RunAlgorithm()
        {
            PlayerTeam pt = new PlayerTeam();
            GetBestTeam(m_Deck, ref pt, StartIndex, ref m_Max.MAX, "BestForm");
        }
        private void GetBestTeam(List<Player> deck, ref PlayerTeam roster, int deckIndex, ref double max, string fileName)
        {
            //bool foundMatch = false;
            while (deckIndex + 1 < deck.Count)
            {
                int attempt = deckIndex + 1;
                if (roster.AddPlayer(deck[attempt]))
                {
                    double teamPoints = roster.Points;
                    if (roster.IsTeamFull)
                    {
                        if (teamPoints > max)
                        {
                            roster.WriteToFile(fileName + String.Format("{0:0.######}", teamPoints));
                            Interlocked.Exchange(ref max, teamPoints);
                            
                        }
                        
                    }
                    else if (roster.MoneyRemaining >= roster.MoneyNeeded/* hack only if sorted by assending price (deck[attempt].Price * roster.PlayersNeeded)*/ )
                    {
                        GetBestTeam(deck, ref roster, ++deckIndex, ref max, fileName);
                        
                    }
                    roster.RemovePlayer(deck[attempt]);
                }
                ++deckIndex;
            }
        }

        public int StartIndex
        {
            set { this.m_StartIndex = value; }
            get { return this.m_StartIndex; }
        }
        public MyMax Max
        {
            set { this.m_Max = value; }
            get { return m_Max; }
        }
        public List<Player> Deck
        {
            set { this.m_Deck = value; }
        }
    }
    public class MyMax
    {
        public double MAX;
    }
}
