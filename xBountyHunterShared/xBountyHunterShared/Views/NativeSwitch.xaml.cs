using System;
using System.Collections.Generic;

using Xamarin.Forms;
using xBountyHunterShared.ViewModels;

namespace xBountyHunterShared.Views
{
    public partial class NativeSwitch : ContentPage
    {
        public NativeSwitch()
        {
            InitializeComponent();
            BindingContext = new NativeSwitchViewModel();
        }
    }
}
