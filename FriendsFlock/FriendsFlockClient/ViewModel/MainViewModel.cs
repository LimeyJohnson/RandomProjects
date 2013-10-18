using FriendsFlockInterface;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace FriendsFlockClient.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        public ServiceModel serviceModel { get; private set; }

        public MainViewModel()
        {
            serviceModel = new ServiceModel();
            WireRelayCommands();
        }

        private void WireRelayCommands()
        {
            //Machine
            cmdStartTheMachine = new RelayCommand(StartTheMachine);

            //Layout
            cmdLayoutGrid = new RelayCommand(serviceModel.GotoGridLayout);
            cmdLayoutFlock = new RelayCommand(serviceModel.GotoFlockLayout);

            //Highlight
            cmdHighlightProfile = new RelayCommand(serviceModel.HighlightProfile);
            cmdHighlightStatus = new RelayCommand(serviceModel.HighlightStatus);
            cmdHighlightBirthday = new RelayCommand(serviceModel.HighlightBirthday);
            cmdHighlightRelationship = new RelayCommand(serviceModel.HighlightRelationships);
            cmdHighlightClear = new RelayCommand(serviceModel.ClearHighlights);

            //Fb Website
            cmdAppUserProfile = new RelayCommand(serviceModel.ShowUserProfile);
            cmdAppFriendProfile = new RelayCommand(serviceModel.ShowFriendProfile);
            cmdAppRate = new RelayCommand(serviceModel.ShowAppRate);
    
            //App Website
            cmdAppAbout = new RelayCommand(serviceModel.ShowAppAbout);
            cmdAppSupport = new RelayCommand(serviceModel.ShowAppSupport);
            cmdAppPrivacy = new RelayCommand(serviceModel.ShowAppPrivacy);
            
            //App Share
            cmdAppShare = new RelayCommand(serviceModel.ShowAppShare);
            cmdAppWallPost = new RelayCommand(serviceModel.ShowWallPost);
            cmdAppLike = new RelayCommand(serviceModel.ShowAppLike);
            cmdAppLogOff = new RelayCommand(serviceModel.ShowAppLogOff);
        }

        #region Machine Workflow: GetGridLayout, GetFlockLayout, UpdateProgress
        public RelayCommand cmdStartTheMachine { get; set; }
        public void StartTheMachine()
        {
            string fbToken = UserInfo.fbToken;
            long userUid = UserInfo.userUid;
            serviceModel.StartTheMachine(fbToken, userUid);
        }
        #endregion

        #region Menu Commands
        //Menu->Layout
        public RelayCommand cmdLayoutFlock
        {
            get;
            private set;
        }
        public RelayCommand cmdLayoutGrid
        {
            get;
            private set;
        }

        //Menu->Highlight
        public RelayCommand cmdHighlightProfile
        {
            get;
            private set;
        }
        public RelayCommand cmdHighlightStatus
        {
            get;
            private set;
        }
        public RelayCommand cmdHighlightBirthday
        {
            get;
            private set;
        }
        public RelayCommand cmdHighlightRelationship
        {
            get;
            private set;
        }
        public RelayCommand cmdHighlightClear
        {
            get;
            private set;
        }
        #endregion

        #region App Commands
        public RelayCommand cmdAppUserProfile
        {
            get;
            private set;
        }
        public RelayCommand cmdAppFriendProfile
        {
            get;
            private set;
        }
        public RelayCommand cmdAppRate
        {
            get;
            private set;
        }
        
        public RelayCommand cmdAppAbout
        {
            get;
            private set;
        }
        public RelayCommand cmdAppPrivacy
        {
            get;
            private set;
        }
        public RelayCommand cmdAppSupport
        {
            get;
            private set;
        }

        public RelayCommand cmdAppShare
        {
            get;
            private set;
        }

        public RelayCommand cmdAppWallPost { get; private set; }

        public RelayCommand cmdAppLike
        {
            get;
            private set;
        }
        public RelayCommand cmdAppLogOff
        {
            get;
            private set;
        }

        public RelayCommand cmdShorestPath 
        { 
            get; 
            private set; 
        }

        public RelayCommand cmdAppColors { get; private set; }
        #endregion
    }
}