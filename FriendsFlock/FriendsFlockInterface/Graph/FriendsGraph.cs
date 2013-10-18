using QuickGraph;
using QuickGraph.Algorithms;
using QuickGraph.Algorithms.ShortestPath;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media.Imaging;
using System;
using System.Diagnostics;
using System.Windows;
namespace FriendsFlockInterface
{
    public class FriendsGraph : UndirectedGraph<FriendsVertex, FriendsEdge>
    {
        public FriendsUserInfo UserInfo;
        public Dictionary<long, FriendsVertex> VertexDictionary;
        public List<FriendsEdge> EdgeList;
        public WriteableBitmap EdgeMapBitmap;

        public FriendsGraph()
        {
            VertexDictionary = new Dictionary<long, FriendsVertex>();
            EdgeList = new List<FriendsEdge>();
            this.VertexAdded += new VertexAction<FriendsVertex>(FriendsGraph_VertexAdded);
            this.EdgeAdded += new EdgeAction<FriendsVertex, FriendsEdge>(FriendsGraph_EdgeAdded);
        }

        //Cache Adjacent Vertexes
        void FriendsGraph_EdgeAdded(FriendsEdge e)
        {
            EdgeList.Add(e);
            if (!e.Source.AdjacentVertexes.ContainsKey(e.Target.Uid))
                e.Source.AdjacentVertexes.Add(e.Target.Uid, e.Target);
            if (!e.Target.AdjacentVertexes.ContainsKey(e.Source.Uid))
                e.Target.AdjacentVertexes.Add(e.Source.Uid, e.Source);
        }

        //Cache Vertexes by Uid
        void FriendsGraph_VertexAdded(FriendsVertex vertex)
        {
            if (!VertexDictionary.ContainsKey(vertex.Uid))
                VertexDictionary.Add(vertex.Uid, vertex);
        }

        public override string ToString()
        {
            return string.Format("FriendsGraph: V={0}, E={1}", this.VertexCount, this.EdgeCount);
        }

        public List<FriendsEdge> GetShorestPath(FriendsVertex Source, FriendsVertex Target)
        {
            List<FriendsEdge> shorestPath = new List<FriendsEdge>();

            Func<FriendsEdge, double> edgeCost = e => 1; // constant cost

            var tryGetPath = this.ShortestPathsDijkstra<FriendsVertex, FriendsEdge>(edgeCost, Source);
            IEnumerable<FriendsEdge> path;
            if (tryGetPath(Target, out path))
                foreach (FriendsEdge e in path)
                {
                    Debug.WriteLine(e.ToString());
                    shorestPath.Add(e);
                }
            else
                Debug.WriteLine("No Path Found");

            return shorestPath;
        }

        public void ComputeGridLayout()
        {
            FriendsGraph Graph = this;

            int side;
            double dSide = Math.Sqrt(Graph.VertexCount);
            if (dSide == Math.Floor(dSide))
                side = Convert.ToInt32(Math.Floor(dSide));
            else
                side = Convert.ToInt32(Math.Floor(dSide)) + 1;

            Queue<FriendsVertex> VertexQ = new Queue<FriendsVertex>();

            for (int i = 0; i < Graph.VertexCount; i++)
            {
                VertexQ.Enqueue(Graph.Vertices.ElementAt(i));
            }

            for (int l = 0; l < side; l++)
            {
                for (int t = 0; t < side; t++)
                {
                    if (VertexQ.Count == 0)
                        return;

                    FriendsVertex v = VertexQ.Dequeue();
                    v.Layout.GridPoint = new Point(t * 61, l * 61);
                    v.Layout.CurrentX = v.Layout.GridPoint.X;
                    v.Layout.CurrentY = v.Layout.GridPoint.Y;
                }
            }
        }
    }
}