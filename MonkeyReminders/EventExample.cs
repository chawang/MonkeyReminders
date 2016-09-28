using System;
namespace MonkeyReminders
{
    public class EventExample
    {
        //Example Events
        public void Updated()
        {
            //logic

            //parameters to pass to the eventlistener
            var sample = new MyEventArgs();
            sample.Sample = "sample";
            sample.Whatever = "whatever";

            //#1
            if (Changed != null)
                Changed(this, sample);

            //or

            //#2
            //if (DifferentChanged != null)
            //    DifferentChanged(this, sample);

            //or

            //OnChanged(EventArgs.Empty);
            //Null checks required to make sure there is an event listener subscribed to this event
        }
        //#1
        protected virtual void OnChanged(MyEventArgs e)
        {
            if (Changed != null)
                Changed(this, e);
        }
        //#2
        //protected virtual void OnDifferentChanged(MyEventArgs e)
        //{
        //    if (DifferentChanged != null)
        //        DifferentChanged(this, e);
        //}

        //#1
        public event EventHandler<MyEventArgs> Changed;
        //#2
        //public event MyEvent DifferentChanged;
    }

    //#1 & #2
    public class MyEventArgs : EventArgs
    {
        public string Whatever { get; set; }
        public string Sample { get; set; }
        // ...
    }
    //#2
    //public delegate void MyEvent(object sender, MyEventArgs e);
}
