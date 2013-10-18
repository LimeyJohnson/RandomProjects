using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuLib
{
    public class Square
    {
        private List<int> m_possiblities;
        private int? m_value;
        public int Row { get; set; }
        public int Column { get; set; }
        public event EventHandler<Square> ValueSetEvent;
        public Square(int row, int column)
        {
            this.Row = row;
            this.Column = column;
            this.m_possiblities = CreateAvailibilityInt();
        }

        public int? Value
        {
            set
            {

                this.m_value = value;
                this.m_possiblities.Clear();
                if (ValueSetEvent != null)
                {
                    ValueSetEvent(this, this);
                }

            }
            get
            {
                return m_value;
            }
        }
        public void RemovePossiblity(int num)
        {
            if (Row == 1 && Column == 2)
            {
                int b = 0;
                b += 1;
            }
            if (this.Value == null && this.m_possiblities.Contains(num))
            {
                this.m_possiblities.Remove(num);
                if (possibiltiesLeft.Count == 1)
                {
                    this.Value = this.m_possiblities[0];
                }
            }
        }
        public List<int> possibiltiesLeft
        {
            get
            {
                return this.m_possiblities;
            }
        }
        public override string ToString()
        {
            if(this.Value != null) return Value.ToString();
            return string.Join<int>(",", this.possibiltiesLeft);
        }
        #region Private Helpers
        public static List<int> CreateAvailibilityInt()
        {
            return new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        }

        #endregion


    }
}
