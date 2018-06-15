using System;
using Android.Content;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using xBountyHunterShared.CustomRenderers;
using xBountyHunterShared.Droid.CustomRenderers;

[assembly: ExportRenderer(typeof(EntryCustomRenderer), typeof(EntryCustomRendererDriod))]
namespace xBountyHunterShared.Droid.CustomRenderers
{
    public class EntryCustomRendererDriod : EntryRenderer
    {
        public EntryCustomRendererDriod(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if(Control != null)
            {
                Control.SetBackgroundColor(Android.Graphics.Color.YellowGreen);
            }
        }
    }
}
