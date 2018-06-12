using System;
using System.Collections.Generic;

namespace xBountyHunterShared.DependencyServices
{
    public interface IGetLocation
    {
        Dictionary<string, string> getLocation();
        void apagarGPS();
        void activarGPS();
    }
}
