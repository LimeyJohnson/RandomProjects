using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace FriendsFlockInterface
{
    #region Event Arguments
    public class FriendsFlockServiceEventArgs : EventArgs
    {
        public bool Success { get; protected set; }
        public Exception Error { get; protected set; }
    }

    public class GetGridLayoutEventArgs : FriendsFlockServiceEventArgs
    {
        public GetGridLayoutEventArgs(bool Success, Exception Error)
        {
            base.Success = Success;
            base.Error = Error;
        }
    }

    public class GetFlockLayoutEventArgs : FriendsFlockServiceEventArgs
    {
        public GetFlockLayoutEventArgs(bool Success, Exception Error)
        {
            base.Success = Success;
            base.Error = Error;
        }
    }

    public class GetFlockProgressEventArgs : EventArgs
    {
        public double Progress;
        public string ProgressString;

        public GetFlockProgressEventArgs(double Progress)
        {
            this.Progress = Progress;
            this.ProgressString = string.Format("{0}%", Math.Round(Progress));
        }
    }
    #endregion

    public class ServiceAgent
    {
        #region Events
        public delegate void GetGridLayoutEventHandler(object sender, GetGridLayoutEventArgs e);
        public event GetGridLayoutEventHandler GetGridLayout_Complete;
        public delegate void GetFlockLayoutEventHandler(object sender, GetFlockLayoutEventArgs e);
        public event GetFlockLayoutEventHandler GetFlockLayout_Complete;
        public delegate void GetFlockProgressLayoutEventHandler(object sender, GetFlockProgressEventArgs e);
        public event GetFlockProgressLayoutEventHandler GetFlockProgressLayoutComplete;
        #endregion

        #region Private Fields
        string fbToken;
        long userUid;
        #endregion

        public ServiceAgent(string fbToken, long userUid)
        {
            this.fbToken = fbToken;
            this.userUid = userUid;
        }

        public void GetGridLayoutAsync()
        {
            //Get User Info
            //Get Friends Info
            //Get Grid Layout

            //Package
        }

        public void GetFlockLayoutAsync()
        {
            //Get Friends Map
            //Report Progresss
            //Get Flock Layout
            //Report Progress
            //Get Path Points

            //Package
        }
    }
}