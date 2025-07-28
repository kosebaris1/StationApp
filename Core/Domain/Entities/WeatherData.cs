using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class WeatherData : Entity
    {
        public int Id { get; set; }
        public int WeatherStationId { get; set; }
        public WeatherStation WeatherStation { get; set; }

        public DateTime Timestamp { get; set; }
        public double Temperature { get; set; }  // Sıcaklık
        public double Humidity { get; set; }     // Nem
        public double WindSpeed { get; set; }    // Rüzgar Hızı
        public string Description { get; set; }  // "Rüzgarlı", "Sisli" vb.
    }

}
