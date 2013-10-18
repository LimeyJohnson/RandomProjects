using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace FriendsFlockInterface.Controls
{
    public delegate void ContentSizeChangedHandler(object sender, Size newSize);

    public class ZoomContentPresenter : ContentPresenter
    {
        public event ContentSizeChangedHandler ContentSizeChanged;
        private Size _contentSize;

        //Content SIZE Propertery, Calls Event "ContentSizeChanged"
        public Size ContentSize
        {
            get { return _contentSize; }
            private set
            {
                if (value == _contentSize)
                    return;

                _contentSize = value;
                if (ContentSizeChanged != null)
                    ContentSizeChanged(this, _contentSize);
            }
        }

        public ZoomContentPresenter()
        {

        }

        //Ovveride Base ContentPresenter for Max Size
        protected override Size MeasureOverride(Size constraint)
        {
            base.MeasureOverride(new Size(double.PositiveInfinity, double.PositiveInfinity));
            var max = 6000; //TODO
            //var maxX = 500;
            //var maxY = 1400;
            var x = double.IsInfinity(constraint.Width) ? max : constraint.Width;
            var y = double.IsInfinity(constraint.Height) ? max : constraint.Height;
            return new Size(x, y);
        }

        //? Stretches Content Size with Child Elements
        protected override Size ArrangeOverride(Size arrangeBounds)
        {
            UIElement child = VisualTreeHelper.GetChildrenCount(this) > 0
                                  ? VisualTreeHelper.GetChild(this, 0) as UIElement
                                  : null;
            if (child == null)
                return arrangeBounds;

            //set the ContentSize
            ContentSize = child.DesiredSize;
            child.Arrange(new Rect(new Point(), child.DesiredSize));

            return arrangeBounds;
        }
    }
}
