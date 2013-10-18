using FriendsFlockCore.AnalyticsServiceReference;
using System;
using System.Diagnostics;

namespace FriendsFlockCore
{
    public static class Analytics
    {
        private static string guid;
        public enum Metric
        {
            Singles,
            Friends,
            MapTime,
            MaxReturnFQL,
            GraphTime, 
            Edges,
            Islands
        }
        public static void SendMetric(Metric metric, object metricObject)
        {
            try
            {
                AnalyticsSoapClient client = new AnalyticsSoapClient();
                client.UpdateAnalyticAsync(getGuid(), metric.ToString(), metricObject.ToString());
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.StackTrace);
            }
        }
        private static string getGuid()
        {
            if(string.IsNullOrEmpty(guid)) guid = System.Guid.NewGuid().ToString();
            return guid;
        }
    }
}
