using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BUKI
{
    public class Manager :Employee
    {
        private string m_dealership = string.Empty;
        public Manager()
        {
            Position = Enums.Position.Manager;
        }
         public Manager(String dealership)
        {
            Position = Enums.Position.Manager;
            Dealership = dealership;
        }
        public string Dealership
        {
            get { return m_dealership; }
            set { m_dealership = value; }
        }
        public void AddSalesperson()
        {
        
        }

        public void RemoveSalesperson()
        {

        }

        public void MakeVehicleRequest()
        {

        }

        public void AllowVehicleRequest()
        {

        }
    }
}
