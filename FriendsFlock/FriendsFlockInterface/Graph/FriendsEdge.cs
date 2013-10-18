using QuickGraph;

namespace FriendsFlockInterface
{
    public class FriendsEdge : UndirectedEdge<FriendsVertex>
    {
        public FriendsEdge(FriendsVertex Source, FriendsVertex Target)
            : base(Source, Target)
        {
        }

        public override string ToString()
        {
            return string.Format("{0}--{1}", Source.Uid, Target.Uid);
        }
    }
}
