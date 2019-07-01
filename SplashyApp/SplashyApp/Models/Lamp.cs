using System;
using System.Collections.Generic;
using System.Text;

namespace SplashyApp.Models
{
   public class Lamp : Device
    {
        public bool IsOn { get; set; } = false;//send from server mosquitto
        public DateTime TimeOn { get; set; } // когда данные пришли                                           

        public string State { get; set; }
        public bool boolState { get; set; }
        

        public Lamp() { }

        //public void UpdateSchedule(string result, string schedule_id)
        //{

        //    string[] words = ParseMessage(result);

        //    try
        //    {
        //        // string id = words[0];//trigger name
        //        string action = words[1];// что делать
        //                                 //  string cronexpression = words[2]; //-+
        //                                 //  string command = words[3]; //-+

        //        if (action == "create")
        //        {
        //            Create(schedule_id, Id, words[2], words[3]);

        //        }
        //        else if (action == "update")
        //        {

        //            Console.WriteLine(" Date:" + DateTime.Now + " Update schedule  Class:MqttServer line:38");
        //            Deletee(schedule_id, Id);
        //            Create(schedule_id, Id, words[2], words[3]);

        //        }
        //        else if (action == "delete")
        //        {

        //            Deletee(schedule_id, Id);
        //        }
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //}

        //void Create(string id, string lamp_id, string cronexpression, string command)
        //{
        //    Console.WriteLine(" Date:" + DateTime.Now + " Create schedule  Class:Lamp line:57");

        //    // define the job and tie it to our HelloJob class
        //    IJobDetail job = JobBuilder.Create<SendMqttMessage>()
        //        .WithIdentity(id, "lamp" + lamp_id)
        //        .UsingJobData("topicname", "devices/lamps/" + lamp_id + "/command")
        //        .UsingJobData("message", command)
        //        .Build();


        //    Console.WriteLine(" Date:" + DateTime.Now + " Create job  Class:Lamp line:60");

        //    // Trigger the job to run now, and then every 40 seconds
        //    ITrigger trigger = TriggerBuilder.Create()
        //         .WithIdentity(id, "lamp" + lamp_id)
        //         .WithCronSchedule(cronexpression)   //("0 0/2 8-17 * * ?")
        //                                             //.ForJob("myJob", "group1")
        //          .Build();

        //    Console.WriteLine(" Date:" + DateTime.Now + " Create trigger  Class:Lamp line:70");

        //    Program.sched.ScheduleJob(job, trigger);
        //    Console.WriteLine(" Date:" + DateTime.Now + " ScheduleJob  Class:Lamp line:78");
        //    //   Program.sched.RescheuleJob(new TriggerKey(id, lamp_id), trigger1);
        //}


        //void Deletee(string id, string lamp_id)
        //{
        //    Program.sched.UnscheduleJob(new TriggerKey(id, "lamp" + lamp_id));//group
        //    Console.WriteLine(" Date:" + DateTime.Now + " Delete schedule  Class:Lamp line:86");

        //    MqttServer.client.Publish("/devices/" + Type + "s/" + Id + "/schedules/" + id, // topic
        //    Encoding.UTF8.GetBytes(""), // message body
        //    MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, // QoS level
        //    true);
        //}

    }
}
