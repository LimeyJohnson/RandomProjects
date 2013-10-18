using System;
using System.Windows;

namespace GraphSharp
{
    public class Vector
    {
        public Vector(double x, double y)
        {
            X = x;
            Y = y;
        }

        public Vector()
        {
            X = 0;
            Y = 0;
        }

        public double X { get; set; }

        public double Y { get; set; }

        public void Normalize()
        {
            var length = Length;

            X /= length;
            Y /= length;
        }

        public double Length
        {
            get { return Math.Sqrt(X*X + Y*Y); }
        }

        public static Vector operator -(Vector v)
        {
            return new Vector(-v.X, -v.Y);
        }

        public static Vector operator +(Vector a, Vector b)
        {
            return new Vector(a.X + b.X, a.Y + b.Y);
        }

        public static Vector operator -(Vector a, Vector b)
        {
            return new Vector(a.X - b.X, a.Y - b.Y);
        }

        public static Vector operator /(Vector v, double scale)
        {
            return new Vector(v.X / scale, v.Y / scale);
        }

        public static Vector operator *(Vector v, double scale)
        {
            return new Vector(v.X * scale, v.Y * scale);
        }

        public static Vector operator *(double scale, Vector v)
        {
            return new Vector(v.X * scale, v.Y * scale);
        }

        public static Point operator +(Point a, Vector b)
        {
            return new Point(a.X + b.X, a.Y + b.Y);
        }

        public static Point operator -(Point a, Vector b)
        {
            return new Point(a.X - b.X, a.Y - b.Y);
        }

        public static implicit operator Vector(Point p)
        {
            return p.ToVector();
        }

        public static implicit operator Point(Vector v)
        {
            return new Point(v.X, v.Y);
        }
    }
}