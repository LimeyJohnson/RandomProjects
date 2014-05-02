using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EmailSniffer
{
    public partial class Form1 : Form
    {
        public List<string> Urls;
        public Form1()
        {
            Urls = new List<string>();
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Urls.Add("https://ctfteasers.cloudapp.net/Index.html");
            Browser.DocumentCompleted += Browser_DocumentCompleted;
            PickNext();

        }

        void Browser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            foreach (HtmlElement aTag in Browser.Document.GetElementsByTagName("a"))
            {
                if (aTag.GetAttribute("href").Contains('@'))
                {
                    Console.Write("EMAIL: " + aTag.GetAttribute("href"));
                }
                else
                {
                    Urls.Add(aTag.GetAttribute("href"));
                }
            }
            PickNext();
        }
        public void PickNext()
        {

            if (Urls.Count > 0)
            {
                Browser.Navigate(Urls[0]);
                this.Urls.RemoveAt(0);
            }
        }
    }
}
