using Android.App;
using Android.Content;
using Android.Util;
using Firebase.Iid;

namespace mobilebmkg.droid
{
    [Service]
    [IntentFilter(new[] { "com.google.firebase.INSTANCE_ID_EVENT" })]
    public class MyFirebaseiIdService : FirebaseInstanceIdService
    {

        public override void OnTokenRefresh()
        {
            const string tag = "MyFirebaseIIDService";
            var refreshedtoken = FirebaseInstanceId.Instance.Token;
            Log.Debug(tag, "refreshed token: " + refreshedtoken);
            sendregistrationtoserver(refreshedtoken);
        }
        void sendregistrationtoserver(string token)
        {
            //add custom implementation, as needed.
        }
    }
}