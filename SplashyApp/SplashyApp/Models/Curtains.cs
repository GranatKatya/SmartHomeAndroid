using System;
using System.Collections.Generic;
using System.Text;

namespace SplashyApp.Models
{
   public class Сurtains : Device
    {
        public Motor motor { get; set; }
        public Motor motor2 { get; set; }
        public Сurtains()
        {

        }
        public bool IsOn { get; set; } = false;//send from server mosquitto
        public DateTime TimeOn { get; set; } // когда данные пришли       
        public string State { get; set; }

        public Сurtains(Motor m1, Motor m2)
        {
            motor = m1;
            motor2 = m2;
        }

        //public void UpdateSchedule(string result, string lamp_id)
        //{
        //    string[] words = ParseMessage(result);

        //    try
        //    {
        //        string id = words[0];//trigger name
        //        string action = words[1];// что делать
        //                                 //  string cronexpression = words[2]; //-+
        //                                 //  string command = words[3]; //-+

        //        if (action == "create")
        //        {
        //            Create(id, lamp_id, words[3], action);
        //        }
        //        else if (action == "update")
        //        {
        //            Deletee(id, lamp_id);
        //            Create(id, lamp_id, words[3], action);
        //        }
        //        else if (action == "delete")
        //        {
        //            Deletee(id, lamp_id);
        //        }
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //}







        //void Create(string id, string lamp_id, string cronexpression, string command)
        //{
        //    // define the job and tie it to our HelloJob class
        //    IJobDetail job = JobBuilder.Create<SendMqttMessage>()
        //        .WithIdentity(id, "curnains" + lamp_id)
        //        .UsingJobData("topicname", "devices/curnains/" + lamp_id + "/command")
        //        .UsingJobData("message", command)
        //        .Build();

        //    // Trigger the job to run now, and then every 40 seconds
        //    ITrigger trigger = TriggerBuilder.Create()
        //         .WithIdentity(id, "curnains" + lamp_id)
        //         .WithCronSchedule(cronexpression)   //("0 0/2 8-17 * * ?")
        //                                             // .ForJob("myJob", "group1")
        //          .Build();


        //    Program.sched.ScheduleJob(job, trigger);
        //    //   Program.sched.RescheduleJob(new TriggerKey(id, lamp_id), trigger1);
        //}


        //void Deletee(string id, string lamp_id)
        //{
        //    Program.sched.UnscheduleJob(new TriggerKey(id, "curnains" + lamp_id));//group
        //}
    }
}
