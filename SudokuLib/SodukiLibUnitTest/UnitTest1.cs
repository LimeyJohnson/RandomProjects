using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SudokuLib;

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

    }
}

