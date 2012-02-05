using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Google.API.Search;
using System.IO;


namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public WebBrowser wb1Ref;
        public string webHTML = null;
        public string webURL = null;
        public string webTitle = null;
        public IList<INewsResult> results;
        public int count;
        public Form1()
        {
            InitializeComponent();
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            wb1Ref = webBrowser1;
        }


        //--------------------
        //UI & Browswer
        //--------------------
        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            webURL = wb1Ref.Url.ToString();
            textBoxURL.Text = webURL;

            webHTML = wb1Ref.DocumentText.ToString();
            
            webTitle = wb1Ref.DocumentTitle.ToString();
            this.Text = "News Crawler: " + webTitle;
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            wb1Ref.GoBack();
        }

        private void btnForward_Click(object sender, EventArgs e)
        {
            wb1Ref.GoForward();
        }

        private void textBoxURL_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnGo_Click(object sender, EventArgs e)
        {
            wb1Ref.Navigate(textBoxURL.Text);
        }

        private void btnAbout_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Copyright 2009 by John Morrison and Andrew Johnson");
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
           // if (MessageBox.Show("Confirm Search for \"" + txtSearchString.Text + "\"", "Search String", MessageBoxButtons.OKCancel) == DialogResult.OK)
         //   {
                GoogleInterface GI = new GoogleInterface();
                results = GI.getTopicQueryResults(txtSearchString.Text);
                count = results.Count;
                ShowFirst();

           // }
            
         //   else
          //  {
                
          //  }
            
        }

        //--------------------
        //Commit & Back
        //--------------------
        private void btnRecordBack_Click(object sender, EventArgs e)
        {
            DataAccess DA = new AccessDataAccess();
            DA.InsertArticle("Test", "Test", "Test", 21, "Test", "Test", "Test", "07/20/1986", "Test", "Test", "Test");
            //TextWriter tw = new StreamWriter("NoHTMLText.txt");
            //tw.WriteLine(removeHTML(wb1Ref.DocumentText.ToString()));
            //tw.Close();
                
        }

        private void btnRecordCommit_Click(object sender, EventArgs e)
        {
            if (results.Count > 0) results.Remove(results.ElementAt(0));

            ShowFirst();
        }

        private void btnCancelCommit_Click(object sender, EventArgs e)
        {
            if(results.Count>0)results.Remove(results.ElementAt(0));

            ShowFirst();
        }
        void MoveToNextResult()
        {
            results.Remove(results.ElementAt(0));
            ShowFirst();
        }
        void ShowFirst()
        {
            if (results.Count > 0) wb1Ref.Navigate(results.ElementAt(0).ClusterUrl);
            else wb1Ref.Navigate("about:blank");
            btnRecordCommit.Text = "Commit Record\n"+((count - results.Count)+1)+" of "+count;
            lblInfo.Text = results.Count + " Articles Left\nContent: " + results.ElementAt(0).Content + "\nPublish Date: " + results.ElementAt(0).PublishedDate + "\nAuthor: " + results.ElementAt(0).Author;
                
        }
        String removeHTML(String input)
        {

            Boolean print = true;
            String output = "";
            for (int x = 0; x < input.Length; x++)
            {
                if (input[x] == '<') print = false;
                if (input[x] == '>') print = true;

                if (print && input[x] != '>') output += (input[x]);
            }
            return output;
        }

        private void btnDeleteToday_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you Sure you want to Delete all entries from today","Search String", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                DataAccess DA = new AccessDataAccess();
                DA.DeleteEntriesFromToday();
                lblInfo.Text = "Today's Entries have been erased";
            }
        }
    }
}





//        private void button1_Click(object sender, EventArgs e)
//        {
//            //listBox1.Items.Clear();
//            if (unClusteterRadioButton.Checked)
//            {
//                GoogleInterface GI = new GoogleInterface();
//                if (textBox1.Text != null)
//                {
//                    IList<INewsResult> results = GI.getNewsQueryResults(textBox1.Text);
//                    foreach (INewsResultItem result in results)
//                    {
//                        listBox1.Items.Add(result.Title + " - " + result.Publisher);

//                    }
//                }
//            }
//            if (clusteredRadioButton.Checked)
//            {
//                GoogleInterface GI = new GoogleInterface();
//                if (textBox1.Text != null)
//                {
//                    IList<INewsResult> results = GI.getNewsQueryResults(textBox1.Text);
//                    foreach (INewsResult result in results)
//                    {
//                        listBox1.Items.Add(result.Title + " - " + result.ClusterUrl + result.Publisher.ToString());
//                    }
//                }
//            }

            
//        }

//        private void Form1_Load(object sender, EventArgs e)
//        {

//        }

//        private void button2_Click(object sender, EventArgs e)
//        {
//            string searchString = "Obama";

//            GoogleInterface GI = new GoogleInterface();

//            IList<INewsResult> results = GI.getTopicQueryResults(searchString);

//            string nl = "\n";
//            string t = "\t";
//            int id = 0;

//            for (int i = 0; i < results.Count; i++)
//            {
//                id = i + 1;
//                textBox3.AppendText("\n\n#" + id + ": -----New Story-----" + nl);
//                textBox3.AppendText("Title: " + results.ElementAt(i).Title + nl);
//                textBox3.AppendText("Publisher: " + results.ElementAt(i).Publisher + nl);
//                textBox3.AppendText("Date: " + results.ElementAt(i).PublishedDate.ToString() + nl);
//                textBox3.AppendText("Snippet: " + results.ElementAt(i).Content + nl);
//                textBox3.AppendText("URL: " + results.ElementAt(i).Url + nl);
//                textBox3.AppendText("Cluster: " + results.ElementAt(i).ClusterUrl + nl);

//                //textBox3.AppendText(results.ElementAt(i).Author);
//                //textBox3.AppendText(results.ElementAt(i).IsQuote.ToString() + nl);
//                //textBox3.AppendText(results.ElementAt(i).Language + nl);
//                //textBox3.AppendText(results.ElementAt(i).Location + nl);
                
//                if (results.ElementAt(i).RelatedStories != null)
//                {
//                    foreach (INewsResultItem newsItem in results.ElementAt(i).RelatedStories)
//                    {
//                        textBox3.AppendText(nl + t + "-----New Article-----" + nl);
//                        textBox3.AppendText(t + newsItem.Title + nl);
//                        textBox3.AppendText(t + newsItem.Publisher + nl);
//                        textBox3.AppendText(t + newsItem.PublishedDate + nl);
//                        textBox3.AppendText(t + newsItem.Url + nl);

//                        //textBox3.AppendText(t + newsItem.Language + nl);
//                        //textBox3.AppendText(t + newsItem.Location + nl);
//                    }
//                }
//            }
//        }

//        private void button2_Click_1(object sender, EventArgs e)
//        {

//        }
//    }
//}
