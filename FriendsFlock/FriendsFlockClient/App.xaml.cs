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
using System.Diagnostics;
using FriendsFlockClient.ViewModel;
using FriendsFlockInterface;

namespace FriendsFlockClient
{
    public partial class App : Application
    {

        public App()
        {
            this.Startup += this.Application_Startup;
            this.Exit += this.Application_Exit;
            this.UnhandledException += this.Application_UnhandledException;

            InitializeComponent();
        }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            Debug.WriteLine("Application_Startup");

            //Make Sure overrideToken isn't set to true during production
            if (App.Current.Host.Source.ToString().Contains("friendsflock.com"))
                Dashboard.overrideToken = false;

            if ((e.InitParams.ContainsKey("fbToken")) && (e.InitParams.ContainsKey("userUid")))
            {
                UserInfo.fbToken = e.InitParams["fbToken"];
                UserInfo.userUid = long.Parse(e.InitParams["userUid"]);
                Dashboard.overrideToken = false;
            }
            else
            {
                Debug.WriteLine("Test Mode");

                UserInfo.fbToken = "Test Mode";
                UserInfo.userUid = 0L;
            }

            GalaSoft.MvvmLight.Threading.DispatcherHelper.Initialize();
            this.RootVisual = new MainPage();
        }

        private void Application_Exit(object sender, EventArgs e)
        {

        }

        private void Application_UnhandledException(object sender, ApplicationUnhandledExceptionEventArgs e)
        {
            // If the app is running outside of the debugger then report the exception using
            // the browser's exception mechanism. On IE this will display it a yellow alert 
            // icon in the status bar and Firefox will display a script error.
            if (!System.Diagnostics.Debugger.IsAttached)
            {

                // NOTE: This will allow the application to continue running after an exception has been thrown
                // but not handled. 
                // For production applications this error handling should be replaced with something that will 
                // report the error to the website and stop the application.
                e.Handled = true;
                Deployment.Current.Dispatcher.BeginInvoke(delegate { ReportErrorToDOM(e); });
            }

            MessageBox.Show(string.Format("Application_UnhandledException: {0}: ", e.ExceptionObject.Message)); //TODO Remove
        }

        private void ReportErrorToDOM(ApplicationUnhandledExceptionEventArgs e)
        {
            try
            {
                string errorMsg = e.ExceptionObject.Message + e.ExceptionObject.StackTrace;
                errorMsg = errorMsg.Replace('"', '\'').Replace("\r\n", @"\n");

                System.Windows.Browser.HtmlPage.Window.Eval("throw new Error(\"Unhandled Error in Silverlight Application " + errorMsg + "\");");
            }
            catch (Exception)
            {
            }
        }
    }
}