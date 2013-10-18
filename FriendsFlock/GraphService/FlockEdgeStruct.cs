using System;

namespace GraphService
{
    public struct FlockEdgeStruct
    {
        public int X1;
        public int Y1;
        public int X2;
        public int Y2;

        public FlockEdgeStruct(double x1, double y1, double x2, double y2)
        {
            X1 = Convert.ToInt32(x1) + 28;
            Y1 = Convert.ToInt32(y1) + 28;
            X2 = Convert.ToInt32(x2) + 28;
            Y2 = Convert.ToInt32(y2) + 28;
        }
    }
}
