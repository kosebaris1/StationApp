using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class WeatherStation : Entity
    {
        public int Id { get; set; }
        public string StationName { get; set; }
        public string Indicator { get; set; } // örn: "Hava Kalitesi", "Nem Ölçer" gibi
        public string Location { get; set; }
        public ICollection<WeatherData> WeatherDataEntries { get; set; }
    }

}
