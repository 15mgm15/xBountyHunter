using System;
using Foundation;
using Xamarin.Forms;
using xBountyHunterShared.iOS;

[assembly: Dependency(typeof(GetDeviceInfoiOS))]
namespace xBountyHunterShared.iOS
{
    public class GetDeviceInfoiOS : IApplicationInfo
    {
        public string GetVersionNumber => NSBundle.MainBundle.ObjectForInfoDictionary("CFBundleVersion").ToString();
    }
}
