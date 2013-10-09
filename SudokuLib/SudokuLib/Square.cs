using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuLib
{
    public class Square
    {
        private bool[] m_possiblities = new bool[9];
        private int? m_value;
        public int Row { get; set; }
        public int Column { get; set; }
        public event EventHandler<Square> ValueSetEvent;
        public Square(int column, int row)
        {
            this.Row = row;
            this.Column = column;
            for (int x = 0; x < 9; x++) m_possiblities[x] = true;
        }

        public int? Value
        {
            set
            {

                this.m_value = value;
                for (int x = 0; x < 9; x++) m_possiblities[x] = x == (value - 1) ? true : false;
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
            m_possiblities[num] = false;
            if (possibiltiesLeft.Length == 1)
            {
                this.Value = Array.FindIndex(m_possiblities, b => b == true) + 1;
            }

        }
        public bool[] possibiltiesLeft
        {
            get
            {
                return Array.FindAll(m_possiblities, b =>  b );
            }
        }
        

    }
}
