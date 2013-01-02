using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connect4
{
    public class GameBoard : ICloneable, IEquatable<GameBoard>
    {
        int Length, Height, ConnectLength;
        bool?[,] Board;
        public GameBoard(int length, int height, int connectLength)
            : this(new bool?[length, height], length, height, connectLength)
        {

        }
        public GameBoard(bool?[,] board, int length, int height, int connectLength)
        {
            this.Board = board;
            this.Length = length;
            this.Height = height;
            this.ConnectLength = connectLength;
        }
        public bool? this[int length, int height]
        {
            get
            {
                return Board[length, height];
            }
            set
            {
                this.Board[length, height] = value;
            }
        }
        public bool HasWinner
        {
            get
            {
                return this.HasVerticalWinner || this.HasHorizontalWinner || this.HasDiagonalWinner;
            }
        }
        private bool HasVerticalWinner
        {
            get
            {
                for (int x = 0; x < Length; x++)
                {
                    for (int y = 0; y <= (Height - ConnectLength); y++)
                    {
                        bool? matchType;
                        bool keepLooking = true;
                        if ((matchType = Board[x, y]) != null)
                        {
                            for (int i = 0; i < ConnectLength && keepLooking; i++)
                            {
                                if (Board[x, y + i] != matchType) keepLooking = false;
                            }
                            if (keepLooking) return true;//We have found a match
                        }
                    }
                }
                return false;
            }
        }
        public bool HasHorizontalWinner
        {
            get
            {
                for (int y = 0; y < Height; y++)
                {
                    for (int x = 0; x <= (Length - ConnectLength); x++)
                    {
                        bool? matchType;
                        bool keepLooking = true;
                        if ((matchType = Board[x, y]) != null)
                        {
                            for (int i = 0; i < ConnectLength && keepLooking; i++)
                            {
                                if (Board[x + i, y] != matchType) keepLooking = false;
                            }
                            if (keepLooking) return true;
                        }
                    }
                }
                return false;
            }
        }
        public bool HasDiagonalWinner
        {
            get
            {
                //Starting Lower left Winner
                for (int x = 0; x <= (Length - ConnectLength); x++)
                {
                    for (int y = 0; y <= (Height - ConnectLength); y++)
                    {
                        bool? matchType;
                        bool keepLooking = true;
                        if ((matchType = Board[x, y]) != null)
                        {
                            for (int i = 0; i < ConnectLength && keepLooking; i++)
                            {
                                if (Board[x + i, y + i] != matchType) keepLooking = false;
                            }
                            if (keepLooking) return true;
                        }
                    }
                }
                //Starting Lower Right Winner
                for (int x = Length - 1; x >= (ConnectLength - 1); x--)
                {
                    for (int y = 0; y <= (Height - ConnectLength); y++)
                    {
                        bool? matchType;
                        bool keepLooking = true;
                        if ((matchType = Board[x, y]) != null)
                        {
                            for (int i = 0; i < ConnectLength && keepLooking; i++)
                            {
                                if (Board[x - i, y + i] != matchType) keepLooking = false;
                            }
                            if (keepLooking) return true;
                        }
                    }
                }
                return false;
            }
        }
        public bool IsFull
        {
            get
            {
                for (int x = 0; x < Length; x++)
                {
                    for (int y = 0; y < Height; y++)
                    {
                        if (Board[x, y] == null) return false;
                    }

                }
                return true;
            }
        }
        public int? getNextAvailibleRow(int column)
        {
            for (int y = 0; y < Height; y++)
            {
                if (Board[column, y] == null) return y;
            }
            return null;
        }
        public void WriteOutGame(string fileName)
        {
            using (StreamWriter stream = new StreamWriter(Path.Combine(@"C:\Development\RandomProjects\Connect4\Results", fileName + ".txt")))
            {

                for (int y = 0; y < Height; y++)
                {
                    for (int x = 0; x < Length; x++)
                    {
                        if (Board[x, y] == null) { stream.Write('n'); }
                        else
                        {
                            stream.Write(Board[x, y] == true ? 'r' : 'y');
                        }
                    }
                    stream.WriteLine(" ");
                }
            }
        }
        public object Clone()
        {
            return new GameBoard((bool?[,])this.Board.Clone(), this.Length, this.Height, this.ConnectLength);
        }
        private int? m_HashCode;
        public int? HashCode
        {
            get
            {
                if (m_HashCode == null)
                {
                    m_HashCode = 0;
                    for (int x = 0; x < Length; x++)
                    {
                        for (int y = 0; y < Height; y++)
                        {
                            m_HashCode += (Height + Length) * (Board[x, y] == null ? 1 : 3);
                        }
                    }
                }
                return m_HashCode;
            }
        }
        public bool Equals(GameBoard other)
        {
            for (int x = 0; x < Length; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    if (other.Board[x, y] != this.Board[x, y]) return false;
                }
            }
            return true;
        }
        public static GameBoard InitialBoard(int length, int height, int connectLength)
        {
            bool current = false;
            GameBoard b = new GameBoard(length, height, connectLength);
            Program.RunAll((x, y) =>
            {
                b[x, y] = current;
                current = !current;
                return false;
            });
            return b;
        }
    }
}
