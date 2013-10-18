using System;
using System.Diagnostics;
using System.Linq;
using Facebook;
using System.Collections.Generic;
using System.Windows;

namespace FriendsService
{
    #region Event Arguments, DTO
    //Tmp DTO
    public struct FriendsEdgeStruct
    {
        public long Source;
        public long Target;
    }

    //Completed
    public class GetFriendsMapEventArgs : FriendsEventArgs
    {
        //Optimized DTO
        public Dictionary<long, List<long>> Result { get; private set; }

        public GetFriendsMapEventArgs(Dictionary<long, List<long>> Result, bool Success, Exception Error)
        {
            this.Result = Result;
            base.Success = Success;
            base.Error = Error;
        }
    }

    //Progress
    public class FriendsMapProgressEventArgs : FriendsEventArgs
    {
        public double Progress { get; private set; }

        public FriendsMapProgressEventArgs(double Progress)
        {
            this.Progress = Progress;
        }
    }
    #endregion

    public class GetFriendsMap
    {
        private int bundleCount = 10;
        
        string fbToken;
        long userUid;
        List<long> friendsList;
        Queue<Dictionary<string, object>> Parameters;
        double batchTripsTotal;

        FacebookClient client;
        List<JsonArray> resultsList = new List<JsonArray>();

        public delegate void GetFriendsMapEventHandler(object sender, GetFriendsMapEventArgs e);
        public event GetFriendsMapEventHandler GetFriendsMap_Complete;

        public delegate void FriendsMapProgressEventHandler(object sender, FriendsMapProgressEventArgs e);
        public event FriendsMapProgressEventHandler FriendsMapProgress_Changed;

        public GetFriendsMap(string fbToken, long userUid, List<long> friendsList)
        {
            this.fbToken = fbToken;
            this.userUid = userUid;
            this.friendsList = friendsList;
        }

        public void Run()
        {
            Debug.WriteLine(string.Format("GetFriendsMap: {0}", "Run"));
            

            //Bundle Friends
            Queue<List<long>> batchFriendsList = BundleFriends(friendsList);

            //Build Parameters
            Parameters = BuildParameters(batchFriendsList);

            //foreach (Dictionary<string, object> paramDict in Parameters)
            //{
            //    foreach (KeyValuePair<string, object> param in paramDict)
            //    {
            //        if (param.Key == "method_feed")
            //        {
            //            string[] methodfeed = (string[])param.Value;
            //            Debug.WriteLine("-----------------------");
            //            foreach (string s in methodfeed)
            //            {
            //                Debug.WriteLine(s);
            //            }
            //        }
            //    }
            //}

            batchTripsTotal = Parameters.Count;

            //Execute
            client = new FacebookClient(fbToken);
            client.GetCompleted += new EventHandler<FacebookApiEventArgs>(client_GetCompleted);
            ExecuteBatch();
        }

        private Queue<Dictionary<string, object>> BuildParameters(Queue<List<long>> batchFriendsList)
        {
            Queue<Dictionary<string, object>> Parameters = new Queue<Dictionary<string, object>>();

            while (batchFriendsList.Count > 0)
            {
                Dictionary<string, object> parameters = new Dictionary<string, object>();
                string[] methodFeed = BuildMethodFeed(batchFriendsList.Dequeue(), userUid);
                parameters.Add("method", "batch.run");
                parameters.Add("method_feed", methodFeed);
                parameters.Add("serial_only", "true");
                Parameters.Enqueue(parameters);
            }

            return Parameters;
        }

        private void ExecuteBatch()
        {
            if(Parameters.Count > 0)
            {
                client.GetAsync(Parameters.Dequeue());   
            }
        }

        void client_GetCompleted(object sender, FacebookApiEventArgs e)
        {
            if (e.Error == null)
            {
                //Success
                resultsList.Add(e.GetResultData<JsonArray>());

                UpdateProgress();

                if (Parameters.Count > 0)
                    ExecuteBatch();
                else
                {
                    List<FriendsEdgeStruct> EdgeList = ParseResults(resultsList, friendsList);
                    RemoveDuplicates(EdgeList);
                    //Done
                }
            }
            else
            {
                //Error
                Debug.WriteLine(e.Error.Message);
                throw e.Error;
            }
        }

        private void UpdateProgress()
        {
            double batchTripsRemaining = Parameters.Count;
            double Progress = ((batchTripsTotal - batchTripsRemaining) / batchTripsTotal) * 100;
            
            if (FriendsMapProgress_Changed != null)
                FriendsMapProgress_Changed.Invoke(this, new FriendsMapProgressEventArgs(Progress));
        }

        //Bundles Friends into List<long>.count = 20
        private Queue<List<long>> BundleFriends(List<long> friendsList)
        {
            Queue<List<long>> friendsBundle = new Queue<List<long>>();

            //1 Create a tmp Q of All
            Queue<long> tmpBatchList = new Queue<long>();
            foreach (long f in friendsList)
            {
                tmpBatchList.Enqueue(f);
            }

            //2 Bundle Them into 20
            while (tmpBatchList.Count > 0)
            {
                List<long> Batch = new List<long>();

                for (int i = 0; i < bundleCount; i++)
                {
                    if (tmpBatchList.Count == 0)
                        break;

                    Batch.Add(tmpBatchList.Dequeue());
                }

                friendsBundle.Enqueue(Batch);
            }

            return friendsBundle;
        }

        //Build Parameters
        private string[] BuildMethodFeed(List<long> friendsList, long userUid)
        {
            //1-Build Q of Friends
            Queue<long> Friends = new Queue<long>();
            foreach (long L in friendsList)
            {
                Friends.Enqueue(L);
            }

            List<string> MethodFeed = new List<string>();

            //2-Create String Array of Q Friends
            while (Friends.Count > 0)
            {
                string source = Friends.Dequeue().ToString();
                MethodFeed.Add("method=friends.getMutualFriends&target_uid=" + source + "&source_uid=" + userUid.ToString());

                //Break last item in batch to prevent "," after last item
                if (Friends.Count == 0)
                    break;
            }

            return MethodFeed.ToArray();
        }

        //Parse Results
        private List<FriendsEdgeStruct> ParseResults(List<JsonArray> jsonResults, List<long> Friends)
        {
            List<FriendsEdgeStruct> FlockEdgeList = new List<FriendsEdgeStruct>();

            for (int r = 0; r < jsonResults.Count; r++)
            {
                JsonArray Results = jsonResults.ElementAt(r);

                for (int s = 0; s < Results.Count; s++)
                {
                    long SourceUid = Friends.ElementAt(s + (r * bundleCount));

                    string SourceResults = Results.ElementAt(s) as string;
                    SourceResults = SourceResults.Replace("[", "");
                    SourceResults = SourceResults.Replace("]", "");
                    SourceResults = SourceResults.Replace("\"", "");
                    SourceResults = SourceResults.Replace("\\", "");
                    string[] Targets = SourceResults.Split(new char[] { ',' });

                    for (int t = 0; t < Targets.Count<string>(); t++)
                    {
                        string TargetUid = Targets.ElementAt(t);
                        if (!string.IsNullOrWhiteSpace(TargetUid))
                        {
                            FriendsEdgeStruct e = new FriendsEdgeStruct();
                            e.Source = SourceUid;
                            e.Target = long.Parse(TargetUid);
                            FlockEdgeList.Add(e);
                        }
                    }
                }
            }

            return FlockEdgeList;
        }

        //List<FriendsEdgeStruct> --> Dictionary
        private void RemoveDuplicates(List<FriendsEdgeStruct> EdgeList)
        {
            Dictionary<long, List<long>> FriendsMap = new Dictionary<long, List<long>>();

            for (int i = 0; i < EdgeList.Count; i++)
            {
                FriendsEdgeStruct edge = EdgeList.ElementAt(i);
                long source;
                long target;

                //Order Them so that sourece <= target
                if (edge.Source <= edge.Target)
                {
                    source = edge.Source;
                    target = edge.Target;
                }
                else
                {
                    target = edge.Source;
                    source = edge.Target;
                }

                //Check Duplicate
                if (FriendsMap.ContainsKey(source))
                {
                    if (!FriendsMap[source].Contains(target))
                        FriendsMap[source].Add(target);
                }
                else
                {
                    FriendsMap.Add(source, new List<long>());
                    FriendsMap[source].Add(target);
                }
            }

            ApiComplete(FriendsMap);
        }

        private void ApiComplete(Dictionary<long, List<long>> FriendsMap)
        {
            //Success
            if (GetFriendsMap_Complete != null)
                GetFriendsMap_Complete.Invoke(this, new GetFriendsMapEventArgs(FriendsMap, true, null));
        }
    }
}