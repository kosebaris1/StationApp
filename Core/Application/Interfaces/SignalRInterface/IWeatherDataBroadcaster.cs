using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.SignalRInterface
{
    public interface IWeatherDataBroadcaster
    {
        Task BroadcastWeatherDataAsync(object dto);
    }
}
