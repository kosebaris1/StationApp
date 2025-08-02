using Application.Interfaces.SignalRInterface;
using Microsoft.AspNetCore.SignalR;
using Persistence.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Broadcasters
{
    public class WeatherDataBroadcaster : IWeatherDataBroadcaster
    {
        private readonly IHubContext<WeatherDataHub> _hubContext;

        public WeatherDataBroadcaster(IHubContext<WeatherDataHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task BroadcastWeatherDataAsync(object dto)
        {
            try
            {
                await _hubContext.Clients.All.SendAsync("StationWithDataAdded", dto);
                Console.WriteLine("SignalR: StationWithDataAdded mesajı gönderildi");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"SignalR broadcast hatası: {ex.Message}");
            }
        }
    }
}
