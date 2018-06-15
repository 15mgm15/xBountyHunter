using System;
using Xamarin.Forms;
using xBountyHunterShared.CustomRenderers;
using xBountyHunterShared.iOS.CustomRenderers;
using Xamarin.Forms.Maps.iOS;
using MapKit;
using Xamarin.Forms.Platform.iOS;
using UIKit;

[assembly: ExportRenderer(typeof(CustomMap), typeof(CustomMapiOS))]
namespace xBountyHunterShared.iOS.CustomRenderers
{
    public class CustomMapiOS : MapRenderer
    {
        MKCircleRenderer circleRenderer;

        protected override void OnElementChanged(ElementChangedEventArgs<View> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null)
            {
                var nativeMap = Control as MKMapView;
                if(nativeMap != null)
                {
                    nativeMap.RemoveOverlays(nativeMap.Overlays);
                    nativeMap.OverlayRenderer = null;
                    circleRenderer = null;
                }
            }

            if(e.NewElement != null)
            {
                var formsMap = (CustomMap)e.NewElement;
                var nativeMap = Control as MKMapView;
                var circle = formsMap.Circle;

                nativeMap.OverlayRenderer = GetOverlayRenderer;

                var circleOverlay = MKCircle.Circle(new CoreLocation.CLLocationCoordinate2D(circle.Position.Latitude, circle.Position.Longitude), circle.Radious);
            }
        }

        MKOverlayRenderer GetOverlayRenderer(MKMapView mapView, IMKOverlay overlayWrapper)
        {
            if(circleRenderer == null && !Equals(overlayWrapper, null))
            {
                var overlay = ObjCRuntime.Runtime.GetNSObject(overlayWrapper.Handle) as IMKOverlay;
                circleRenderer = new MKCircleRenderer(overlay as MKCircle)
                {
                    FillColor = UIColor.Red,
                    Alpha = 0.4f
                };
            }
            return circleRenderer;
        }
    }
}
