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

namespace FriendsService
{
    public static class Helpers
    {
        /// <summary>
        /// Cleans Url parameters of https because SL image controls do not support them.
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string CleanHttps(string url)
        {
            string https = "https://";
            string http = "http://";

            if (url.Contains(https))
            {
                url = url.Replace(https, http);
            }

            return url;
        }


        /// <summary>
        /// Big Pic:
        /// FB sends back a default pic of *.gif if no profile updated
        /// SL image controls do not support .gif, local png is used instead
        /// </summary>
        private static string imagesFolder = "Images/";
        public static string DefaultPictureBig(string url, bool isMale)
        {
            if (url.Contains(".gif"))
            {
                if (isMale)
                {
                    url = imagesFolder + "Dad_Profile.png";
                }
                else
                {
                    url = imagesFolder + "Mom_Profile.png";
                }
            }
            
            return url;
        }

        /// <summary>
        /// Quare Pic:
        /// FB sends back a default pic of *.gif if no profile updated
        /// SL image controls do not support .gif, local png is used instead
        /// </summary>
        public static string DefaultPictureSquare(string url, bool isMale)
        {
            if (url.Contains(".gif"))
            {
                if (isMale)
                {
                    url = imagesFolder + "Dad_Square.png";
                }
                else
                {
                    url = imagesFolder + "Mom_Sqaure.png";
                }
            }

            return url;
        }
    }
}
