using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
using GalaSoft.MvvmLight.Threading;
using System.Windows.Browser;

namespace FriendsFlockInterface
{
    public enum LayoutStateEnum { Grid, Flock }
    public enum HighlighStateEnum { Profile, Status, Birthday, Relationship, Clear }

    public class ServiceModel : ModelBase
    {
        //Private
        private string FbToken;
        private long UserUid;
        private bool IsTestMode;

        //Service Agents
        private FriendsInfoAgent friendsInfoAgent;
        private FlockLayoutAgent flockLayoutAgent;

        //Models
        public FriendsGraph friendsGraph { get; private set; }
        public AppModel appModel { get; private set; }
        public CanvasModel canvasModel { get; private set; }

        //States
        private LayoutStateEnum _layoutState;
        public LayoutStateEnum LayoutState
        {
            get
            {
                return _layoutState;
            }

            set
            {
                if (value == _layoutState)
                    return;

                _layoutState = value;

                UpdateLayoutState();
            }
        }
        private HighlighStateEnum _highlightState;
        public HighlighStateEnum HighlighState
        {
            get
            {
                return _highlightState;
            }

            set
            {
                if (value == _highlightState)
                    return;

                _highlightState = value;

                UpdateHighlightState();
            }
        }

        public ServiceModel()
        {
            friendsGraph = new FriendsGraph();
            appModel = new AppModel(friendsGraph);
            canvasModel = new CanvasModel(friendsGraph);
            LayoutState = LayoutStateEnum.Grid;
            HighlighState = HighlighStateEnum.Clear;
        }

        #region Machine
        //Start Machine
        public void StartTheMachine(string fbToken, long userUid)
        {
            FbToken = Dashboard.UpdateToken(fbToken);
            UserUid = Dashboard.UpdateUid(userUid);

            if (FbToken == "Test Mode")
                IsTestMode = true;
            else
                IsTestMode = false;

            friendsInfoAgent = new FriendsInfoAgent(FbToken, UserUid, IsTestMode);
            friendsInfoAgent.FriendsInfoAgent_Complete += new FriendsInfoAgent.FriendsInfoServiceEventHandler(friendsInfoAgent_FriendsInfoAgent_Complete);
            friendsInfoAgent.RunAsync();
        }
        
        //Friends Info
        void friendsInfoAgent_FriendsInfoAgent_Complete(object sender, FriendsInfoAgentEventArgs e)
        {
            if (e.Success)
            {
                //Sync Graph
                List<long> friendsList = new List<long>();
                lock (friendsGraph)
                {
                    friendsGraph.UserInfo = e.UserInfo;

                    foreach (FriendsInfo fi in e.FriendsInfo.Values)
                    {
                        FriendsVertex fv = new FriendsVertex(fi.Uid);
                        fv.Info = fi;
                        friendsGraph.AddVertex(fv);
                        friendsList.Add(fv.Uid);
                    }
                }

                //Sync Grid Controls
                canvasModel.SyncGridControls(appModel);

                //Start Flock Service
                flockLayoutAgent = new FlockLayoutAgent(FbToken, UserUid, friendsList, IsTestMode);
                flockLayoutAgent.FlockLayoutAgent_Complete += new FlockLayoutAgent.FlockLayoutAgentEventHandler(flockLayoutAgent_FlockLayoutAgent_Complete);
                flockLayoutAgent.FlockLayoutAgentProgress_Update += new FlockLayoutAgent.FlockLayoutAgentProgressEventHandler(flockLayoutAgent_FlockLayoutAgentProgress_Update);
                flockLayoutAgent.RunAsync();
            }
            else
            {
                ShowFriendsFailed();

            }
        }

        //Flock Layout
        void flockLayoutAgent_FlockLayoutAgentProgress_Update(object sender, FlockLayoutAgentProgressEventArgs e)
        {
            GalaSoft.MvvmLight.Threading.DispatcherHelper.UIDispatcher.BeginInvoke(() =>
            {
                appModel.LoadProgress = Math.Round(e.Progress);
            });
        }
        void flockLayoutAgent_FlockLayoutAgent_Complete(object sender, FlockLayoutAgentEventArgs e)
        {
            if (e.Success)
            {
                lock (friendsGraph)
                {
                    //Sync Flock Points
                    foreach (KeyValuePair<long, Point> kvp in e.FlockPoints)
                    {
                        friendsGraph.VertexDictionary[kvp.Key].Layout.FlockPoint = kvp.Value;
                    }

                    //Sync Edges
                    foreach (KeyValuePair<long, List<long>> kvp in e.EdgeMap)
                    {
                        long sourceUid = kvp.Key;
                        FriendsVertex sourceVertex = friendsGraph.VertexDictionary[sourceUid];

                        foreach (long targetUid in kvp.Value)
                        {
                            FriendsVertex targetVertex = friendsGraph.VertexDictionary[targetUid];
                            friendsGraph.AddEdge(new FriendsEdge(sourceVertex, targetVertex));
                        }
                    }
                }

                canvasModel.SyncFlockControls();

                ShowFlocksReady();
                //Done
            }
            else
            {
                ShowFlockFailed();
            }
        }
        #endregion

        #region Public Commands
        #region Menu Commands
        public void GotoGridLayout()
        {
            Debug.WriteLine("GotoGridLayout");
            LayoutState = LayoutStateEnum.Grid;
        }
        public void GotoFlockLayout()
        {
            Debug.WriteLine("GotoFlockLayout");

            if (appModel.LoadProgress == 100d)
            {
                LayoutState = LayoutStateEnum.Flock;
            }
            else
            {
                ShowFlocksNotReady();
            }
                
        }

        #region Highlights Done
        public void HighlightProfile()
        {
            Debug.WriteLine("GotoProfileHighlight");
            HighlighState = HighlighStateEnum.Profile;            
        }

        public void HighlightStatus()
        {
            Debug.WriteLine("GotoStatusHighlight");
            HighlighState = HighlighStateEnum.Status;
        }

        public void HighlightBirthday()
        {
            Debug.WriteLine("GotoBirthdayHighlight");
            HighlighState = HighlighStateEnum.Birthday;
        }

        public void HighlightRelationships()
        {
            Debug.WriteLine("GotoRelationshipHighlight");
            HighlighState = HighlighStateEnum.Relationship;
        }

        public void ClearHighlights()
        {
            Debug.WriteLine("GotoClearHighlight");
            HighlighState = HighlighStateEnum.Clear;
        }

        public void ShorestPath()
        {
            Debug.WriteLine("ShorestPath");
        }
        #endregion
        #endregion

        #region Fb Website
        public void ShowUserProfile()
        {
            Debug.WriteLine("ShowUserProfile");
            ShowWindow(friendsGraph.UserInfo.Profile_Url);
        }
        public void ShowFriendProfile()
        {
            Debug.WriteLine("ShowFriendProfile");
            ShowWindow(appModel.CurrentSelectedFriend.Info.Profile_Url);
        }
        public void ShowAppRate()
        {
            Debug.WriteLine("ShowAppRate");
            MessageBox.Show("Comming Soon..."); //TODO - App Rate Website
        }
        #endregion

        #region App Website
        public void ShowAppAbout()
        {
            Debug.WriteLine("ShowAppAbout");
            ShowWindow("http://www.friendsflock.com/about.aspx");
        }

        public void ShowAppPrivacy()
        {
            Debug.WriteLine("ShowAppPrivacy");
            ShowWindow("http://www.friendsflock.com/privacy.aspx");
        }

        public void ShowAppSupport()
        {
            Debug.WriteLine("ShowAppHelp");
            ShowWindow("http://www.friendsflock.com/support.aspx");
        }
        #endregion

        #region Fb Api
        public void ShowAppShare()
        {
            Debug.WriteLine("ShowAppShare");
            string shareDialog =  @"http://www.facebook.com/dialog/feed?" +
                                  "app_id=240082229369859&" + 
                                  "display=popup&" +
                                  "to=" + appModel.CurrentSelectedFriend.Uid.ToString() + "&" + 
                                  "link=http://www.friendsflock.com/&" +       
                                  "picture=http://www.friendsflock.com/images/friendsflock_demo.jpg&" +   
                                  "name=Friends%20Flock&" +
                                  "caption=" + Words.ShareCaption + "&" +
                                  "description=" + Words.ShareDescription + "&" +
                                  "redirect_uri=http://www.friendsflock.com/response.aspx";

            ShowPopup(shareDialog);
        }

        public void ShowWallPost()
        {
            Debug.WriteLine("ShowWallPost");
            string feedDialog = @"http://www.facebook.com/dialog/feed?" +
                      "app_id=240082229369859&" +
                      "display=popup&" +
                      "to=" + appModel.CurrentSelectedFriend.Uid.ToString() + "&" +
                      "redirect_uri=http://www.friendsflock.com/response.aspx";
            ShowPopup(feedDialog);
        }

        public void ShowAppLike()
        {
            Debug.WriteLine("ShowAppLike");
            ShowPopup("http://www.friendsflock.com/like.aspx");
        }

        public void ShowAppLogOff()
        {
            Debug.WriteLine("ShowAppLogOff");
            HtmlPage.Window.CreateInstance("logout");
        }

        #endregion
        #endregion

        #region Internal Commands
        private void ShowWindow(string url)
        {
            Debug.WriteLine("ShowWindow: {0}", url);
            try
            {
                HtmlPopupWindowOptions options = new HtmlPopupWindowOptions();
                options.Height = 600;
                options.Width = 1020;
                options.Top = 50;
                options.Left = 50;
                options.Resizeable = true;
                options.Scrollbars = true;

                HtmlPage.PopupWindow(new Uri(url, UriKind.RelativeOrAbsolute), null, options);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                MessageBox.Show("Popup Unavilable");
            }
        }
        private void ShowPopup(string url)
        {
            Debug.WriteLine("ShowPopup: {0}", url);

            try
            {
                HtmlPopupWindowOptions options = new HtmlPopupWindowOptions();
                options.Height = 300;
                options.Width = 400;
                options.Top = 50;
                options.Left = 50;
                options.Resizeable = false;
                options.Scrollbars = false;

                HtmlPage.PopupWindow(new Uri(url, UriKind.RelativeOrAbsolute), null, options);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                MessageBox.Show("Popup Unavilable");
            }
        }
        private void RestartMachine()
        {
            System.Windows.Browser.HtmlPage.Window.Navigate(
                    new Uri("http://www.friendsflock.com/app.aspx", UriKind.Absolute));
        }

        private void UpdateLayoutState()
        {
            switch (LayoutState)
            {
                case LayoutStateEnum.Grid:
                    appModel.contentModel.LayoutGridMenuItem.ToggleAccent(true);
                    appModel.contentModel.LayoutFlockMenuItem.ToggleAccent(false);
                    canvasModel.GotoGridLayout();
                    break;
                case LayoutStateEnum.Flock:
                    appModel.contentModel.LayoutGridMenuItem.ToggleAccent(false);
                    appModel.contentModel.LayoutFlockMenuItem.ToggleAccent(true);
                    canvasModel.GotoFlockLayout();
                    break;
                default:
                    break;
            }
        }
        private void UpdateHighlightState()
        {
            switch (HighlighState)
            {
                case HighlighStateEnum.Profile:
                    appModel.contentModel.FriendsProfileMenuItem.ToggleAccent(true);
                    appModel.contentModel.FriendsStatusMenuItem.ToggleAccent(false);
                    appModel.contentModel.FriendsBirthdayMenuItem.ToggleAccent(false);
                    appModel.contentModel.FriendsRelationshipMenuItem.ToggleAccent(false);
                    appModel.contentModel.FriendsClearMenuItem.ToggleAccent(false);

                    appModel.sidebarModel.FriendsProfileMenuItem.ToggleAccent(true);
                    appModel.sidebarModel.FriendsStatusMenuItem.ToggleAccent(false);
                    appModel.sidebarModel.FriendsBirthdayMenuItem.ToggleAccent(false);
                    appModel.sidebarModel.FriendsRelationshipMenuItem.ToggleAccent(false);
                    appModel.sidebarModel.FriendsClearMenuItem.ToggleAccent(false);

                    canvasModel.HighlightUpdates(HighlightUpdatesEnum.Profile);
                    break;
                case HighlighStateEnum.Status:
                    appModel.contentModel.FriendsProfileMenuItem.ToggleAccent(false);
                    appModel.contentModel.FriendsStatusMenuItem.ToggleAccent(true);
                    appModel.contentModel.FriendsBirthdayMenuItem.ToggleAccent(false);
                    appModel.contentModel.FriendsRelationshipMenuItem.ToggleAccent(false);
                    appModel.contentModel.FriendsClearMenuItem.ToggleAccent(false);

                    appModel.sidebarModel.FriendsProfileMenuItem.ToggleAccent(false);
                    appModel.sidebarModel.FriendsStatusMenuItem.ToggleAccent(true);
                    appModel.sidebarModel.FriendsBirthdayMenuItem.ToggleAccent(false);
                    appModel.sidebarModel.FriendsRelationshipMenuItem.ToggleAccent(false);
                    appModel.sidebarModel.FriendsClearMenuItem.ToggleAccent(false);

                    canvasModel.HighlightUpdates(HighlightUpdatesEnum.Status);
                    break;
                case HighlighStateEnum.Birthday:
                    appModel.contentModel.FriendsProfileMenuItem.ToggleAccent(false);
                    appModel.contentModel.FriendsStatusMenuItem.ToggleAccent(false);
                    appModel.contentModel.FriendsBirthdayMenuItem.ToggleAccent(true);
                    appModel.contentModel.FriendsRelationshipMenuItem.ToggleAccent(false);
                    appModel.contentModel.FriendsClearMenuItem.ToggleAccent(false);

                    appModel.sidebarModel.FriendsProfileMenuItem.ToggleAccent(false);
                    appModel.sidebarModel.FriendsStatusMenuItem.ToggleAccent(false);
                    appModel.sidebarModel.FriendsBirthdayMenuItem.ToggleAccent(true);
                    appModel.sidebarModel.FriendsRelationshipMenuItem.ToggleAccent(false);
                    appModel.sidebarModel.FriendsClearMenuItem.ToggleAccent(false);

                    canvasModel.HighlightUpdates(HighlightUpdatesEnum.Birthday);
                    break;
                case HighlighStateEnum.Relationship:
                    appModel.contentModel.FriendsProfileMenuItem.ToggleAccent(false);
                    appModel.contentModel.FriendsStatusMenuItem.ToggleAccent(false);
                    appModel.contentModel.FriendsBirthdayMenuItem.ToggleAccent(false);
                    appModel.contentModel.FriendsRelationshipMenuItem.ToggleAccent(true);
                    appModel.contentModel.FriendsClearMenuItem.ToggleAccent(false);

                    appModel.sidebarModel.FriendsProfileMenuItem.ToggleAccent(false);
                    appModel.sidebarModel.FriendsStatusMenuItem.ToggleAccent(false);
                    appModel.sidebarModel.FriendsBirthdayMenuItem.ToggleAccent(false);
                    appModel.sidebarModel.FriendsRelationshipMenuItem.ToggleAccent(true);
                    appModel.sidebarModel.FriendsClearMenuItem.ToggleAccent(false);

                    canvasModel.HighlightUpdates(HighlightUpdatesEnum.Relationshiop);
                    break;
                case HighlighStateEnum.Clear:
                    appModel.contentModel.FriendsProfileMenuItem.ToggleAccent(false);
                    appModel.contentModel.FriendsStatusMenuItem.ToggleAccent(false);
                    appModel.contentModel.FriendsBirthdayMenuItem.ToggleAccent(false);
                    appModel.contentModel.FriendsRelationshipMenuItem.ToggleAccent(false);
                    appModel.contentModel.FriendsClearMenuItem.ToggleAccent(true);

                    appModel.sidebarModel.FriendsProfileMenuItem.ToggleAccent(false);
                    appModel.sidebarModel.FriendsStatusMenuItem.ToggleAccent(false);
                    appModel.sidebarModel.FriendsBirthdayMenuItem.ToggleAccent(false);
                    appModel.sidebarModel.FriendsRelationshipMenuItem.ToggleAccent(false);
                    appModel.sidebarModel.FriendsClearMenuItem.ToggleAccent(true);

                    canvasModel.HighlightUpdates(HighlightUpdatesEnum.Clear);
                    break;
                default:
                    break;
            }
        }
        #endregion

        #region Dialogs
        private void ShowFriendsFailed()
        {
            GalaSoft.MvvmLight.Threading.DispatcherHelper.UIDispatcher.BeginInvoke(() =>
            {
                YesNoDialog frindsInfoFailed = new YesNoDialog();
                frindsInfoFailed.Title = "Error";
                frindsInfoFailed.Message = Words.FriendsFailedDescription;
                frindsInfoFailed.Yes = "Try Again";
                frindsInfoFailed.No = "Cancel";
                frindsInfoFailed.Closed += new EventHandler(frindsInfoFailed_Closed);
                frindsInfoFailed.Show();
            });
        }
        private void ShowFlockFailed()
        {
            GalaSoft.MvvmLight.Threading.DispatcherHelper.UIDispatcher.BeginInvoke(() =>
            {
                YesNoDialog flockFailed = new YesNoDialog();
                flockFailed.Title = "Error";
                flockFailed.Message = Words.FlocksFailedDescription;
                flockFailed.Yes = "Try Again";
                flockFailed.No = "Cancel";
                flockFailed.Closed += new EventHandler(flockFailed_Closed);
                flockFailed.Show();
            });
        }
        private void ShowFlocksNotReady()
        {
            GalaSoft.MvvmLight.Threading.DispatcherHelper.UIDispatcher.BeginInvoke(() =>
                {
                    NotificationDialog flocksStillLoading = new NotificationDialog();
                    flocksStillLoading.Message = "Your flocks are still loading. After they are finished you will be prompted to change the layout.";
                    flocksStillLoading.Show();
                });
        }
        private void ShowFlocksReady()
        {
            GalaSoft.MvvmLight.Threading.DispatcherHelper.UIDispatcher.BeginInvoke(() =>
            {
                YesNoDialog flockLayout = new YesNoDialog();
                flockLayout.Title = "Flocks Ready";
                flockLayout.Message = "Your flocks are ready! Do you want to switch to Flock Layout now?";
                flockLayout.Yes = "Yes";
                flockLayout.No = "No";
                flockLayout.Closed += new EventHandler(flockLayout_Closed);
                flockLayout.Show();
            });
        }
        void frindsInfoFailed_Closed(object sender, EventArgs e)
        {
            YesNoDialog frindsInfoFailed = (YesNoDialog)sender;
            if (frindsInfoFailed.DialogResult == true)
            {
                RestartMachine();
            }
        }
        void flockLayout_Closed(object sender, EventArgs e)
        {
            YesNoDialog flockLayout = (YesNoDialog)sender;
            if (flockLayout.DialogResult == true)
                GotoFlockLayout();
        }
        void flockFailed_Closed(object sender, EventArgs e)
        {
            YesNoDialog flockFailed = (YesNoDialog)sender;
            if (flockFailed.DialogResult == true)
            {
                RestartMachine();
            }
        }
        #endregion
    }
}