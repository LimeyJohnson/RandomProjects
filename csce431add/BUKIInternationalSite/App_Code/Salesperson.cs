using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BUKI.DataObjects;

namespace BUKI
{
    public class Salesperson: Employee
    {
        private string m_dealership = string.Empty;
        
        private double m_totalSales = 0.0;
        private double m_salesGoal = 0.0;
        public Salesperson()
        {
            Position = Enums.Position.Sales_Person;
        }
        public Salesperson(String dealership)
        {
            Position = Enums.Position.Sales_Person;
            Dealership = dealership;
        }

        public string Dealership 
        {
            get { return m_dealership; }
            set { m_dealership = value; }
        }
        public double TotalSales 
        {
            get { return m_totalSales; }
            set { m_totalSales = value; }
        }
        public double SalesGoal 
        {
            get { return m_salesGoal; }
            set { m_salesGoal = value; }
        }

        public void CreateSale()
        {

        }

        public void ApproveSaleRequest()
        {
        }
    }
}
