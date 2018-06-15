using System;
using Xamarin.Forms;
using xBountyHunterShared.CustomRenderers;
using xBountyHunterShared.Droid.CustomRenderers;
using Xamarin.Forms.Maps.Android;
using Android.Content;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Platform.Android;
using System.ComponentModel;
using Android.Gms.Maps.Model;

[assembly: ExportRenderer(typeof(CustomMap), typeof(CustomMapDroid))]
namespace xBountyHunterShared.Droid.CustomRenderers
{
    public class CustomMapDroid : MapRenderer
    {
        MapCircle circle;
        bool isDrawn;
        
        public CustomMapDroid(Context context) : base(context){}

        protected override void OnElementChanged(ElementChangedEventArgs<Map> e)
        {
            base.OnElementChanged(e);

            if(e.OldElement != null) {}

            if(e.NewElement != null)
            {
                var formsMap = (CustomMap)e.NewElement;
                circle = formsMap.Circle;
                Control.GetMapAsync(this);

            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if(e.PropertyName.Equals("VisibleRegion") && !isDrawn)
            {
                var circleOptions = new CircleOptions();
                circleOptions.InvokeCenter(new LatLng(circle.Position.Latitude, circle.Position.Longitude));
                circleOptions.InvokeRadius(circle.Radious);
                circleOptions.InvokeFillColor(0X66FF0000);
                circleOptions.InvokeStrokeColor(0X66FF0000);
                circleOptions.InvokeStrokeWidth(0);

                NativeMap.AddCircle(circleOptions);
                isDrawn = true;
            }
        }
    }
}
