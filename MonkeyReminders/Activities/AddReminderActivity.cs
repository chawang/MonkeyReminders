using System;
using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Widget;
using V7Toolbar = Android.Support.V7.Widget.Toolbar;

namespace MonkeyReminders
{
    [Activity(Label = "Add A Reminder", ScreenOrientation = ScreenOrientation.Portrait)]
    public class AddReminderActivity : AppCompatActivity
    {
        internal Spinner spinner;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.AddReminderPage);
            var toolbar = FindViewById<V7Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);

            spinner = FindViewById<Spinner>(Resource.Id.prioritySpinner);
            //spinner.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(spinner_ItemSelected);
            var adapter = ArrayAdapter.CreateFromResource(
                this, Resource.Array.priority_level, Android.Resource.Layout.SimpleSpinnerItem);

            adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            spinner.Adapter = adapter;

            var save = FindViewById<Button>(Resource.Id.saveReminder);
            save.Click += SaveReminder;
        }

        //private void spinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        //{
        //    Snackbar.Make((Android.Views.View)sender, "Priority Level Changed!", Snackbar.LengthLong)
        //            .SetAction("OK", _ => { })
        //            .Show();
        //}

        private void SaveReminder(object sender, EventArgs e)
        {
            var title = FindViewById<TextView>(Resource.Id.editTextTitle);
            var details = FindViewById<TextView>(Resource.Id.editTextDetails);
            var priority = 0;
            if (spinner.SelectedItem.ToString() == "High")
                priority = 1;

            var newReminder = new Reminders()
            {
                UserId = LoginActivity.user.UserId,
                Title = title.Text,
                Details = details.Text,
                Priority = priority,
                CreatedOn = DateTime.Now,
                Completed = false
            };

            MainActivity.RemindersRepo.AddReminderAsync(newReminder);

            Snackbar.Make((Android.Views.View)sender, "Reminder Saved!", Snackbar.LengthLong)
                    .SetAction("OK", _ => { })
                    .Show();

            base.OnBackPressed();
        }
    }
}

