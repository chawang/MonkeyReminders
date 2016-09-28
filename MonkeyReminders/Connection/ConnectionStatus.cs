using System;
using Android.App;
using Android.Content;
using Android.Net;

namespace MonkeyReminders
{
    public static class ConnectionStatus
    {
        static NetworkInfo CheckStatus()
        {
            ConnectivityManager connection = (ConnectivityManager)Application.Context.GetSystemService(Context.ConnectivityService);
            return connection.ActiveNetworkInfo;
        }

        public static string IsConnected()
        {
            var netinfo = CheckStatus();
            bool isConnected = netinfo != null && netinfo.IsConnected;
            return $"Connection is established: {isConnected}";
        }

        public static string ConnectionType()
        {
            string type = "null";
            string roaming = "No connection";

            var netinfo = CheckStatus();

            //if (netinfo.Type == ConnectivityType.Wifi)
            //{
            //    ...
            //}
            //else if (netinfo.Type == ConnectivityType.Mobile)
            //{
            //    ...
            //}

            if (netinfo != null)
            {
                type = netinfo.TypeName;
                roaming = netinfo.IsRoaming.ToString();
            }
            return "Of type: " + type + $"\nRoaming: {roaming}";
        }
    }
}

