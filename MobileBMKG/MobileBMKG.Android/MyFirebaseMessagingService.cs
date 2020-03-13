using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Media;
using Android.OS;
using Android.Support.V4.App;
using Android.Util;
using Android.Views;
using Android.Widget;
using Firebase.Messaging;
using Xamarin.Forms;

namespace MobileBMKG.Droid
{
    [Service]
    [IntentFilter(new[] { "com.google.firebase.MESSAGING_EVENT" })]
    public class MyFirebaseMessagingService : FirebaseMessagingService
    {
        public override void OnCreate()
        {
            base.OnCreate();
        }

        public override void OnMessageReceived(RemoteMessage message)
        {
            SendNotification(message.GetNotification().Title, message.GetNotification().Body, message.Data);
            base.OnMessageReceived(message);
        }

        void SendNotification(string title, string messageBody, IDictionary<string, string> data)
        {
            var bigStyle = new NotificationCompat.BigTextStyle().BigText(messageBody);
            // Create a PendingIntent; we're only using one PendingIntent (ID = 0):
            var context = MainActivity.Instance;
            NotificationManager notificationManager = context.GetSystemService(Context.NotificationService) as NotificationManager;
            Android.Net.Uri soundUri = Android.Net.Uri.Parse("android.resource://" + "com.stimik1011.sirinesunami/" + Resource.Raw.tsunami);
            NotificationCompat.Builder builder = new NotificationCompat.Builder(context, MainActivity.CHANNEL_ID)
                //  .SetContentIntent(pendingIntent)
                .SetContentTitle(title)
                .SetContentText("Sirine Tsunami")
                .SetAutoCancel(false)
                .SetStyle(bigStyle)
                .SetSound(soundUri)
                .SetPriority(NotificationCompat.PriorityMax)
                .SetSmallIcon(Resource.Drawable.icontsunami);

            if (Android.OS.Build.VERSION.SdkInt >= Android.OS.BuildVersionCodes.Lollipop)
            {
                builder.SetVisibility(NotificationCompat.VisibilityPublic);
            }

            Intent intent = new Intent(Intent.ActionView,Android.Net.Uri.Parse("http://inatews.bmkg.go.id/terkini.php"));
            PendingIntent pendingIntent =  PendingIntent.GetActivity(context, MainActivity.NOTIFICATION_ID, intent, PendingIntentFlags.UpdateCurrent);

            Intent buttonIntent = new Intent(context,typeof(DissmisService));

            buttonIntent.PutExtra("notificationId", MainActivity.CHANNEL_ID);
            PendingIntent dismissIntent = PendingIntent.GetBroadcast(context, MainActivity.NOTIFICATION_ID, buttonIntent, PendingIntentFlags.CancelCurrent);

            builder.AddAction(Resource.Drawable.abc_ic_menu_overflow_material, "VIEW", pendingIntent);
            builder.AddAction(Resource.Drawable.abc_ic_menu_cut_mtrl_alpha, "DISMISS", dismissIntent);
            notificationManager.Notify(MainActivity.NOTIFICATION_ID, builder.Build());
        }
    }
}