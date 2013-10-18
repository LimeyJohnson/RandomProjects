using QuickGraph;
using System.Collections.Generic;
using System.Windows.Media.Imaging;
using System.Linq;
using System.Diagnostics;
namespace GraphService
{
    public class FlockGraph : UndirectedGraph<FlockVertex, FlockEdge>
    {
        public Dictionary<long, FlockVertex> VertexDictionary = new Dictionary<long, FlockVertex>();

        public FlockGraph()
        {
            this.VertexAdded +=new VertexAction<FlockVertex>(FlockGraph_VertexAdded);
        }

        void FlockGraph_VertexAdded(FlockVertex vertex)
        {
            if (!VertexDictionary.ContainsKey(vertex.Uid))
                VertexDictionary.Add(vertex.Uid, vertex);
        }

        public override string ToString()
        {
            return string.Format("FlockGraph: V={0}, E={1}", this.VertexCount, this.EdgeCount);
        }
    }

    public class FlockGraphDirected : BidirectionalGraph<FlockVertex, FlockEdge>
    {
        public FlockGraphDirected()
        {
        }
    }
}
