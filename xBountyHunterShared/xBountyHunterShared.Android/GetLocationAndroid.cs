using System;
using System.Collections.Generic;
using Android.Content;
using Android.Locations;
using Android.OS;
using Android.Runtime;
using Xamarin.Forms;
using xBountyHunterShared.DependencyServices;
using xBountyHunterShared.Droid;

[assembly : Dependency(typeof(GetLocationAndroid))]

namespace xBountyHunterShared.Droid
{
    public class GetLocationAndroid : Java.Lang.Object, IGetLocation, ILocationListener
    {
        LocationManager locationManager;
        Dictionary<string, string> loc;

		public Dictionary<string, string> getLocation()
		{
            return loc;
		}

		public void OnLocationChanged(Location location)
		{
            newLocation(location);
		}

		public void newLocation(Location location)
		{
            loc = new Dictionary<string, string>();
            loc.Add("Lat", location.Latitude.ToString());
            loc.Add("Lon", location.Longitude.ToString());
            System.Diagnostics.Debug.WriteLine("Detectado(Lat " + loc["Lat"] + ", Lon" + loc["Lon"] + ")");
		}

        public void activarGPS()
        {
            try
            {
				Context cnt = Android.App.Application.Context;
				locationManager = cnt.GetSystemService(Context.LocationService) as LocationManager;
				locationManager.RequestLocationUpdates(LocationManager.GpsProvider, 0, 0, this);
				System.Diagnostics.Debug.WriteLine("Activando GPS");

				Criteria criteria = new Criteria();
				criteria.Accuracy = Accuracy.Fine;
				string provider = locationManager.GetBestProvider(criteria, true);
				Location location = locationManager.GetLastKnownLocation(provider);
				if (location != null)
				{
					newLocation(location);
				}
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("GPS error: " + ex.Message);
            }
        }

        public void apagarGPS()
        {
            if (locationManager != null)
            {
                try
                {
                    locationManager.RemoveUpdates(this);
                    System.Diagnostics.Debug.WriteLine("Activando GPS");
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(nameof(apagarGPS) + " error: " + ex.Message);
                }
            }
        }

        public void OnProviderDisabled(string provider)
        {
            
        }

        public void OnProviderEnabled(string provider)
        {
            
        }

        public void OnStatusChanged(string provider, [GeneratedEnum] Availability status, Bundle extras)
        {
            
        }
    }
}
