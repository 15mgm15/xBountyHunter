using System;
using Xamarin.Forms;
using xBountyHunterShared;
using xBountyHunterShared.iOS;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(MiLabel), typeof(MiLabeliOS))]
namespace xBountyHunterShared.iOS
{
    public class MiLabeliOS : LabelRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Label> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                
            }
        }
    }
}
