using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.WeatherDataInterface
{
    public interface IWeatherDataRepository
    {
        Task<List<WeatherData>> GetAllWeatherDataByStationId(int id);

    }
}
