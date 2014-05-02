using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;
using System.Windows.Forms;

namespace ViewStateCracker
{
    
    class Program
    {
        public static WebBrowser Browser;
        [STAThread]
        static void Main(string[] args)
        {
            Browser = new WebBrowser();
            Browser.Navigate("https://ctfteasers.cloudapp.net/Index.html");
            Browser.DocumentCompleted += browser_DocumentCompleted;
           

        }

        static void browser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            string s = Browser.DocumentText;
            Console.Write(s);
        }

    }
}
