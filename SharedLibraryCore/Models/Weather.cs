using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLibrary.Models
{
    public class Weather
    {
        public string Temperature { get; set; }
        public string Humidity { get; set; }

        public Weather()
        {

        }
        public Weather(string temperature, string humidity)
        {
            Temperature = temperature;
            Humidity = humidity;
        }
    }
}
