using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BUKI.DataObjects;

namespace BUKI
{
    public class Employee
    {
        private string m_firstName = string.Empty;
        private string m_lastName = string.Empty;
        private string m_phone= string.Empty;
        private string m_city = string.Empty;
        private string m_state = string.Empty;
        private string m_zip = string.Empty;
        private string m_address = string.Empty;
        private Enums.Position m_position;
        private DateTime m_employedDate;
        private DateTime m_terminatedDate;
        private int m_id = 0;
        private int m_dealership = 0;
        private string m_performanceDescription = string.Empty;
        private string m_username = string.Empty;
        private string m_password = string.Empty;
        private string m_email = string.Empty;
        private List<Message> m_rec_messages = new List<Message>();
        private List<Message> m_sent_messages = new List<Message>();
      

        public List<Message> SentMessages
        {
            get { return m_sent_messages; }
        }
        public List<Message> RecMessages
        {
            get { return m_rec_messages; }
        }
        public void AddSentMessage(Message msg)
        {
            m_sent_messages.Add(msg);
        }
        public void AddRecMessage(Message msg)
        {
            m_rec_messages.Add(msg);
        }
        public int UnreadMessageCount
        {
            get
            {
                int count = 0;
                foreach (Message msg in m_rec_messages)
                {
                    if (msg.ViewCount == 0) count++;
                }
                return count;
            }
        }
        public string FirstName 
        {
            get { return m_firstName; }
            set { m_firstName = value; }
        }
        public string LastName 
        {
            get { return m_lastName; }
            set { m_lastName = value; }
        }
        public string City 
        {
            get { return m_city; }
            set { m_city = value; }
        }
        public string State
        {
            get { return m_state; }
            set { m_state = value; }
        }
        public string Zip
        {
            get { return m_zip; }
            set { m_zip = value; }
        }
        public string Address
        {
            get { return m_address; }
            set { m_address = value; }
        }
        public Enums.Position Position 
        {
            get { return m_position; }
            set { m_position = value; }
        }
        public DateTime EmployedDate 
        {
            get { return m_employedDate; }
            set { m_employedDate = value; }
        }
        public DateTime TerminatedDate 
        {
            get { return m_terminatedDate; }
            set { m_terminatedDate = value; }
        }
        public int ID 
        {
            get { return m_id; }
            set { m_id = value; }
        }
      
        public string PerformanceDescription 
        {
            get { return m_performanceDescription; }
            set { m_performanceDescription = value; }
        }
        public string Phone
        {
            get { return m_phone; }
            set { m_phone = value; }
        }
        public int Dealership
        {
            get { return m_dealership; }
            set { m_dealership = value; }
        }
        public string Username 
        {
            get { return m_username; }
            set { m_username = value; }
        }
        public string Password 
        {
            get { return m_password; }
            set { m_password = value; }
        }
        public string Email 
        {
            get { return m_email; }
            set { m_email = value; }
        }
     
        public Message getMessage(int id)
        {
            foreach(Message msg in RecMessages)
            {
                if (msg.ID == id)
                {
                    msg.ViewCount++;
                    DataAccess.IncrementViewCount(msg.ID);
                    return msg;
                }
            }
            foreach (Message msg in SentMessages)
            {

                if (msg.ID == id)  return msg;
                
            }
            return null;
        }
        public void SendMessage(int reciever,Message msg)
        {
            DataAccess.AddMessage(ID, reciever, msg.Text);
        }
        public void DeleteMessage(int msgID)
    {
        RecMessages.Remove(getMessage(msgID));
        DataAccess.DeleteMessage(msgID);
    }

     
    }
}
