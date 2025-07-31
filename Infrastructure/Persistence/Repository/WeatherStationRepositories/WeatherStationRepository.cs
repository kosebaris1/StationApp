using Application.Interfaces.WeatherStationInterface;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repository.WeatherStationRepositories
{
    public class WeatherStationRepository : IWeatherStationRepository
    {
        private readonly ApplicationDbContext _context;

        public WeatherStationRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<(WeatherStation Station, WeatherData LastData)>> GetStationsWithLastDataAsync()
        {
            var stations = await _context.Set<WeatherStation>().ToListAsync();

            var result = stations.Select(station =>
            {
                var lastData = _context.Set<WeatherData>()
                    .Where(x => x.WeatherStationId == station.Id)
                    .OrderByDescending(x => x.Timestamp)
                    .FirstOrDefault();

                return (station, lastData);
            }).ToList();

            return result;
        }

       
    }
}
