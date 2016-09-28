using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.MobileServices.SQLiteStore;

namespace MonkeyReminders
{
    public class DataManager
    {
        static StreamReader stream = new StreamReader(Assembly.GetExecutingAssembly().GetManifestResourceStream("MonkeyReminders.azure.txt"));
        public static MobileServiceClient MobileService = new MobileServiceClient(stream.ReadLine());
        //IMobileServiceTable<Reminders> ReminderTable;

        public DataManager()
        {
            CurrentPlatform.Init();
        }

        public async Task PreLoad()
        {
            //setup our local sqlite store and intialize our table
            //var store = new MobileServiceSQLiteStore(path);
            //store.DefineTable();
            //await MobileService.SyncContext.InitializeAsync(store, new MobileServiceSyncHandler());

            //Get our sync table that will call out to azure
            //ReminderTable = MobileService.GetSyncTable<Reminders>();

            var t = await GetAllRemindersAsync();

            if (t.Count == 0)
            {
                foreach (Reminders reminder in ReminderMockData.ReminderMockList)
                {
                    await AddReminderAsync(reminder);
                }
            }
        }

        public async Task<List<Reminders>> GetAllRemindersAsync()
        {
            var list = await MobileService.GetTable<Reminders>().ToListAsync();

            return list;
        }

        public async Task AddReminderAsync(Reminders reminder)
        {
            await MobileService.GetTable<Reminders>().InsertAsync(reminder);

            OnUpdated();
        }

        public async Task CompleteReminderAsync(Reminders reminder)
        {
            reminder.Completed = true;
            reminder.FinishedOn = DateTime.Now;
            await MobileService.GetTable<Reminders>().UpdateAsync(reminder);

            OnUpdated();
        }

        public async Task DeleteReminderAsync(Reminders reminder)
        {
            await MobileService.GetTable<Reminders>().DeleteAsync(reminder);
        }

        async Task InitializeLocalDBAsync()
        {
            var dbpath = FileAccessHelper.GetLocalFilePath("reminders.db3");
            var store = new MobileServiceSQLiteStore(dbpath);
            await MobileService.SyncContext.InitializeAsync(store);
        }

        public event EventHandler<EventArgs> Updated;

        public void OnUpdated()
        {
            if (Updated != null)
                Updated(this, EventArgs.Empty);
        }
    }
}