using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BUKI
{
    public class MessageBoard
    {
        #region Members
        private List<Message> m_messages;
        private string m_name = string.Empty;
        private int m_id = 0;
        #endregion Members

        #region Properties
        public List<Message> Messages
        {
            get { return m_messages; }
            set { m_messages = value; }
        }

        public string Name
        {
            get { return m_name; }
            set { m_name = value; }
        }

        public int ID
        {
            get { return m_id; }
            set { m_id = value; }
        }
        #endregion Properties

        #region Public Methods
        public void AddMessage(Message msg)
        {
            m_messages.Add(msg);
        }

        public void RemoveMessage(Message msg)
        {
            m_messages.Remove(msg);
        }
        #endregion Public Methods
    }
}
