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
    public class FriendsUserInfo
    {
        public long Uid { get; set; }
        public string Name { get; set; }
        public string Pic_Big_Url { get; set; }
        public string Pic_Square_Url { get; set; }
        public string Profile_Url { get; set; }
    }
}
