using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace FriendsFlockInterface.Controls
{
    public enum MenuLayoutEnum { Left, Center, Right }

    public class MenuControl : Control
    {
        public static DependencyProperty HeaderContentProperty = DependencyProperty.Register("HeaderContent", typeof(object), typeof(MenuControl), null);
        public static DependencyProperty PanelContentProperty = DependencyProperty.Register("PanelContent", typeof(object), typeof(MenuControl), null);
        public static DependencyProperty IsOpenProperty = DependencyProperty.Register("IsOpen", typeof(bool), typeof(MenuControl), new PropertyMetadata(false));
        public object HeaderContent
        {
            get
            {
                return GetValue(HeaderContentProperty);
            }
            set
            {
                SetValue(HeaderContentProperty, value);
            }
        }
        public object PanelContent
        {
            get
            {
                return GetValue(PanelContentProperty);
            }
            set
            {
                SetValue(PanelContentProperty, value);
            }
        }
        public bool IsOpen
        {
            get
            {
                return (bool)GetValue(IsOpenProperty);
            }
            set
            {
                SetValue(IsOpenProperty, value);
            }
        }

        public MenuControl()
        {
            DefaultStyleKey = typeof(MenuControl);
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            this.MouseEnter += new MouseEventHandler(MenuControl_MouseEnter);
            this.MouseLeave += new MouseEventHandler(MenuControl_MouseLeave);
            //this.MouseLeftButtonUp += new MouseButtonEventHandler(MenuControl_MouseLeftButtonUp);
        }

        void MenuControl_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            UIElement pc = (UIElement)PanelContent;
            pc.Visibility = System.Windows.Visibility.Collapsed;
        }

        void MenuControl_MouseLeave(object sender, MouseEventArgs e)
        {
            UIElement pc = (UIElement)PanelContent;
            pc.Visibility = System.Windows.Visibility.Collapsed;
        }

        void MenuControl_MouseEnter(object sender, MouseEventArgs e)
        {
            UIElement pc = (UIElement)PanelContent;
            pc.Visibility = System.Windows.Visibility.Visible;
        }
    }
}
