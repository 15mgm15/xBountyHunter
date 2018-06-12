using System;

using Xamarin.Forms;
using Xamarin.Forms.Maps;
using xBountyHunterShared.Models;

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
            Map capturadosMap = new Map(span);
            capturadosMap.MapType = MapType.Street;
            capturadosMap.IsShowingUser = false;

            Pin pin = new Pin();
            pin.Type = PinType.Place;
            pin.Position = pos;
            pin.Label = fugitivo.Name;
            capturadosMap.Pins.Add(pin);

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

