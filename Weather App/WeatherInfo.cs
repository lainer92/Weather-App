using System.Collections.Generic;

namespace Weather_App
{
    internal class WeatherInfo
    {
        public class weather
        {
            public string main { get; set; }
            public string description { get; set; }
            public string icon { get; set; }
        }

        public class main
        {
            public double temp { get; set; }
        }

        public class wind
        {
            public double speed { get; set; }
        }

        internal class root
        {
            public List<weather> weather { get; set; }
            public main main { get; set; }
            public wind wind { get; set; }
        }
    }
}
