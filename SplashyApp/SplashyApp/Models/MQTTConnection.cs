using SplashyApp.Views;
using SplashyApp.Views.AboutUS;
using SplashyApp.Views.Rooms;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;
using Xamarin.Forms;

namespace SplashyApp.Models
{

    public class MQTTConnection
    {




        MainPage mainPage;

     
        public static MqttClient client = null;
        public MQTTConnection(string brokerHostName, string username, string password, MainPage mp)
        {
            mainPage = mp;



            /// MqttClient client1 = new MqttClient("35.158.109.193");
            //client = new MqttClient(brokerHostName,
            //       MqttSettings.MQTT_BROKER_DEFAULT_SSL_PORT, true, new X509Certificate(Resources.m2mqtt_ca));


            //  client.ProtocolVersion = MqttProtocolVersion.Version_3_1;
            //  MqttClient client = new MqttClient("broker.hivemq.com");


            //byte code = client.Connect(Guid.NewGuid().ToString(), "server", "ri0Xohsuozahr2aB");
            byte code = 1;
            try
            {
                client = new MqttClient("35.158.109.193");
                code = client.Connect(Guid.NewGuid().ToString(), "server", "ri0Xohsuozahr2aB");//, false, 150);
            }
            catch (Exception)
            {
                // int i = 0;


                //   You can not connect to the broker
                throw;
            }

            // client.Connect(Guid.NewGuid().ToString());
            if (code == 0)
            {
                Console.WriteLine("Connection established  Class:MqttServer line:22");

            }

            //ClientId и IsConnected5
            string clid = client.ClientId;
            if (client.IsConnected)
            {
                Console.WriteLine("Connected");
            }

            //  client.CleanSession = true;

            client.MqttMsgPublished += client_MqttMsgPublished;
            //ushort msgId = client.Publish("/my_topic", // topic
            //   Encoding.UTF8.GetBytes("MyMessageBody"), // message body
            //   MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, // QoS level
            //   true); // retained
            //Console.WriteLine($"msgId = {msgId} ");




            try
            {

                ushort subMsgId = client.Subscribe(new string[] { "/devices/#", "/mobiledevice/#", "/rooms/#" },
                 new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE,
                          MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE,
                            MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE });
                Console.WriteLine("Subscribe on topic: /devices/#, /mobiledevice/# Class:MqttServer line:51");
                // Console.WriteLine($"subMsgId = {subMsgId}");
            }
            catch (Exception)
            {

                throw;
            }






            client.MqttMsgPublishReceived += client_MqttMsgPublishReceived;
            client.ConnectionClosed += client_ConnectionClosed;
        }

        public void Publish(string mytopic, string message, bool retain = false)
        {
            try
            {
                if (message == ("|") || (!String.IsNullOrEmpty(message)))
                {

                    ushort msgId = client.Publish("/" + mytopic, // topic
                     Encoding.UTF8.GetBytes(message), // message body
                     MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, // QoS level
                     retain); // retained
                    Console.WriteLine($"msgId = {msgId} ");

                    Console.WriteLine($"Publish in topic:{mytopic}  message:{message}  Class:MqttServer line:65");

                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void client_ConnectionClosed(object sender, EventArgs e)
        {
            //  Debug.WriteLine("Subscribed for id = " + e.MessageId);
            Console.WriteLine("Connection Closed class:MqttServer line:77");

            try
            {
                client = new MqttClient("35.158.109.193");
                client.Connect(Guid.NewGuid().ToString(), "server", "ri0Xohsuozahr2aB");//, false, 150);
            }
            catch (Exception)
            {
                //throw;
            }
        }

        public string[] ParseMessage(string mss)
        {

            string[] separators = { "|" };
            return mss.Split(separators, StringSplitOptions.RemoveEmptyEntries);
        }



        void client_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
        {

            Console.WriteLine("Received:" + Encoding.UTF8.GetString(e.Message) + " Class:MqttServer line:83");
            //  Debug.WriteLine("Received = " + Encoding.UTF8.GetString(e.Message) + " on topic " + e.Topic);

            //  client.;

            // Task.Run(() => MqttSendMessage(e));

            try
            {

                string message = System.Text.Encoding.UTF8.GetString(e.Message);
                Console.WriteLine(" class client_MqttMsgPublishReceived "  + " Class:MqttServer line:164");

                // string[] separators = { "/" };

                string[] topic = e.Topic.Split(new string[] { "/" }, StringSplitOptions.RemoveEmptyEntries);
                Console.WriteLine(" class client_MqttMsgPublishReceived " + " Class:MqttServer line:19");

                //string  lamp_ids = words[words.Length - 1];
                //string[] separators1 = { "p" };
                //string[] words1 = result.Split(separators1, StringSplitOptions.RemoveEmptyEntries);

                if (Encoding.UTF8.GetString(e.Message) == "")
                {
                    Console.WriteLine(" class client_MqttMsgPublishReceived " + " Class:MqttServer line:177");
                    return;
                }

                if (topic[0] == "devices")
                {
                    Console.WriteLine(" class client_MqttMsgPublishReceived " + " Class:MqttServer line:183");
                    if (topic[1] == "lamps")
                    {
                        string lamp_id = topic[2];
                        if (topic[3] == "schedules")
                        {
                            // arduino.Lamps[lamp_id].UpdateSchedule(message, topic[topic.Length - 1]);
                            if (mainPage.arduino.Lamps.ContainsKey(lamp_id))
                            {
                            }

                        }
                        else if (topic[3] == "config")
                        {
                            if (!mainPage.arduino.Lamps.ContainsKey(lamp_id))
                            {
                                Lamp l = new Lamp() { Id = lamp_id, Type = "lamp" };
                                mainPage.arduino.Lamps.Add(lamp_id, l);



                                string[] parsedmessage = ParseMessage(message);
                                //  Name = parsedmessage[0];


                                mainPage.arduino.MyTemp.Add(new Temp()
                                {
                                    // Name = mainPage.arduino.TemperaturePressureHumidity[TemperaturePressureHumidity.ElementAt(i).Key].Name,
                                    Name = parsedmessage[0],
                                    Key = l.Id,
                                    Selected = false,
                                    myDevice = l,
                                    MyRoom = l.InRoom
                                });

                                //Temp t = new Views.AboutUS.Temp()
                                //{
                                //    Key = lamp_id,
                                //    Selected = false,
                                //    myDevice = new Lamp() { Id = lamp_id, Type = "lamp" }
                                //};
                                //mainPage.arduino.MyTemp.Add(t);
                                //t.UpdateConfig(message);



                                // mainPage.arduino.MyTemp[t.Key].UpdateConfig(message, e.Topic);

                                //mainPage.arduino.MyTemp.FirstOrDefault();


                                //Temp temp = (from d in mainPage.arduino.MyTemp
                                //            where d.Key.Contains(lamp_id)
                                //            select d).FirstOrDefault();



                                //    mainPage.arduino.MyTemp = new ObservableCollection<Temp>(mainPage.arduino.MyTemp.OrderBy(p => p.Name));              // mainPage.arduino.MyTemp.OrderBy(p => p.Name).ToList();

                                //  mainPage.arduino.CreateDeviceList();
                            }


                            int del = mainPage.arduino.Lamps[lamp_id].UpdateConfig(message, e.Topic);
                            // mainPage.arduino.CreateDeviceList();
                            mainPage.arduino.UpdateInTemp(lamp_id, mainPage.arduino.Lamps[lamp_id]);

                            Console.WriteLine("Update lamp config  Class:MqttServer line:113");
                            if (del == 1)
                            {
                                //mainPage.arduino.DeleteDevInRoom(mainPage.arduino.Lamps[lamp_id], mainPage);//!!!!!!!!!!!!!!!!!!!!!


                              
                                mainPage.arduino.Lamps.Remove(lamp_id);
                                mainPage.arduino.DeleteFromTemp(lamp_id);



                                //mainPage.arduino.CreateDeviceList();
                                //MessagingCenter.Send<MQTTConnection>(this, "UpdateMyTemp");
                                //  mainPage.arduino.CreateDeviceList();
                            }
                        }
                        else if (topic[3] == "command")
                        {
                            if (mainPage.arduino.Lamps.ContainsKey(lamp_id))
                            {
                                
                                mainPage.arduino.Lamps[lamp_id].State = message;
                                if (mainPage.arduino.Lamps[lamp_id].State.IndexOf("on") > -1)
                                {
                                    mainPage.arduino.Lamps[lamp_id].boolState = true;
                                }
                                else if (mainPage.arduino.Lamps[lamp_id].State.IndexOf("off") > -1)
                                {
                                    mainPage.arduino.Lamps[lamp_id].boolState = false;
                                }
                                else
                                {
                                    mainPage.arduino.Lamps[lamp_id].boolState = false;
                                }
                                   
                                mainPage.arduino.UpdateInTemp(lamp_id, mainPage.arduino.Lamps[lamp_id]);
                                Console.WriteLine("set command state on lamp Class:MqttServer line:118");
                            }
                           
                        }
                    }
                    else if (topic[1] == "motionsensors")
                    {
                        string motor_id = topic[2];
                        if (topic[3] == "config")
                        {
                            // if (mainPage.arduino.MotionSensor == null)
                            if (!mainPage.arduino.MotionSensor.ContainsKey(motor_id))
                            {
                                MotionSensor m = new MotionSensor() { Id = motor_id, Type = "motionsensor" };
                                mainPage.arduino.MotionSensor.Add(motor_id, m);

                                string[] parsedmessage = ParseMessage(message);
                                //  Name = parsedmessage[0];


                                mainPage.arduino.MyTemp.Add(new Temp()
                                {
                                    // Name = mainPage.arduino.TemperaturePressureHumidity[TemperaturePressureHumidity.ElementAt(i).Key].Name,
                                    Name = parsedmessage[0],
                                    Key = m.Id,
                                    Selected = false,
                                    myDevice = m,
                                    MyRoom = m.InRoom
                                });
                                //MotionSensor
                                //  mainPage.arduino.MotionSensor = new MotionSensor() { Id = "1", Type = "motionsensor" };
                                //  mainPage.arduino.CreateDeviceList();
                            }
                            int del = mainPage.arduino.MotionSensor[motor_id].UpdateConfig(message, e.Topic);
                            //  mainPage.arduino.CreateDeviceList();
                            mainPage.arduino.UpdateInTemp(motor_id, mainPage.arduino.MotionSensor[motor_id]);
                            Console.WriteLine("Update lamp config  Class:MqttServer line:113");
                            if (del == 1)
                            {
                                mainPage.arduino.MotionSensor.Remove(motor_id);
                                mainPage.arduino.DeleteFromTemp(motor_id);
                               // mainPage.arduino.CreateDeviceList();
                                // mainPage.arduino.CreateDeviceList();
                            }
                        }
                        else if (topic[3] == "data")
                        {
                            if (mainPage.arduino.MotionSensor.ContainsKey(motor_id))
                            {
                                mainPage.arduino.MotionSensor[motor_id].IsActive = message;
                                mainPage.arduino.MotionSensor[motor_id].Time = DateTime.Now;

                                // done
                            }

                        }
                    }
                    else if (topic[1] == "temperatures")
                    {
                        Console.WriteLine(" class client_MqttMsgPublishReceived " + " Class:MqttServer line:287");

                        string temperature_id = topic[2];
                        if (topic[3] == "config")
                        {
                            Console.WriteLine(" class client_MqttMsgPublishReceived " + " Class:MqttServer line:293");

                            if (!mainPage.arduino.TemperaturePressureHumidity.ContainsKey(temperature_id))
                            {

                                Console.WriteLine(" class client_MqttMsgPublishReceived " + " Class:MqttServer line:297");

                                TemperaturePressureHumidity t = new TemperaturePressureHumidity() { Id = temperature_id, Type = "temperature" };

                                mainPage.arduino.TemperaturePressureHumidity.Add(temperature_id, t);


                                string[] parsedmessage = ParseMessage(message);
                                //  Name = parsedmessage[0];


                                mainPage.arduino.MyTemp.Add(new Temp()
                                {
                                    // Name = mainPage.arduino.TemperaturePressureHumidity[TemperaturePressureHumidity.ElementAt(i).Key].Name,
                                    Name = parsedmessage[0],
                                    Key = t.Id,
                                    Selected = false,
                                    myDevice = t,
                                    MyRoom = t.InRoom
                                });




                                //  mainPage.arduino.TemperaturePressureHumidity = new TemperaturePressureHumidity() { Id = "1", Type = "temperature" };
                                //    mainPage.arduino.CreateDeviceList();
                            }

                            Console.WriteLine(" class client_MqttMsgPublishReceived " + " Class:MqttServer line:307");


                            int del = mainPage.arduino.TemperaturePressureHumidity[temperature_id].UpdateConfig(message, e.Topic);
                            mainPage.arduino.UpdateInTemp(temperature_id, mainPage.arduino.TemperaturePressureHumidity[temperature_id]);

                            Console.WriteLine(" class client_MqttMsgPublishReceived " + " Class:MqttServer line:311");
                           // mainPage.arduino.CreateDeviceList();


                            Console.WriteLine("Update lamp config  Class:MqttServer line:113");
                            if (del == 1)
                            {

                                Console.WriteLine(" class client_MqttMsgPublishReceived " + " Class:MqttServer line:319");

                                mainPage.arduino.TemperaturePressureHumidity.Remove(temperature_id);

                                mainPage.arduino.DeleteFromTemp(temperature_id);
                                // mainPage.arduino.CreateDeviceList();

                                // mainPage.arduino.CreateDeviceList();
                            }

                        }
                        else if (topic[3] == "data")
                        {
                            Console.WriteLine(" class client_MqttMsgPublishReceived " + " Class:MqttServer line:329");

                            if (topic[4] == "temperature")
                            {
                                Console.WriteLine(" class client_MqttMsgPublishReceived " + " Class:MqttServer line:333");

                                if (mainPage.arduino.TemperaturePressureHumidity.ContainsKey(temperature_id))
                                {

                                    Console.WriteLine(" class client_MqttMsgPublishReceived " + " Class:MqttServer line:337");

                                    mainPage.arduino.TemperaturePressureHumidity[temperature_id].UpdateTemperature(message);
                                    mainPage.arduino.TemperaturePressureHumidity[temperature_id].Time = DateTime.Now;





                                    // mainPage.arduino.CreateDeviceList();    

                                    //  mainPage.arduino.CreateDeviceList();
                                }
                                Console.WriteLine(" class client_MqttMsgPublishReceived " + " Class:MqttServer line:344");
                            }
                            else if (topic[4] == "pressure")
                            {

                                Console.WriteLine(" class client_MqttMsgPublishReceived " + " Class:MqttServer line:350");

                                if (mainPage.arduino.TemperaturePressureHumidity.ContainsKey(temperature_id))
                                {
                                    Console.WriteLine(" class client_MqttMsgPublishReceived " + " Class:MqttServer line:345");


                                    mainPage.arduino.TemperaturePressureHumidity[temperature_id].UpdatePressure(message);
                                    mainPage.arduino.TemperaturePressureHumidity[temperature_id].Time = DateTime.Now;
                                    //  mainPage.arduino.CreateDeviceList();
                                }
                                Console.WriteLine(" class client_MqttMsgPublishReceived " + " Class:MqttServer line:31");
                            }
                            else if (topic[4] == "humidity")
                            {

                                Console.WriteLine(" class client_MqttMsgPublishReceived " + " Class:MqttServer line:366");

                                if (mainPage.arduino.TemperaturePressureHumidity.ContainsKey(temperature_id))
                                {
                                    Console.WriteLine(" class client_MqttMsgPublishReceived " + " Class:MqttServer line:370");

                                    mainPage.arduino.TemperaturePressureHumidity[temperature_id].UpdateHumidity(message);
                                    mainPage.arduino.TemperaturePressureHumidity[temperature_id].Time = DateTime.Now;
                                    // mainPage.arduino.CreateDeviceList();
                                }
                                Console.WriteLine(" class client_MqttMsgPublishReceived " + " Class:MqttServer line:376");
                            }
                            else if (topic[4] == "altitude")
                            {

                                Console.WriteLine(" class client_MqttMsgPublishReceived " + " Class:MqttServer line:380");

                                if (mainPage.arduino.TemperaturePressureHumidity.ContainsKey(temperature_id))
                                {
                                    Console.WriteLine(" class client_MqttMsgPublishReceived " + " Class:MqttServer line:384");

                                    mainPage.arduino.TemperaturePressureHumidity[temperature_id].UpdateAltitude(message);
                                    mainPage.arduino.TemperaturePressureHumidity[temperature_id].Time = DateTime.Now;
                                    // mainPage.arduino.CreateDeviceList();
                                }
                                Console.WriteLine(" class client_MqttMsgPublishReceived " + " Class:MqttServer line:390");
                            }


                        }
                    }
                    else if (topic[1] == "motors")
                    {
                        string motor_id = topic[2];
                        if (topic[3] == "config")
                        {
                            if (!mainPage.arduino.Motors.ContainsKey(motor_id))
                            {
                                Motor m = new Motor() { Id = motor_id, Type = "motor" };
                                mainPage.arduino.Motors.Add(motor_id,m);

                                
                                string[] parsedmessage = ParseMessage(message);
                                //  Name = parsedmessage[0];


                                mainPage.arduino.MyTemp.Add(new Temp()
                                {
                                    // Name = mainPage.arduino.TemperaturePressureHumidity[TemperaturePressureHumidity.ElementAt(i).Key].Name,
                                    Name = parsedmessage[0],
                                    Key = m.Id,
                                    Selected = false,
                                    myDevice = m,
                                    MyRoom = m.InRoom
                                });

                                //  mainPage.arduino.CreateDeviceList();
                            }

                            int del = mainPage.arduino.Motors[motor_id].UpdateConfig(message, e.Topic);
                            // mainPage.arduino.CreateDeviceList();
                            mainPage.arduino.UpdateInTemp(motor_id, mainPage.arduino.Motors[motor_id]);

                            Console.WriteLine("Update motor config  Class:MqttServer line:171");
                            if (del == 1)
                            {
                                mainPage.arduino.Motors.Remove(motor_id);
                                mainPage.arduino.DeleteFromTemp(motor_id);
                                //mainPage.arduino.CreateDeviceList();
                                // mainPage.arduino.CreateDeviceList();
                            }
                        }
                        else if (topic[2] == "data")
                        {
                            if (mainPage.arduino.Motors.ContainsKey(motor_id))
                            {

                                string[] parsedmessage = mainPage.arduino.Motors[motor_id].ParseMessage(message);

                                mainPage.arduino.Motors[motor_id].Percent = message;
                                mainPage.arduino.Motors[motor_id].Time = DateTime.Now;
                            }
                        }
                    }
                    else if (topic[1] == "curtains")
                    {
                        string curtains_id = topic[2];
                        if (topic[3] == "shedule")
                        {
                            if (mainPage.arduino.Motors.ContainsKey(curtains_id))
                            { }
                                // arduino.Сurtains[curtains_id].UpdateSchedule(message, curtains_id);
                            }
                        else if (topic[3] == "config")
                        {
                            if (!mainPage.arduino.Сurtains.ContainsKey(curtains_id))
                            {
                                Сurtains c = new Сurtains() { Id = curtains_id, Type = "curtain" };
                                mainPage.arduino.Сurtains.Add(curtains_id, c);

                                string[] parsedmessage = ParseMessage(message);
                        
                                mainPage.arduino.MyTemp.Add(new Temp()
                                {
                                    Name = parsedmessage[0],
                                    Key = c.Id,
                                    Selected = false,
                                    myDevice = c,
                                    MyRoom = c.InRoom
                                });


                                //  mainPage.arduino.CreateDeviceList();
                            }
                            int del = mainPage.arduino.Сurtains[curtains_id].UpdateConfig(message, e.Topic);
                            //   mainPage.arduino.CreateDeviceList();
                            mainPage.arduino.UpdateInTemp(curtains_id, mainPage.arduino.Сurtains[curtains_id]);

                            Console.WriteLine("Update curtains config  Class:MqttServer line:171");
                            if (del == 1)
                            {
                                mainPage.arduino.Сurtains.Remove(curtains_id);
                                mainPage.arduino.DeleteFromTemp(curtains_id);
                                // mainPage.arduino.CreateDeviceList();
                                // mainPage.arduino.CreateDeviceList();
                            }
                        }
                        else if (topic[3] == "command")
                        {
                            if (mainPage.arduino.Сurtains.ContainsKey(curtains_id))
                            {
                                mainPage.arduino.Сurtains[curtains_id].State = message;
                            }
                        }
                    }

                }
                else if (topic[0] == "mobiledevices")
                {
                    int dev_id = Int32.Parse(topic[topic.Length - 2]);

                    if (topic[2] == "config")
                    {
                        if (mainPage.mobileDevices.ElementAtOrDefault(dev_id) == null)
                        {
                            mainPage.mobileDevices.Add(new MobileDevice());
                        }
                        mainPage.mobileDevices[dev_id].UpdateConfig(message); // name
                                                                              //  mainPage.arduino.CreateDeviceList();


                        Console.WriteLine("Update mobileDevices config  Class:MqttServer line:171");
                        //if (del == 1)
                        //{
                        //    mainPage.arduino.mobileDevices.Remove(dev_id);
                        //    mainPage.arduino.CreateDeviceList();
                        //}
                    }
                    else if (topic[2] == "command")
                    {
                        if (mainPage.mobileDevices.ElementAtOrDefault(dev_id) == null)
                        {
                            mainPage.mobileDevices.RemoveAt(dev_id);  //delete
                        }
                    }
                    else if (topic[2] == "position")
                    {
                        if (mainPage.mobileDevices.ElementAtOrDefault(dev_id) == null)
                        {
                                
                            mainPage.mobileDevices[dev_id].SetPosition(message);// log  lat 
                        }
                    }
                    else if (topic[2] == "message")
                    {
                        if (mainPage.mobileDevices.ElementAtOrDefault(dev_id) == null)
                        {
                            mainPage.mobileDevices[dev_id].Message = message; // message
                        }
                    }

                }
                else if (topic[0] == "rooms")
                {
                    if (topic[1] == "kitchens")
                    {
                        string room_id = topic[2];
                        if (topic[3] == "config")
                        {
                            if (!mainPage.Rooms.Kitchens.ContainsKey(room_id))
                            {
                                Kitchen k = new Kitchen() { Key = room_id, Type = "kitchen", RoomDevice = new ObservableCollection<Temp>() };
                                mainPage.Rooms.Kitchens.Add(room_id, k);
                                // mainPage.Rooms.CreateRoomList();


                                mainPage.Rooms.MyRooms.Add(new SelectedRoom()
                                { 
                                    Key = k.Key,
                                    Selected = false,
                                    myRoom = k
                                });
                            }

                                int del = mainPage.Rooms.Kitchens[room_id].UpdateConfig(message, e.Topic, mainPage.arduino.MyTemp, "kitchen" , mainPage.NeedLoadedRoomsAndDevices);
                                mainPage.Rooms.UpdateInRoomList(room_id, mainPage.Rooms.Kitchens[room_id]);

                            // mainPage.Rooms.CreateRoomList();
                            Console.WriteLine("Update lamp config  Class:MqttServer line:404");
                                if (del == 1)
                                {
                                    mainPage.Rooms.Kitchens.Remove(room_id);
                                //mainPage.Rooms.CreateRoomList();
                                    mainPage.Rooms.DeleteFromRoomList(room_id);
                                }
                            
                        }
                        else if (topic[3] == "command")
                        {
                            if (mainPage.Rooms.Kitchens.ContainsKey(room_id))
                            {
                            }
                        }
                    }
                    else if (topic[1] == "toilets")
                    {
                        Debug.WriteLine("Entered  else if (topic[1] == toilets) :MqttServer line:421");
                        string toilet_id = topic[2];
                        if (topic[3] == "config")
                        {
                            Debug.WriteLine("Entered   if (topic[] == config) :MqttServer line:426");
                            if (!mainPage.Rooms.Toilets.ContainsKey(toilet_id))
                            {
                                Debug.WriteLine("Entered    if (!mainPage.Rooms.Toilets.ContainsKey(toilet_id))    :MqttServer line:428");
                                Toilet t = new Toilet() { Key = toilet_id, Type = "toilet", RoomDevice = new ObservableCollection<Temp>() };
                                mainPage.Rooms.Toilets.Add(toilet_id, t);
                                Debug.WriteLine("Entered  mainPage.Rooms.Toilets.Add() line:426");


                                mainPage.Rooms.MyRooms.Add(new SelectedRoom()
                                {
                                    Key = t.Key,
                                    Selected = false,
                                    myRoom = t
                                });
                            }
                                int del = mainPage.Rooms.Toilets[toilet_id].UpdateConfig(message, e.Topic, mainPage.arduino.MyTemp, "toilet", mainPage.NeedLoadedRoomsAndDevices);
                                  Debug.WriteLine("Entered  mainPage.Rooms.Toilets[toilet_id].UpdateConfig(message, e.Topic);    :MqttServer  line:426");
                                  mainPage.Rooms.UpdateInRoomList(toilet_id, mainPage.Rooms.Toilets[toilet_id]);

                            //mainPage.Rooms.CreateRoomList();
                            Debug.WriteLine("Entered      mainPage.Rooms.CreateRoomList();  :MqttServer  line:426");


                            //  Debug.WriteLine("Update lamp config  Class:MqttServer line:431");
                            if (del == 1)
                            {
                                Debug.WriteLine("Entered         if (del == 1)  :MqttServer  line:426");
                                mainPage.Rooms.Toilets.Remove(toilet_id);
                                Debug.WriteLine("Entered        mainPage.Rooms.Toilets.Remove(toilet_id);  :MqttServer  line:426");
                                mainPage.Rooms.DeleteFromRoomList(toilet_id);
                                // mainPage.Rooms.CreateRoomList();
                                Debug.WriteLine("Entered        mainPage.Rooms.CreateRoomList();  :MqttServer  line:426");
                            }
                            Debug.WriteLine("end       if (topic[] == config) :MqttServer   :MqttServer  line:426");
                        }
                     
                        else if (topic[3] == "command")
                        {
                            if (mainPage.Rooms.Toilets.ContainsKey(toilet_id))
                            {
                            }
                        }

                    }
                    else if (topic[1] == "hallways")
                    {
                        string hallway_id = topic[2];
                        if (topic[3] == "config")
                        {
                            if (!mainPage.Rooms.Hallwaies.ContainsKey(hallway_id))
                            {
                                Hallway h = new Hallway(){Key = hallway_id, Type = "hallway", RoomDevice = new ObservableCollection<Temp>() };
                                mainPage.Rooms.Hallwaies.Add(hallway_id, h);
                                
                                mainPage.Rooms.MyRooms.Add(new SelectedRoom()
                                {
                                    Key = h.Key,
                                    Selected = false,
                                    myRoom = h
                                });
                            }
                                int del = mainPage.Rooms.Hallwaies[hallway_id].UpdateConfig(message, e.Topic, mainPage.arduino.MyTemp, "hallway", mainPage.NeedLoadedRoomsAndDevices);

                            mainPage.Rooms.UpdateInRoomList(hallway_id, mainPage.Rooms.Hallwaies[hallway_id]);

                            //mainPage.Rooms.CreateRoomList();
                            Console.WriteLine("Update lamp config  Class:MqttServer line:431");
                                if (del == 1)
                                {
                                    mainPage.Rooms.Hallwaies.Remove(hallway_id);
                                //   mainPage.Rooms.CreateRoomList();
                                mainPage.Rooms.DeleteFromRoomList(hallway_id);
                            }
                            
                        }
                        else if (topic[3] == "command")
                        {
                            if (mainPage.Rooms.Hallwaies.ContainsKey(hallway_id))
                            {
                            }
                        }
                    }
                    else if (topic[1] == "livingrooms")
                    {
                        string livingroom_id= topic[2];
                        if (topic[3] == "config")
                        {
                            if (!mainPage.Rooms.LivingRooms.ContainsKey(livingroom_id))
                            {
                                LivingRoom l = new LivingRoom() { Key = livingroom_id, Type = "livingroom", RoomDevice = new ObservableCollection<Temp>() };
                                mainPage.Rooms.LivingRooms.Add(livingroom_id, l);

                                mainPage.Rooms.MyRooms.Add(new SelectedRoom()
                                {
                                    Key = l.Key,
                                    Selected = false,
                                    myRoom = l
                                });
                            }
                                int del = mainPage.Rooms.LivingRooms[livingroom_id].UpdateConfig(message, e.Topic, mainPage.arduino.MyTemp, "livingroom", mainPage.NeedLoadedRoomsAndDevices);
                            // mainPage.Rooms.CreateRoomList();
                            mainPage.Rooms.UpdateInRoomList(livingroom_id, mainPage.Rooms.LivingRooms[livingroom_id]);
                            Console.WriteLine("Update lamp config  Class:MqttServer line:431");
                                if (del == 1)
                                {
                                    mainPage.Rooms.LivingRooms.Remove(livingroom_id);
                                  //  mainPage.Rooms.CreateRoomList();
                                    mainPage.Rooms.DeleteFromRoomList(livingroom_id);
                            }
                            
                        }
                        else if (topic[3] == "command")
                        {
                            if (mainPage.Rooms.LivingRooms.ContainsKey(livingroom_id))
                            {
                            }
                        }
                    }
                    else if (topic[1] == "bedrooms")
                    {
                        string bedroom_id = topic[2];
                        if (topic[3] == "config")
                        {
                            if (!mainPage.Rooms.Bedrooms.ContainsKey(bedroom_id))
                            {
                                Bedroom b = new Bedroom() { Key = bedroom_id, Type = "bedroom", RoomDevice = new ObservableCollection<Temp>() };
                                mainPage.Rooms.Bedrooms.Add(bedroom_id, b);


                                mainPage.Rooms.MyRooms.Add(new SelectedRoom()
                                {
                                    Key = b.Key,
                                    Selected = false,
                                    myRoom = b
                                });
                            }
                                int del = mainPage.Rooms.Bedrooms[bedroom_id].UpdateConfig(message, e.Topic, mainPage.arduino.MyTemp, "bedroom", mainPage.NeedLoadedRoomsAndDevices);
                            // mainPage.Rooms.CreateRoomList();
                            mainPage.Rooms.UpdateInRoomList(bedroom_id, mainPage.Rooms.Bedrooms[bedroom_id]);
                            Console.WriteLine("Update lamp config  Class:MqttServer line:431");
                                if (del == 1)
                                {
                                    mainPage.Rooms.Bedrooms.Remove(bedroom_id);
                                // mainPage.Rooms.CreateRoomList();
                                mainPage.Rooms.DeleteFromRoomList(bedroom_id);
                            }
                            
                        }
                        else if (topic[3] == "command")
                        {
                            if (mainPage.Rooms.Bedrooms.ContainsKey(bedroom_id))
                            {
                            }
                        }
                    }
                    else if (topic[1] == "balconys")
                    {
                        string balcony_id = topic[2];
                        if (topic[3] == "config")
                        {
                            if (!mainPage.Rooms.Balconies.ContainsKey(balcony_id))
                            {
                                Balcony b = new Balcony() { Key = balcony_id, Type = "balcony", RoomDevice = new ObservableCollection<Temp>() };
                                mainPage.Rooms.Balconies.Add(balcony_id, b);


                                mainPage.Rooms.MyRooms.Add(new SelectedRoom()
                                {
                                    Key = b.Key,
                                    Selected = false,
                                    myRoom = b
                                });
                            }
                                int del = mainPage.Rooms.Balconies[balcony_id].UpdateConfig(message, e.Topic, mainPage.arduino.MyTemp, "balcony", mainPage.NeedLoadedRoomsAndDevices);
                            // mainPage.Rooms.CreateRoomList();
                            mainPage.Rooms.UpdateInRoomList(balcony_id, mainPage.Rooms.Balconies[balcony_id]);
                            Console.WriteLine("Update lamp config  Class:MqttServer line:431");
                                if (del == 1)
                                {
                                    mainPage.Rooms.Balconies.Remove(balcony_id);
                                //  mainPage.Rooms.CreateRoomList();
                                mainPage.Rooms.DeleteFromRoomList(balcony_id);
                            }
                            
                        }
                        else if (topic[3] == "command")
                        {
                            if (mainPage.Rooms.Balconies.ContainsKey(balcony_id))
                            {
                            }
                        }
                    }
                    else if (topic[1] == "porchs")
                    {
                        string porch_id = topic[2];
                        if (topic[3] == "config")
                        {
                            if (!mainPage.Rooms.Porches.ContainsKey(porch_id))
                            {
                                Porch p = new Porch() { Key = porch_id, Type = "porch", RoomDevice = new ObservableCollection<Temp>() };
                                mainPage.Rooms.Porches.Add(porch_id, p);


                                mainPage.Rooms.MyRooms.Add(new SelectedRoom()
                                {
                                    Key = p.Key,
                                    Selected = false,
                                    myRoom = p
                                });
                            }
                                int del = mainPage.Rooms.Porches[porch_id].UpdateConfig(message, e.Topic, mainPage.arduino.MyTemp, "porch", mainPage.NeedLoadedRoomsAndDevices);
                            // mainPage.Rooms.CreateRoomList();
                            mainPage.Rooms.UpdateInRoomList(porch_id, mainPage.Rooms.Porches[porch_id]);
                            Console.WriteLine("Update lamp config  Class:MqttServer line:431");
                                if (del == 1)
                                {
                                    mainPage.Rooms.Porches.Remove(porch_id);
                                  //  mainPage.Rooms.CreateRoomList();
                                mainPage.Rooms.DeleteFromRoomList(porch_id);
                            }
                            
                        }
                        else if (topic[3] == "command")
                        {
                            if (mainPage.Rooms.Porches.ContainsKey(porch_id))
                            {
                            }
                        }
                    }
                }
            }   

            catch (Exception ex)
            {

                throw;
            }
        }




        public void MqttSendMessage(MqttMsgPublishEventArgs e) {

            try
            {

                string message = System.Text.Encoding.UTF8.GetString(e.Message);

                // string[] separators = { "/" };

                string[] topic = e.Topic.Split(new string[] { "/" }, StringSplitOptions.RemoveEmptyEntries);

                //string  lamp_ids = words[words.Length - 1];
                //string[] separators1 = { "p" };
                //string[] words1 = result.Split(separators1, StringSplitOptions.RemoveEmptyEntries);

                if (Encoding.UTF8.GetString(e.Message) == "")
                {
                    return;
                }

                if (topic[0] == "devices")
                {
                    if (topic[1] == "lamps")
                    {
                        string lamp_id = topic[2];
                        if (topic[3] == "schedules")
                        {
                            // arduino.Lamps[lamp_id].UpdateSchedule(message, topic[topic.Length - 1]);


                        }
                        else if (topic[3] == "config")
                        {
                            if (!mainPage.arduino.Lamps.ContainsKey(lamp_id))
                            {
                                mainPage.arduino.Lamps.Add(lamp_id, new Lamp() { Id = lamp_id, Type = "lamp" });

                            
                               

                                //Temp t = new Views.AboutUS.Temp()
                                //{
                                //    Key = lamp_id,
                                //    Selected = false,
                                //    myDevice = new Lamp() { Id = lamp_id, Type = "lamp" }
                                //};
                                //mainPage.arduino.MyTemp.Add(t);
                                //t.UpdateConfig(message);



                                // mainPage.arduino.MyTemp[t.Key].UpdateConfig(message, e.Topic);

                                //mainPage.arduino.MyTemp.FirstOrDefault();


                                //Temp temp = (from d in mainPage.arduino.MyTemp
                                //            where d.Key.Contains(lamp_id)
                                //            select d).FirstOrDefault();



                                //    mainPage.arduino.MyTemp = new ObservableCollection<Temp>(mainPage.arduino.MyTemp.OrderBy(p => p.Name));              // mainPage.arduino.MyTemp.OrderBy(p => p.Name).ToList();

                                //  mainPage.arduino.CreateDeviceList();
                            }


                            int del = mainPage.arduino.Lamps[lamp_id].UpdateConfig(message, e.Topic);
                            Console.WriteLine("Update lamp config  Class:MqttServer line:113");
                            if (del == 1)
                            {
                                mainPage.arduino.Lamps.Remove(lamp_id);
                                //MessagingCenter.Send<MQTTConnection>(this, "UpdateMyTemp");
                              //  mainPage.arduino.CreateDeviceList();
                            }
                        }
                        else if (topic[3] == "command")
                        {
                            mainPage.arduino.Lamps[lamp_id].State = message;
                            Console.WriteLine("set command state on lamp Class:MqttServer line:118");
                        }
                    }
                    else if (topic[1] == "motionsensors")
                    {
                        string motor_id = topic[2];
                        if (topic[3] == "config")
                        {
                            // if (mainPage.arduino.MotionSensor == null)
                            if (!mainPage.arduino.MotionSensor.ContainsKey(motor_id))
                            {
                                mainPage.arduino.MotionSensor.Add(motor_id, new MotionSensor() { Id = motor_id, Type = "motionsensor" });
                                //MotionSensor
                                //  mainPage.arduino.MotionSensor = new MotionSensor() { Id = "1", Type = "motionsensor" };
                              //  mainPage.arduino.CreateDeviceList();
                            }
                            int del = mainPage.arduino.MotionSensor[motor_id].UpdateConfig(message, e.Topic);

                            Console.WriteLine("Update lamp config  Class:MqttServer line:113");
                            if (del == 1)
                            {
                                mainPage.arduino.MotionSensor.Remove(motor_id);
                               // mainPage.arduino.CreateDeviceList();
                            }
                        }
                        else if (topic[3] == "data")
                        {
                            mainPage.arduino.MotionSensor[motor_id].IsActive = message;
                            mainPage.arduino.MotionSensor[motor_id].Time = DateTime.Now;

                        }
                    }
                    else if (topic[1] == "temperatures")
                    {
                        string temperature_id = topic[2];
                        if (topic[3] == "config")
                        {
                            if (!mainPage.arduino.TemperaturePressureHumidity.ContainsKey(temperature_id))
                            {
                                mainPage.arduino.TemperaturePressureHumidity.Add(temperature_id, new TemperaturePressureHumidity() { Id = temperature_id, Type = "temperature" });

                                //  mainPage.arduino.TemperaturePressureHumidity = new TemperaturePressureHumidity() { Id = "1", Type = "temperature" };
                            //    mainPage.arduino.CreateDeviceList();
                            }
                            mainPage.arduino.TemperaturePressureHumidity[temperature_id].UpdateConfig(message, e.Topic);
                        }
                        else if (topic[3] == "data")
                        {
                            mainPage.arduino.TemperaturePressureHumidity[temperature_id].UpdateState(message);
                            mainPage.arduino.TemperaturePressureHumidity[temperature_id].Time = DateTime.Now;
                          //  mainPage.arduino.CreateDeviceList();

                        }
                    }
                    else if (topic[1] == "motors")
                    {
                        string motor_id = topic[2];
                        if (topic[3] == "config")
                        {
                            if (!mainPage.arduino.Motors.ContainsKey(motor_id))
                            {
                                mainPage.arduino.Motors.Add(motor_id, new Motor() { Id = motor_id, Type = "motor" });
                              //  mainPage.arduino.CreateDeviceList();
                            }

                            int del = mainPage.arduino.Motors[motor_id].UpdateConfig(message, e.Topic);
                            Console.WriteLine("Update motor config  Class:MqttServer line:171");
                            if (del == 1)
                            {
                                mainPage.arduino.Motors.Remove(motor_id);
                               // mainPage.arduino.CreateDeviceList();
                            }
                        }
                        else if (topic[2] == "data")
                        {
                            string[] parsedmessage = mainPage.arduino.Motors[motor_id].ParseMessage(message);

                            mainPage.arduino.Motors[motor_id].Percent = message;
                            mainPage.arduino.Motors[motor_id].Time = DateTime.Now;
                        }
                    }
                    else if (topic[1] == "curtains")
                    {
                        string curtains_id = topic[2];
                        if (topic[3] == "shedule")
                        {
                            // arduino.Сurtains[curtains_id].UpdateSchedule(message, curtains_id);
                        }
                        else if (topic[3] == "config")
                        {
                            if (!mainPage.arduino.Сurtains.ContainsKey(curtains_id))
                            {

                                mainPage.arduino.Сurtains.Add(curtains_id, new Сurtains() { Id = curtains_id, Type = "curtain" });
                              //  mainPage.arduino.CreateDeviceList();
                            }
                            int del = mainPage.arduino.Сurtains[curtains_id].UpdateConfig(message, e.Topic);

                            Console.WriteLine("Update curtains config  Class:MqttServer line:171");
                            if (del == 1)
                            {
                                mainPage.arduino.Сurtains.Remove(curtains_id);
                               // mainPage.arduino.CreateDeviceList();
                            }
                        }
                        else if (topic[3] == "command")
                        {
                            mainPage.arduino.Сurtains[curtains_id].State = message;
                        }
                    }

                }
                else if (topic[0] == "mobiledevices")
                {
                    int dev_id = Int32.Parse(topic[topic.Length - 2]);

                    if (topic[2] == "config")
                    {
                        if (mainPage.mobileDevices.ElementAtOrDefault(dev_id) == null)
                        {
                            mainPage.mobileDevices.Add(new MobileDevice());
                        }
                        mainPage.mobileDevices[dev_id].UpdateConfig(message); // name
                      

                        Console.WriteLine("Update mobileDevices config  Class:MqttServer line:171");
                        //if (del == 1)
                        //{
                        //    mainPage.arduino.mobileDevices.Remove(dev_id);
                        //    mainPage.arduino.CreateDeviceList();
                        //}
                    }
                    else if (topic[2] == "command")
                    {
                        mainPage.mobileDevices.RemoveAt(dev_id);  //delete
                    }
                    else if (topic[2] == "position")
                    {
                        mainPage.mobileDevices[dev_id].SetPosition(message);// log  lat 
                    }
                    else if (topic[2] == "message")
                    {
                        mainPage.mobileDevices[dev_id].Message = message; // message
                    }

                }

            }
            catch (Exception)
            {

                throw;
            }
        }


        void client_MqttMsgSubscribed(object sender, MqttMsgSubscribedEventArgs e)
        {
            Debug.WriteLine("Subscribed for id = " + e.MessageId);
        }

        void client_MqttMsgPublished(object sender, MqttMsgPublishedEventArgs e)
        {
            Debug.WriteLine("MessageId = " + e.MessageId + " Published = " + e.IsPublished);
        }

    }
}
