using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.System.Profile;
using Xamarin.Forms;
using xBountyHunterShared.DependencyServices;
using xBountyHunterShared.UWP;

[assembly: Dependency(typeof(WindowsUDID))]

namespace xBountyHunterShared.UWP
{
    public class WindowsUDID : IUDID
    {
        public string getUDID()
        {
            var token = HardwareIdentification.GetPackageSpecificToken(null);
            var hardwareId = token.Id;
            var dataReader = Windows.Storage.Streams.DataReader.FromBuffer(hardwareId);

            byte[] bytes = new byte[hardwareId.Length];
            dataReader.ReadBytes(bytes);

            return BitConverter.ToString(bytes);

        }
    }
}
