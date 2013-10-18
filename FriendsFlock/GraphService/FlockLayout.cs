using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using GraphSharp.Algorithms.Layout;
using GraphSharp.Algorithms.Layout.Simple.FDP;
using GraphSharp.Algorithms.OverlapRemoval;
using core = FriendsFlockCore;


namespace GraphService
{
    public class FlockLayout
    {
        private FlockGraph Graph;
        private BackgroundWorker Worker;
        private FlockGraphDirected directed;
        /// <summary>
        /// Computes XY Layout of Graph.
        /// <remarks>
        /// Layout algorithms only support bidirected, 
        /// so class packs/unpack from undirected to directed
        /// </remarks>
        /// </summary>
        /// <param name="graph">Graph to Compute Vertex.X/Y Points</param>
        /// <returns></returns>
        public FlockLayout(FlockGraph Graph, BackgroundWorker Worker)
        {
            this.Graph = Graph;
            this.Worker = Worker;
        }

        public void Run()
        {
            core.Stopwatch swFlockLayout = new core.Stopwatch();
            swFlockLayout.Start("FlockLayout");

            //1. Undirected --> Directed
            directed = ToDirected(Graph);

            //2. Compute Flock Layout
            ComputeLayout();

            //3. Compute Overlap Removal
            ComputerOverlapRemoval();


            swFlockLayout.Stop();
            core.Analytics.SendMetric(core.Analytics.Metric.GraphTime, swFlockLayout.SecondsEllapsed());
        }

        //Done
        private FlockGraphDirected ToDirected(FlockGraph Graph)
        {
            FlockGraphDirected directed = new FlockGraphDirected();

            foreach (FlockVertex v in Graph.VertexDictionary.Values)
            {
                directed.AddVertex(v);
            }

            foreach (FlockEdge e in Graph.Edges)
            {
                directed.AddEdge(e);
            }

            return directed;
        }

        private void ComputeLayout()
        {
            KKLayoutParameters kkp = new KKLayoutParameters();
            kkp.Height = 600;
            kkp.Width = 900;

            kkp.MaxIterations = 1500;
            kkp.AdjustForGravity = true;
            kkp.ExchangeVertices = true;

            KKLayoutAlgorithm<FlockVertex, FlockEdge, FlockGraphDirected> kka;
            kka = new KKLayoutAlgorithm<FlockVertex, FlockEdge, FlockGraphDirected>(directed, kkp);
            kka.IterationEnded += new LayoutIterationEndedEventHandler<FlockVertex, FlockEdge>(kka_IterationEnded);
            kka.Compute();

            //Get Points, Set Size
            for (int i = 0; i < directed.VertexCount; i++)
            {
                FlockVertex v = directed.Vertices.ElementAt(i);
                Point point = kka.VertexPositions.ElementAt(i).Value;
                Size size = v.VertexSize;

                v.FlockRect = new Rect(point, size);
            }
        }

        private void kka_IterationEnded(object sender, ILayoutIterationEventArgs<FlockVertex> e)
        {
            Worker.ReportProgress(Convert.ToInt32(Math.Round(e.StatusInPercent * 100)));
        }

        private void ComputerOverlapRemoval()
        {
            //Overlap Param
            OverlapRemovalParameters orp = new OverlapRemovalParameters();
            orp.VerticalGap = 2;
            orp.HorizontalGap = 2;

            //Get Rect for Each Vertex
            Dictionary<FlockVertex, Rect> rectangles = new Dictionary<FlockVertex, Rect>();
            for (int i = 0; i < directed.VertexCount; i++)
            {
                FlockVertex v = directed.Vertices.ElementAt(i);
                rectangles.Add(v, v.FlockRect);
            }

            try
            {
                FSAAlgorithm<FlockVertex> ora = new FSAAlgorithm<FlockVertex>(rectangles, orp);
                ora.Compute();

                //Update New Rect Postions
                for (int i = 0; i < ora.Rectangles.Count; i++)
                {
                    FlockVertex v = directed.Vertices.ElementAt(i);
                    v.FlockRect = ora.Rectangles.ElementAt(i).Value;
                    v.FlockPoint = new Point(v.FlockRect.X, v.FlockRect.Y);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(string.Format("Error at Overlap: {0}. {1}", ex.Message, ex.StackTrace));
            }
        }
    }
}