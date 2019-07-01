using System;
using System.Collections.Generic;
using System.Text;

namespace SplashyApp.Models
{
  public  class TemperaturePressureHumidity : Device
    {
        Queue<int> temperaturequeue = new Queue<int>();
        Queue<int> pressurequeue = new Queue<int>();
        Queue<int> humidityqueue = new Queue<int>();
   

        public int AvarageTemp { get; set; }
        public int AvaragePress { get; set; }
        public int AvarageHumid { get; set; }

        public string Temperature { get; set; }
        public string Pressure { get; set; }
        public string Humidity { get; set; }
        public string Altitude { get; set; }
        public DateTime Time { get; set; }

        public string Pin2 { get; set; }
        public string Pin3 { get; set; }
        public string Pin4 { get; set; }


        public void UpdateTemperature(string mss)
        {
            Temperature = mss;
           // temperaturequeue.Enqueue(Convert.ToSingle(Temperature));//    
            temperaturequeue.Enqueue(Int32.Parse(Temperature));//           

        }
        public void UpdatePressure(string mss)
        {
            Pressure = mss;
            pressurequeue.Enqueue(Int32.Parse(Pressure));
        }
        public void UpdateHumidity(string mss)
        {
            Humidity = mss;
            humidityqueue.Enqueue(Int32.Parse(Humidity));
        }
        public void UpdateAltitude(string mss)
        {
            Altitude = mss;
           // humidityqueue.Enqueue(Int32.Parse(Humidity));
        }
        

        public void UpdateState(string mss)
        {

            string[] data = ParseMessage(mss);

            Temperature = data[0];
            Pressure = data[1];
            Humidity = data[2];
            temperaturequeue.Enqueue(Int32.Parse(Temperature));//
            pressurequeue.Enqueue(Int32.Parse(Pressure));
            humidityqueue.Enqueue(Int32.Parse(Humidity));
        }

        public int UpdateConfig(string mss, string Topic)
        {
            //int res = base.UpdateConfig(mss, Topic);

            try
            {
                if (base.UpdateConfig(mss, Topic) == 1)
                {
                    return 1;
                }
                string[] data = ParseMessage(mss);
                Pin2 = data[2];
                Pin3 = data[3];
                Pin4 = data[4];
              
            }
            catch (Exception)
            {

               // throw;
            }

            return 0;
        }


        public void AvarageT()
        {
          //  AvarageTemp = ((temperaturequeue.Dequeue() + temperaturequeue.Peek() + temperaturequeue.Peek()) / 3);
        }

        public void AvarageP()
        {
           // AvaragePress = ((pressurequeue.Dequeue() + pressurequeue.Peek() + pressurequeue.Peek()) / 3);
        }

        public void AvarageH()
        {
          //  AvarageHumid = ((humidityqueue.Dequeue() + humidityqueue.Peek() + humidityqueue.Peek()) / 3);
        }

        internal static object ElementAt(object i)
        {
            throw new NotImplementedException();
        }
    }
}
