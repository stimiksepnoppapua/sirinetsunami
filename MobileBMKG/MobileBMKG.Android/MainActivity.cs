using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Content;
using Android.Util;
using Android.Gms.Common;
using Android.Media;
using Android.Support.V4.App;

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
            Firebase.Messaging.FirebaseMessaging.Instance.SubscribeToTopic("tsunami");
           // IsPlayServicesAvailable();
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

            Android.Net.Uri soundUri = Android.Net.Uri.Parse("android.resource://" + "com.stimik1011.sirinesunami/" + Resource.Raw.tsunami);

            var notificationManager = (NotificationManager)GetSystemService(Android.Content.Context.NotificationService);

            if (Build.VERSION.SdkInt >= BuildVersionCodes.O)
            {
                var channel = new NotificationChannel(CHANNEL_ID, "Sirine Tsunami", NotificationImportance.Max)
                {
                    Description = "Firebase Cloud Sirine Tsunami Channel"
                };

                AudioAttributes att = new AudioAttributes.Builder()
                      .SetUsage(AudioUsageKind.VoiceCommunicationSignalling)
                      .SetContentType(AudioContentType.Speech)
                      .Build();

                channel.SetSound(soundUri, att);
                notificationManager.CreateNotificationChannel(channel);
            }
        }


    }
}