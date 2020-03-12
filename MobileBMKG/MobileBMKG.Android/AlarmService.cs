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
    }
}