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

namespace GraphSharp
{
    public static class SupportExtensions
    {
        public static Rect Offset(this Rect source, double x, double y)
        {
            return new Rect(source.X + x, source.Y + y, source.Width, source.Height);
        }

        public static bool IntersectsWith(this Rect a, Rect b)
        {
            double aXmin = a.X;             //Left
            double aXmax = a.X + a.Width;   //Right
            double aYmin = a.Y;             //Top
            double aYmax = a.Y + a.Height;  //Bottom

            double bXmin = b.X;             //Left
            double bXmax = b.X + b.Width;   //Right
            double bYmin = b.Y;             //Top
            double bYmax = b.Y + b.Height;  //Bottom

            //Checks Rect b Top-Left
            if (((aXmin <= bXmin) && (bXmin <= aXmax)) && ((aYmin <= bYmin) && (bYmin <= aYmax)))
                return true;

            //Checks Rect b Bottom-Left
            if (((aXmin <= bXmin) && (bXmin <= aXmax)) && ((aYmin <= bYmax) && (bYmax <= aYmax)))
                return true;

            //Checks Rect b Top-Right
            if (((aXmin <= bXmax) && (bXmax <= aXmax)) && ((aYmin <= bYmin) && (bYmin <= aYmax)))
                return true;

            //Checks Rect b Bottm-Right
            if (((aXmin <= bXmax) && (bXmax <= aXmax)) && ((aYmin <= bYmax) && (bYmax <= aYmax)))
                return true;

            //No Overlap
            return false;
        }

        public static Vector ToVector(this Point p)
        {
            return new Vector(p.X, p.Y);
        }
    }
}
