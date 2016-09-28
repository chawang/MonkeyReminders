using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Android.App;
using Android.Text;
using Android.Text.Style;
using Android.Views;
using Android.Widget;

namespace MonkeyReminders
{
    public class TopReminderAdapter : BaseAdapter<Reminders>
    {
        internal List<Reminders> reminders;
        //Activity activity;
        DataManager Data;

        public TopReminderAdapter(List<Reminders> reminders, Activity activity)
        {
            var orderedReminders = from reminder in reminders
                                   where reminder.UserId == LoginActivity.user.UserId
                                   orderby reminder.CreatedOn
                                   where reminder.Completed == false
                                   orderby reminder.Priority descending
                                   select reminder;
            this.reminders = orderedReminders.ToList();
            //this.activity = activity;
        }

        private async Task<List<Reminders>> Init()
        {
            return await Data.GetAllRemindersAsync();
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
                if (reminders.Count < 3)
                {
                    return reminders.Count;
                }
                else
                {
                    return 3;
                }
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
                view = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.TopRemindersRow, parent, false);

                var t = view.FindViewById<TextView>(Resource.Id.topTitle);
                var d = view.FindViewById<TextView>(Resource.Id.topDetails);
                var check = view.FindViewById<CheckBox>(Resource.Id.reminderCheckBox);

                check.Click += (sender, e) =>
                {
                    CompletedByChecking(check, t, d, position);
                };

                view.Tag = new ViewHolder() { Title = t, Details = d, Checkbox = check };

                //To use this event handler: use tags
                //check.Click += CompletedByChecking;
            }

            var reminderRow = view.FindViewById<LinearLayout>(Resource.Id.topReminderRow);
            var row = (ViewHolder)view.Tag;

            row.Title.Text = reminders[position].Title;
            row.Details.Text = reminders[position].Details;

            if (reminders[position].Priority == 0)
            {
                reminderRow.SetBackgroundColor(Android.Graphics.Color.White);
            }
            if (reminders[position].Priority == 1)
            {
                reminderRow.SetBackgroundColor(Android.Graphics.Color.IndianRed);
            }

            //row.Checkbox.Tag = view.Tag;

            return view;
        }

        //To use this event handler: use tags
        //void CompletedByChecking(object sender, EventArgs e)
        //{
        //    CheckBox checkbox = (CheckBox)sender;
        //
        //    var inputTag = (ViewHolder)checkbox.Tag;
        //
        //    if (checkbox.Checked)
        //    {
        //        ApplyStrikeThrough(inputTag.Title);
        //        ApplyStrikeThrough(inputTag.Details);
        //
        //        var test = new AlertDialog.Builder(activity);
        //        test.SetMessage("Testing via event handler");
        //        test.SetNeutralButton("OK", delegate
        //        {
        //            //checkbox.Toggle();
        //        });
        //        test.Show();
        //    }
        //    if (!checkbox.Checked)
        //    {
        //        RemoveStrikeThrough(inputTag.Title);
        //        RemoveStrikeThrough(inputTag.Details);
        //    }
        //}

        async void CompletedByChecking(CheckBox check, TextView t, TextView d, int position)
        {
            if (check.Checked)
            {
                ApplyStrikeThrough(t);
                ApplyStrikeThrough(d);

                await MainActivity.RemindersRepo.CompleteReminderAsync(reminders[position]);
            }
            await Task.Delay(250);
            check.Checked = false;

            Update();
        }

        internal async void Update()
        {
            this.reminders = await MainActivity.RemindersRepo.GetAllRemindersAsync();
            var orderReminders = from reminder in this.reminders
                                 where reminder.UserId == LoginActivity.user.UserId
                                 orderby reminder.CreatedOn
                                 where reminder.Completed == false
                                 orderby reminder.Priority descending
                                 select reminder;
            this.reminders = orderReminders.ToList();
            this.NotifyDataSetChanged();
        }

        void ApplyStrikeThrough(TextView textToFormat)
        {
            SpannableString content = new SpannableString(textToFormat.Text);
            StrikethroughSpan strikethrough = new StrikethroughSpan();
            content.SetSpan(strikethrough, 0, content.Length(), 0);
            textToFormat.TextFormatted = content;
        }

        void RemoveStrikeThrough(TextView textToFormat)
        {
            SpannableString content = new SpannableString(textToFormat.Text);
            StrikethroughSpan strikethrough = new Android.Text.Style.StrikethroughSpan();
            content.RemoveSpan(strikethrough);
            textToFormat.TextFormatted = content;
        }
    }
}

