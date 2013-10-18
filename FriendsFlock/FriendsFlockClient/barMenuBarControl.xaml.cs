using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace FriendsFlockClient
{
	public partial class barMenuBarControl : UserControl
	{
		public barMenuBarControl()
		{
			// Required to initialize variables
			InitializeComponent();
		}

        private void LayoutRoot_MouseEnter(object sender, MouseEventArgs e)
        {
            VisualStateManager.GoToState(this, "Hoover", false);
        }

        private void LayoutRoot_MouseLeave(object sender, MouseEventArgs e)
        {
            VisualStateManager.GoToState(this, "Normal", false);
        }
	}
}