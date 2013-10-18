using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace FriendsFlockClient
{
    public partial class MenuItem : UserControl
    {
        public MenuItem()
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(MenuItem_Loaded);
            this.MouseEnter += new MouseEventHandler(MenuItem_MouseEnter);
            this.MouseLeave += new MouseEventHandler(MenuItem_MouseLeave);
        }

        void MenuItem_Loaded(object sender, RoutedEventArgs e)
        {
            VisualStateManager.GoToState(this, "Normal", true);
        }

        void MenuItem_MouseLeave(object sender, MouseEventArgs e)
        {
            VisualStateManager.GoToState(this, "Normal", true);
        }

        void MenuItem_MouseEnter(object sender, MouseEventArgs e)
        {
            VisualStateManager.GoToState(this, "MouseOver", true);
        }
    }
}
