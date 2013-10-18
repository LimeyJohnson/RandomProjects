//using System;
//using System.Net;
//using System.Windows;
//using System.Windows.Controls;
//using System.Windows.Data;
//using System.Windows.Documents;
//using System.Windows.Ink;
//using System.Windows.Input;
//using System.Windows.Media;
//using System.Windows.Media.Animation;
//using System.Windows.Shapes;

//namespace FriendsFlockInterface.Controls
//{
//    public class EdgeControl : Control
//    {
//        #region Dependecy Properties
//        public VertexControl Source
//        {
//            get { return (VertexControl)GetValue(SourceProperty); }
//            set { SetValue(SourceProperty, value); }
//        }
//        public static readonly DependencyProperty SourceProperty =
//            DependencyProperty.Register("Source", typeof(VertexControl), typeof(EdgeControl),
//            new PropertyMetadata(null, VertexChanged));

//        public VertexControl Target
//        {
//            get { return (VertexControl)GetValue(TargetProperty); }
//            set { SetValue(TargetProperty, value); }
//        }
//        public static readonly DependencyProperty TargetProperty =
//            DependencyProperty.Register("Target", typeof(VertexControl), typeof(EdgeControl),
//            new PropertyMetadata(null, VertexChanged));

//        public double SourceX
//        {
//            get { return (double)GetValue(SourceXProperty); }
//            set { SetValue(SourceXProperty, value); }
//        }
//        public static readonly DependencyProperty SourceXProperty =
//            DependencyProperty.Register("SourceX", typeof(double), typeof(EdgeControl),
//            new PropertyMetadata(0.0d));

//        public double SourceY
//        {
//            get { return (double)GetValue(SourceYProperty); }
//            set { SetValue(SourceYProperty, value); }
//        }
//        public static readonly DependencyProperty SourceYProperty =
//            DependencyProperty.Register("SourceY", typeof(double), typeof(EdgeControl),
//            new PropertyMetadata(0.0d));

//        public double TargetX
//        {
//            get { return (double)GetValue(TargetXProperty); }
//            set { SetValue(TargetXProperty, value); }
//        }
//        public static readonly DependencyProperty TargetXProperty =
//            DependencyProperty.Register("TargetX", typeof(double), typeof(EdgeControl),
//            new PropertyMetadata(0.0d));

//        public double TargetY
//        {
//            get { return (double)GetValue(TargetYProperty); }
//            set { SetValue(TargetYProperty, value); }
//        }
//        public static readonly DependencyProperty TargetYProperty =
//            DependencyProperty.Register("TargetY", typeof(double), typeof(EdgeControl),
//            new PropertyMetadata(0.0d));

//        public bool TargetLayoutFlock
//        {
//            get { return (bool)GetValue(TargetLayoutFlockProperty); }
//            set { SetValue(TargetLayoutFlockProperty, value); }
//        }
//        public static readonly DependencyProperty TargetLayoutFlockProperty =
//            DependencyProperty.Register("TargetLayoutFlock", typeof(bool), typeof(EdgeControl),
//            new PropertyMetadata(false, VertexChanged));

//        public LinesStateEnum StateLines
//        {
//            get { return (LinesStateEnum)GetValue(StateLinesProperty); }
//            set { SetValue(StateLinesProperty, value); }
//        }
//        public static readonly DependencyProperty StateLinesProperty =
//            DependencyProperty.Register("StateLines", typeof(LinesStateEnum), typeof(EdgeControl),
//            new PropertyMetadata(LinesStateEnum.Show, StateEdgeChanged));

//        public bool StateSourceMouseOver
//        {
//            get { return (bool)GetValue(StateSourceMouseOverProperty); }
//            set { SetValue(StateSourceMouseOverProperty, value); }
//        }
//        public static readonly DependencyProperty StateSourceMouseOverProperty =
//            DependencyProperty.Register("StateSourceMouseOver", typeof(bool), typeof(EdgeControl),
//            new PropertyMetadata(false, StateEdgeChanged));

//        public bool StateTargetMouseOver
//        {
//            get { return (bool)GetValue(StateTargetMouseOverProperty); }
//            set { SetValue(StateTargetMouseOverProperty, value); }
//        }
//        public static readonly DependencyProperty StateTargetMouseOverProperty =
//            DependencyProperty.Register("StateTargetMouseOver", typeof(bool), typeof(EdgeControl),
//            new PropertyMetadata(false, StateEdgeChanged));

//        public LayoutStateEnum StateLayout
//        {
//            get { return (LayoutStateEnum)GetValue(StateLayoutProperty); }
//            set { SetValue(StateLayoutProperty, value); }
//        }
//        public static readonly DependencyProperty StateLayoutProperty =
//            DependencyProperty.Register("StateLayout", typeof(LayoutStateEnum), typeof(EdgeControl),
//            new PropertyMetadata(LayoutStateEnum.Cube, VertexChanged));

//        public bool StateShortestPath
//        {
//            get { return (bool)GetValue(StateShortestPathProperty); }
//            set { SetValue(StateShortestPathProperty, value); }
//        }
//        public static readonly DependencyProperty StateShortestPathProperty =
//            DependencyProperty.Register("StateShortestPath", typeof(bool), typeof(EdgeControl), new PropertyMetadata(false, StateEdgeChanged));

//        public SolidColorBrush EdgeColor
//        {
//            get { return (SolidColorBrush)GetValue(EdgeColorProperty); }
//            set { SetValue(EdgeColorProperty, value); }
//        }
//        public static readonly DependencyProperty EdgeColorProperty =
//            DependencyProperty.Register("EdgeColor", typeof(SolidColorBrush), typeof(EdgeControl), new PropertyMetadata(new SolidColorBrush(Colors.Black)));

//        public double EdgeThickness
//        {
//            get { return (double)GetValue(EdgeThicknessProperty); }
//            set { SetValue(EdgeThicknessProperty, value); }
//        }
//        public static readonly DependencyProperty EdgeThicknessProperty =
//            DependencyProperty.Register("EdgeThickness", typeof(double), typeof(EdgeControl), new PropertyMetadata(2d));

//        public double EdgeZIndex
//        {
//            get { return (double)GetValue(EdgeZIndexProperty); }
//            set { SetValue(EdgeZIndexProperty, value); }
//        }
//        public static readonly DependencyProperty EdgeZIndexProperty =
//            DependencyProperty.Register("EdgeZIndex", typeof(double), typeof(EdgeControl), new PropertyMetadata(20d));
//        #endregion

//        public EdgeControl()
//        {
//            DefaultStyleKey = typeof(EdgeControl);

//            //Set Default
//            StateLines = LinesStateEnum.Show;
//        }

//        #region Visual State Manager
//        private static void VertexChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
//        {
//            EdgeControl ec = (EdgeControl)d;

//            if (ec.Source != null)
//            {
//                ec.SourceX = ec.Source.CurrentX + 28;
//                ec.SourceY = ec.Source.CurrentY + 28;

//                Binding bs = new Binding("StateMouseOver");
//                bs.Mode = BindingMode.TwoWay;
//                bs.Source = ec.Source;
//                ec.SetBinding(StateSourceMouseOverProperty, bs);
//            }

//            if (ec.Target != null)
//            {
//                ec.TargetX = ec.Target.CurrentX + 28;
//                ec.TargetY = ec.Target.CurrentY + 28;

//                Binding bt = new Binding("StateMouseOver");
//                bt.Mode = BindingMode.TwoWay;
//                bt.Source = ec.Target;
//                ec.SetBinding(StateTargetMouseOverProperty, bt);
//            }

//        }

//        private static void StateEdgeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
//        {
//            EdgeControl ec = (EdgeControl)d;
//            if (ec.StateLayout == LayoutStateEnum.Cube)
//                ec.VisualStateHide();
//            else if (ec.StateShortestPath)
//                ec.VisualStateShortestPath();
//            else if
//                (ec.StateSourceMouseOver)
//                ec.VisualStateHighlight();
//            else if (ec.StateTargetMouseOver)
//                ec.VisualStateHighlight();
//            else if (ec.StateLines == LinesStateEnum.Hide)
//                ec.VisualStateHide();
//            else
//                ec.VisualStateNormal();

//            //if (ec.StateLines == LinesStateEnum.Hide)
//            //    ec.VisualStateHide();
//            //else if (ec.StateSourceMouseOver)
//            //    ec.VisualStateHighlight();
//            //else if (ec.StateTargetMouseOver)
//            //    ec.VisualStateHighlight();
//            //else
//            //    ec.VisualStateNormal();

//            if (ec.StateSourceMouseOver)
//                ec.Target.StateEdgeHighlighted = true;
//            else
//                ec.Target.StateEdgeHighlighted = false;

//            if (ec.StateTargetMouseOver)
//                ec.Source.StateEdgeHighlighted = true;
//            else
//                ec.Source.StateEdgeHighlighted = false;
//        }

//        private void VisualStateNormal()
//        {
//            this.Visibility = Visibility.Collapsed;

//            //this.Visibility = Visibility.Visible;
//            //EdgeColor = new SolidColorBrush(Colors.White);
//            //EdgeThickness = 1d;
//            //Canvas.SetZIndex(this, 0);
//        }
//        private void VisualStateHighlight()
//        {
//            this.Visibility = Visibility.Visible;
//            EdgeColor = new SolidColorBrush(Colors.Blue);
//            EdgeThickness = 4d;
//            Canvas.SetZIndex(this, 1);
//        }

//        private void VisualStateShortestPath()
//        {
//            this.Visibility = Visibility.Visible;
//            EdgeColor = new SolidColorBrush(Colors.Green);
//            EdgeThickness = 4d;
//            Canvas.SetZIndex(this, 1);
//        }

//        private void VisualStateHide()
//        {
//            this.Visibility = Visibility.Collapsed;
//        }
//        #endregion
//    }
//}
