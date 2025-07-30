using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Mediator.Results.WeatherStationResults
{
    public class GetAllWeatherStationsWithLastDataQueryResult
    {
        public int Id { get; set; }
        public string StationName { get; set; }
        public string Indicator { get; set; }
        public string Location { get; set; }

        public WeatherDataResult LastWeatherData { get; set; } // EN GÜNCEL VERİ
    }

    public class WeatherDataResult
    {
        public double Temperature { get; set; }
        public double Humidity { get; set; }
        public double WindSpeed { get; set; }
        public string WindDirection { get; set; }
        public double Pressure { get; set; }
        public string Description { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
