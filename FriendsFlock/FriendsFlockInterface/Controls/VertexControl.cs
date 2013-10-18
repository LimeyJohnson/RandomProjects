using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Diagnostics;
using System.Windows.Media.Imaging;
using System;

namespace FriendsFlockInterface.Controls
{
    [TemplateVisualState(Name = "Selected", GroupName = "VisualStateGroup")]
    [TemplateVisualState(Name = "ShorestPathTarget", GroupName = "VisualStateGroup")]
    [TemplateVisualState(Name = "ShorestPathMember", GroupName = "VisualStateGroup")]
    [TemplateVisualState(Name = "MutualFriend", GroupName = "VisualStateGroup")]
    [TemplateVisualState(Name = "Highlighted", GroupName = "VisualStateGroup")]
    [TemplateVisualState(Name = "Normal", GroupName = "VisualStateGroup")]
    public class VertexControl : Control
    {
        public FriendsVertex Friend { get; private set; }

        #region Friend
        //public const string FriendPropertyName = "Friend";
        //public FriendsVertex Friend
        //{
        //    get
        //    {
        //        return (FriendsVertex)GetValue(FriendProperty);
        //    }
        //    set
        //    {
        //        SetValue(FriendProperty, value);
        //    }
        //}
        //public static readonly DependencyProperty FriendProperty = DependencyProperty.Register(
        //    FriendPropertyName,
        //    typeof(FriendsVertex),
        //    typeof(VertexControl),
        //    new PropertyMetadata(null, FriendChanged));

        //private static void FriendChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        //{
            
        //}
        #endregion

        #region Current Selected Friend
        public const string CurrentSelectedFriendPropertyName = "CurrentSelectedFriend";
        public FriendsVertex CurrentSelectedFriend
        {
            get
            {
                return (FriendsVertex)GetValue(CurrentSelectedFriendProperty);
            }
            set
            {
                SetValue(CurrentSelectedFriendProperty, value);
            }
        }

        public static readonly DependencyProperty CurrentSelectedFriendProperty = DependencyProperty.Register(
            CurrentSelectedFriendPropertyName,
            typeof(FriendsVertex),
            typeof(VertexControl),
            new PropertyMetadata(null, CurrentSelectedFriendChanged));

        private static void CurrentSelectedFriendChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            VertexControl vc = (VertexControl)d;
            if (vc.CurrentSelectedFriend.Uid == vc.Friend.Uid)
                vc.IsSelected = true;
            else
                vc.IsSelected = false;

            vc.RefreshState();
        }
        #endregion

        #region Current Path Target
        public const string CurrentPathTargetPropertyName = "CurrentShorestPathTarget";
        public FriendsVertex CurrentPathTarget
        {
            get
            {
                return (FriendsVertex)GetValue(CurrentPathTargetProperty);
            }
            set
            {
                SetValue(CurrentPathTargetProperty, value);
            }
        }
        public static readonly DependencyProperty CurrentPathTargetProperty = DependencyProperty.Register(
            CurrentPathTargetPropertyName,
            typeof(FriendsVertex),
            typeof(VertexControl),
            new PropertyMetadata(null, CurrentPathTargetChanged));

        private static void CurrentPathTargetChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            VertexControl vc = (VertexControl)d;
            if (vc.CurrentPathTarget.Uid == vc.Friend.Uid)
                vc.IsShorestPathTarget = true;
            else
                vc.IsShorestPathTarget = false;

            vc.RefreshState();
        }
        #endregion

        #region PicSource
        public ImageSource PicSource
        {
            get { return (ImageSource)GetValue(PicSourceProperty); }
            set { SetValue(PicSourceProperty, value); }
        }
        public static readonly DependencyProperty PicSourceProperty =
            DependencyProperty.Register("PicSource", typeof(ImageSource), typeof(VertexControl), null);
        #endregion

        #region State
        /// <summary>
        /// The <see cref="IsSelected" /> dependency property's name.
        /// </summary>
        public const string IsSelectedPropertyName = "IsSelected";

        /// <summary>
        /// Gets or sets the value of the <see cref="IsSelected" />
        /// property. This is a dependency property.
        /// </summary>
        public bool IsSelected
        {
            get
            {
                return (bool)GetValue(IsSelectedProperty);
            }
            set
            {
                SetValue(IsSelectedProperty, value);
            }
        }

        /// <summary>
        /// Identifies the <see cref="IsSelected" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty IsSelectedProperty = DependencyProperty.Register(
            IsSelectedPropertyName,
            typeof(bool),
            typeof(VertexControl),
            new PropertyMetadata(false, RefreshState));

        /// <summary>
        /// The <see cref="IsShorestPathTarget" /> dependency property's name.
        /// </summary>
        public const string IsShorestPathTargetPropertyName = "IsShorestPathTarget";

        /// <summary>
        /// Gets or sets the value of the <see cref="IsShorestPathTarget" />
        /// property. This is a dependency property.
        /// </summary>
        public bool IsShorestPathTarget
        {
            get
            {
                return (bool)GetValue(IsShorestPathTargetProperty);
            }
            set
            {
                SetValue(IsShorestPathTargetProperty, value);
            }
        }

        /// <summary>
        /// Identifies the <see cref="IsShorestPathTarget" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty IsShorestPathTargetProperty = DependencyProperty.Register(
            IsShorestPathTargetPropertyName,
            typeof(bool),
            typeof(VertexControl),
            new PropertyMetadata(false, RefreshState));

        /// <summary>
        /// The <see cref="IsShorestPathMember" /> dependency property's name.
        /// </summary>
        public const string IsShorestPathMemberPropertyName = "IsShorestPathMember";

        /// <summary>
        /// Gets or sets the value of the <see cref="IsShorestPathMember" />
        /// property. This is a dependency property.
        /// </summary>
        public bool IsShorestPathMember
        {
            get
            {
                return (bool)GetValue(IsShorestPathMemberProperty);
            }
            set
            {
                SetValue(IsShorestPathMemberProperty, value);
            }
        }

        /// <summary>
        /// Identifies the <see cref="IsShorestPathMember" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty IsShorestPathMemberProperty = DependencyProperty.Register(
            IsShorestPathMemberPropertyName,
            typeof(bool),
            typeof(VertexControl),
            new PropertyMetadata(false, RefreshState));

        /// <summary>
        /// The <see cref="IsMutualFriend" /> dependency property's name.
        /// </summary>
        public const string IsMutualFriendPropertyName = "IsMutualFriend";

        /// <summary>
        /// Gets or sets the value of the <see cref="IsMutualFriend" />
        /// property. This is a dependency property.
        /// </summary>
        public bool IsMutualFriend
        {
            get
            {
                return (bool)GetValue(IsMutualFriendProperty);
            }
            set
            {
                SetValue(IsMutualFriendProperty, value);
            }
        }

        /// <summary>
        /// Identifies the <see cref="IsMutualFriend" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty IsMutualFriendProperty = DependencyProperty.Register(
            IsMutualFriendPropertyName,
            typeof(bool),
            typeof(VertexControl),
            new PropertyMetadata(false, RefreshState));

        /// <summary>
        /// The <see cref="IsHighlighted" /> dependency property's name.
        /// </summary>
        public const string IsHighlightedPropertyName = "IsHighlighted";

        /// <summary>
        /// Gets or sets the value of the <see cref="IsHighlighted" />
        /// property. This is a dependency property.
        /// </summary>
        public bool IsHighlighted
        {
            get
            {
                return (bool)GetValue(IsHighlightedProperty);
            }
            set
            {
                SetValue(IsHighlightedProperty, value);
            }
        }

        /// <summary>
        /// Identifies the <see cref="IsHighlighted" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty IsHighlightedProperty = DependencyProperty.Register(
            IsHighlightedPropertyName,
            typeof(bool),
            typeof(VertexControl),
            new PropertyMetadata(false, RefreshState));

        #endregion

        public VertexControl(FriendsVertex friend, AppModel appModel)
        {
            DefaultStyleKey = typeof(VertexControl);
            this.Friend = friend;

            SetFriendBindings(appModel);
            SetLayoutBindings();
            SetStateBindings();

            ////Mouse Over
            //this.MouseEnter += new MouseEventHandler(VertexControl_MouseEnter);
            //this.MouseLeave += new MouseEventHandler(VertexControl_MouseLeave);

            ////Select Vertex
            this.MouseLeftButtonDown += new MouseButtonEventHandler(VertexControl_MouseLeftButtonDown);
            this.MouseRightButtonDown += new MouseButtonEventHandler(VertexControl_MouseRightButtonDown);

        }

        #region Bindings
        private void SetFriendBindings(AppModel appModel)
        {
            //ToolTip
            this.SetValue(ToolTipService.ToolTipProperty, Friend.Info.Name);

            //Friend Image
            if (!string.IsNullOrEmpty(Friend.Info.Pic_Sqaure_Url))
                PicSource = new BitmapImage(new Uri(Friend.Info.Pic_Sqaure_Url, UriKind.RelativeOrAbsolute));

            Binding bindSelected = new Binding(appModel.CurrentSelectedFriendPropertyName);
            bindSelected.Source = appModel;
            bindSelected.Mode = BindingMode.TwoWay;
            this.SetBinding(CurrentSelectedFriendProperty, bindSelected);

            Binding bindTarget = new Binding(appModel.CurrentPathTargetPropertyName);
            bindTarget.Source = appModel;
            bindTarget.Mode = BindingMode.TwoWay;
            this.SetBinding(CurrentPathTargetProperty, bindTarget);
        }

        private void SetLayoutBindings()
        {
            //Bind Canvas X
            Binding bX = new Binding("CurrentX");
            bX.Mode = BindingMode.TwoWay;
            bX.Source = Friend.Layout;
            this.SetBinding(Canvas.LeftProperty, bX);

            //Bind Canvas Y
            Binding bY = new Binding("CurrentY");
            bY.Mode = BindingMode.TwoWay;
            bY.Source = Friend.Layout;
            this.SetBinding(Canvas.TopProperty, bY);

            //Set Default Layout
            this.Friend.Layout.GotoGridLayout();
        }

        private void SetStateBindings()
        {
            FriendsState state = Friend.State;

            Binding bindIsSelected = new Binding(state.IsSelectedPropertyName);
            bindIsSelected.Source = state;
            bindIsSelected.Mode = BindingMode.TwoWay;
            this.SetBinding(IsSelectedProperty, bindIsSelected);

            Binding bindIsShorestPathTarget = new Binding(state.IsShorestPathTargetPropertyName);
            bindIsShorestPathTarget.Source = state;
            bindIsShorestPathTarget.Mode = BindingMode.TwoWay;
            this.SetBinding(IsShorestPathTargetProperty, bindIsShorestPathTarget);

            Binding bindIsShorestPathMember = new Binding(state.IsShorestPathMemberPropertyName);
            bindIsShorestPathMember.Source = state;
            bindIsShorestPathMember.Mode = BindingMode.OneWay;
            this.SetBinding(IsShorestPathMemberProperty, bindIsShorestPathMember);

            Binding bindIsMutualFriend = new Binding(state.IsMutualFriendPropertyName);
            bindIsMutualFriend.Source = state;
            bindIsMutualFriend.Mode = BindingMode.OneWay;
            this.SetBinding(IsMutualFriendProperty, bindIsMutualFriend);

            Binding bindIsHighlighted = new Binding(state.IsHighlightedPropertyName);
            bindIsHighlighted.Source = state;
            bindIsHighlighted.Mode = BindingMode.OneWay;
            this.SetBinding(IsHighlightedProperty, bindIsHighlighted);
        }
        #endregion

        #region State
        public static void RefreshState(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            VertexControl vc = (VertexControl)d;
            vc.RefreshState();
        }

        public void RefreshState()
        {
            VertexControl vc = this;

            if (vc.IsSelected)
                VisualStateManager.GoToState(vc, "Selected", false);
            else if (vc.IsHighlighted)
                VisualStateManager.GoToState(vc, "Highlighted", false);
            else if (vc.IsMutualFriend)
                VisualStateManager.GoToState(vc, "MutualFriend", false);
            else if ((vc.IsShorestPathTarget) && (vc.Friend.Layout.IsFlockLayout))
                VisualStateManager.GoToState(vc, "ShorestPathTarget", false);
            else if ((vc.IsShorestPathMember) && (vc.Friend.Layout.IsFlockLayout))
                VisualStateManager.GoToState(vc, "ShorestPathMember", false);
            else
                VisualStateManager.GoToState(vc, "Normal", false);
        }
        #endregion

        #region Mouse
        void VertexControl_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            CurrentSelectedFriend = Friend;
        }

        void VertexControl_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            CurrentPathTarget = Friend;
            e.Handled = true;
        }

        void VertexControl_MouseLeave(object sender, MouseEventArgs e)
        {
        }

        void VertexControl_MouseEnter(object sender, MouseEventArgs e)
        {
        }
        #endregion
    }
}