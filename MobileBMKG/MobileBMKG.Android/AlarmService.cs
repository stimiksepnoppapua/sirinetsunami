using Android.Media;
using MobileBMKG.Droid;
using MobileBMKG.Services;

[assembly: Xamarin.Forms.Dependency(typeof(AlarmService))]
namespace MobileBMKG.Droid
{
    public class AlarmService  : IAlarmService
    {
        private MediaPlayer _mediaPlayer;

        public void PlaySound()
        {
            _mediaPlayer = MediaPlayer.Create(Android.App.Application.Context, Resource.Raw.tsunami);
            _mediaPlayer.Start();
        }


        public void StopSound()
        {
            var context = MainActivity.Instance;
            var soundUri = global::Android.Net.Uri.Parse("android.resource://" + "com.stimik1011.sirinesunami" + "/raw/tsunami");
            NotifyBroadcastReceived.ringtone = NotifyBroadcastReceived.ringtone ?? RingtoneManager.GetRingtone(context, soundUri);

            if (NotifyBroadcastReceived.ringtone.IsPlaying)
                NotifyBroadcastReceived.ringtone.Stop();

        }
    }
}