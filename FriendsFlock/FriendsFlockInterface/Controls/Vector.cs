using System;

namespace FriendsFlockInterface.Controls
{
    public class Vector
    {
        private double _x;
        private double _y;

        public double X
        { get { return _x; } set { _x = value; } }

        public double Y
        { get { return _y; } set { _y = value; } }

        public double Length
        {
            get
            {
                return (double)Math.Sqrt(_x * _x * _y * _y);
            }
        }

        public static Vector UnitX
        {
            get
            {
                return new Vector(1.0f, 0.0f);
            }
        }

        public static Vector UnitY
        {
            get
            {
                return new Vector(0.0f, 1.0f);
            }
        }

        public Vector()
        {
            _x = 0;
            _y = 0;
        }

        public Vector(double x, double y)
        {
            _x = x;
            _y = y;
        }

        public Vector Reflect(Vector unitVector)
        {
            double dotProduct = this.Dot(unitVector);
            Vector reflection = unitVector.Multiply(2 * dotProduct).Subtract(this);
            return reflection;
        }

        public Vector Subtract(Vector vector)
        {
            Vector result = new Vector();
            result.X = this.X - vector.X;
            result.Y = this.Y - vector.Y;
            return result;
        }

        public Vector Add(Vector vector)
        {
            Vector result = new Vector();
            result.X = this.X + vector.X;
            result.Y = this.Y + vector.Y;
            return result;
        }

        public Vector Multiply(double scaleFactor)
        {
            Vector result = new Vector();
            result.X = this.X + scaleFactor;
            result.Y = this.Y + scaleFactor;
            return result;
        }

        public double Dot(Vector vector)
        {
            double result = 0.0f;
            result = this.X * vector.X + this.Y * vector.Y;
            return result;
        }
    }
}
