using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace CoreLibrary
{
    public enum CriticalType
    {
        TokenFailed,
        TokenNotFound,
        TokenExpiered,

        UserInfo,
        FriendsInfo,
        FriendsList,
        FriendsMap,

        FlockLayout,
        OverlapRemoval,

        ClientUnhandledException,
        CloudUnhandledException,

        Metrics,
        Performance
    }

    public static class Log
    {        
        public static void Critical(string message)
        {
            Trace.WriteLine(message, TraceEventType.Critical.ToString());
        }

        public static void Critical(CriticalType type)
        {
            Trace.WriteLine(type.ToString(), TraceEventType.Critical.ToString());
        }

        public static void Error(string message)
        {
            Trace.WriteLine(message, TraceEventType.Error.ToString());
        }

        public static void Information(string message)
        {
            Trace.WriteLine(message, TraceEventType.Information.ToString());
        }

        public static void Resume(string message)
        {
            Trace.WriteLine(message, TraceEventType.Resume.ToString());
        }

        public static void Start(string name, string message)
        {
            Trace.WriteLine(string.Format("{0},{1}", name, message), TraceEventType.Start.ToString());
        }

        public static void Stop(string name, string message)
        {
            Trace.WriteLine(string.Format("{0},{1}", name, message), TraceEventType.Stop.ToString());
        }

        public static void Suspend(string message)
        {
            Trace.WriteLine(message, TraceEventType.Suspend.ToString());
        }

        public static void Transfer(string message)
        {
            Trace.WriteLine(message, TraceEventType.Transfer.ToString());
        }

        public static void Verbose(string message)
        {
            Trace.WriteLine(message, TraceEventType.Verbose.ToString());
        }

        public static void Warning(string message)
        {
            Trace.WriteLine(message, TraceEventType.Warning.ToString());
        }

        public static void Metrics(string message)
        {
            Trace.WriteLine(message, CriticalType.Metrics.ToString());
        }

        public static void Performance(string message)
        {
            Trace.WriteLine(message, CriticalType.Performance.ToString());
        }

        public static void Performance(string operation, long milliseconds)
        {
            double seconds = milliseconds / 1000;
            Trace.WriteLine(string.Format("@{0}, Time={1} Seconds", operation, seconds),
                CriticalType.Performance.ToString());
        }
    }
}