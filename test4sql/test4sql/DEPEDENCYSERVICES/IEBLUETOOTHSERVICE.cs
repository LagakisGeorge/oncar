using System.Collections.Generic;
using System.Threading.Tasks;

namespace oncar.DEPEDENCYSERVICES
{
    public interface IBlueToothService
    {
        IList<string> GetDeviceList();
        Task Print(string deviceName, string text);
    }
}
