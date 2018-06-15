using System;
namespace xBountyHunterShared.ViewModels
{
    public class NativeSwitchViewModel : BaseViewModel
    {
        bool _isSwitchOn;
        public bool IsSwitchOn
        {
            get
            {
                return _isSwitchOn;
            }
            set
            {
                if(_isSwitchOn != value)
                {
                    _isSwitchOn = value;
                    OnPropertyChanged(nameof(IsSwitchOn));
                }
            }
        }
    }
}
