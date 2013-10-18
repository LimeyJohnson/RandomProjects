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
using FriendsService;
using Facebook;

namespace FriendsFlockTest
{
    public partial class MainPage : UserControl
    {
        private List<long> friendsList;

        public MainPage()
        {
            InitializeComponent();
        }

        private string Token()
        {
            return tboxToken.ToString();
        }

        private long UserUid()
        {
            return long.Parse(tboxUserUid.Text);
        }

        private void btnGetUserInfo_Click(object sender, RoutedEventArgs e)
        {
            GetUserInfo gui = new GetUserInfo(tboxToken.Text, long.Parse(tboxUserUid.Text));
            gui.GetUserInfo_Complete +=new GetUserInfo.GetUserInfoEventHandler(gui_GetUserInfo_Complete);
            gui.Run();
        }

        void gui_GetUserInfo_Complete(object sender, GetUserInfoEventArgs e)
        {

        }

        private void btnGetMutualFriends_Click(object sender, RoutedEventArgs e)
        {
            GetFriendsMapFql gfm = new GetFriendsMapFql(tboxToken.Text, long.Parse(tboxUserUid.Text), friendsList);
            gfm.GetFriendsMap_Complete += new GetFriendsMapFql.GetFriendsMapEventHandler(gfm_GetFriendsMap_Complete);
            gfm.Run();
        }

        void gfm_GetFriendsMap_Complete(object sender, GetFriendsMapEventArgs e)
        {

        }

        private void btnGetFriendsInfo_Click(object sender, RoutedEventArgs e)
        {
            GetFriendsInfo gfi = new GetFriendsInfo(tboxToken.Text, long.Parse(tboxUserUid.Text));
            gfi.GetFriendsInfo_Complete += new GetFriendsInfo.GetFriendsInfoEventHandler(gfi_GetFriendsInfo_Complete);
            gfi.Run();
        }

        void gfi_GetFriendsInfo_Complete(object sender, GetFriendsInfoEventArgs e)
        {
            friendsList = e.FriendsList;
        }

        private void btnGetFriendsList_Click(object sender, RoutedEventArgs e)
        {
            FacebookClient client = new FacebookClient(Token());
            string fqlFriendsList = "SELECT uid2 FROM friend where uid1=me()";
            client.GetCompleted += new EventHandler<FacebookApiEventArgs>(client_GetCompleted);
            client.QueryAsync(fqlFriendsList, Token());

        }

        void client_GetCompleted(object sender, FacebookApiEventArgs e)
        {

        }

        private void btnGetMutualFriendsFQL_Click(object sender, RoutedEventArgs e)
        {
            //TODO
        }
    }
}