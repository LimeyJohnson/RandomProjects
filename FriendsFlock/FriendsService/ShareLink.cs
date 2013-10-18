using System;
using System.Diagnostics;
using System.Linq;
using Facebook;
using System.Collections.Generic;

namespace FriendsService
{
    #region Event Arguments
    public class ShareLinkEventArgs : FriendsEventArgs
    {
        public ShareLinkEventArgs(bool Success, Exception Error)
        {
            base.Success = Success;
            base.Error = Error;
        }
    }
    #endregion

    public class ShareLink
    {
        string fbToken;
        long userUid;
        long[] shareList;
        string message;
        Queue<List<FacebookBatchParameter>> BatchTrips;
        FacebookClient client;

        public delegate void ShareLinkEventHandler(object sender, ShareLinkEventArgs e);
        public event ShareLinkEventHandler ShareLink_Complete;

        public ShareLink(string fbToken, long userUid, long[] shareList, string message)
        {
            this.fbToken = fbToken;
            this.userUid = userUid;
            this.shareList = shareList;
            this.message = message;
        }


        public void Run()
        {
            //Build Paramters
            List<FacebookBatchParameter> parameters = BuildParameters();

            //Bundle into 20s
            BatchTrips = BundleBatchTrips(parameters);

            //Execute Batch
            client = new FacebookClient(fbToken);
            client.PostCompleted += new EventHandler<FacebookApiEventArgs>(client_PostCompleted);
            ExecuteBatch();
        }

        private void ExecuteBatch()
        {
            if (BatchTrips.Count > 0)
            {
                //Execute Batch
                client.BatchAsync(BatchTrips.Dequeue().ToArray());
            }
            else
            {
                //Batch Trips Completed
                if (ShareLink_Complete != null)
                    ShareLink_Complete.Invoke(this, new ShareLinkEventArgs(true, null));
            }
        }

        void client_PostCompleted(object sender, FacebookApiEventArgs e)
        {
            if (e.Error == null)
            {
                //Success
                ExecuteBatch();
            }
            else
            {
                //Error
                if (ShareLink_Complete != null)
                    ShareLink_Complete.Invoke(this, new ShareLinkEventArgs(false, e.Error));
            }
        }

        private Queue<List<FacebookBatchParameter>> BundleBatchTrips(List<FacebookBatchParameter> parameters)
        {
            Queue<List<FacebookBatchParameter>> BatchTrips = new Queue<List<FacebookBatchParameter>>();

            Queue<FacebookBatchParameter> tmpQ = new Queue<FacebookBatchParameter>();
            foreach (FacebookBatchParameter p in parameters)
            {
                tmpQ.Enqueue(p);
            }

            //Bundle into groups of 20
            while (tmpQ.Count > 0)
            {
                List<FacebookBatchParameter> Batch = new List<FacebookBatchParameter>();

                for (int i = 0; i < 20; i++)
                {
                    if (tmpQ.Count == 0)
                        break;

                    Batch.Add(tmpQ.Dequeue());
                }

                BatchTrips.Enqueue(Batch);
            }

            return BatchTrips;
        }

        private List<FacebookBatchParameter> BuildParameters()
        {
            List<FacebookBatchParameter> parameters = new List<FacebookBatchParameter>();

            foreach (long friend in shareList)
            {
                FacebookBatchParameter param = new FacebookBatchParameter();
                param.HttpMethod = HttpMethod.Post;
                param.Path = string.Format("/{0}/feed", friend.ToString());

                Dictionary<string, object> extenParam = new Dictionary<string, object>();
                extenParam.Add("link", "www.friendsflock.cloudapp.com");
                extenParam.Add("message", message);
                param.Parameters = extenParam;
                parameters.Add(param);
            }

            return parameters;
        }
    }
}