using System;

using Xamarin.Forms;
using Xamarin.Forms.Maps;
using xBountyHunterShared.Models;
using xBountyHunterShared.CustomRenderers;

namespace xBountyHunterShared.Views
{
    public class mapPage : ContentPage
    {
        public mapPage(mFugitivos fugitivo)
        {
            double lat = Convert.ToDouble(fugitivo.Lat);
            double lon = Convert.ToDouble(fugitivo.Lon);

            Position pos = new Position(lat, lon);
            MapSpan span = MapSpan.FromCenterAndRadius(pos, Distance.FromKilometers(3));
            CustomMap capturadosMap = new CustomMap(span)
            {
                MapType = MapType.Street,
                IsShowingUser = false
            };

            Pin pin = new Pin
            {
                Type = PinType.Place,
                Position = pos,
                Label = fugitivo.Name
            };
            capturadosMap.Circle = new MapCircle{ Position = pos, Radious = 100 };
            capturadosMap.MoveToRegion(span);
            //capturadosMap.Pins.Add(pin);

            StackLayout verticalStackLayout = new StackLayout
            {
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand
            };

            verticalStackLayout.Children.Add(capturadosMap);
            Content = verticalStackLayout;

        }
    }
}

