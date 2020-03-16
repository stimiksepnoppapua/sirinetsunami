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

            Device.BeginInvokeOnMainThread(() =>
            {
                Xamarin.Forms.MessagingCenter.Send(new MobileBMKG.MessagingCenterAlert { Title = title, Message = messageBody, Cancel = "Ok" }, "message");
            });

           
          
        }
    }
}