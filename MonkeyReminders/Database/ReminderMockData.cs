using System;
using System.Collections.Generic;

namespace MonkeyReminders
{
    public static class ReminderMockData
    {
        public static List<Reminders> ReminderMockList { get; private set; }

        static ReminderMockData()
        {
            var temp = new List<Reminders>();

            AddReminder(temp);

            ReminderMockList = temp;
        }

        static void AddReminder(List<Reminders> reminderMockList)
        {
            var date = DateTime.Now;

            reminderMockList.Add(new Reminders()
            {
                UserId = LoginActivity.user.UserId,
                Title = "First Reminder",
                Details = "Fake Normal Details",
                Priority = 0,
                CreatedOn = date.AddDays(-1),
                Completed = false
            });

            reminderMockList.Add(new Reminders()
            {
                UserId = LoginActivity.user.UserId,
                Title = "Second\nReminder",
                Details = "High Priority",
                Priority = 1,
                CreatedOn = date.AddDays(-5),
                Completed = false
            });

            reminderMockList.Add(new Reminders()
            {
                UserId = LoginActivity.user.UserId,
                Title = "Completed High Priority Reminder",
                Details = "Text",
                Priority = 1,
                CreatedOn = date.AddDays(-10),
                Completed = false
            });
            //reminderMockList.Add(new Reminders()
            //{
            //    UserId = LoginActivity.user.UserId,
            //    Title = "First Finished Reminder",
            //    Details = "Fake Details",
            //    Priority = 0,
            //    CreatedOn = date.AddDays(-10),
            //    FinishedOn = date,
            //    Completed = true
            //});
            //reminderMockList.Add(new Reminders()
            //{
            //    UserId = LoginActivity.user.UserId,
            //    Title = "Last Finished Reminder High Priority",
            //    Details = "Fake Details",
            //    Priority = 1,
            //    CreatedOn = date.AddDays(-1),
            //    FinishedOn = date,
            //    Completed = true
            //});
            //reminderMockList.Add(new Reminders()
            //{
            //    UserId = "FakeID",
            //    Title = "This should not show up",
            //    Details = "Should not show up",
            //    Priority = 1,
            //    CreatedOn = date.AddDays(-1),
            //    FinishedOn = date,
            //    Completed = true
            //});
        }
    }
}