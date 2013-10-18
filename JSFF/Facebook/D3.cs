// Class1.cs
//

using System;
using System.Html;
using System.Runtime.CompilerServices;
using System.Collections;
namespace D3Wrapper
{
    public delegate int IntDelegate(Dictionary D);
    public delegate string StringDelegate(Dictionary D);
    [Imported]
    [IgnoreNamespace]
    [ScriptName("d3")]
    public static class D3
    {
        public static LayoutObject Layout;
        public static extern SelectObject Select(string selector);
        public static ScaleObject Scale;
    }
    [Imported, IgnoreNamespace, ScriptName("Object")]
    public class GraphData
    {
        Node[] Nodes;
        Link[] Links;
    }
    [Imported, IgnoreNamespace, ScriptName("Object")]
    public class Node
    {
        public string Name;
        public int Group;
    }
    [Imported, IgnoreNamespace, ScriptName("Object")]
    public class Link
    {
        public int Source;
        public int Target;
        public int Value;
    }
    [Imported, IgnoreNamespace]
    public class SelectObject
    {
        public extern SelectObject Append(string append);
        public extern SelectObject Attr(string attribute, object value);
        public extern SelectObject Attr(string attribute, IntDelegate action);
        public extern SelectObject SelectAll(string selector);
        public extern SelectObject Enter();
        public extern SelectObject Style(string style, IntDelegate callback);
        public extern SelectObject Data(Link[] links);
        public extern SelectObject Data(Node[] nodes);
        public extern SelectObject Call(Action action);
        public extern SelectObject Text(StringDelegate callback);
    }
    [Imported, IgnoreNamespace]
    public class ForceObject
    {
        public extern ForceObject Charge(int charge);
        public extern ForceObject LinkDistance(int distance);
        public extern ForceObject Size(int[] size);
        public extern ForceObject Nodes(Node[] nodes);
        public extern ForceObject Links(Link[] links);
        public extern ForceObject Start();
        public Action Drag;
        public extern ForceObject On(string on, Action action);
    }
    public class LayoutObject
    {
        public extern ForceObject Force();

    }
    public class ScaleObject
    {
        public extern int[] Catagory20();
    }

}


