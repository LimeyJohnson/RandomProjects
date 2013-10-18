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
using FriendsService;
using System.Diagnostics;
using QuickGraph;
using FriendsFlockInterface;
using FriendsFlockInterface.Controls;
using GraphService;
using GalaSoft.MvvmLight.Messaging;

namespace FriendsFlockClient
{
    public partial class MainPage : UserControl
    {
        public MainPage()
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(MainPage_Loaded);
        }

        void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine(string.Format("MainPage_Loaded"));

            //Send Canvas Pointer
            Messenger.Default.Send<Canvas>(ZoomPanCanvas);

            //Send Image Pointer
            Messenger.Default.Send<Image>(EdgeMapImage);

            //Send Loading Pointer
            Messenger.Default.Send<Border>(brdLoadingProgress, "brdLoadingProgress");

            //Send Title Pointer
            Messenger.Default.Send<Border>(brdTitle, "brdTitle");
            
            //Start the Machine
            btnStartTheMachine.Command.Execute(null);
        }

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            ZoomPanControl.Mode = ZoomControlModes.Reset;
        }

        private void brdSidebarPicture_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            Border brd = (Border)sender;
            if (e.NewSize.Height < 10)
                brd.Opacity = 0d;
            else
                brd.Opacity = 100d;
        }

        private void brdSidebarPicture_MouseEnter(object sender, MouseEventArgs e)
        {
            brdSidebarPicture.Background = new SolidColorBrush((Color)Resources["colorAccent"]);
        }

        private void brdSidebarPicture_MouseLeave(object sender, MouseEventArgs e)
        {
            brdSidebarPicture.Background = new SolidColorBrush((Color)Resources["colorBase0"]);
        }
    }
}