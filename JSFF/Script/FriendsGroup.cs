// Class1.cs
//

using System;
using System.Html;
using System.Runtime.CompilerServices;
using System.Collections;
using FreindsLibrary;
using System.Html.Media.Graphics;
using jQueryApi;
namespace JSFFScript
{
    public sealed class Vector
    {
        public double xCord;
        public double yCord;
        public Vector(double X, double Y)
        {
            this.xCord = X;
            this.yCord = Y;
        }
        public void Add(Vector add)
        {
            this.xCord += add.xCord;
            this.yCord += add.yCord;
        }
    }
    public sealed class Point
    {
        public int X;
        public int Y;
        public void AddVector(Vector v)
        {
            this.X += (int)v.xCord;
            this.Y += (int)v.yCord;
        }
    }

}


