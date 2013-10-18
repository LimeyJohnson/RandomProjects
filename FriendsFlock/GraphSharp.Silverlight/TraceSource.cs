using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace GraphSharp
{
    public enum SourceLevels
    {
        All
    }

    public class TraceSource
    {
        public TraceSource(string source, SourceLevels levels)
        {
        }
    }

    static class Debug
    {
        public static void Write(string ignore)
        {
        }

        public static void WriteLine(string line)
        {
            System.Diagnostics.Debug.WriteLine(line);
        }

        public static void Assert(bool condition)
        {
            System.Diagnostics.Debug.Assert(condition);
        }

        public static void Assert(bool condition, string a, string b)
        {
            System.Diagnostics.Debug.Assert(condition, a, b);
        }
    }
}
