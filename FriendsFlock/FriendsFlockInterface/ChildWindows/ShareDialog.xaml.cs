using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace FriendsFlockInterface
{
    public class ShareModel : ModelBase
    {
        public string Name { get; set; }
        public string SquareUrl { get; set; }
        public long Uid { get; set; }
        public const string IsSelectedPropertyName = "IsSelected";
        private bool _IsSelected = false;
        public bool IsSelected
        {
            get
            {
                return _IsSelected;
            }

            set
            {
                if (_IsSelected == value)
                {
                    return;
                }

                var oldValue = _IsSelected;
                _IsSelected = value;
                RaisePropertyChanged(IsSelectedPropertyName);
            }
        }
    }

    public partial class ShareDialog : ChildWindow, INotifyPropertyChanged
    {
        public FriendsGraph friendsGraph = new FriendsGraph();
        public List<FriendsVertex> shareSelected = new List<FriendsVertex>();
        public string shareMessage = "Friends Flock Rocks!";

        public const string FriendsListPropertyName = "FriendsList";
        private ObservableCollection<ShareModel> _friendsList = new ObservableCollection<ShareModel>();
        public ObservableCollection<ShareModel> FriendsList
        {
            get
            {
                return _friendsList;
            }

            set
            {
                if (_friendsList == value)
                {
                    return;
                }

                var oldValue = _friendsList;
                _friendsList = value;

                OnPropertyChanged(FriendsListPropertyName);
            }
        }

        public ShareDialog()
        {
            InitializeComponent();
            LayoutRoot.DataContext = this;
            this.Loaded += new RoutedEventHandler(ShareDialog_Loaded);
            lbShareList.SelectionChanged += new SelectionChangedEventHandler(lbShareList_SelectionChanged);
        }

        void lbShareList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListBox lb = (ListBox)sender;
            ShareModel sm = (ShareModel)lb.SelectedItem;
            if (sm.IsSelected)
                sm.IsSelected = false;
            else
                sm.IsSelected = true;
        }

        void ShareDialog_Loaded(object sender, RoutedEventArgs e)
        {
            foreach (FriendsVertex fv in friendsGraph.VertexDictionary.Values)
            {
                ShareModel sm = new ShareModel();
                sm.Uid = fv.Uid;
                sm.Name = fv.Info.Name;
                sm.SquareUrl = fv.Info.Pic_Sqaure_Url;
                sm.IsSelected = false;
                FriendsList.Add(sm);
            }
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            foreach (ShareModel sm in FriendsList)
            {
                if (sm.IsSelected)
                    if (friendsGraph.VertexDictionary.ContainsKey(sm.Uid))
                        shareSelected.Add(friendsGraph.VertexDictionary[sm.Uid]);
            }
            
            this.DialogResult = true;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        //Notify Property Changed
        protected void OnPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }
        #endregion
    }
}

