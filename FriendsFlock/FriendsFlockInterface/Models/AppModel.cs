using GalaSoft.MvvmLight.Messaging;
using System.Collections.Generic;

namespace FriendsFlockInterface
{
    public class AppModel : ModelBase
    {
        public string CurrentSelectedFriendPropertyName = "CurrentSelectedFriend";
        private FriendsVertex _CurrentSelectedFriend = new FriendsVertex(0L);
        public FriendsVertex CurrentSelectedFriend
        {
            get
            {
                return _CurrentSelectedFriend;
            }

            set
            {
                if (_CurrentSelectedFriend == value)
                {
                    return;
                }

                var oldValue = _CurrentSelectedFriend;
                _CurrentSelectedFriend = value;

                // Update bindings and broadcast change using GalaSoft.MvvmLight.Messenging
                RaisePropertyChanged(CurrentSelectedFriendPropertyName, oldValue, value, true);
                SendCurrentFriendsNotification();
                UpdateSidebar();
            }
        }

        private void UpdateSidebar()
        {
            if (CurrentSelectedFriend.Uid != 0)
                ShareString = string.Format("Share with {0}", CurrentSelectedFriend.Info.Name);

            sidebarModel.FriendsProfileMenuItem.ItemDescription = CurrentSelectedFriend.Info.Profile_Update_Time_String;
            sidebarModel.FriendsStatusMenuItem.ItemDescription = CurrentSelectedFriend.Info.Status_Message;
            sidebarModel.FriendsBirthdayMenuItem.ItemDescription = CurrentSelectedFriend.Info.Birthday_date;
            sidebarModel.FriendsRelationshipMenuItem.ItemDescription = CurrentSelectedFriend.Info.Relationship_Status;
            
            if ((CurrentSelectedFriend.Uid != 0) && (CurrentShorestPathTarget.Uid != 0))
            {
                List<FriendsEdge> shorestPath = friendsGraph.GetShorestPath(CurrentSelectedFriend, CurrentShorestPathTarget);
                if (shorestPath.Count == 0)
                    sidebarModel.ShorestPathMenuItem.ItemDescription = string.Format("There is not connection between {1} and {2}.",
                    shorestPath.Count, CurrentSelectedFriend.Info.Name, CurrentShorestPathTarget.Info.Name);
                else
                    sidebarModel.ShorestPathMenuItem.ItemDescription = string.Format("{0} Degrees of Separation between {1} and {2}.",
                    shorestPath.Count, CurrentSelectedFriend.Info.Name, CurrentShorestPathTarget.Info.Name);
            }
        }

        public const string ShareStringPropertyName = "ShareString";
        private string _shareString = "";
        public string ShareString
        {
            get
            {
                return _shareString;
            }

            set
            {
                if (_shareString == value)
                {
                    return;
                }

                var oldValue = _shareString;
                _shareString = value;

                RaisePropertyChanged(ShareStringPropertyName);
            }
        }

        public string CurrentPathTargetPropertyName = "CurrentShorestPathTarget";
        private FriendsVertex _CurrentShorestPathTarget = new FriendsVertex(0L);
        public FriendsVertex CurrentShorestPathTarget
        {
            get
            {
                return _CurrentShorestPathTarget;
            }

            set
            {
                if (_CurrentShorestPathTarget == value)
                {
                    return;
                }

                var oldValue = _CurrentShorestPathTarget;
                _CurrentShorestPathTarget = value;

                RaisePropertyChanged(CurrentPathTargetPropertyName, oldValue, value, true);

                SendCurrentFriendsNotification();
                UpdateSidebar();
            }
        }

        private void SendCurrentFriendsNotification()
        {
            Messenger.Default.Send<CurrentFriends>(new CurrentFriends(CurrentSelectedFriend, CurrentShorestPathTarget));
        }
        private void UpdateSelectedFriend(FriendsVertex selectedFriend)
        {
            CurrentSelectedFriend = selectedFriend;
        }
        private void UpdateShorestPathTarget(FriendsVertex shorestPathTarget)
        {
            CurrentShorestPathTarget = shorestPathTarget;
        }

        public const string LoadProgressPropertyName = "LoadProgress";
        private double _LoadProgress = 0d;
        public double LoadProgress
        {
            get
            {
                return _LoadProgress;
            }

            set
            {
                if (_LoadProgress == value)
                {
                    return;
                }

                var oldValue = _LoadProgress;
                _LoadProgress = value;

                // Update bindings, no broadcast
                RaisePropertyChanged(LoadProgressPropertyName);
            }
        }

        public MenuModel contentModel { get; set; }
        public MenuModel sidebarModel { get; set; }

        private FriendsGraph friendsGraph;

        public AppModel(FriendsGraph friendsGraph)
        {
            this.friendsGraph = friendsGraph;
            contentModel = new MenuModel();
            sidebarModel = new MenuModel();
        }
    }

    /// <summary>
    /// DTO for Messenging
    /// </summary>
    public class CurrentFriends
    {
        public FriendsVertex SelectedFriend { get; private set; }
        public FriendsVertex PathTarget { get; private set; }

        public CurrentFriends(FriendsVertex SelectedFriend, FriendsVertex PathTarget)
        {
            this.SelectedFriend = SelectedFriend;
            this.PathTarget = PathTarget;
        }
    }
}