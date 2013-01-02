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
        /// <summary>
        /// True when it breaks
        /// </summary>
        /// <param name="x">x co-ord</param>
        /// <param name="y">y-co-ord</param>
        /// <returns>if it should break</returns>
        public delegate bool RunLoop(int x, int y);
        public static void RunAll(RunLoop loop)
        {
            for (int x = 0; x < LENGTH; x++)
            {
                for(int y = 0; y<HEIGHT; y++)
                {
                    if(loop(x,y)) return;
                }
            }
        }

        static int LENGTH = 7, HEIGHT = 6, CONNECTLENGTH = 4;
        static int IterationCount = 0, SolutionCount = 0;
        static GameBoardCollection Solutions, Visited;
        static GameBoard Board;
        static void Main(string[] args)
        {
            Board = GameBoard.InitialBoard (LENGTH, HEIGHT, CONNECTLENGTH);
            Solutions = new GameBoardCollection();
            Visited = new GameBoardCollection();
           
            Console.WriteLine("Solution Count = " + Solutions.Count);
        }
        //Set up Board, then move through locking down positions

        static void PlayRound(bool turn, int deep = 0)
        {
            for (int curX = 0; curX < LENGTH; curX++)
            {
                for (int curY = 0; curY < HEIGHT; curY++)
                {
                    for(int x = curX; x < LENGTH; x++)
                    {
                        for(int y = curY; y< HEIGHT; y++)
                        {
                            Board[x,y] = !Board[x,y];
                            if(!Board.HasWinner)
                            {
                                Board.WriteOutGame(Board.HashCode.ToString());
                                Solutions.Add((GameBoard)Board.Clone());
                                Console.WriteLine("Solution Found");
                            }
                        }
                    }
                    Console.WriteLine("Set new piece moving on");
                    Board[curY,curX] = !Board[curY, curX];
                }

            }

            IterationCount++;
            Console.WriteLine("Iteration: " + IterationCount + " Solutin Count: " + Solutions.Count + " deep : " + deep);
                if (board[x, HEIGHT - 1] == null)
                {
                    // We can play this column
                    int? availibleRow;
                    if((availibleRow = board.getNextAvailibleRow(x)) == null) break;
                    GameBoard playBoard = (GameBoard)board.Clone();
                    playBoard[x, availibleRow.Value] = turn;
                    //run the next ones
                    if (Visited.MyContains(playBoard))
                    {
                        return;
                    }
                    else
                    {
                        Visited.Add(playBoard);
                       
                    }
                    if (!playBoard.HasWinner)
                    {
                        if (!playBoard.IsFull)
                        {//Board is not full go again
                            PlayRound(playBoard, !turn, ++deep);
                        }
                        else
                        {// We have filled the board and have no winners
                            SolutionCount++;
                            Solutions.Add(playBoard);
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
                for (int y = 0; y <= (HEIGHT - CONNECTLENGTH); y++)
                {
                    bool? matchType;
                    bool keepLooking = true;
                    if ((matchType = board[x, y]) != null)
                    {
                        for (int i = 0; i < CONNECTLENGTH && keepLooking; i++)
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
                for (int x = 0; x <= (LENGTH - CONNECTLENGTH); x++)
                {
                    bool? matchType;
                    bool keepLooking = true;
                    if ((matchType = board[x, y]) != null)
                    {
                        for (int i = 0; i < CONNECTLENGTH && keepLooking; i++)
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
            for (int x = 0; x <= (LENGTH - CONNECTLENGTH); x++)
            {
                for (int y = 0; y <= (HEIGHT - CONNECTLENGTH); y++)
                {
                    bool? matchType;
                    bool keepLooking = true;
                    if ((matchType = board[x, y]) != null)
                    {
                        for (int i = 0; i < CONNECTLENGTH && keepLooking; i++)
                        {
                            if (board[x + i, y + i] != matchType) keepLooking = false;
                        }
                        if (keepLooking) return true;
                    }
                }
            }
            //Starting Lower Right Winner
            for (int x = LENGTH - 1; x >= (CONNECTLENGTH - 1); x--)
            {
                for (int y = 0; y <= (HEIGHT - CONNECTLENGTH); y++)
                {
                    bool? matchType;
                    bool keepLooking = true;
                    if ((matchType = board[x, y]) != null)
                    {
                        for (int i = 0; i < CONNECTLENGTH && keepLooking; i++)
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
