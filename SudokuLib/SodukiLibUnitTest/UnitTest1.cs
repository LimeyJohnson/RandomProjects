using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SudokuLib;
using System.Collections.Generic;

namespace SodukiLibUnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestRows()
        {
            Board b = new Board();
            int x = 0;
            b.EachSquare(s =>
                {
                    x += s.Row;
                });
            Assert.AreEqual(x, 324);
        }
        [TestMethod]
        public void TestColumns()
        {
            Board b = new Board();
            int x = 0;
            b.EachSquare(s =>
            {
                x += s.Column;
            });
            Assert.AreEqual(x, 324);
        }
        [TestMethod]
        public void FireValueChangedEvent()
        {
            Square s = new Square(1, 1);
            Square eventSquare = null;
            s.ValueSetEvent += delegate(object sender, Square square)
            {
                eventSquare = square;
            };
            s.Value = 9;
            Assert.IsNotNull(eventSquare);
            Assert.AreEqual(s, eventSquare);
        }
        [TestMethod]
        public void TestBoardComplete()
        {
            Board b = new Board();
            b.EachSquare(s => s.Value = 1);
            Assert.AreEqual(b.Complete, true);
        }
        [TestMethod]
        public void TestSetValueAfterRemovingPossiblities()
        {
            Square s = new Square(1, 1);
            Square eventSquare = null;
            s.ValueSetEvent += delegate(object sender, Square square)
            {
                eventSquare = square;
            };

            for (int x = 0; x < 8; x++) s.RemovePossiblity(x);
            Assert.IsNotNull(eventSquare);
            Assert.AreEqual(eventSquare.Value, 9);
        }
        [TestMethod]
        public void TestEachSquare()
        {
            Board b = new Board();
            b.EachGroup(4, 5, s => s.RemovePossiblity(5));

            int[] possiblexy = { 3, 4, 5 };

            b.EachSquare(s =>
                {
                    if (Array.IndexOf(possiblexy, s.Row) >= 0 && Array.IndexOf(possiblexy, s.Column) >= 0)
                    {
                        Assert.AreEqual(s.possibiltiesLeft.Length, 8);
                    }
                    else
                    {
                        Assert.AreEqual(s.possibiltiesLeft.Length, 9);
                    }
                });
        }
        [TestMethod]
        public void TestValueSetRemovesPossibities()
        {
            Board b = new Board();
            b[0, 0].Value = 1;
            List<string> affectedSquares = new List<string>() { "0,0", "0,1", "0,2", "0,3", "0,4" , "0,5", "0,6", "0,7", "0,8", "1,0", "2,0", "3,0", "4,0", "5,0", "6,0", "7,0", "8,0", "1,1", "1,2",  "2,1", "2,2" };
            b.EachSquare(s =>
                {
                    if (affectedSquares.Contains(s.Row+","+ s.Column))
                    {
                        Assert.AreEqual(s.possibiltiesLeft.Length, s == b[0,0]? 1 : 8);
                    }
                    else
                    {
                        Assert.AreEqual(s.possibiltiesLeft.Length, 9);
                    }
                });
        }

    }
}

