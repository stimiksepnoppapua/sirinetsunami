using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace MobileBMKG.Droid
{
    [Activity(Label = "NotifyReceived", Theme = "@style/MainTransparant")]
    public class NotifyReceived : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
         /*   base.OnCreate(savedInstanceState);
            AlarmManager manager = (AlarmManager)GetSystemService(Context.AlarmService);
           Intent myIntent = new Intent(this, typeof(NotifyBroadcastReceived));
            PendingIntent pendingIntent = PendingIntent.GetBroadcast(this, 0, myIntent, PendingIntentFlags.UpdateCurrent);
            myIntent.SetFlags(ActivityFlags.ClearTop);
            manager.Set(AlarmType.RtcWakeup, SystemClock.ElapsedRealtime() + 1000, pendingIntent);*/
        }

    }


    
}