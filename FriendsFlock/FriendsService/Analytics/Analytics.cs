using System;
using System.Net;
using System.Data;
using System.Data.Services.Client;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using FriendsService.FriendsFlockWebService;
using System.ServiceModel;

namespace FriendsService.Analytics
{

    public static class Analytics
    {
        private static string address = "http://limeysrv1/Analytics.asmx";
        private static string Guid;

        public static void SendMetric(string metricName, object metric)
        {
            GetClient().UpdateAnalyticAsync(GetGuid(), metricName, metric.ToString());
        }


        private static string GetGuid()
        {
            if (string.IsNullOrEmpty(Guid)) Guid = System.Guid.NewGuid().ToString();
            return Guid;

        }
        private static AnalyticsSoapClient GetClient()
        {

            EndpointAddressBuilder eb = new EndpointAddressBuilder();
            eb.Uri = new Uri(address);
            return new AnalyticsSoapClient(new BasicHttpBinding(), eb.ToEndpointAddress());
        }

    }
}
