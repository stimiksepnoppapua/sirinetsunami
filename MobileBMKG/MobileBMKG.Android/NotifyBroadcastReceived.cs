using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Media;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.App;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace MobileBMKG.Droid
{
    [BroadcastReceiver]
    public class NotifyBroadcastReceived : BroadcastReceiver
    {
        public static Ringtone ringtone;
        global::Android.Net.Uri soundUri = global::Android.Net.Uri.Parse("android.resource://" + "com.stimik1011.sirinesunami" + "/raw/tsunami");
        public override void OnReceive(Context context, Intent intent)
        {
            var bigStyle = new NotificationCompat.BigTextStyle().BigText("Telah Terjadi Tsunami");
            // Create a PendingIntent; we're only using one PendingIntent (ID = 0):
            NotificationManager notificationManager = context.GetSystemService(Context.NotificationService) as NotificationManager;

            NotificationCompat.Builder builder = new NotificationCompat.Builder(context, MainActivity.CHANNEL_ID)
                //  .SetContentIntent(pendingIntent)
                .SetContentTitle("Sirine Tsunami")
                .SetContentText("Sirine Tsunami")
                .SetAutoCancel(true)
                .SetStyle(bigStyle)
                .SetSound(soundUri)
                .SetPriority(NotificationCompat.PriorityMax)
                .SetSmallIcon(Resource.Drawable.icontsunami);

            if (Android.OS.Build.VERSION.SdkInt >= Android.OS.BuildVersionCodes.Lollipop)
            {
                builder.SetVisibility(NotificationCompat.VisibilityPublic);
            }

            Intent intents = new Intent(Intent.ActionView, Android.Net.Uri.Parse("http://inatews.bmkg.go.id/terkini.php"));
            PendingIntent pendingIntent = PendingIntent.GetActivity(context, MainActivity.NOTIFICATION_ID, intents, PendingIntentFlags.UpdateCurrent);

            Intent buttonIntent = new Intent(context, typeof(DissmisService));

            buttonIntent.PutExtra("notificationId", MainActivity.CHANNEL_ID);
            PendingIntent dismissIntent = PendingIntent.GetBroadcast(context, MainActivity.NOTIFICATION_ID, buttonIntent, PendingIntentFlags.CancelCurrent);

            builder.AddAction(Resource.Drawable.abc_ic_menu_overflow_material, "VIEW", pendingIntent);
            builder.AddAction(Resource.Drawable.abc_ic_menu_cut_mtrl_alpha, "DISMISS", dismissIntent);

            NotifyBroadcastReceived.ringtone = NotifyBroadcastReceived.ringtone ?? RingtoneManager.GetRingtone(context, soundUri);

            if (!NotifyBroadcastReceived.ringtone.IsPlaying)
                NotifyBroadcastReceived.ringtone.Play();

            notificationManager.Notify(MainActivity.NOTIFICATION_ID, builder.Build());
        }


    }
}