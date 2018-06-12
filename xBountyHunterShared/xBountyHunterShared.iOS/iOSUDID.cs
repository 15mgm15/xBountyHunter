using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;
using Xamarin.Forms;
using xBountyHunterShared.iOS;
using xBountyHunterShared.DependencyServices;

[assembly: Dependency(typeof(iOSUDID))]

namespace xBountyHunterShared.iOS
{
    public class iOSUDID : IUDID
    {
        public string getUDID()
        {
            return UIDevice.CurrentDevice.IdentifierForVendor.AsString();
        }
    }
}