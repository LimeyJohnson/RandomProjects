using System.Diagnostics;

namespace FriendsFlockCore
{
    public enum LogType
    {

    }

    public static class Log
    {
        private static string formatBase = "{0}\t{1}";
        private static string formatParamters = formatBase + "\t{2}";

        private static void logLine(string message)
        {
            Debug.WriteLine(message);
        }

        public static void Start(string message)
        {
            logLine(string.Format(formatBase, "Start", message));
        }

        public static void Start(string message, string parameters)
        {
            logLine(string.Format(formatParamters, "Start", message, parameters));
        }

        public static void End(string message)
        {
            logLine(string.Format(formatBase, "End", message));
        }

        public static void End(string message, string returnValue)
        {
            logLine(string.Format(formatParamters, "End", message, returnValue));
        }

        public static void Info(string message)
        {
            logLine(string.Format(formatBase, "Info", message));
        }
    }
}