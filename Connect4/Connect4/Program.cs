using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connect4
{
    class Program
    {
        static int LENGTH = 7, HEIGHT = 6, CONNECTLEGTH = 4;
        static int IterationCount = 0, SolutionCount = 0;
     
        static List<bool?[,]> Solutions = new List<bool?[,]>();
        static void Main(string[] args)
        {
            bool?[,] board = new bool?[LENGTH, HEIGHT];
            PlayRound(board, true);
            Console.WriteLine("Solution Count = " + Solutions.Count);
        }

        static void PlayRound(bool?[,] board, bool turn,int deep = 0)
        {
            IterationCount++;
            Console.WriteLine("Iteration: " + IterationCount + " Solutin Count: " + Solutions.Count+ " deep : "+deep);
            for (int x = 0; x < LENGTH; x++)
            {
                if (board[x, HEIGHT - 1] == null)
                {
                    // We can play this column
                    int availiblerow = getNextAvailibleRow(board, x);
                    board[x, availiblerow] = turn;
                    //run the next ones

                    if (!HasWinner(board))
                    {
                        if (!IsFull(board))
                        {//Board is not full go again
                            PlayRound((bool?[,])board.Clone(), !turn, ++deep);
                        }
                        else
                        {// We have filled the board and have no winners
                            SolutionCount++;
                            Solutions.Add(board);
                          //  if (SolutionCount % 100 == 0) WriteOutGame(board);
                            
                            
                        }
                    }

                }
            }
        }

        static bool HasWinner(bool?[,] board)
        {
            return HasVerticalWinner(board) || HasHorizontalWinner(board) || HasDiagonalWinner(board);
        }
        static bool HasVerticalWinner(bool?[,] board)
        {
            for (int x = 0; x < LENGTH; x++)
            {
                for (int y = 0; y <= (HEIGHT - CONNECTLEGTH); y++)
                {
                    bool? matchType;
                    bool keepLooking = true;
                    if ((matchType = board[x, y]) != null)
                    {
                        for (int i = 0; i < CONNECTLEGTH && keepLooking; i++)
                        {
                            if (board[x, y + i] != matchType) keepLooking = false;
                        }
                        if (keepLooking) return true;//We have found a match
                    }
                }
            }
            return false;
        }
        static bool HasHorizontalWinner(bool?[,] board)
        {
            for (int y = 0; y < HEIGHT; y++)
            {
                for (int x = 0; x <= (LENGTH - CONNECTLEGTH); x++)
                {
                    bool? matchType;
                    bool keepLooking = true;
                    if ((matchType = board[x, y]) != null)
                    {
                        for (int i = 0; i < CONNECTLEGTH && keepLooking; i++)
                        {
                            if (board[x + i, y] != matchType) keepLooking = false;
                        }
                        if (keepLooking) return true;
                    }
                }
            }
            return false;
        }
        static bool HasDiagonalWinner(bool?[,] board)
        {
            if (IterationCount == 25781)
            {
                IterationCount = IterationCount + 1;
            }
            //Starting Lower left Winner
            for (int x = 0; x <= (LENGTH - CONNECTLEGTH); x++)
            {
                for (int y = 0; y <= (HEIGHT - CONNECTLEGTH); y++)
                {
                    bool? matchType;
                    bool keepLooking = true;
                    if ((matchType = board[x, y]) != null)
                    {
                        for (int i = 0; i < CONNECTLEGTH && keepLooking; i++)
                        {
                            if (board[x + i, y + i] != matchType) keepLooking = false;
                        }
                        if (keepLooking) return true;
                    }
                }
            }
            //Starting Lower Right Winner
            for (int x = LENGTH - 1; x >= (CONNECTLEGTH - 1); x--)
            {
                for (int y = 0; y <= (HEIGHT - CONNECTLEGTH); y++)
                {
                    bool? matchType;
                    bool keepLooking = true;
                    if ((matchType = board[x, y]) != null)
                    {
                        for (int i = 0; i < CONNECTLEGTH && keepLooking; i++)
                        {
                            if (board[x - i, y + i] != matchType) keepLooking = false;
                        }
                        if (keepLooking) return true;
                    }
                }
            }
            return false;
        }
        static bool IsFull(bool?[,] board)
        {
            for (int x = 0; x < LENGTH; x++)
            {
                for (int y = 0; y < HEIGHT; y++)
                {
                    if (board[x, y] == null) return false;
                }

            }
            return true;
        }
        static int getNextAvailibleRow(bool?[,] board, int column)
        {
            for (int y = 0; y < HEIGHT; y++)
            {
                if (board[column, y] == null) return y;
            }
            return -1;
        }
        static void WriteOutGame(bool?[,] board)
        {
            using (StreamWriter stream = new StreamWriter(Path.Combine(@"C:\Development\RandomProjects\Connect4\Results", IterationCount.ToString() + ".txt")))
            {

                for (int y = 0; y < HEIGHT; y++)
                {
                    for (int x = 0; x < LENGTH; x++)
                    {
                        if (board[x, y] == null) { stream.Write('n'); }
                        else
                        {
                            stream.Write(board[x, y] == true ? 'r' : 'y');
                        }
                    }
                    stream.WriteLine(" ");
                }
            }
        }


    }
}
