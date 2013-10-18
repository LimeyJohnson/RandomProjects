using System;
using System.Diagnostics;
using System.Linq;
using Facebook;
using System.Collections.Generic;
using System.Windows;
using System.Globalization;
using core = FriendsFlockCore;

namespace FriendsService
{
    public class GetFriendsMapFql
    {
        string fbToken;
        long userUid;
        List<long> friendsList;
        FacebookClient client;
        int pageNum = 0;
        const int PAGESIZE = 35;
        int pageTotal;
        int resultsRecieved = 0;
        
        
        //Variables for Analytics
        DateTime StartTime;
        int MaxFqlReturnSize = 0;
        int EdgeNum = 0;

        Dictionary<long, List<long>> FriendsMap = new Dictionary<long, List<long>>();
        public delegate void GetFriendsMapEventHandler(object sender, GetFriendsMapEventArgs e);
        public event GetFriendsMapEventHandler GetFriendsMap_Complete;

        public delegate void FriendsMapProgressEventHandler(object sender, FriendsMapProgressEventArgs e);
        public event FriendsMapProgressEventHandler FriendsMapProgress_Changed;

        public GetFriendsMapFql(string fbToken, long userUid, List<long> friendsList)
        {
            this.fbToken = fbToken;
            this.userUid = userUid;
            this.friendsList = friendsList;
        }

        public void Run()
        {
            core.Analytics.SendMetric(core.Analytics.Metric.Friends, friendsList.Count);
            StartTime = DateTime.Now;
            Debug.WriteLine(string.Format("GetFriendsMap: {0}", "Run"));
            pageTotal = (friendsList.Count / PAGESIZE) + 1;
            //add all the friends to the results
            foreach (long uid in friendsList)
            {
                FriendsMap.Add(uid, new List<long>());
            }
            //queue up all of the pages
            while (pageNum < pageTotal) kickOffPage();


        }
        private void kickOffPage()
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            string query = "{\"friendsAll\":\"SELECT uid1, uid2 from friend WHERE uid1 = " + userUid.ToString() + "\", \"friendsLimit\":\"SELECT uid1, uid2 from friend WHERE uid1 = " + userUid.ToString() + " ORDER BY uid2 " + limitArg() + "\", \"friendsoffriends\":\"SELECT uid1, uid2 FROM friend WHERE uid1 IN (SELECT uid2 from #friendsLimit) AND uid2 IN (SELECT uid2 from #friendsAll) AND uid1 < uid2\"}";

            parameters.Add("queries", query);
            parameters.Add("method", "fql.multiquery");


            //Execute
            client = new FacebookClient(fbToken);
            client.GetCompleted += new EventHandler<FacebookApiEventArgs>(client_GetCompleted);
            client.GetAsync(parameters);
        }
        private string limitArg()
        {
            int start = pageNum * PAGESIZE;
            int end = ++pageNum * PAGESIZE;
            string limit = "LIMIT " + start + ", " + end;
            return limit;
        }
        void client_GetCompleted(object sender, FacebookApiEventArgs e)
        {
            if (e.Error == null)
            {
                resultsRecieved++;
                UpdateProgress();
                //Success
                JsonArray results = e.GetResultData<JsonArray>();

                ParseResults(results);

                //If we need another page the run it, continue
                if (resultsRecieved >= pageTotal)
                {
#if DEBUG
                    RunMetrics();
#endif
                    TimeSpan duration = DateTime.Now - StartTime;
                    core.Analytics.SendMetric(core.Analytics.Metric.MapTime, duration.Seconds);
                    core.Analytics.SendMetric(core.Analytics.Metric.MaxReturnFQL, MaxFqlReturnSize);
                    core.Analytics.SendMetric(core.Analytics.Metric.Edges, EdgeNum);
                    ApiComplete(FriendsMap);
                }
            }
            else
            {
                //Error
                Debug.WriteLine(e.Error.Message);
                throw e.Error;
            }
        }

        private void UpdateProgress() //Probably not needed as this does not take long now. If we have to batch these requests then we can reconsider
        {

            double Progress = ((double)resultsRecieved / pageTotal) * 100;

            if (FriendsMapProgress_Changed != null)
                FriendsMapProgress_Changed.Invoke(this, new FriendsMapProgressEventArgs(Progress));
        }


        //Parse Results
        private void ParseResults(JsonArray jsonResults)
        {


            JsonObject obj = (JsonObject)jsonResults[2];
            obj.Values.ElementAt(1);

            JsonArray values = (JsonArray)obj.ElementAt(1).Value;
            if(values.Count > MaxFqlReturnSize) MaxFqlReturnSize = values.Count;
            
            foreach (JsonObject pairs in values)
            {
                long Source = long.Parse(pairs.ElementAt(0).Value.ToString());
                long Target = long.Parse(pairs.ElementAt(1).Value.ToString());
                if (FriendsMap.Keys.Contains(Source))
                {
                    FriendsMap[Source].Add(Target);
                    EdgeNum++;
                }
            }
        }

        //List<FriendsEdgeStruct> --> Dictionary
        private void RunMetrics()
        {
            CountIslands(FriendsMap);

        }
        private void CountIslands(Dictionary<long, List<long>> FriendsMap)
        {
            Dictionary<long, int> islandList = new Dictionary<long, int>();

            foreach (long uid in FriendsMap.Keys)
            {
                islandList.Add(uid, 0);
            }

            foreach (long uid in FriendsMap.Keys)
            {
                islandList[uid] += FriendsMap[uid].Count; //add the number of friends they have
                foreach (long uid2 in FriendsMap[uid])
                {
                    islandList[uid2]++;
                }
            }

            int islands = 0;
            int singles = 0;
            foreach (int b in islandList.Values)
            {
                if (b == 0) islands++;
                if (b == 1) singles++;
            }

            core.Analytics.SendMetric(core.Analytics.Metric.Islands, islands);
            core.Analytics.SendMetric(core.Analytics.Metric.Singles, singles);
        }
        private void ApiComplete(Dictionary<long, List<long>> FriendsMap)
        {
            //Success
            if (GetFriendsMap_Complete != null)
                GetFriendsMap_Complete.Invoke(this, new GetFriendsMapEventArgs(FriendsMap, true, null));
        }
    }
}