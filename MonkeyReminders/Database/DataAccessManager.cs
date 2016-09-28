//using System;
//using System.Collections.Generic;
//using System.Diagnostics;
//using System.Linq;
//using System.Threading;
//using System.Threading.Tasks;
//using Microsoft.WindowsAzure.MobileServices.Sync;
//using SQLite;

//namespace MonkeyReminders
//{
//    public static class DataAccessManager
//    {
//        private static async Task Preload()
//        {
//            var t = await DataManager.Instance.RemindersTable.ToListAsync();
//            if (t == null && t.Count() == 0)
//            {
//                foreach (Reminders reminder in ReminderMockData.ReminderMockList)
//                {
//                    await AddRemindersAsync(reminder);
//                }
//            }
//        }

//        public static async Task<List<Reminders>> GetAllRemindersAsync()
//        {
//            await DataManager.Instance.Initialize().ConfigureAwait(true);
//            await SyncTable().ConfigureAwait(false);
//            await Preload();

//            return await DataManager.Instance.RemindersTable.ToListAsync();
//        }

//        public static async Task AddRemindersAsync(Reminders reminder)
//        {
//            await DataManager.Instance.Initialize();
//            await SyncTable().ConfigureAwait(false);

//            await DataManager.Instance.RemindersTable.InsertAsync(reminder);
//        }

//        public static async Task CompleteReminderAsync(Reminders reminder)
//        {
//            await DataManager.Instance.Initialize();
//            await SyncTable().ConfigureAwait(false);

//            reminder.Completed = true;
//            reminder.FinishedOn = DateTime.Now;
//            await DataManager.Instance.RemindersTable.UpdateAsync(reminder);
//        }

//        internal static async Task SyncTable()
//        {
//            using (var cancelToken = new CancellationTokenSource())
//            {
//                await DataManager.Instance.RemindersTable.PullAsync("remindersTable", DataManager.Instance.RemindersTable.CreateQuery(), false, cancelToken.Token, new PullOptions());
//                await DataManager.Instance.MobileService.SyncContext.PushAsync(cancelToken.Token);
//            }
//        }
//        //await MobileService.GetTable<TodoItem>().InsertAsync(item);

//        //public List<Reminders> GetAllReminders()
//        //{
//        //    return conn.Table<Reminders>().ToList();
//        //}

//        //public int AddReminder(Reminders reminder)
//        //{
//        //    return conn.Insert(reminder);
//        //}

//        //public void CompleteReminder(Reminders reminder)
//        //{
//        //    reminder.Completed = true;
//        //    reminder.FinishedOn = DateTime.Now;
//        //    conn.Update(reminder);
//        //}
//    }
//}

