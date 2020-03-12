using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Media;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace MobileBMKG.Droid
{

    [BroadcastReceiver]
    public class DissmisService:BroadcastReceiver
    {
        global::Android.Net.Uri soundUri = global::Android.Net.Uri.Parse("android.resource://" + "com.ocph23.bmkg" + "/raw/tsunami");
        public override void OnReceive(Context context, Intent intent)
        {
           int notificationId = intent.GetIntExtra("notificationId", 0);
           NotificationManager notificationManager = context.GetSystemService(Context.NotificationService) as NotificationManager;
            notificationManager.Cancel(notificationId);
            if(NotifyBroadcastReceived.ringtone!=null && NotifyBroadcastReceived.ringtone.IsPlaying)
            {
                NotifyBroadcastReceived.ringtone.Stop();
            }
        }
    }
}