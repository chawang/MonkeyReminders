using System;
using System.Collections.Generic;
using Android.Support.V4.App;
using Java.Lang;

namespace MonkeyReminders
{
    public class MonkeyReminderAdapter : FragmentPagerAdapter
    {
        List<Fragment> fragments = new List<Fragment>();
        List<string> titles = new List<string>();

        public MonkeyReminderAdapter(FragmentManager fragManager)
            : base(fragManager)
        {
        }

        public void AddFragments(Fragment frag, string title)
        {
            fragments.Add(frag);
            titles.Add(title);
        }

        public override int Count
        {
            get { return fragments.Count; }
        }

        public override Fragment GetItem(int position)
        {
            return fragments[position];
        }

        public override ICharSequence GetPageTitleFormatted(int position)
        {
            return new Java.Lang.String(titles[position]);
        }
    }
}