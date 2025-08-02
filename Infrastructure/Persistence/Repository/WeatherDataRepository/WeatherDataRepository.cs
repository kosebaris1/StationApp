using Application.Interfaces.WeatherDataInterface;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repository.WeatherDataRepository
{
    public class WeatherDataRepository : IWeatherDataRepository
    {
        private readonly ApplicationDbContext _context;

        public WeatherDataRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<WeatherData>> GetAllWeatherDataByStationId(int id)
        {
            return await _context.WeatherDatas
         .Where(w => w.WeatherStationId == id)
         .OrderByDescending(w => w.Timestamp) // 📌 en günceli en üste getirir
         .ToListAsync();
        }
    }
}
