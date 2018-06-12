using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace xBountyHunterShared.Droid
{
    [Activity(Label = "xBountyHunterShared", Icon = "@drawable/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        internal static MainActivity Instance { get; private set; }
        
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            Instance = this;
            Xamarin.Forms.Forms.Init(this, bundle);
            Xamarin.FormsMaps.Init(this, bundle);

            LoadApplication(new App());
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Android.Content.Intent data)
        {

			if (resultCode == Result.Canceled)
			{
				CameraAndroid.tcs.TrySetResult(null);
				return;
			}
			else if (resultCode != Result.Ok)
			{
				CameraAndroid.tcs.TrySetException(new Exception("Unexpected Error"));
				return;
			}

			string res = "";
			res = CameraAndroid.file.Path;
			CameraAndroid.tcs.TrySetResult(res);

            base.OnActivityResult(requestCode, resultCode, data);
        }
    }
}

