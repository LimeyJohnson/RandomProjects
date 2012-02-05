using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BUKI
{
    public class Message
    {
        private string m_sender = string.Empty;
        private string m_receiver = string.Empty;
        private string m_text = string.Empty;
        private int m_viewCount = 0;
        private int m_id = 0;
        private bool m_modified = false;
        private bool m_created = false;
        private string m_conceptionDate;
        private DateTime m_sent_date = DateTime.Now;

        public string Sender
        {
            get { return m_sender; }
            set { m_sender = value; }
        }

        public string Receiver
        {
            get { return m_receiver; }
            set { m_receiver = value; }
        }

        public string Text
        {
            get 
			{
                if (m_text == string.Empty) return "(Blank)";
                else return m_text;
			}
            set { m_text = value; }
        }

             public string ShortText
        {
            get
            {
                if (Text.Length > 50) return Text.Substring(0, 50) + "...";
                else return Text;
            }
        }
		
		public int ViewCount
        {
            get { return m_viewCount; }
            set { m_viewCount = value; }
        }

        public int ID
        {
            get { return m_id; }
            set { m_id = value; }
        }

        public bool Modified
        {
            get { return m_modified; }
            set { m_modified = value; }
        }

        public bool Created
        {
            get { return m_created; }
            set { m_created = value; }
        }

		 public string Read
        {
            get 
            {
                if (m_viewCount > 0) return "read";
                else return "unread";
            }
        }
		
        public string Conception
        {
            get { return m_conceptionDate; }
            set { m_conceptionDate = value; }
        }

        public string SentDate
        {
            get { return m_sent_date.ToString("d"); }
            set { m_sent_date = DateTime.Parse(value); }
        }
    }
}
