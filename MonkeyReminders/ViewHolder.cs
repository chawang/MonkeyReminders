using Android.Views;
using Android.Widget;
using Java.Lang;

namespace MonkeyReminders
{
    class ViewHolder : Object
    {
        public TextView Details { get; set; }
        public TextView Title { get; set; }
        public TextView Created { get; set; }
        public TextView Finished { get; set; }
        public CheckBox Checkbox { get; set; }
    }
}