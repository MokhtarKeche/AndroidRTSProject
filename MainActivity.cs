using Android.App;
using Android.OS;
using Android.Runtime;
using AndroidX.AppCompat.App;
using LibVLCSharp.Shared;
using LibVLCSharp.Platforms.Android;

namespace RTSPView_Net
{
    [Activity(Label = "RTSP Viewer", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        LibVLC libVLC;
        MediaPlayer mediaPlayer;
        VideoView videoView;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Core.Initialize();

            SetContentView(Resource.Layout.activity_main);
            videoView = FindViewById<VideoView>(Resource.Id.videoView);

            libVLC = new LibVLC(this);
            mediaPlayer = new MediaPlayer(libVLC);
            videoView.MediaPlayer = mediaPlayer;

            var media = new Media(libVLC, "rtsp://adresse_de_la_camera", FromType.FromLocation);
            mediaPlayer.Media = media;
            mediaPlayer.Play();
        }

        protected override void OnDestroy()
        {
            mediaPlayer?.Stop();
            mediaPlayer?.Dispose();
            libVLC?.Dispose();
            base.OnDestroy();
        }
    }
}
