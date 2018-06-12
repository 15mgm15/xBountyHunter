using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Forms;
using xBountyHunterShared.Droid;
using xBountyHunterShared.DependencyServices;
using Android.Telephony;

[assembly: Dependency(typeof(AndroidUDID))]

namespace xBountyHunterShared.Droid
{
    public class AndroidUDID : IUDID
    {
        public string getUDID()
        {
            Context cnt = Forms.Context;
            TelephonyManager tm = cnt.GetSystemService(Context.TelecomService) as TelephonyManager;
            if(tm == null)
            {
                return Build.Serial;
            }
            return tm.DeviceId;
        }
    }
}