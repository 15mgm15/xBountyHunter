using System;
using Xamarin.Forms.Maps;
namespace xBountyHunterShared.CustomRenderers
{
    public class MapCircle
    {
        public Position Position { get; set; }
        public double Radious { get; set; }
    }
    
    public class CustomMap : Map
    {
        public CustomMap(MapSpan span)
        {
            //new Map(span);
        }

        public MapCircle Circle { get; set; }
    }
}
