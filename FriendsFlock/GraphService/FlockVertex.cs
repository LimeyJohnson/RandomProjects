using System;
using QuickGraph;
using System.Windows;
using System.Collections.Generic;
using System.Collections;

namespace GraphService
{
    public class FlockVertex
    {
        public long Uid { get; private set; }
        public Size VertexSize { get; set; }
        public Point GridPoint { get; set; }
        public Point FlockPoint { get; set; }
        public Rect FlockRect { get; set; }

        public FlockVertex(long Uid)
        {
            this.Uid = Uid;

            VertexSize = new Size(58d, 58d);
            GridPoint = new Point();
            FlockPoint = new Point();
            FlockRect = new Rect();
        }

        public override string ToString()
        {
            return Uid.ToString();
        }
    }
}