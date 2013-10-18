using System.Windows;

namespace FriendsFlockInterface
{
    public class FriendsLayout : ModelBase
    {
        public bool IsFlockLayout = false;

        private double _CurrentX;
        public double CurrentX
        {
            get { return _CurrentX; }
            set
            {
                if (_CurrentX != value)
                {
                    _CurrentX = value;
                    RaisePropertyChanged("CurrentX");
                }
            }
        }

        private double _CurrentY;
        public double CurrentY
        {
            get { return _CurrentY; }
            set
            {
                if (_CurrentY != value)
                {
                    _CurrentY = value;
                    RaisePropertyChanged("CurrentY");
                }
            }
        }

        public Point GridPoint { get; set; }
        public Point FlockPoint { get; set; }

        public void GotoGridLayout()
        {
            if (GridPoint != null)
            {
                IsFlockLayout = false;
                CurrentX = GridPoint.X;
                CurrentY = GridPoint.Y;
            }
        }

        public void GotoFlockLayout()
        {
            if (FlockPoint != null)
            {
                IsFlockLayout = true;
                CurrentX = FlockPoint.X;
                CurrentY = FlockPoint.Y;
            }
        }
    }
}
