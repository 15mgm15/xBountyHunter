using System;
using System.Threading.Tasks;

namespace xBountyHunterShared.DependencyServices
{
    public interface ICamera
    {
        Task<string> TakePhoto();
    }
}
