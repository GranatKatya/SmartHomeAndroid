using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;

namespace SplashyApp.Models
{
    public class Device: INotifyPropertyChanged
    {
        public string name;//{ get; set; }
     //   private Boolean _selected;
        public string Name
        {
            get
            {
                return name;
            }
            set
            {

                name = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("Name"));
            }

        }

        public string Pin { get; set; }
        public string Id { get; set; }
        public string type { get; set; }
        public string Type
        {
            get
            {
                return type;
            }
            set
            {

                type = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("Type"));
            }

        }
        public override string ToString()
        {
            return "Name: " + Name + " Type: " + Type;
        }
        public Room InRoom { get; set; } = new Room();


        public int UpdateConfig(string result, string topic)
        {
            string[] parsedmessage = ParseMessage(result);

            Name = parsedmessage[0];
            if (parsedmessage[1].IndexOf("delete") == -1)// name pin/delete
            {
                Pin = parsedmessage[1];
            }
            else
            {

                //  Delete(topic);
                return 1;

            }
            return 0;
        }

        public string[] ParseMessage(string result)
        {

            string[] separators = { "|" };
            return result.Split(separators, StringSplitOptions.RemoveEmptyEntries);
        }











        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }







        //public void Delete(string topic)
        //{
        //    Task<IReadOnlyCollection<TriggerKey>> triggers = Program.sched.GetTriggerKeys(GroupMatcher<TriggerKey>.GroupEquals("lamp" + Id));

        //    foreach (var trigger in triggers.Result)
        //    {
        //        MqttServer.client.Publish("/devices/" + Type + "s/" + Id + "/schedules/" + trigger.Name, // topic
        //        Encoding.UTF8.GetBytes(""), // message body
        //        MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, // QoS level
        //        true);
        //    }
        //    Program.sched.UnscheduleJobs(triggers.Result);

        //    MqttServer.client.Publish("/devices/" + Type + "s/" + Id + "/config", // topic
        //   Encoding.UTF8.GetBytes(""), // message body
        //   MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, // QoS level
        //   true);
        //}

    }
}
