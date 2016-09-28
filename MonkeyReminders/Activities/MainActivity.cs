using System;
using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Support.V4.View;
using Android.Support.V7.App;
using Android.Views;
using V7Toolbar = Android.Support.V7.Widget.Toolbar;

namespace MonkeyReminders
{
    [Activity(Label = "MonkeyDo", ScreenOrientation = ScreenOrientation.Portrait)]
    public class MainActivity : AppCompatActivity
    {
        public static DataManager RemindersRepo { get; set; }
        public static List<Reminders> List { get; set; }

        protected async override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            RemindersRepo = new DataManager();
            await RemindersRepo.PreLoad();

            SetContentView(Resource.Layout.Main);
            var toolbar = FindViewById<V7Toolbar>(Resource.Id.toolbarWithTabs);
            SetSupportActionBar(toolbar);

            var fabAdd = FindViewById<FloatingActionButton>(Resource.Id.fabAddButton);
            fabAdd.Click += (sender, e) =>
            {
                //Snackbar.Make(fabAdd, "Here's a snackbar!", Snackbar.LengthLong).SetAction("Action",
                //    new ClickListener(v =>
                //    {
                //        Console.WriteLine("Action handler");
                //    })).Show();

                Intent intent = new Intent(this, typeof(AddReminderActivity));
                StartActivity(intent);
            };

            var viewPager = FindViewById<ViewPager>(Resource.Id.viewPager);
            if (viewPager != null)
                SetupViewPager(viewPager);

            var tabLayout = FindViewById<TabLayout>(Resource.Id.tabs);
            tabLayout.SetupWithViewPager(viewPager);
            SetupdTabIcons(tabLayout);
        }

        private void SetupViewPager(ViewPager viewPager)
        {
            var adapter = new MonkeyReminderAdapter(SupportFragmentManager);
            adapter.AddFragments(new TopReminderFragment(), "Top");
            adapter.AddFragments(new AllReminderFragment(), "All");

            viewPager.Adapter = adapter;
        }

        private void SetupdTabIcons(TabLayout tabLayout)
        {
            tabLayout.GetTabAt(0).SetIcon(tabIcons[0]);
            tabLayout.GetTabAt(1).SetIcon(tabIcons[1]);
        }
        private int[] tabIcons = { Resource.Drawable.megaphone, Resource.Drawable.mailbox };
    }

    public class ClickListener : Java.Lang.Object, View.IOnClickListener
    {
        public ClickListener(Action<View> handler)
        {
            Handler = handler;
        }

        public Action<View> Handler { get; set; }

        public void OnClick(View v)
        {
            var h = Handler;
            if (h != null)
                h(v);
        }
    }
}


