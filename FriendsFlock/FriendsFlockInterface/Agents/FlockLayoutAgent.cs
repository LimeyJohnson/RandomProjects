using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using FriendsService;
using GraphService;

namespace FriendsFlockInterface
{
    public class FlockLayoutAgentEventArgs : EventArgs
    {
        public bool Success { get; private set; }
        public Exception Error { get; private set; }

        public Dictionary<long, List<long>> EdgeMap;
        public Dictionary<long, Point> FlockPoints;

        public FlockLayoutAgentEventArgs(bool success, Exception error, Dictionary<long, List<long>> edgeMap, Dictionary<long, Point> flockPoints)
        {
            Success = success;
            Error = error;
            EdgeMap = edgeMap;
            FlockPoints = flockPoints;
        }
    }

    public class FlockLayoutAgentProgressEventArgs : EventArgs
    {
        public double Progress { get; private set; }
        public string ProgressString { get; private set; }

        public FlockLayoutAgentProgressEventArgs(double progress)
        {
            Progress = progress;
            ProgressString = string.Format("{0}%", Math.Round(Progress));
        }
    }

    public class FlockLayoutAgent
    {
        public delegate void FlockLayoutAgentEventHandler(object sender, FlockLayoutAgentEventArgs e);
        public event FlockLayoutAgentEventHandler FlockLayoutAgent_Complete;

        public delegate void FlockLayoutAgentProgressEventHandler(object sender, FlockLayoutAgentProgressEventArgs e);
        public event FlockLayoutAgentProgressEventHandler FlockLayoutAgentProgress_Update;

        string FbToken;
        long UserUid;
        List<long> FriendsList;
        bool IsTestMode;

        Dictionary<long, List<long>> EdgeMap;
        Dictionary<long, Point> FlockPoints;

        FlockGraph flockGraph;
        BackgroundWorker bwFlockLayout;

        public FlockLayoutAgent(string fbToken, long userUid, List<long> friendsList, bool isTestMode)
        {
            FbToken = fbToken;
            UserUid = userUid;
            FriendsList = friendsList;
            IsTestMode = isTestMode;

            EdgeMap = new Dictionary<long, List<long>>();
            FlockPoints = new Dictionary<long, Point>();
            flockGraph = new FlockGraph();
        }

        public void RunAsync()
        {
            if (IsTestMode)
            {
                //Test Mode
                //Random Edge Map
                //Edge Map -> Build Flock Graph
                //FlockGraph -> Get Flock Layout
                //FlockGraph -> DTO
                RunTestMode();
            }
            else
            {
                //Production Mode
                //Get Edge Map
                //Edge Map -> Build Flock Graph
                //FlockGraph -> Get Flock Layout
                //FlockGraph -> DTO
                RunProductionMode();
            }
        }

        #region RunTestMode
        private void RunTestMode()
        {
            Debug.WriteLine("RunTestMode");
            
            //Test Edge Map
            BuildTestEdgeMap();

            //Flock Layout
            BuildFlockGraph();

            //Get Flock Layout
            GetFlockLayout();
        }

        private void BuildTestEdgeMap()
        {
            Random r = new Random();

            int first = 0;
            int last = FriendsList.Count - 1;

            int flock1a = first;
            int flock1b = last / 3;

            int flock2a = Convert.ToInt32(Math.Round((double)((last / 3) + 1)));
            int flock2b = Convert.ToInt32(Math.Round((double)(((last / 3) * 2))));

            int flock3a = Convert.ToInt32(Math.Round((double)(((last / 3) * 2) + 1)));
            int flock3b = last;

            int flockEdges = Convert.ToInt32(Math.Round((double)(Dashboard.TestEdges / 3)));

            for (int i = 0; i < flockEdges; i++)
            {
                long source = r.Next(flock1a, flock1b);
                long target = r.Next(flock1a, flock1b);

                if (EdgeMap.ContainsKey(source))
                {
                    List<long> targetList = EdgeMap[source];
                    if (!targetList.Contains(target))
                        targetList.Add(target);
                }
                else
                {
                    List<long> targetList = new List<long>();
                    targetList.Add(target);
                    EdgeMap.Add(source, targetList);
                }
            }

            for (int i = 0; i < flockEdges; i++)
            {
                long source = r.Next(flock2a, flock2b);
                long target = r.Next(flock2a, flock2b);

                if (EdgeMap.ContainsKey(source))
                {
                    List<long> targetList = EdgeMap[source];
                    if (!targetList.Contains(target))
                        targetList.Add(target);
                }
                else
                {
                    List<long> targetList = new List<long>();
                    targetList.Add(target);
                    EdgeMap.Add(source, targetList);
                }
            }

            for (int i = 0; i < flockEdges; i++)
            {
                long source = r.Next(flock3a, flock3b);
                long target = r.Next(flock3a, flock3b);

                if (EdgeMap.ContainsKey(source))
                {
                    List<long> targetList = EdgeMap[source];
                    if (!targetList.Contains(target))
                        targetList.Add(target);
                }
                else
                {
                    List<long> targetList = new List<long>();
                    targetList.Add(target);
                    EdgeMap.Add(source, targetList);
                }
            }
        }
        #endregion

        #region Run Proudction Mode
        private void RunProductionMode()
        {
            Debug.WriteLine("RunProductionMode");
            try
            {
                GetFriendsMapFql gfm = new GetFriendsMapFql(FbToken, UserUid, FriendsList);
                gfm.FriendsMapProgress_Changed += new GetFriendsMapFql.FriendsMapProgressEventHandler(gfm_FriendsMapProgress_Changed);
                gfm.GetFriendsMap_Complete += new GetFriendsMapFql.GetFriendsMapEventHandler(gfm_GetFriendsMap_Complete);
                gfm.Run();
            }
            catch (Exception ex)
            {
                FinishedUnsuccessfully(ex);
            }
        }

        void gfm_GetFriendsMap_Complete(object sender, GetFriendsMapEventArgs e)
        {
            EdgeMap = e.Result;
            BuildFlockGraph();
            GetFlockLayout();
        }

        void gfm_FriendsMapProgress_Changed(object sender, FriendsMapProgressEventArgs e)
        {
            double flockProgress = .2 * e.Progress;

            if (FlockLayoutAgentProgress_Update != null)
                FlockLayoutAgentProgress_Update.Invoke(this, new FlockLayoutAgentProgressEventArgs(flockProgress));
        }

        private void BuildFlockGraph()
        {
            flockGraph = new FlockGraph();

            foreach (long friendUid in FriendsList)
            {
                flockGraph.AddVertex(new FlockVertex(friendUid));
            }

            foreach (KeyValuePair<long, List<long>> kvp in EdgeMap)
            {
                long source = kvp.Key;

                foreach (long target in kvp.Value)
                {
                    //Error at 20%
                    if ((flockGraph.VertexDictionary.ContainsKey(source)) &&
                        (flockGraph.VertexDictionary.ContainsKey(target)))
                        flockGraph.AddEdge(new FlockEdge(flockGraph.VertexDictionary[source],
                            flockGraph.VertexDictionary[target]));
                    else
                        Debug.WriteLine("Edge Dropped");
                }
            }
        }

        private void CountIslands()
        {
            Dictionary<long, bool> islandList = new Dictionary<long, bool>();

            foreach (long  uid in flockGraph.VertexDictionary.Keys)
            {
                islandList.Add(uid, true);
            }

            foreach (FlockEdge e in flockGraph.Edges)
            {
                islandList[e.Source.Uid] = false;
                islandList[e.Target.Uid] = false;
            }

            int i = 0;
            foreach (bool b in islandList.Values)
            {
                if (b == true)
                    i++;
            }

            Debug.WriteLine("Islands={0}", i);
        }

        private void GetFlockLayout()
        {
            bwFlockLayout = new BackgroundWorker();
            bwFlockLayout.WorkerReportsProgress = true;
            bwFlockLayout.ProgressChanged += new ProgressChangedEventHandler(bwFlockLayout_ProgressChanged);
            bwFlockLayout.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bwFlockLayout_RunWorkerCompleted);
            bwFlockLayout.DoWork += new DoWorkEventHandler(bwFlockLayout_DoWork);
            bwFlockLayout.RunWorkerAsync();
        }

        void bwFlockLayout_DoWork(object sender, DoWorkEventArgs e)
        {
            GraphServiceFacade gsf = new GraphServiceFacade();
            gsf.GetFlockLayout(flockGraph, bwFlockLayout);
        }

        void bwFlockLayout_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            double layoutProgress = Convert.ToDouble(e.ProgressPercentage);
            double flockProgress = 20d + (.8 * layoutProgress);

            if (FlockLayoutAgentProgress_Update != null)
                FlockLayoutAgentProgress_Update.Invoke(this, new FlockLayoutAgentProgressEventArgs(flockProgress));
        }

        void bwFlockLayout_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            foreach (FlockVertex fv in flockGraph.VertexDictionary.Values)
            {
                FlockPoints.Add(fv.Uid, fv.FlockPoint);
            }

            FinishedSuccessfully();
        }
        #endregion

        #region Finished
        private void FinishedSuccessfully()
        {
            Debug.WriteLine("FinishedSuccessfully");

            bool isSuccess = true;

            if (FlockLayoutAgent_Complete != null)
                FlockLayoutAgent_Complete.Invoke(this,
                    new FlockLayoutAgentEventArgs(isSuccess, null, EdgeMap, FlockPoints));
        }

        private void FinishedUnsuccessfully(Exception error)
        {
            Debug.WriteLine("FinishedUnsuccessfully: {0}", error.Message);

            bool isSuccess = false;

            if (FlockLayoutAgent_Complete != null)
                FlockLayoutAgent_Complete.Invoke(this,
                    new FlockLayoutAgentEventArgs(isSuccess, error, null, null));
        }
        #endregion
    }
}