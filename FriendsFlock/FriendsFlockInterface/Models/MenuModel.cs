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
using System.Windows.Media.Imaging;

namespace FriendsFlockInterface
{
    public class MenuModel : ModelBase
    {
        public ItemModel LayoutGridMenuItem { get; set; }
        public ItemModel LayoutFlockMenuItem { get; set; }

        public ItemModel FriendsProfileMenuItem { get; set; }
        public ItemModel FriendsStatusMenuItem { get; set; }
        public ItemModel FriendsBirthdayMenuItem { get; set; }
        public ItemModel FriendsRelationshipMenuItem { get; set; }
        public ItemModel FriendsClearMenuItem { get; set; }
        public ItemModel ShorestPathMenuItem { get; set; }

        private string imageFolder = "Images/";

        public MenuModel()
        {
            LoadGridMenuItem();
            LoadFlockMenuItem();
            LoadFriendsProfileMenuItem();
            LoadFriendsStatusMenuItem();
            LoadFriendsBirthdayMenuItem();
            LoadFriendsRelationshipMenuItem();
            LoadFriendsClearMenuItem();
            LoadShorestPathMenuItem();
        }

        private void LoadGridMenuItem()
        {
            LayoutGridMenuItem = new ItemModel();
            LayoutGridMenuItem.IconNormal = imageFolder + "Grid_Icon.png";
            LayoutGridMenuItem.IconAccent = imageFolder + "Grid_Icon_Accent.png";
            LayoutGridMenuItem.ToggleAccent(true);

            LayoutGridMenuItem.ItemTitle = "Grid";
            LayoutGridMenuItem.ItemDescription = Words.GridDescription;
        }

        private void LoadFlockMenuItem()
        {
            LayoutFlockMenuItem = new ItemModel();
            LayoutFlockMenuItem.IconNormal = imageFolder + "Flock_Icon.png";
            LayoutFlockMenuItem.IconAccent = imageFolder + "Flock_Icon_Accent.png";
            LayoutFlockMenuItem.ToggleAccent(false);

            LayoutFlockMenuItem.ItemTitle = "Flock";
            LayoutFlockMenuItem.ItemDescription = Words.FlockDescription;
        }

        private void LoadFriendsProfileMenuItem()
        {
            FriendsProfileMenuItem = new ItemModel();
            FriendsProfileMenuItem.IconNormal = imageFolder + "Profile_Icon.png";
            FriendsProfileMenuItem.IconAccent = imageFolder + "Profile_Icon_Accent.png";
            FriendsProfileMenuItem.ToggleAccent(false);

            FriendsProfileMenuItem.ItemTitle = "Profile";
            FriendsProfileMenuItem.ItemDescription = Words.ProfileDescription;
        }

        private void LoadFriendsStatusMenuItem()
        {
            FriendsStatusMenuItem = new ItemModel();
            FriendsStatusMenuItem.IconNormal = imageFolder + "Status_Icon.png";
            FriendsStatusMenuItem.IconAccent = imageFolder + "Status_Icon_Accent.png";
            FriendsStatusMenuItem.ToggleAccent(false);

            FriendsStatusMenuItem.ItemTitle = "Status";
            FriendsStatusMenuItem.ItemDescription = Words.StatusDescription;
        }

        private void LoadFriendsBirthdayMenuItem()
        {
            FriendsBirthdayMenuItem = new ItemModel();
            FriendsBirthdayMenuItem.IconNormal = imageFolder + "Birthday_Icon.png";
            FriendsBirthdayMenuItem.IconAccent = imageFolder + "Birthday_Icon_Accent.png";
            FriendsBirthdayMenuItem.ToggleAccent(false);

            FriendsBirthdayMenuItem.ItemTitle = "Birthday";
            FriendsBirthdayMenuItem.ItemDescription = Words.BirthdayDescription;
        }

        private void LoadFriendsRelationshipMenuItem()
        {
            FriendsRelationshipMenuItem = new ItemModel();
            FriendsRelationshipMenuItem.IconNormal = imageFolder + "Relationship_Icon.png";
            FriendsRelationshipMenuItem.IconAccent = imageFolder + "Relationship_Icon_Accent.png";
            FriendsRelationshipMenuItem.ToggleAccent(false);

            FriendsRelationshipMenuItem.ItemTitle = "Relationship";
            FriendsRelationshipMenuItem.ItemDescription = Words.RelationshipDescription;
        }

        private void LoadFriendsClearMenuItem()
        {
            FriendsClearMenuItem = new ItemModel();
            FriendsClearMenuItem.IconNormal = imageFolder + "Clear_Icon.png";
            FriendsClearMenuItem.IconAccent = imageFolder + "Clear_Icon_Accent.png";
            FriendsClearMenuItem.ToggleAccent(true);

            FriendsClearMenuItem.ItemTitle = "Clear";
            FriendsClearMenuItem.ItemDescription = Words.ClearDescription;
        }

        private void LoadShorestPathMenuItem()
        {
            ShorestPathMenuItem = new ItemModel();
            ShorestPathMenuItem.IconNormal = imageFolder + "ShorestPath_Icon.png";
            ShorestPathMenuItem.ToggleAccent(false);

            ShorestPathMenuItem.ItemTitle = "Shortest Path";
            ShorestPathMenuItem.ItemDescription = Words.ShorestPathDescription;
        }
    }
}