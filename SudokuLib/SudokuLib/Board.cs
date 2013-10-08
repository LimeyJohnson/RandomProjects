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
            EachGrid((x, y) => { 
                m_squares[x, y] = new Square(x, y);
                m_squares[x, y].ValueSetEvent += HandleSquareValueSet;
            });
            this.Complete = false;
        }

        
        #region EventHandlers
        void HandleSquareValueSet(object sender, Square square)
        {
            bool complete = true;
            EachSquare(s =>
                {
                    if (s.Value == null) complete = false;
                });
            this.Complete = complete;

            Action<Square> action = new Action<Square>(s => s.RemovePossiblity(square.Value.Value));
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
    }
}
