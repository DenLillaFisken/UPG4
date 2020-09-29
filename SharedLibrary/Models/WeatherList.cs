using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLibrary.Models
{
    public class WeatherList : ObservableCollection<Weather>
    {
        public WeatherList()
        {

        }
        public WeatherList(string weather1, string weather2)
        {
            Add(new Weather(weather1, weather2));
        }
    }
}
