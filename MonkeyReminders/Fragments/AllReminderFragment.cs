using System;
using System.Threading.Tasks;
using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;

namespace MonkeyReminders
{
    class AllReminderFragment : Android.Support.V4.App.Fragment
    {
        internal ListView listView;
        internal AllReminderAdapter myAdapter;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = inflater.Inflate(Resource.Layout.AllRemindersList, container, false);
            listView = view.FindViewById<ListView>(Resource.Id.allRemindersList);
            listView.ItemClick += OnReminderClick;

            var activty = view.FindViewById<ProgressBar>(Resource.Id.allActivityIndicator);
            activty.Visibility = ViewStates.Gone;

            LoadListView();
            MainActivity.RemindersRepo.Updated += (sender, e) =>
            {
                myAdapter.Update();
            };
            return view;
        }

        private async Task LoadListView()
        {
            var t = await MainActivity.RemindersRepo.GetAllRemindersAsync();
            myAdapter = new AllReminderAdapter(t, this.Activity);
            listView.Adapter = myAdapter;
            listView.DividerHeight = 0;
            listView.OverScrollMode = OverScrollMode.Always;
        }

        void OnReminderClick(object s, AdapterView.ItemClickEventArgs e)
        {
            var reminder = myAdapter.reminders[e.Position];
            var dialog = new AlertDialog.Builder(this.Activity);
            if (reminder.Completed == true)
            {
                var finished = reminder.FinishedOn;
                dialog.SetMessage($"{reminder.Title}\n{reminder.Details}\n{reminder.CreatedOn}\n{reminder.FinishedOn}");
            }
            else
            {
                dialog.SetMessage($"{reminder.Title}\n{reminder.Details}\n{reminder.CreatedOn}");
            }
            dialog.SetPositiveButton("DELETE", delegate
            {
                DialogDeleteReminder(reminder);
            });
            dialog.SetNegativeButton("DISMISS", delegate { });

            dialog.Show();
        }

        async Task DialogDeleteReminder(Reminders reminder)
        {
            await MainActivity.RemindersRepo.DeleteReminderAsync(reminder);
            myAdapter.Update();
        }
    }
}