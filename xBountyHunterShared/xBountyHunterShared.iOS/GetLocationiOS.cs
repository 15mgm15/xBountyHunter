using System;
using System.Collections.Generic;
using CoreLocation;
using UIKit;
using Xamarin.Forms;
using xBountyHunterShared.DependencyServices;
using xBountyHunterShared.iOS;

[assembly: Dependency(typeof(GetLocationiOS))]

namespace xBountyHunterShared.iOS
{
    public class GetLocationiOS : IGetLocation
    {
        CLLocationManager locMgr;
        Dictionary<string, string> loc;

        public void activarGPS()
        {
            locMgr = new CLLocationManager();
            locMgr.PausesLocationUpdatesAutomatically = false;
            if(UIDevice.CurrentDevice.CheckSystemVersion(8, 0))
            {
                locMgr.RequestAlwaysAuthorization();
            }

            if(UIDevice.CurrentDevice.CheckSystemVersion(9, 0))
            {
                locMgr.AllowsBackgroundLocationUpdates = true;
            }

            if(CLLocationManager.LocationServicesEnabled)
            {
                locMgr.DesiredAccuracy = 1;
                locMgr.LocationsUpdated += (sender, e) => 
                {
                    loc = new Dictionary<string, string>();
                    loc.Add("Lat", e.Locations[e.Locations.Length - 1].Coordinate.Latitude.ToString());
                    loc.Add("Lon", e.Locations[e.Locations.Length - 1].Coordinate.Longitude.ToString());
                    System.Diagnostics.Debug.WriteLine("Detectado(Lat " + loc["Lat"] + ", Lon" + loc["Lon"] + ")");
                };
                locMgr.StartUpdatingLocation();
            }
        }

        public void apagarGPS()
        {
            locMgr.StopUpdatingLocation();   
        }

        public Dictionary<string, string> getLocation()
        {
            return loc;
        }
    }
}
