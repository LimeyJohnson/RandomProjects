using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace FriendsFlockInterface.Controls
{
    [TemplatePart(Name = PART_Presenter, Type = typeof(ZoomContentPresenter))]
    public class ZoomControl : ContentControl
    {
        #region TESTING
        public static DependencyProperty MyTextProperty =
             DependencyProperty.Register("MyText", typeof(string), typeof(object),
                new PropertyMetadata("Howdy World!", null));
        public string MyText
        {
            get { return (string)GetValue(MyTextProperty); }
            set { SetValue(MyTextProperty, value); }
        }

        public void UpdateTesting()
        {
            MyText = "X:" + TranslateX + " Y:" + TranslateY + " Z:" + Zoom + " Mode:" + Mode.ToString();
        }
        #endregion

        #region Properties: Private
        private const string PART_Presenter = "PART_Presenter";

        //Point Mouse
        private Point _mouseDownPos;

        //Refernce to ZoomContentPresenter
        private ZoomContentPresenter _presenter;

        /// <summary>
        /// Applied to the presenter.
        /// </summary>
        private ScaleTransform _scaleTransform;                 //ScaleTransform >> shrinks or stretches an object    
        private Vector _startTranslate;                         //Vectors >> Has XY like Points, but also lenth
        private TransformGroup _transformGroup;                 //TransformGroup >> shrinks or stretches a group


        /// <summary>
        /// Applied to the scrollviewer.
        /// </summary>
        private TranslateTransform _translateTransform;         //TranslateTransfom moves an object XY pixels

        private int _zoomAnimCount;                             //Animiation Count
        private bool _isZooming = false;
        #endregion

        #region Constructors
        public ZoomControl()
        {
            DefaultStyleKey = typeof(ZoomControl);
            //PreviewMouseWheel += ZoomControl_MouseWheel;
            //PreviewMouseDown += ZoomControl_PreviewMouseDown;
            //MouseDown += ZoomControl_MouseDown;
            //MouseUp += ZoomControl_MouseUp;

            MouseLeftButtonDown += new MouseButtonEventHandler(ZoomControl_MouseDown);
            MouseLeftButtonUp += new MouseButtonEventHandler(ZoomControl_MouseUp);
            MouseRightButtonDown += new MouseButtonEventHandler(ZoomControl_MouseRightButtonDown);
            MouseRightButtonUp += new MouseButtonEventHandler(ZoomControl_MouseRightButtonUp);

            MouseWheel += new MouseWheelEventHandler(ZoomControl_MouseWheel);
            this.Loaded += new RoutedEventHandler(ZoomControl_Loaded);
        }

        void ZoomControl_Loaded(object sender, RoutedEventArgs e)
        {
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            Presenter = GetTemplateChild(PART_Presenter) as ZoomContentPresenter;
            ZoomToReset();
        }

        //Actual ZoomContentPresenter Object
        protected ZoomContentPresenter Presenter
        {
            get { return _presenter; }
            set
            {
                _presenter = value;
                if (_presenter == null)
                    return;

                //add the ScaleTransform to the presenter
                _transformGroup = new TransformGroup();
                _scaleTransform = new ScaleTransform();
                _translateTransform = new TranslateTransform();
                _transformGroup.Children.Add(_scaleTransform);
                _transformGroup.Children.Add(_translateTransform);
                _presenter.RenderTransform = _transformGroup;
                _presenter.RenderTransformOrigin = new Point(0.5, 0.5);
            }
        }

        #endregion

        #region DP: AnimationLenth, ControlMode, ModiferMode
        //AnimationLenth (1:1, Fill, Load)
        public static DependencyProperty AnimationLengthProperty =
            DependencyProperty.Register("AnimationLength", typeof(TimeSpan), typeof(ZoomControl),
                                        new PropertyMetadata(TimeSpan.FromMilliseconds(500)));
        public TimeSpan AnimationLength
        {
            get { return (TimeSpan)GetValue(AnimationLengthProperty); }
            set { SetValue(AnimationLengthProperty, value); }
        }

        //ModiferMode (ENUM >> None, Pan, ZoomIn, ZoomOut, ZoomBox)
        public static DependencyProperty ModifierModeProperty =
            DependencyProperty.Register("ModifierMode", typeof(ZoomViewModifierMode), typeof(ZoomControl),
                                        new PropertyMetadata(ZoomViewModifierMode.None, ModifierModeProperty_Changed));
        public ZoomViewModifierMode ModifierMode
        {
            get { return (ZoomViewModifierMode)GetValue(ModifierModeProperty); }
            set { SetValue(ModifierModeProperty, value); }
        }

        //Control ModeProperty (Enum >> Fill, Original, Custom)
        public static DependencyProperty ModeProperty =
            DependencyProperty.Register("Mode", typeof(ZoomControlModes), typeof(ZoomControl),
                                        new PropertyMetadata(ZoomControlModes.Custom, Mode_PropertyChanged));
        public ZoomControlModes Mode
        {
            get { return (ZoomControlModes)GetValue(ModeProperty); }
            set
            {
                SetValue(ModeProperty, value);
                //RaisePropertyChanged("Mode");
            }
        }

        #endregion

        #region DP: ZoomBox Control
        //ZoomBoxBackgroundProperty
        public static readonly DependencyProperty ZoomBoxBackgroundProperty =
            DependencyProperty.Register("ZoomBoxBackground", typeof(Brush), typeof(ZoomControl),
                                new PropertyMetadata(null));
        public Brush ZoomBoxBackground
        {
            get { return (Brush)GetValue(ZoomBoxBackgroundProperty); }
            set { SetValue(ZoomBoxBackgroundProperty, value); }
        }

        //ZoomBoxBorderBrushProperty
        public static readonly DependencyProperty ZoomBoxBorderBrushProperty =
            DependencyProperty.Register("ZoomBoxBorderBrush", typeof(Brush), typeof(ZoomControl),
                                        new PropertyMetadata(null));
        public Brush ZoomBoxBorderBrush
        {
            get { return (Brush)GetValue(ZoomBoxBorderBrushProperty); }
            set { SetValue(ZoomBoxBorderBrushProperty, value); }
        }

        //ZoomBoxBorderThicknessProperty
        public static readonly DependencyProperty ZoomBoxBorderThicknessProperty =
            DependencyProperty.Register("ZoomBoxBorderThickness", typeof(Thickness), typeof(ZoomControl),
                                        new PropertyMetadata(null));
        public Thickness ZoomBoxBorderThickness
        {
            get { return (Thickness)GetValue(ZoomBoxBorderThicknessProperty); }
            set { SetValue(ZoomBoxBorderThicknessProperty, value); }
        }

        //ZoomBoxOpacityProperty
        public static readonly DependencyProperty ZoomBoxOpacityProperty =
            DependencyProperty.Register("ZoomBoxOpacity", typeof(double), typeof(ZoomControl),
                                        new PropertyMetadata(0.5));
        public double ZoomBoxOpacity
        {
            get { return (double)GetValue(ZoomBoxOpacityProperty); }
            set { SetValue(ZoomBoxOpacityProperty, value); }
        }

        //ZoomBoxProperty
        public static readonly DependencyProperty ZoomBoxProperty =
            DependencyProperty.Register("ZoomBox", typeof(Rect), typeof(ZoomControl),
                                        new PropertyMetadata(new Rect()));
        public Rect ZoomBox
        {
            get { return (Rect)GetValue(ZoomBoxProperty); }
            set { SetValue(ZoomBoxProperty, value); }
        }

        //MinZoom
        public static readonly DependencyProperty MinZoomProperty =
            DependencyProperty.Register("MinZoom", typeof(double), typeof(ZoomControl), new PropertyMetadata(.01));

        public double MinZoom
        {
            get { return (double)GetValue(MinZoomProperty); }
            set { SetValue(MinZoomProperty, value); }
        }

        //MaxZoom
        public static readonly DependencyProperty MaxZoomProperty =
            DependencyProperty.Register("MaxZoom", typeof(double), typeof(ZoomControl), new PropertyMetadata(10.0));

        public double MaxZoom
        {
            get { return (double)GetValue(MaxZoomProperty); }
            set { SetValue(MaxZoomProperty, value); }
        }

        //MaxZoomDelta
        public static readonly DependencyProperty MaxZoomDeltaProperty =
            DependencyProperty.Register("MaxZoomDelta", typeof(double), typeof(ZoomControl),
                                        new PropertyMetadata(5.0));
        public double MaxZoomDelta
        {
            get { return (double)GetValue(MaxZoomDeltaProperty); }
            set { SetValue(MaxZoomDeltaProperty, value); }
        }

        //ZoomDeltaMultiplier
        public static readonly DependencyProperty ZoomDeltaMultiplierProperty =
            DependencyProperty.Register("ZoomDeltaMultiplier", typeof(double), typeof(ZoomControl),
                                        new PropertyMetadata(10.0));
        public double ZoomDeltaMultiplier
        {
            get { return (double)GetValue(ZoomDeltaMultiplierProperty); }
            set { SetValue(ZoomDeltaMultiplierProperty, value); }
        }
        #endregion

        #region DP: Zoom
        public static readonly DependencyProperty ZoomProperty =
           DependencyProperty.Register("Zoom", typeof(double), typeof(ZoomControl),
                                       new PropertyMetadata(1.0, Zoom_PropertyChanged));
        public double Zoom
        {
            get { return (double)GetValue(ZoomProperty); }
            set
            {
                if (value == (double)GetValue(ZoomProperty))
                    return;
                //Animate the Zoom Slider
                //BeginAnimation(ZoomProperty, null);
                SetValue(ZoomProperty, value);
            }
        }

        private static void Zoom_PropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var zc = (ZoomControl)d;
            if (zc._scaleTransform == null)
                return;

            double zoom = (double)e.NewValue;
            zc._scaleTransform.ScaleX = zoom;
            zc._scaleTransform.ScaleY = zoom;
            if (!zc._isZooming)
            {
                double delta = (double)e.NewValue / (double)e.OldValue;
                zc.TranslateX *= delta;
                zc.TranslateY *= delta;
                zc.Mode = ZoomControlModes.Custom;
            }
        }
        #endregion

        #region DP: Translate XY (Pan)
        //X
        //1. TranslateXProperty
        public static readonly DependencyProperty TranslateXProperty =
         DependencyProperty.Register("TranslateX", typeof(double), typeof(ZoomControl),
                    new PropertyMetadata(0.0, routeX));
        //2. TranslateX
        public double TranslateX
        {
            get { return (double)GetValue(TranslateXProperty); }
            set
            {
                //BeginAnimation(TranslateXProperty, null);
                SetValue(TranslateXProperty, value);
            }
        }
        //3. RouteX
        private static void routeX(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            TranslateX_PropertyChanged(d, e);
            TranslateX_Coerce(d, e);
        }
        //4. TranslateX_PropertyChanged
        private static void TranslateX_PropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var zc = (ZoomControl)d;
            if (zc._translateTransform == null)
                return;
            zc._translateTransform.X = (double)e.NewValue;
            if (!zc._isZooming)
                zc.Mode = ZoomControlModes.Custom;
        }
        //5. TranslateX_Coerce
        private static void TranslateX_Coerce(DependencyObject d, object basevalue)
        {
            var zc = (ZoomControl)d;
            //return zc.GetCoercedTranslateX((double)basevalue, zc.Zoom);
        }
        //6. GetCoercedTranslateX
        private double GetCoercedTranslateX(double baseValue, double zoom)
        {
            if (_presenter == null)
                return 0.0;

            return GetCoercedTranslate(baseValue, zoom,
                                       _presenter.ContentSize.Width,
                                       _presenter.DesiredSize.Width,
                                       ActualWidth);
        }

        //Y
        //1. TranslateYProperty
        public static readonly DependencyProperty TranslateYProperty =
           DependencyProperty.Register("TranslateY", typeof(double), typeof(ZoomControl),
                       new PropertyMetadata(0.0, routeY));
        //2. TranslateY
        public double TranslateY
        {
            get { return (double)GetValue(TranslateYProperty); }
            set
            {
                //BeginAnimation(TranslateYProperty, null);
                SetValue(TranslateYProperty, value);
            }
        }
        //3. RouteY
        private static void routeY(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            TranslateY_PropertyChanged(d, e);
            TranslateY_Coerce(d, e);
        }
        //4. TranslateY_PropertyChanged
        private static void TranslateY_PropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var zc = (ZoomControl)d;
            if (zc._translateTransform == null)
                return;
            zc._translateTransform.Y = (double)e.NewValue;
            if (!zc._isZooming)
                zc.Mode = ZoomControlModes.Custom;
        }
        //5. TranslateY_Coerce
        private static void TranslateY_Coerce(DependencyObject d, object basevalue)
        {
            var zc = (ZoomControl)d;
            //return zc.GetCoercedTranslateY((double)basevalue, zc.Zoom);
        }

        //6. GetCorecedTranslateY
        private double GetCoercedTranslateY(double baseValue, double zoom)
        {
            if (_presenter == null)
                return 0.0;

            return GetCoercedTranslate(baseValue, zoom,
                                       _presenter.ContentSize.Height,
                                       _presenter.DesiredSize.Height,
                                       ActualHeight);
        }

        //XY
        //7. GetCoercedTranslate
        private double GetCoercedTranslate(double translate, double zoom, double contentSize, double desiredSize, double actualSize)
        {
            /*if (_presenter == null)
                return 0.0;

            //the scaled size of the zoomed content
            var scaledSize = desiredSize * zoom;

            //the plus size above the desired size of the contentpresenter
            var plusSize = contentSize > desiredSize ? (contentSize - desiredSize) * zoom : 0.0;

            //is the zoomed content bigger than actual size of the zoom control?
            /*var bigger = 
                _presenter.ContentSize.Width * zoom > ActualWidth && 
                _presenter.ContentSize.Height * zoom > ActualHeight;*/
            /*var bigger = contentSize * zoom > actualSize;
            var m = bigger ? -1 : 1;

            if (bigger)
            {
                var topRange = m*(actualSize - scaledSize)/2.0;
                var bottomRange = m*((actualSize - scaledSize)/2.0 - plusSize);

                var minusRange = bigger ? bottomRange : topRange;
                var plusRange = bigger ? topRange : bottomRange;

                translate = Math.Max(-minusRange, translate);
                translate = Math.Min(plusRange, translate);
                return translate;
            } else
            {
                return -plusSize/2.0;
            }*/
            return translate;
        }

        #endregion

        #region SwitchBoard: ZoomToOriginal, ZoomToFill
        //1. ZoomToOriginal
        public void ZoomToOriginal()
        {
            Mode = ZoomControlModes.Original;
        }

        //3. DoZoomToOriginal
        private void DoZoomToOriginal()
        {
            if (_presenter == null)
                return;

            var initialTranslate = GetInitialTranslate();
            DoZoomAnimation(1.0, initialTranslate.X, initialTranslate.Y);
        }

        //1. ZoomToFill
        public void ZoomToFill()
        {
            Mode = ZoomControlModes.Fill;
        }

        //3. DoZoomToFill
        private void DoZoomToFill()
        {
            if (_presenter == null)
                return;

            var deltaZoom = Math.Min(
                ActualWidth / _presenter.ContentSize.Width,
                ActualHeight / _presenter.ContentSize.Height);

            var initialTranslate = GetInitialTranslate();
            DoZoomAnimation(deltaZoom, initialTranslate.X * deltaZoom, initialTranslate.Y * deltaZoom);
        }

        //3.5 GetInitialTranslate
        private Vector GetInitialTranslate()
        {
            if (_presenter == null)
                return new Vector(0.0, 0.0);
            var w = _presenter.ContentSize.Width - _presenter.DesiredSize.Width;
            var h = _presenter.ContentSize.Height - _presenter.DesiredSize.Height;
            var tX = -w / 2.0;
            var tY = -h / 2.0;

            return new Vector(tX, tY);
            //return new Vector(0, 0);
        }
        #endregion

        #region SwitchBoard: Zoom Custom (Slider)
        public Point OrigoPosition
        {
            get { return new Point(ActualWidth / 2, ActualHeight / 2); }
        }

        private void DoZoom(double deltaZoom, Point origoPosition, Point startHandlePosition, Point targetHandlePosition)
        {
            double startZoom = Zoom;
            double currentZoom = startZoom * deltaZoom;
            currentZoom = Math.Max(MinZoom, Math.Min(MaxZoom, currentZoom));

            Zoom = currentZoom;
            Mode = ZoomControlModes.Custom;
        }
        #endregion

        #region SwitchBoard: Control, Actions

        //2. Control ModePropertyChanged (Enum >> Fill, Original, Custom)
        private static void Mode_PropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {

            var zc = (ZoomControl)d;
            var mode = (ZoomControlModes)e.NewValue;

            switch (mode)
            {
                case ZoomControlModes.Fill:
                    zc.DoZoomToFill();              //Calls Method DoZoomToFill
                    break;
                case ZoomControlModes.Original:
                    zc.DoZoomToOriginal();          //DoZoomToOriginal
                    break;
                case ZoomControlModes.Reset:
                    zc.DoZoomToReset();
                    break;
                case ZoomControlModes.Custom:
                    break;                          //Nothing
                default:
                    throw new ArgumentOutOfRangeException();
            }

            zc.UpdateTesting();
        }

        private void ZoomToReset()
        {
            Mode = ZoomControlModes.Reset;
        }

        private void DoZoomToReset()
        {
            Mode = ZoomControlModes.Reset;

            var initialTranslate = GetInitialTranslate();
            DoZoomAnimation(.8, initialTranslate.X, initialTranslate.Y);
        }

        private static void ModifierModeProperty_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var zc = (ZoomControl)d;
        }

        //4. DoZoomAnimation
        private void DoZoomAnimation(double targetZoom, double transformX, double transformY)
        {
            _isZooming = true;
            Duration duration = new Duration(AnimationLength);

            StartAnimationX(TranslateXProperty, transformX, duration);
            StartAnimationY(TranslateYProperty, transformY, duration);
            StartAnimationZ(ZoomProperty, targetZoom, duration);

            _isZooming = false;
        }

        //5. StartAnimation
        private void StartAnimation(DependencyProperty dp, double toValue, Duration duration)
        {

            if (double.IsNaN(toValue) || double.IsInfinity(toValue))
            {
                if (dp == ZoomProperty)
                {
                    _isZooming = false;
                }
                return;
            }

            var animation = new DoubleAnimation();
            animation.To = toValue;
            animation.Duration = duration;

            if (dp == ZoomProperty)
            {
                _zoomAnimCount++;
                animation.Completed += (s, args) =>
                {
                    _zoomAnimCount--;
                    if (_zoomAnimCount > 0)
                        return;
                    var zoom = Zoom;
                    //BeginAnimation(ZoomProperty, null);
                    SetValue(ZoomProperty, zoom);
                    _isZooming = false;
                };
            }

            //BeginAnimation(dp, animation, HandoffBehavior.Compose);
        }
        #endregion

        #region Animation
        private void StartAnimationX(DependencyProperty dp, double toValue, Duration duration)
        {
            if (double.IsNaN(toValue) || double.IsInfinity(toValue))
            {
                if (dp == ZoomProperty)
                {
                    _isZooming = false;
                }
                return;
            }

            if (Mode != ZoomControlModes.Reset)
            {
                if ((Zoom < MaxZoom) && (Zoom > MinZoom))
                {
                    //if (Zoom < 1)
                    //    TranslateX = toValue;
                    //else
                    //    TranslateX = toValue;
                    //TranslateX = toValue;
                }

            }
            else
            {
                TranslateX = 0.0;
            }
        }
        private void StartAnimationY(DependencyProperty dp, double toValue, Duration duration)
        {
            if (double.IsNaN(toValue) || double.IsInfinity(toValue))
            {
                if (dp == ZoomProperty)
                {
                    _isZooming = false;
                }
                return;
            }

            if (Mode != ZoomControlModes.Reset)
            {
                //if (Zoom < 1)
                //    TranslateY = toValue;
                //else
                //    TranslateY = toValue;
            }
            else
            {
                TranslateY = 0.0;
            }
        }
        private void StartAnimationZ(DependencyProperty dp, double toValue, Duration duration)
        {
            if (double.IsNaN(toValue) || double.IsInfinity(toValue))
            {
                if (dp == ZoomProperty)
                {
                    _isZooming = false;
                }
                return;
            }


            if (_isZooming == true)
            {
                Zoom = toValue;
            }
            else
            {
                Zoom = 0.0;
            }
        }
        #endregion

        #region Mouse
        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            OnMouseDown(e, true);
        }

        private void ZoomControl_MouseDown(object sender, MouseButtonEventArgs e)
        {
            OnMouseDown(e, false);
        }

        //Pan, Zoombox
        private void OnMouseDown(MouseButtonEventArgs e, bool isPreview)
        {
            if (ModifierMode != ZoomViewModifierMode.None)
                return;

            switch (Keyboard.Modifiers)
            {
                case ModifierKeys.None:
                    if (!isPreview)
                        ModifierMode = ZoomViewModifierMode.Pan;
                    break;
                case ModifierKeys.Alt:
                    //ModifierMode = ZoomViewModifierMode.ZoomBox;
                    break;
                case ModifierKeys.Control:
                    break;
                case ModifierKeys.Shift:
                    //ModifierMode = ZoomViewModifierMode.Pan;
                    break;
                case ModifierKeys.Windows:
                    break;
                default:
                    return;
            }

            if (ModifierMode == ZoomViewModifierMode.None)
                return;

            _mouseDownPos = e.GetPosition(this);
            _startTranslate = new Vector(TranslateX, TranslateY);

            //Mouse.Capture(this);
            CaptureMouse();
            MouseMove += ZoomControl_PreviewMouseMove;

            UpdateTesting();
        }

        //Pan, ZoomBox
        private void ZoomControl_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            switch (ModifierMode)
            {
                case ZoomViewModifierMode.None:
                    return;
                case ZoomViewModifierMode.Pan:
                    //Help Performance Only Pan if 2pxs moved
                    //if ((((e.GetPosition(this).X - _mouseDownPos.X) % 2) != 0) ||
                    //    (((e.GetPosition(this).X - _mouseDownPos.X) % 2) != 0))
                    //    return;    

                    //var translate = _startTranslate + (e.GetPosition(this) - _mouseDownPos);
                    var translate = Add(_startTranslate, (Subtract(e.GetPosition(this), _mouseDownPos)));
                    TranslateX = translate.X;
                    TranslateY = translate.Y;
                    break;
                case ZoomViewModifierMode.ZoomIn:
                    break;
                case ZoomViewModifierMode.ZoomOut:
                    break;
                case ZoomViewModifierMode.ZoomBox:
                //var pos = e.GetPosition(this);
                //var x = Math.Min(_mouseDownPos.X, pos.X);
                //var y = Math.Min(_mouseDownPos.Y, pos.Y);
                //var sizeX = Math.Abs(_mouseDownPos.X - pos.X);
                //var sizeY = Math.Abs(_mouseDownPos.Y - pos.Y);
                //ZoomBox = new Rect(x, y, sizeX, sizeY);
                //break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            UpdateTesting();
        }

        //Case ZoomBox
        private void ZoomControl_MouseUp(object sender, MouseButtonEventArgs e)
        {
            switch (ModifierMode)
            {
                case ZoomViewModifierMode.None:
                    return;
                case ZoomViewModifierMode.Pan:
                    break;
                case ZoomViewModifierMode.ZoomIn:
                    break;
                case ZoomViewModifierMode.ZoomOut:
                    break;
                case ZoomViewModifierMode.ZoomBox:
                    //ZoomTo(ZoomBox);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            ModifierMode = ZoomViewModifierMode.None;
            //PreviewMouseMove -= ZoomControl_PreviewMouseMove;
            MouseMove -= ZoomControl_PreviewMouseMove;
            ReleaseMouseCapture();
        }

        //MouseWheel     
        private void ZoomControl_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            e.Handled = true;
            Point origoPosition = new Point(ActualWidth / 2, ActualHeight / 2); //484, 418.5
            Point mousePosition = e.GetPosition(this);

            DoZoom(
                Math.Max(.1 / MaxZoomDelta, Math.Min(MaxZoomDelta, e.Delta / 10000.0 * ZoomDeltaMultiplier + 1)),
                origoPosition,
                mousePosition,
                mousePosition);
        }


        //Disable SL Context Menu
        void ZoomControl_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
        }

        //Disable SL Context Menu
        void ZoomControl_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
        }
        #endregion

        #region PointOpps
        /// <summary>
        /// Point Math Operations
        /// </summary>
        /// <param name="point1"></param>
        /// <param name="point2"></param>
        /// <returns></returns>
        private Point Add(Point point1, Point point2)
        {
            Point result = new Point();
            result.X = point1.X + point2.X;
            result.Y = point1.Y + point2.Y;
            return result;
        }

        private Point Add(Vector point1, Point point2)
        {
            Point result = new Point();
            result.X = point1.X + point2.X;
            result.Y = point1.Y + point2.Y;
            return result;
        }

        private Point Subtract(Point point1, Point point2)
        {
            Point result = new Point();
            result.X = point1.X - point2.X;
            result.Y = point1.Y - point2.Y;
            return result;
        }

        private Point Subtract(Point point1, Vector point2)
        {
            Point result = new Point();
            result.X = point1.X - point2.X;
            result.Y = point1.Y - point2.Y;
            return result;
        }

        private Point Multiply(Point point1, Double point2)
        {
            Point result = new Point();
            result.X = point1.X * point2;
            result.Y = point1.Y * point2;
            return result;
        }

        private Point Divide(Point point1, Double point2)
        {
            Point result = new Point();
            result.X = point1.X / point2;
            result.Y = point1.Y / point2;
            return result;
        }
        #endregion
    }
}
