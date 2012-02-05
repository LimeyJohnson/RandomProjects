using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BUKI.DataObjects;

namespace BUKI
{
    public class Sale
    {
        private int m_id = 0;
        private Enums.Status m_status;
        private Vehicle m_vehicle;
        private Salesperson m_salesperson;
        private Customer m_customer;
        private double m_price = 0.0;
        private string m_otherTerms = string.Empty;
        private string m_notes = string.Empty;
        private Manager m_manager;
        private Date m_date;

        public int ID 
        {
            get { return m_id; }
            set { m_id = value; }
        }

        public Enums.Status Status
        {
            get { return m_status; }
            set { m_status = value; }
        }

        public Vehicle Vehicle
        {
            get { return m_vehicle; }
            set { m_vehicle = value; }
        }

        public Salesperson Salesperson
        {
            get { return m_salesperson; }
            set { m_salesperson = value; }
        }

        public Customer Customer 
        {
            get { return m_customer; }
            set { m_customer = value; }
        }

        public double Price 
        {
            get { return m_price; }
            set { m_price = value; }
        }

        public string OtherTerms 
        {
            get { return m_otherTerms; }
            set { m_otherTerms = value; }
        }

        public string Notes 
        {
            get { return m_notes; }
            set { m_notes = value; }
        }

        public Manager Manager 
        {
            get { return m_manager; }
            set { m_manager = value; }
        }

        public Date Date 
        {
            get { return m_date; }
            set { m_date = value; }
        }

    }
}
