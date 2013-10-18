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
    public partial class YesNoDialog : ChildWindow
    {
        public string Message { get; set; }
        public string Yes = "Ok";
        public string No = "Cancel";

        public YesNoDialog()
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(YesNoDialog_Loaded);
        }

        void YesNoDialog_Loaded(object sender, RoutedEventArgs e)
        {
            tbNotification.Text = Message;
            OKButton.Content = Yes;
            CancelButton.Content = No;
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

