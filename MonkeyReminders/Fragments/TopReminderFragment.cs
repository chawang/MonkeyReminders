using System;
using System.Threading.Tasks;
using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;

namespace MonkeyReminders
{
    public class TopReminderFragment : Android.Support.V4.App.Fragment
    {
        internal ListView listView;
        internal TopReminderAdapter myAdapter;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = inflater.Inflate(Resource.Layout.TopRemindersList, container, false);
            listView = view.FindViewById<ListView>(Resource.Id.topRemindersList);
            listView.ItemClick += TopReminderClicked;

            var activity = view.FindViewById<ProgressBar>(Resource.Id.topActivityIndicator);
            activity.Visibility = ViewStates.Gone;

            LoadListView();
            MainActivity.RemindersRepo.Updated += ReloadListView;
            return view;
        }

        private async Task LoadListView()
        {
            var t = await MainActivity.RemindersRepo.GetAllRemindersAsync();
            myAdapter = new TopReminderAdapter(t, this.Activity);
            listView.Adapter = myAdapter;
            listView.DividerHeight = 0;
            listView.OverScrollMode = OverScrollMode.Always;
        }

        async void ReloadListView(object s, EventArgs e)
        {
            myAdapter.Update();
        }

        void TopReminderClicked(object s, AdapterView.ItemClickEventArgs e)
        {
            var reminder = myAdapter.reminders[e.Position];
            var dialog = new AlertDialog.Builder(this.Activity);
            dialog.SetMessage($"{reminder.Title}\n{reminder.CreatedOn}");
            dialog.SetNeutralButton("OK", delegate { });
            dialog.Show();
        }
    }
}

