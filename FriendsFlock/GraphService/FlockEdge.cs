using QuickGraph;

namespace GraphService
{
    public class FlockEdge : UndirectedEdge<FlockVertex>
    {
        public FlockEdge(FlockVertex Source, FlockVertex Target)
            : base(Source, Target)
        {
        }

        public override string ToString()
        {
            return string.Format("{0}--{1}", Source.Uid, Target.Uid);
        }
    }
}
