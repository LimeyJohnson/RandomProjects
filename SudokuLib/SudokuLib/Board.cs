using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuLib
{
    public class Board
    {
        private Square[,] m_squares = new Square[9, 9];
        public bool Complete { get; protected set; }
        public Board()
        {
            EachGrid((x, y) =>
            {
                m_squares[x, y] = new Square(x, y);
                m_squares[x, y].ValueSetEvent += HandleSquareValueSet;
            });
            this.Complete = false;
        }
        public Square this[int x, int y]
        {
            get
            {
                return m_squares[x, y];
            }
        }

        #region GroupTesters
        
        public bool TestRowsForSingleAvailible()
        {
            return TestForSingleAvailible(this.EachRow);
        }
        public bool TestColumnForSingleAvailible()
        {
            return TestForSingleAvailible(this.EachColumn);
        }
        public bool TestGridForSingleAvailible()
        {
            return TestForSingleAvailible(this.EachGroup);
        }
        #endregion
        #region EventHandlers
        void HandleSquareValueSet(object sender, Square square)
        {
            bool complete = true;
            EachSquare(s =>
                {
                    if (s.Value == null) complete = false;
                });
            this.Complete = complete;

            Action<Square> action = new Action<Square>(s => { if (s != square) s.RemovePossiblity(square.Value.Value); });
            EachRow(square.Row, action);
            EachColumn(square.Column, action);
            EachGroup(square.Row, square.Column, action);
        }
        #endregion
        #region Each Methods
        public void EachSquare(Action<Square> action)
        {
            EachGrid((x, y) => action(m_squares[x, y]));
        }
        public void EachGrid(Action<int, int> action)
        {
            for (int x = 0; x < 9; x++)
            {
                for (int y = 0; y < 9; y++)
                {
                    action(x, y);
                }
            }
        }
        public void EachRow(int row, Action<Square> action)
        {
            for (int x = 0; x < 9; x++)
            {
                action(m_squares[row, x]);
            }
        }
        public void EachColumn(int column, Action<Square> action)
        {
            for (int x = 0; x < 9; x++)
            {
                action(m_squares[x, column]);
            }
        }
        public void EachGroup(int sectionNum, Action<Square> action)
        {
            this.EachGroup((sectionNum % 3) * 3, (sectionNum / 3)*3, action);
        }
        public void EachGroup(int row, int column, Action<Square> action)
        {
            int squareX = row / 3;
            int squareY = column / 3;
            int startX = squareX * 3;
            int startY = squareY * 3;
            int endY = startY + 3;
            int endX = startX + 3;
            for (; startX < endX; startX++)
            {
                int y = startY;
                for (; y < endY; y++)
                {
                    action(m_squares[startX, y]);
                }
            }
        }
        #endregion
        #region Private Helpers 
        private Dictionary<int, int> CreateAvailibityDictionary()
        {
            Dictionary<int, int> dict = new Dictionary<int,int>();
            dict[1] = 0;
            dict[2] = 0;
            dict[3] = 0;
            dict[4] = 0;
            dict[5] = 0;
            dict[6] = 0;
            dict[7] = 0;
            dict[8] = 0;
            dict[9] = 0;
            return dict;
             
        }
        private bool TestForSingleAvailible(Action<int, Action<Square>> action)
        {
            bool found = false;
            for (int x = 0; x < 9; x++)
            {
                
                Dictionary<int, int> nums = this.CreateAvailibityDictionary();
                action(x, s =>
                {
                    if (s.Row == 7 && s.Column == 2)
                    {
                        int xs = 0;
                    }
                    if (s.Value == null)
                    {
                        foreach (int possibility in s.possibiltiesLeft)
                        {
                            if (nums.ContainsKey(possibility)) nums[possibility]++;

                        }
                    }
                    else
                    {
                        nums.Remove(s.Value.Value);
                    }
                });
                if (nums.ContainsValue(1))
                {
                    foreach (int key in nums.Keys)
                    {
                        if (nums[key] == 1)
                        {
                            action(x, s =>
                            {
                                if (s.possibiltiesLeft.Contains(key)) s.Value = key;
                            });
                        }
                    }
                    found = true;
                }

            }
            return found;
        }
        #endregion
    }
}
