using System;
using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Content;
using Android.Util;
using Microsoft.AppCenter.Push;
using System.Collections.Generic;
using Android.Gms.Common;
using Android.Media;

namespace MobileBMKG.Droid
{
    [Activity(Label = "MobileBMKG", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, 
        ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        static readonly string TAG = "MainActivity";
        internal static readonly string CHANNEL_ID = "my_notification_channel";
        internal static readonly int NOTIFICATION_ID = 100;

        string msgText = "";
       
        internal static MainActivity Instance { get; private set; }

        public  Ringtone Ringtone {get;set;}

        public static bool IsBacground { get; private set; }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;
            base.OnCreate(savedInstanceState);

            IsPlayServicesAvailable();
            CreateNotificationChannel();
            Instance = this;
            FFImageLoading.Forms.Platform.CachedImageRenderer.Init(enableFastRenderer: false);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            LoadApplication(new App());
        }

        protected override void OnResume()
        {
            base.OnResume();
            IsBacground = false;
        }


        protected override void OnPause()
        {
            base.OnPause();
            IsBacground = true;
        }

        protected override void OnNewIntent(Intent intent)
        {
            base.OnNewIntent(intent);
            Push.CheckLaunchedFromNotification(this, intent);

        }

        public bool IsPlayServicesAvailable()
        {
            int resultCode = GoogleApiAvailability.Instance.IsGooglePlayServicesAvailable(this);
            if (resultCode != ConnectionResult.Success)
            {
                if (GoogleApiAvailability.Instance.IsUserResolvableError(resultCode))
                    msgText = GoogleApiAvailability.Instance.GetErrorString(resultCode);
                else
                {
                    msgText = "This device is not supported";
                    Finish();
                }

                Log.Debug(TAG,msgText);
                return false;
            }
            else
            {
                msgText = "Google Play Services is available.";
                Log.Debug(TAG, msgText);
                return true;
            }
        }


        void CreateNotificationChannel()
        {
            if (Build.VERSION.SdkInt < BuildVersionCodes.O)
            {
                    // Notification channels are new in API 26 (and not a part of the
                    // support library). There is no need to create a notification
                    // channel on older versions of Android.
                return;
            }

            var channel = new NotificationChannel(CHANNEL_ID,
                                                  "FCM Notifications",
                                                  NotificationImportance.Default)
            {
                Description = "Firebase Cloud Messages appear in this channel"
            };

            var notificationManager = (NotificationManager)GetSystemService(Android.Content.Context.NotificationService);
            notificationManager.CreateNotificationChannel(channel);
        }

    }
}