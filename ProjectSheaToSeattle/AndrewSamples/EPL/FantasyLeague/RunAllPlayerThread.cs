using FantasyLeague.FEPLObjects;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Xml;

namespace FantasyLeague
{
     class RunAllPlayerThread
    {
        public int GameWeek;
        Form1 CallingForm;
        public RunAllPlayerThread(int gw, Form1 callingForm)
        {
            GameWeek = gw;
            this.CallingForm = callingForm;
        }
        public void Run()
        {
            //int maxFLID = DataAccess.Instance.GetPlayers("FLID IS NOT NULL ORDER BY FLID DESC")[0].FLID;
            int maxFLID = 1000;
            try
            {
                int x = 1;
                while(true)
                {
                    UpdateStatus((((double)x) / maxFLID) * 100);
                    RunPlayer(x++);
                }
            }
            catch (System.Net.WebException e)
            {
                if (e.Message != "The remote server returned an error: (404) Not Found." && e.Message!="The remote server returned an error: (500) Internal Server Error.")
                {
                    //We have a real unexpected exception
                    throw new System.Net.WebException(e.Message, e);
                }
            }
            UpdateStatus(100); 

        }
        private void RunPlayer(int FLID)
        {
             WebRequest request = WebRequest.Create("http://fantasy.premierleague.com/web/api/elements/"+FLID + "/");
            request.Method = "GET";
            Stream response = request.GetResponse().GetResponseStream();

            StreamReader responseReader = new StreamReader(response);
            String responseString = responseReader.ReadToEnd();

            XmlDocument doc = (XmlDocument)JsonConvert.DeserializeXmlNode(responseString, "PlayerData");

            Player p = DataAccess.Instance.GetPlayer(FLID);

            if (p != null)
            { // We know this player, lets update
                //Get the gameweek data
                XmlNode historyList = doc.GetElementsByTagName("fixture_history")[0];
                foreach (XmlNode node in historyList.ChildNodes)
                {
                    //child child #1 is gameweek
                    int jsonGameweek;
                    if (int.TryParse(node.ChildNodes[1].InnerText, out jsonGameweek))
                    {
                        if (jsonGameweek == GameWeek)
                            PlayerGameData.InsertGameData(node, p);
                    }
                }
                p.Price = double.Parse(doc.GetElementsByTagName("now_cost")[0].InnerText) / 10.0;
                p.FirstName = doc.GetElementsByTagName("first_name")[0].InnerText;
                p.Active = doc.GetElementsByTagName("status")[0].InnerText != "a" ? false : true;
                p.PhotoURL = doc.GetElementsByTagName("photo_mobile_url")[0].InnerText;
                p.Position = (Player.Positions) Enum.Parse(typeof(Player.Positions), doc.GetElementsByTagName("type_name")[0].InnerText);
                p.Update();
            }
            else
            {
                //this is a new Player
                string Name = doc.GetElementsByTagName("web_name")[0].InnerText;
                string TeamID = (new Team(doc.GetElementsByTagName("team_name")[0].InnerText)).teamID.ToString();
                DataAccess.Instance.InsertPlayer(Name, TeamID, FLID);
                this.RunPlayer(FLID);
                
            }
        }
        private void UpdateStatus(double status)
        {
            CallingForm.Invoke(CallingForm.UpdateRunAllPlayerStatusDelegate,status);
        }
    }
}
