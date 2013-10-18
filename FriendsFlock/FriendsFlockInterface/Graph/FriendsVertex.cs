using GalaSoft.MvvmLight.Messaging;
using System.Collections.Generic;
namespace FriendsFlockInterface
{
    public class FriendsVertex : ModelBase
    {
        public long Uid { get; set; }

        #region Graph
        public Dictionary<long, FriendsVertex> AdjacentVertexes;
        #endregion

        #region Friends
        public FriendsInfo Info { get; set; }
        #endregion

        #region Layout
        public FriendsLayout Layout { get; set; }
        #endregion

        #region Interface
        public FriendsState State { get; set; }
        #endregion

        public FriendsVertex(long Uid)
        {
            this.Uid = Uid;
            AdjacentVertexes = new Dictionary<long, FriendsVertex>();
            Info = new FriendsInfo();
            Layout = new FriendsLayout();
            State = new FriendsState();

            if (Uid == 0d)
                SetDefaults();
        }

        private void SetDefaults()
        {
            Info.Name = "Select a Friend's Square...";
        }

        public override string ToString()
        {
            return string.Format("{0}\t{1}", Uid.ToString(), Info.Name);
        }
    }
}