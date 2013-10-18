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

namespace FriendsFlockInterface
{
    public partial class NotificationDialog : ChildWindow
    {
        public string Message { get; set; }

        public NotificationDialog()
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(NotificationDialog_Loaded);
        }

        void NotificationDialog_Loaded(object sender, RoutedEventArgs e)
        {
            tbNotification.Text = Message;
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}

