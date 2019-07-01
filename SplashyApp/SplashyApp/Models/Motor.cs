using System;
using System.Collections.Generic;
using System.Text;

namespace SplashyApp.Models
{
   public class Motor : Device
    {
        public string Percent { get; set; }
        public DateTime Time { get; set; }

        public string PinOnMotorShield { get; set; }
    }
}
