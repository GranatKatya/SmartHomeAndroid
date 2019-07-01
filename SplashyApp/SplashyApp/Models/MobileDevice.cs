using System;
using System.Collections.Generic;
using System.Text;

namespace SplashyApp.Models
{
    public class MobileDevice
    {
        public string Name { get; set; }
        public DateTime Time { get; set; }

        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }

        public Guid PersonalTopicName { get; set; } = new Guid();
        public string Message { get; set; }


        public void UpdateConfig(string mss)
        {
            string[] separators = { "|" };
            string[] data = mss.Split(separators, StringSplitOptions.RemoveEmptyEntries);

            Name = data[0];

        }


        public void SetPosition(string mss)
        {
            string[] separators = { "|" };
            string[] data = mss.Split(separators, StringSplitOptions.RemoveEmptyEntries);


            Time = DateTime.Now;
            Latitude = Decimal.Parse(data[1]);
            Longitude = Decimal.Parse(data[2]);

        }


    }
}
