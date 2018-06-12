using System;
using System.IO;
using System.Threading.Tasks;
using Foundation;
using UIKit;
using Xamarin.Forms;
using xBountyHunterShared.DependencyServices;
using xBountyHunterShared.iOS;

[assembly: Dependency(typeof(CameraiOS))]

namespace xBountyHunterShared.iOS
{
    public class CameraiOS : ICamera
    {
        public Task<string> TakePhoto()
        {
            TaskCompletionSource<string> tcs = new TaskCompletionSource<string>();

            Camera.TakePicture(UIApplication.SharedApplication.KeyWindow.RootViewController,
                               (imagePickerResult) => 
            {
                if(imagePickerResult == null)
                {
                    tcs.TrySetResult(null);
                    return;
                }

                var photo = imagePickerResult.ValueForKey(new
                                                          NSString("UIImagePickerControllerOriginalImage")) as UIImage;

                var documentDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                string jpgFileName = Path.Combine(documentDirectory, string.Format("fugitivo_{0}.jpg", Guid.NewGuid()));

                NSData imgData = photo.AsJPEG();
                NSError err = null;

                if(imgData.Save(jpgFileName, false, out err))
                {
                    string result = "";
                    result = jpgFileName;
                    tcs.TrySetResult(result);
                }
                else
                {
                    tcs.TrySetException(new Exception(err.LocalizedDescription));
                }
            });

            return tcs.Task;
        }
    }

	public static class Camera
	{
		static UIImagePickerController picker;
		static Action<NSDictionary> _callback;

		static void CameraInit()
		{
			if (picker != null)
			{
				return;
			}

			picker = new UIImagePickerController();
			picker.Delegate = new CameraDelegate();
		}

		public static void TakePicture(UIViewController parent, Action<NSDictionary> callback)
		{
            if(UIImagePickerController.IsSourceTypeAvailable(UIImagePickerControllerSourceType.Camera))
            {
				CameraInit();
				picker.SourceType = UIImagePickerControllerSourceType.Camera;
				_callback = callback;
				parent.PresentViewController((UIViewController)picker, true, (Action)null);   
            }
            else
            {
                System.Diagnostics.Debug.Write("Camera not available!!!!!!!! \n");
            }
		}

		class CameraDelegate : UIImagePickerControllerDelegate
		{
			public override void FinishedPickingMedia(UIImagePickerController picker, NSDictionary info)
			{
				var cb = _callback;
				_callback = null;

				picker.DismissViewController(true, (Action)null);
				cb(info);
			}

			public override void Canceled(UIImagePickerController picker)
			{
				var cb = _callback;
				_callback = null;

				picker.DismissViewController(true, (Action)null);
				cb(null);
			}
		}
	}
}
