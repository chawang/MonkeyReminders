using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Android.App;
using Android.Views;
using Android.Widget;

namespace MonkeyReminders
{
    public class AllReminderAdapter : BaseAdapter<Reminders>
    {
        internal List<Reminders> reminders;
        //Activity activity;

        public AllReminderAdapter(List<Reminders> reminders, Activity activity)
        {
            var orderReminders = from reminder in reminders
                                 where reminder.UserId == LoginActivity.user.UserId
                                 orderby reminder.CreatedOn
                                 orderby reminder.Completed == true
                                 select reminder;
            this.reminders = orderReminders.ToList();
            //this.activity = activity;
        }

        public override Reminders this[int position]
        {
            get
            {
                return reminders[position];
            }
        }

        public override int Count
        {
            get
            {
                return reminders.Count;
            }
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var view = convertView;

            if (view == null)
            {
                view = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.AllReminderRow, parent, false);

                var t = view.FindViewById<TextView>(Resource.Id.reminderTitle);
                var d = view.FindViewById<TextView>(Resource.Id.reminderDetails);
                //var c = view.FindViewById<TextView>(Resource.Id.reminderCreated);
                //var f = view.FindViewById<TextView>(Resource.Id.reminderFinished);

                view.Tag = new ViewHolder() { Title = t, Details = d/*, Created = c, Finished = f*/ };
            }
            var reminderRow = view.FindViewById<LinearLayout>(Resource.Id.allReminderRow);
            var row = (ViewHolder)view.Tag;

            row.Title.Text = reminders[position].Title;
            row.Details.Text = reminders[position].Details;
            //row.Created.Text = $"Created: {reminders[position].CreatedOn.Date.ToString("MM/dd/yy")}";

            if (reminders[position].Completed == false)
            {
                //    row.Finished.Text = "";
                reminderRow.SetBackgroundColor(Android.Graphics.Color.White);
            }
            if (reminders[position].Priority == 1)
            {
                reminderRow.SetBackgroundColor(Android.Graphics.Color.IndianRed);
            }
            if (reminders[position].Completed == true)
            {
                //row.Finished.Text = $"Completed: {reminders[position].FinishedOn.ToString("MM/dd/yy")}";
                reminderRow.SetBackgroundColor(Android.Graphics.Color.Gray);
            }

            return view;
        }

        internal async void Update()
        {
            this.reminders = await MainActivity.RemindersRepo.GetAllRemindersAsync();
            var orderReminders = from reminder in this.reminders
                                 where reminder.UserId == LoginActivity.user.UserId
                                 orderby reminder.CreatedOn
                                 orderby reminder.Completed == true
                                 select reminder;
            this.reminders = orderReminders.ToList();
            this.NotifyDataSetChanged();
        }
    }
}