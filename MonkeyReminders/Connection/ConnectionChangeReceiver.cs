using System;
using Android.App;
using Android.Content;
using Android.Net;

namespace MonkeyReminders
{
    [BroadcastReceiver]
    [IntentFilter(new[] { ConnectivityManager.ConnectivityAction })]
    public class ConnectionChangeReceiver : BroadcastReceiver
    {
        public static event Action<bool> ConnectionChanged = delegate { };

        public override void OnReceive(Context context, Intent intent)
        {
            bool noConnection = intent.GetBooleanExtra(ConnectivityManager.ExtraNoConnectivity, false);
            ConnectionChanged(noConnection);
        }
    }
}

