using System;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Provider;
using Java.IO;
using Xamarin.Forms;
using xBountyHunterShared.DependencyServices;
using xBountyHunterShared.Droid;

[assembly : Dependency(typeof(CameraAndroid))]

namespace xBountyHunterShared.Droid
{
    public class CameraAndroid : ICamera
    {
        public static File file;
        public static File pictureDirectory;
        public static TaskCompletionSource<string> tcs;

        public Task<string> TakePhoto()
        {

            Intent intent = new Intent(MediaStore.ActionImageCapture);

            pictureDirectory = new File(Android.OS.Environment.GetExternalStoragePublicDirectory
                                        (Android.OS.Environment.DirectoryPictures), "CameraAppDemo");

            if (!pictureDirectory.Exists())
            {
                pictureDirectory.Mkdirs();
            }

            file = new File(pictureDirectory, string.Format("fugitivo_{0}.jpg", Guid.NewGuid()));

            intent.PutExtra(MediaStore.ExtraOutput, Android.Net.Uri.FromFile(file));
            var activity = MainActivity.Instance;
            activity.StartActivityForResult(intent, 0);

            tcs = new TaskCompletionSource<string>();
            return tcs.Task;
        }

        public static void OnResult(Result resultCode)
        {
            if (resultCode == Result.Canceled)
            {
                tcs.TrySetResult(null);
                return;
            }
            else if (resultCode != Result.Ok)
            {
                tcs.TrySetException(new Exception("Unexpected Error"));
                return;
            }

            string res = "";
            res = file.Path;
            tcs.TrySetResult(res);
        }
    }
}
