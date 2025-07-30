using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Mediator.Commands.WeatherStationCommands
{
    public class CreateWeatherStationWithDataCommand : IRequest
    {
        // Station bilgileri
        public string StationName { get; set; }
        public string Location { get; set; }
        public string Indicator { get; set; } = "Otomatik İstasyon";

        // Hava durumu bilgileri
        public double Temperature { get; set; }
        public double Humidity { get; set; }
        public double WindSpeed { get; set; }
        public string WindDirection { get; set; }
        public double Pressure { get; set; }
        public string Description { get; set; }
    }

}
