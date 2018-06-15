using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace xBountyHunterShared.ViewModels
{
    public class fugitivosVm : BaseViewModel
    {
        public fugitivosVm()
        {
            Device.StartTimer(new TimeSpan(0, 0, 30), () =>
            {
                try
                {
                    Task.Run(async () => 
                    {
                        Extras.webServicesConnection ws = new Extras.webServicesConnection(Application.Current.MainPage);
                        await ws.connectGET();
                    });
                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
            });
        }
    }
}
