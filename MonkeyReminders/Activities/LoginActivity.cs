using System;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Views;
using Android.Widget;
using Microsoft.WindowsAzure.MobileServices;

namespace MonkeyReminders
{
    [Activity(Label = "MonkeyDo", MainLauncher = true, ScreenOrientation = ScreenOrientation.Portrait)]
    public class LoginActivity : Activity
    {
        internal static MobileServiceUser user;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Login);
            var loginButton = FindViewById<Button>(Resource.Id.loginButton);

            loginButton.Click += LoginUser;

            var metrics = Resources.DisplayMetrics;
            //var widthDP = ConvertPixelsToDP(metrics.WidthPixels);
            //var heightDP = ConvertPixelsToDP(metrics.HeightPixels);

            var logo = FindViewById<ImageView>(Resource.Id.appLogo);
            logo.LayoutParameters = new LinearLayout.LayoutParams(metrics.WidthPixels, (metrics.HeightPixels / 3 * 2));


            //ConnectionChangeReceiver.ConnectionChanged += delegate
            //{
            //    CreateAndShowDialog(ConnectionStatus.ConnectionType(), "Connection type changed.");
            //    CreateAndShowDialog(ConnectionStatus.IsConnected(), "Connection status changed.");
            //};
        }

        internal int ConvertPixelsToDP(float pixelValue)
        {
            var dp = (int)((pixelValue) / Resources.DisplayMetrics.Density);
            return dp;
        }

        public async void LoginUser(object s, EventArgs e)
        {
            // Load data only after authentication succeeds.
            if (await Authenticate())
            {
                Intent intent = new Intent(base.BaseContext, typeof(MainActivity));
                StartActivity(intent);
            }
        }

        private async Task<bool> Authenticate()
        {
            var success = false;
            try
            {
                user = await DataManager.MobileService.LoginAsync(this,
                                                                  MobileServiceAuthenticationProvider.Facebook);
                CreateAndShowDialog(string.Format("Logged in as {0}",
                    user.UserId), "Logged in!");

                success = true;
            }
            catch (Exception ex)
            {
                CreateAndShowDialog(ex, "Authentication failed");
            }

            return success;
        }

        internal void CreateAndShowDialog(Exception exception, String title)
        {
            CreateAndShowDialog(exception.Message, title);
        }

        internal void CreateAndShowDialog(string message, string title)
        {
            AlertDialog.Builder builder = new AlertDialog.Builder(this);

            builder.SetMessage(message);
            builder.SetTitle(title);
            builder.Create().Show();
        }
    }
}