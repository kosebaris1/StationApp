using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Mediator.Commands.WeatherDataCommands
{
    public class CreateWeatherDataCommand : IRequest
    {
        public int WeatherStationId { get; set; }

        public DateTime Timestamp { get; set; }
        public double Temperature { get; set; }  // Sıcaklık
        public double Humidity { get; set; }     // Nem
        public double WindSpeed { get; set; }    // Rüzgar Hızı
        public string WindDirection { get; set; }    // Rüzgar yönü
        public double pressure { get; set; }    // Basınç

        public string Description { get; set; }  // "Rüzgarlı", "Sisli" vb.
    }
}
