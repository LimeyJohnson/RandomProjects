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
    public class FriendsInfo
    {
        public long Uid { get; set; }
        public string Name {get; set;}
        public string Pic_Big_Url { get; set; }
        public string Pic_Sqaure_Url { get; set; }
        public string Profile_Url { get; set; }

        public long Profile_Update_Time { get; set; }
        public string Profile_Update_Time_String { get; set; }        
        public string Status_Time { get; set; }
        public string Birthday_date { get; set; }

        public string Sex { get; set; }
        public string Relationship_Status { get; set; }
        public long Significant_Other_Id { get; set; }
        public bool Is_App_User { get; set; }
        public int Wall_Count { get; set; }
        public string Status_Message { get; set; }
        public string Online_Presence { get; set; }
        public string Current_Location { get; set; }
        public string Webstie { get; set; }
        public string Third_Party_Id { get; set; }
    }
}
