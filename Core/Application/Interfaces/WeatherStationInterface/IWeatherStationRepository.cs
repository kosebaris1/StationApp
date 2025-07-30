using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.WeatherStationInterface
{
    public interface IWeatherStationRepository
    {
        Task<List<(WeatherStation Station, WeatherData LastData)>> GetStationsWithLastDataAsync();
    }
}
