using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using QuickGraph.Algorithms;

namespace GraphService
{
    public class GraphServiceFacade
    {
        public void BuildRandomGraph(FlockGraph Graph, int NumberOfVertexes, int NumberOfEdges)
        {
            Random r = new Random(DateTime.Now.Millisecond);

            //Build Vertexes
            for (int i = 0; i < NumberOfVertexes; i++)
            {
                FlockVertex v = new FlockVertex(i);
                Graph.AddVertex(v);
            }

            //Build Edges
            double vc = Graph.VertexCount;
            int flock1 = (int)Math.Round(vc / 3);
            int flock2 = flock1 * 2;
            int flock3 = Graph.VertexCount - 1;

            int flocks = (int)Math.Round((double)(NumberOfEdges / 3));

            FlockVertex s = Graph.Vertices.ElementAt(1);
            FlockVertex t = Graph.Vertices.ElementAt(flock2);
            Graph.AddEdge(new FlockEdge(s, t));

            s = Graph.Vertices.ElementAt(flock2);
            t = Graph.Vertices.ElementAt(flock3);
            Graph.AddEdge(new FlockEdge(s, t));

            for (int i = 0; i < flocks; i++)
            {
                FlockVertex source = Graph.Vertices.ElementAt(r.Next(1, flock1));
                FlockVertex target = Graph.Vertices.ElementAt(r.Next(1, flock1));
                Graph.AddEdge(new FlockEdge(source, target));
            }

            for (int i = 0; i < flocks; i++)
            {
                FlockVertex source = Graph.Vertices.ElementAt(r.Next(flock1, flock2));
                FlockVertex target = Graph.Vertices.ElementAt(r.Next(flock1, flock2));

                Graph.AddEdge(new FlockEdge(source, target));
            }

            for (int i = 0; i < flocks; i++)
            {
                FlockVertex source = Graph.Vertices.ElementAt(r.Next(flock2, flock3));
                FlockVertex target = Graph.Vertices.ElementAt(r.Next(flock2, flock3));

                Graph.AddEdge(new FlockEdge(source, target));
            }
        }

        //public void GetGridLayout(FlockGraph Graph)
        //{
        //    Log.Start("GetGridLayout", string.Format(":{0}", Graph));
            
        //    int side;
        //    double dSide = Math.Sqrt(Graph.VertexCount);
        //    if (dSide == Math.Floor(dSide))
        //        side = Convert.ToInt32(Math.Floor(dSide));
        //    else
        //        side = Convert.ToInt32(Math.Floor(dSide)) + 1;

        //    Queue<FlockVertex> VertexQ = new Queue<FlockVertex>();

        //    for (int i = 0; i < Graph.VertexCount; i++)
        //    {
        //        VertexQ.Enqueue(Graph.Vertices.ElementAt(i));
        //    }

        //    for (int l = 0; l < side; l++)
        //    {
        //        for (int t = 0; t < side; t++)
        //        {
        //            if (VertexQ.Count == 0)
        //                return;

        //            FlockVertex v = VertexQ.Dequeue();
        //            v.GridPoint = new Point(t * 61, l * 61);
        //        }
        //    }
        //}

        //Requires Background Worker


        public void GetFlockLayout(FlockGraph Graph, BackgroundWorker Worker)
        {
            FlockLayout layout = new FlockLayout(Graph, Worker);
            layout.Run();
        }


        //public List<FlockEdge> GetShorestPath(FlockGraph Graph, FlockVertex Source, FlockVertex Target)
        //{
        //    Log.Start("GetShorestPath", string.Format("{0},{1},{2}", Graph, Source, Target));
        //    List<FlockEdge> ShortestPath = new List<FlockEdge>();

        //    Func<FlockEdge, double> edgeCost = e => 1; // constant cost

        //    var tryGetPath = Graph.ShortestPathsDijkstra<FlockVertex, FlockEdge>(edgeCost, Source);
        //    IEnumerable<FlockEdge> path;

        //    if (tryGetPath(Target, out path))
        //        foreach (FlockEdge e in path)
        //        {
        //            Log.Info(e.ToString());
        //            ShortestPath.Add(e);
        //        }

        //    return ShortestPath;
        //}
    }
}