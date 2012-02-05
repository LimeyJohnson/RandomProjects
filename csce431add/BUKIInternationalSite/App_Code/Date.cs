using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BUKI.DataObjects
{
    public class Date
    {
        private int m_day = 0;
        private int m_month = 0;
        private int m_year = 0;

        public int Day 
        {
            get { return m_day; }
            set { m_day = value; }
        }
        public int Month 
        {
            get { return m_month; }
            set { m_month = value; }
        }
        public int Year 
        {
            get { return m_year; }
            set { m_year = value; }
        }

        public override string ToString()
        {
            return Convert.ToString(Day + "//" + Month + "//" + Year);
        }
    }
}
