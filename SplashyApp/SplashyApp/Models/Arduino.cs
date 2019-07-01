using SplashyApp.Views;
using SplashyApp.Views.AboutUS;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace SplashyApp.Models
{
    public class Arduino
    {
        public Dictionary<string, Lamp> Lamps = null;
        public Dictionary<string, MotionSensor> MotionSensor = null;//
        public Dictionary<string, TemperaturePressureHumidity> TemperaturePressureHumidity = null;
        public Dictionary<string, Motor> Motors = null;
        public Dictionary<string, Сurtains> Сurtains = null;

        //  public  ObservableCollection<string> MyList { get; set; } = new ObservableCollection<string>() { "jkjk", "jjhgk", "kjdfhd" };
        public ObservableCollection<Temp> MyTemp { get; set; }


        public Arduino()
        {
            Lamps = new Dictionary<string, Lamp>();
            Сurtains = new Dictionary<string, Сurtains>();// Сurtains( Motor, Motor2);
            Motors = new Dictionary<string, Motor>();
            MotionSensor = new Dictionary<string, MotionSensor>();
            TemperaturePressureHumidity = new Dictionary<string, TemperaturePressureHumidity>();
            //  Lamps.Add(new Lamp(Program.mqttClientMobile));// = 
            MyTemp = new ObservableCollection<Temp>();

        }




        //public void CreateDeviceList2()
        //{
        //    try
        //    {
        //        Console.WriteLine(" FUNC  CreateDeviceList " + " Class:MqttServer line:44");
        //        // devicesList = new Dictionary<string, SplashyApp.Models.Device>();
        //        //  Lamps.Add(new Lamp(Program.mqttClientMobile));// = 
        //        ObservableCollection<Temp>  MyTemp2 = new ObservableCollection<Temp>();

               
        //        //  MyTemp = new ObservableCollection<Temp>();

        //        Console.WriteLine(" FUNC  CreateDeviceList " + " Class:MqttServer line:49");
        //        int element = 0;
        //        for (int i = 0; i < Lamps.Count; i++)
        //        {
        //            try
        //            {

        //                if (Lamps.ContainsKey(Lamps.ElementAt(i).Key))
        //                {
        //                    try
        //                    {

        //                        Console.WriteLine(" FUNC  CreateDeviceList " + Lamps[Lamps.ElementAt(i).Key].Name + " Class:MqttServer line:61");
        //                        Console.WriteLine(" FUNC  CreateDeviceList " + Lamps[Lamps.ElementAt(i).Key].Id + " Class:MqttServer line:62");
        //                        Console.WriteLine(" FUNC  CreateDeviceList " + Lamps.ElementAt(i).Value + " Class:MqttServer line:63");
        //                        Console.WriteLine(" FUNC  CreateDeviceList " + Lamps.ElementAt(i).Value.InRoom + " Class:MqttServer line:64");



        //                        MyTemp2.Add(new Temp()
        //                        {
        //                            Name = Lamps[Lamps.ElementAt(i).Key].Name,
        //                            Key = Lamps[Lamps.ElementAt(i).Key].Id,
        //                            Selected = false,
        //                            myDevice = Lamps[Lamps.ElementAt(i).Key],
        //                            MyRoom = Lamps[Lamps.ElementAt(i).Key].InRoom

        //                        });
        //                        Console.WriteLine(" FUNC  CreateDeviceList " + MyTemp2[MyTemp2.Count - 1] + " Class:MqttServer line:77");
        //                    }
        //                    catch (Exception)
        //                    {

        //                        throw;
        //                    }

        //                    if (Lamps[Lamps.ElementAt(i).Key].InRoom.RoomDevice.Count > 0)
        //                    {
        //                        MyTemp2[MyTemp2.Count - 1].isHaveRooms = true;//LAST
        //                    }

        //                }

        //            }
        //            catch (Exception)
        //            {

        //                throw;
        //            }

        //            //if (mainPage.arduino.Lamps.ContainsKey(i.ToString()))
        //            //{

        //            //    MyTemp.Add(new Temp()
        //            //    {
        //            //        Name = arduino.Lamps[i.ToString()].Name,
        //            //        Key = arduino.Lamps[i.ToString()].Id,
        //            //        Selected = false,
        //            //        myDevice = arduino.Lamps[i.ToString()]
        //            //    });


        //            //    devicesList.Add(arduino.Lamps[i.ToString()].Id, new SplashyApp.Models.Device()
        //            //    {
        //            //        Id = arduino.Lamps[i.ToString()].Id,
        //            //        Name = arduino.Lamps[i.ToString()].Name,
        //            //        Pin = arduino.Lamps[i.ToString()].Pin,
        //            //        Type = arduino.Lamps[i.ToString()].Type
        //            //    });
        //            //}
        //        }
        //        element = Lamps.Count;

        //        Console.WriteLine(" FUNC  CreateDeviceList " + " Class:MqttServer line:93");
        //        for (int i = 0; i < MotionSensor.Count; i++)
        //        {
        //            Console.WriteLine(" FUNC  CreateDeviceList " + " Class:MqttServer line:97");

        //            if (MotionSensor.ContainsKey(MotionSensor.ElementAt(i).Key))
        //            {

        //                Console.WriteLine(" FUNC  CreateDeviceList " + " Class:MqttServer line:102");


        //                try
        //                {

        //                    Console.WriteLine(" FUNC  CreateDeviceList " + MotionSensor[MotionSensor.ElementAt(i).Key].Name + " Class:MqttServer line:135");
        //                    Console.WriteLine(" FUNC  CreateDeviceList " + MotionSensor[MotionSensor.ElementAt(i).Key].Id + " Class:MqttServer line:136");
        //                    Console.WriteLine(" FUNC  CreateDeviceList " + MotionSensor[MotionSensor.ElementAt(i).Key] + " Class:MqttServer line:137");
        //                    Console.WriteLine(" FUNC  CreateDeviceList " + MotionSensor[MotionSensor.ElementAt(i).Key].InRoom + " Class:MqttServer line:138");



        //                    MyTemp2.Add(new Temp()
        //                    {
        //                        Name = MotionSensor[MotionSensor.ElementAt(i).Key].Name,
        //                        Key = MotionSensor[MotionSensor.ElementAt(i).Key].Id,
        //                        Selected = false,
        //                        myDevice = MotionSensor[MotionSensor.ElementAt(i).Key],
        //                        MyRoom = MotionSensor[MotionSensor.ElementAt(i).Key].InRoom
        //                    });

        //                    Console.WriteLine(" FUNC  CreateDeviceList " + MyTemp2[MyTemp2.Count - 1] + " Class:MqttServer line:152");
        //                }
        //                catch (Exception)
        //                {

        //                    throw;
        //                }


        //                Console.WriteLine(" FUNC  CreateDeviceList " + " Class:MqttServer line:113");
        //            }
        //            Console.WriteLine(" FUNC  CreateDeviceList " + " Class:MqttServer line:115");
        //            //if (mainPage.arduino.MotionSensor.ContainsKey(i.ToString()))
        //            //{

        //            //    MyTemp2.Add(new Temp()
        //            //    {
        //            //        Name = arduino.MotionSensor[i.ToString()].Name,
        //            //        Key = arduino.MotionSensor[i.ToString()].Id,
        //            //        Selected = false,
        //            //        myDevice = arduino.MotionSensor[i.ToString()]
        //            //    });

        //            //    //devicesList.Add((Int32.Parse(arduino.MotionSensor[i.ToString()].Id) + element).ToString(), new SplashyApp.Models.Device()
        //            //    //{
        //            //    //    Id = arduino.MotionSensor[i.ToString()].Id,
        //            //    //    Name = arduino.MotionSensor[i.ToString()].Name,
        //            //    //    Pin = arduino.MotionSensor[i.ToString()].Pin,
        //            //    //    Type = arduino.MotionSensor[i.ToString()].Type
        //            //    //});
        //            //}
        //        }
        //        element += MotionSensor.Count;

        //        Console.WriteLine(" FUNC  CreateDeviceList " + " Class:MqttServer line:132");
        //        for (int i = 0; i < TemperaturePressureHumidity.Count; i++)
        //        {

        //            Console.WriteLine(" FUNC  CreateDeviceList " + " Class:MqttServer line:136");

        //            if (TemperaturePressureHumidity.Count > 0)
        //            {

        //                Console.WriteLine(" FUNC  CreateDeviceList " + " Class:MqttServer line:141");

        //                if (TemperaturePressureHumidity.ContainsKey(TemperaturePressureHumidity.ElementAt(i).Key))
        //                {

        //                    Console.WriteLine(" FUNC  CreateDeviceList " + " Class:MqttServer line:146");
        //                    try
        //                    {

        //                        Console.WriteLine(" FUNC  CreateDeviceList " + TemperaturePressureHumidity[TemperaturePressureHumidity.ElementAt(i).Key].Name + " Class:MqttServer line:204");
        //                        Console.WriteLine(" FUNC  CreateDeviceList " + TemperaturePressureHumidity[TemperaturePressureHumidity.ElementAt(i).Key].Id + " Class:MqttServer line:205");
        //                        Console.WriteLine(" FUNC  CreateDeviceList " + TemperaturePressureHumidity[TemperaturePressureHumidity.ElementAt(i).Key] + " Class:MqttServer line:206");
        //                        Console.WriteLine(" FUNC  CreateDeviceList " + TemperaturePressureHumidity[TemperaturePressureHumidity.ElementAt(i).Key].InRoom + " Class:MqttServer line:207");



        //                        MyTemp2.Add(new Temp()
        //                        {
        //                            Name = TemperaturePressureHumidity[TemperaturePressureHumidity.ElementAt(i).Key].Name,
        //                            Key = TemperaturePressureHumidity[TemperaturePressureHumidity.ElementAt(i).Key].Id,
        //                            Selected = false,
        //                            myDevice = TemperaturePressureHumidity[TemperaturePressureHumidity.ElementAt(i).Key],
        //                            MyRoom = TemperaturePressureHumidity[TemperaturePressureHumidity.ElementAt(i).Key].InRoom
        //                        });
        //                        Console.WriteLine(" FUNC  CreateDeviceList " + MyTemp2[MyTemp2.Count - 1] + " Class:MqttServer line:219");
        //                    }
        //                    catch (Exception)
        //                    {

        //                        throw;
        //                    }


        //                    Console.WriteLine(" FUNC  CreateDeviceList " + " Class:MqttServer line:156");
        //                }
        //            }
        //        }
        //        //if (mainPage.arduino.TemperaturePressureHumidity.ContainsKey(i.ToString()))
        //        //{


        //        //    MyTemp.Add(new Temp()
        //        //    {
        //        //        Name = arduino.TemperaturePressureHumidity[i.ToString()].Name,
        //        //        Key = arduino.TemperaturePressureHumidity[i.ToString()].Id,
        //        //        Selected = false,
        //        //        myDevice = arduino.TemperaturePressureHumidity[i.ToString()]
        //        //    });


        //        //    //devicesList.Add((Int32.Parse(arduino.TemperaturePressureHumidity[i.ToString()].Id) + element).ToString(), new SplashyApp.Models.Device()
        //        //    //{
        //        //    //    Id = arduino.TemperaturePressureHumidity[i.ToString()].Id,
        //        //    //    Name = arduino.TemperaturePressureHumidity[i.ToString()].Name,
        //        //    //    Pin = arduino.TemperaturePressureHumidity[i.ToString()].Pin,
        //        //    //    Type = arduino.TemperaturePressureHumidity[i.ToString()].Type
        //        //    //});
        //        //}
        //        // }
        //        element += TemperaturePressureHumidity.Count;

        //        Console.WriteLine(" FUNC  CreateDeviceList " + " Class:MqttServer line:184");

        //        for (int i = 0; i < Motors.Count; i++)
        //        {

        //            if (Motors.ContainsKey(Motors.ElementAt(i).Key))
        //            {
        //                MyTemp2.Add(new Temp()
        //                {
        //                    Name = Motors[Motors.ElementAt(i).Key].Name,
        //                    Key = Motors[Motors.ElementAt(i).Key].Id,
        //                    Selected = false,
        //                    myDevice = Motors[Motors.ElementAt(i).Key],
        //                    MyRoom = Motors[Motors.ElementAt(i).Key].InRoom
        //                });
        //            }


        //            //if (mainPage.arduino.Motors.ContainsKey(i.ToString()))
        //            //{


        //            //    MyTemp2.Add(new Temp()
        //            //    {
        //            //        Name = arduino.Motors[i.ToString()].Name,
        //            //        Key = arduino.Motors[i.ToString()].Id,
        //            //        Selected = false,
        //            //        myDevice = arduino.Motors[i.ToString()]
        //            //    });

        //            //    //devicesList.Add((Int32.Parse(arduino.Motors[i.ToString()].Id) + element).ToString(), new SplashyApp.Models.Device()
        //            //    //{
        //            //    //    Id = arduino.Motors[i.ToString()].Id,
        //            //    //    Name = arduino.Motors[i.ToString()].Name,
        //            //    //    Pin = arduino.Motors[i.ToString()].Pin,
        //            //    //    Type = arduino.Motors[i.ToString()].Type
        //            //    //});
        //            //}
        //        }
        //        element += Motors.Count;

        //        Console.WriteLine(" FUNC  CreateDeviceList " + " Class:MqttServer line:225");
        //        for (int i = 0; i < Сurtains.Count; i++)
        //        {


        //            if (Сurtains.ContainsKey(Сurtains.ElementAt(i).Key))
        //            {
        //                MyTemp2.Add(new Temp()
        //                {
        //                    Name = Сurtains[Сurtains.ElementAt(i).Key].Name,
        //                    Key = Сurtains[Сurtains.ElementAt(i).Key].Id,
        //                    Selected = false,
        //                    myDevice = Сurtains[Сurtains.ElementAt(i).Key],
        //                    MyRoom = Сurtains[Сurtains.ElementAt(i).Key].InRoom
        //                });
        //            }

        //            //if (mainPage.arduino.Сurtains.ContainsKey(i.ToString()))
        //            //{

        //            //    MyTemp.Add(new Temp()
        //            //    {
        //            //        Name = arduino.Сurtains[i.ToString()].Name,
        //            //        Key = arduino.Сurtains[i.ToString()].Id,
        //            //        Selected = false,
        //            //        myDevice = arduino.Сurtains[i.ToString()]
        //            //    });


        //            //    //devicesList.Add((Int32.Parse(arduino.Сurtains[i.ToString()].Id) + element).ToString(), new SplashyApp.Models.Device()
        //            //    //{
        //            //    //    Id = arduino.Сurtains[i.ToString()].Id,
        //            //    //    Name = arduino.Сurtains[i.ToString()].Name,
        //            //    //    Pin = arduino.Сurtains[i.ToString()].Pin,
        //            //    //    Type = arduino.Сurtains[i.ToString()].Type
        //            //    //});
        //            //}
        //        }
        //        element += Сurtains.Count;

        //        Console.WriteLine(" FUNC  CreateDeviceList " + " Class:MqttServer line:265");

        //        MyTemp = MyTemp2;

        //    }
        //    catch (Exception)
        //    {
        //        Console.WriteLine(" FUNC  CreateDeviceList " + " Class:MqttServer line:270");
        //        throw;
        //    }
        //    //  return devicesList;
        //}



        public void CreateDeviceList()
        {
            try
            {
                Console.WriteLine(" FUNC  CreateDeviceList " + " Class:MqttServer line:44");
                // devicesList = new Dictionary<string, SplashyApp.Models.Device>();
                //  Lamps.Add(new Lamp(Program.mqttClientMobile));// = 
               
                MyTemp.Clear();
                //  MyTemp = new ObservableCollection<Temp>();

                Console.WriteLine(" FUNC  CreateDeviceList " + " Class:MqttServer line:49");
                int element = 0;
                for (int i = 0; i < Lamps.Count; i++)
                {
                    try
                    {

                        if (Lamps.ContainsKey(Lamps.ElementAt(i).Key))
                        {
                            try
                            {

                                Console.WriteLine(" FUNC  CreateDeviceList " + Lamps[Lamps.ElementAt(i).Key].Name + " Class:MqttServer line:61");
                                Console.WriteLine(" FUNC  CreateDeviceList " + Lamps[Lamps.ElementAt(i).Key].Id + " Class:MqttServer line:62");
                                Console.WriteLine(" FUNC  CreateDeviceList " + Lamps.ElementAt(i).Value + " Class:MqttServer line:63");
                                Console.WriteLine(" FUNC  CreateDeviceList " + Lamps.ElementAt(i).Value.InRoom +  " Class:MqttServer line:64");
                              


                                MyTemp.Add(new Temp()
                                {
                                    Name = Lamps[Lamps.ElementAt(i).Key].Name,
                                    Key = Lamps[Lamps.ElementAt(i).Key].Id,
                                    Selected = false,
                                    myDevice = Lamps[Lamps.ElementAt(i).Key],
                                    MyRoom = Lamps[Lamps.ElementAt(i).Key].InRoom

                                });
                                Console.WriteLine(" FUNC  CreateDeviceList " + MyTemp[MyTemp.Count - 1]  + " Class:MqttServer line:77");
                            }
                            catch (Exception)
                            {

                                throw;
                            }

                            if (Lamps[Lamps.ElementAt(i).Key].InRoom.RoomDevice.Count > 0)
                            {
                                MyTemp[MyTemp.Count - 1].isHaveRooms = true;//LAST
                            }

                        }

                    }
                    catch (Exception)
                    {

                        throw;
                    }
         
                    //if (mainPage.arduino.Lamps.ContainsKey(i.ToString()))
                    //{

                    //    MyTemp.Add(new Temp()
                    //    {
                    //        Name = arduino.Lamps[i.ToString()].Name,
                    //        Key = arduino.Lamps[i.ToString()].Id,
                    //        Selected = false,
                    //        myDevice = arduino.Lamps[i.ToString()]
                    //    });


                    //    devicesList.Add(arduino.Lamps[i.ToString()].Id, new SplashyApp.Models.Device()
                    //    {
                    //        Id = arduino.Lamps[i.ToString()].Id,
                    //        Name = arduino.Lamps[i.ToString()].Name,
                    //        Pin = arduino.Lamps[i.ToString()].Pin,
                    //        Type = arduino.Lamps[i.ToString()].Type
                    //    });
                    //}
                }
                element = Lamps.Count;

                Console.WriteLine(" FUNC  CreateDeviceList " + " Class:MqttServer line:93");
                for (int i = 0; i < MotionSensor.Count; i++)
                {
                    Console.WriteLine(" FUNC  CreateDeviceList " + " Class:MqttServer line:97");

                    if (MotionSensor.ContainsKey(MotionSensor.ElementAt(i).Key))
                    {

                        Console.WriteLine(" FUNC  CreateDeviceList " + " Class:MqttServer line:102");


                        try
                        {

                            Console.WriteLine(" FUNC  CreateDeviceList " + MotionSensor[MotionSensor.ElementAt(i).Key].Name + " Class:MqttServer line:135");
                            Console.WriteLine(" FUNC  CreateDeviceList " + MotionSensor[MotionSensor.ElementAt(i).Key].Id + " Class:MqttServer line:136");
                            Console.WriteLine(" FUNC  CreateDeviceList " + MotionSensor[MotionSensor.ElementAt(i).Key] + " Class:MqttServer line:137");
                            Console.WriteLine(" FUNC  CreateDeviceList " + MotionSensor[MotionSensor.ElementAt(i).Key].InRoom + " Class:MqttServer line:138");



                            MyTemp.Add(new Temp()
                            {
                                Name = MotionSensor[MotionSensor.ElementAt(i).Key].Name,
                                Key = MotionSensor[MotionSensor.ElementAt(i).Key].Id,
                                Selected = false,
                                myDevice = MotionSensor[MotionSensor.ElementAt(i).Key],
                                MyRoom = MotionSensor[MotionSensor.ElementAt(i).Key].InRoom
                            });

                            Console.WriteLine(" FUNC  CreateDeviceList " + MyTemp[MyTemp.Count -1 ] + " Class:MqttServer line:152");
                        }
                        catch (Exception)
                        {

                            throw;
                        }
                      

                        Console.WriteLine(" FUNC  CreateDeviceList " + " Class:MqttServer line:113");
                    }
                    Console.WriteLine(" FUNC  CreateDeviceList " + " Class:MqttServer line:115");
                    //if (mainPage.arduino.MotionSensor.ContainsKey(i.ToString()))
                    //{

                    //    MyTemp.Add(new Temp()
                    //    {
                    //        Name = arduino.MotionSensor[i.ToString()].Name,
                    //        Key = arduino.MotionSensor[i.ToString()].Id,
                    //        Selected = false,
                    //        myDevice = arduino.MotionSensor[i.ToString()]
                    //    });

                    //    //devicesList.Add((Int32.Parse(arduino.MotionSensor[i.ToString()].Id) + element).ToString(), new SplashyApp.Models.Device()
                    //    //{
                    //    //    Id = arduino.MotionSensor[i.ToString()].Id,
                    //    //    Name = arduino.MotionSensor[i.ToString()].Name,
                    //    //    Pin = arduino.MotionSensor[i.ToString()].Pin,
                    //    //    Type = arduino.MotionSensor[i.ToString()].Type
                    //    //});
                    //}
                }
                element += MotionSensor.Count;

                Console.WriteLine(" FUNC  CreateDeviceList " + " Class:MqttServer line:132");
                for (int i = 0; i < TemperaturePressureHumidity.Count; i++)
                {

                    Console.WriteLine(" FUNC  CreateDeviceList " + " Class:MqttServer line:136");

                    if (TemperaturePressureHumidity.Count > 0)
                    {

                        Console.WriteLine(" FUNC  CreateDeviceList " + " Class:MqttServer line:141");

                        if (TemperaturePressureHumidity.ContainsKey(TemperaturePressureHumidity.ElementAt(i).Key))
                        {

                            Console.WriteLine(" FUNC  CreateDeviceList " + " Class:MqttServer line:146");
                            try
                            {

                                Console.WriteLine(" FUNC  CreateDeviceList " + TemperaturePressureHumidity[TemperaturePressureHumidity.ElementAt(i).Key].Name + " Class:MqttServer line:204");
                                Console.WriteLine(" FUNC  CreateDeviceList " + TemperaturePressureHumidity[TemperaturePressureHumidity.ElementAt(i).Key].Id + " Class:MqttServer line:205");
                                Console.WriteLine(" FUNC  CreateDeviceList " + TemperaturePressureHumidity[TemperaturePressureHumidity.ElementAt(i).Key] + " Class:MqttServer line:206");
                                Console.WriteLine(" FUNC  CreateDeviceList " + TemperaturePressureHumidity[TemperaturePressureHumidity.ElementAt(i).Key].InRoom + " Class:MqttServer line:207");



                                MyTemp.Add(new Temp()
                                {
                                    Name = TemperaturePressureHumidity[TemperaturePressureHumidity.ElementAt(i).Key].Name,
                                    Key = TemperaturePressureHumidity[TemperaturePressureHumidity.ElementAt(i).Key].Id,
                                    Selected = false,
                                    myDevice = TemperaturePressureHumidity[TemperaturePressureHumidity.ElementAt(i).Key],
                                    MyRoom = TemperaturePressureHumidity[TemperaturePressureHumidity.ElementAt(i).Key].InRoom
                                });
                                Console.WriteLine(" FUNC  CreateDeviceList " + MyTemp[MyTemp.Count-1] + " Class:MqttServer line:219");
                            }
                            catch (Exception)
                            {

                                throw;
                            }
                          

                            Console.WriteLine(" FUNC  CreateDeviceList " + " Class:MqttServer line:156");
                        }
                    }
                }
                    //if (mainPage.arduino.TemperaturePressureHumidity.ContainsKey(i.ToString()))
                    //{


                    //    MyTemp.Add(new Temp()
                    //    {
                    //        Name = arduino.TemperaturePressureHumidity[i.ToString()].Name,
                    //        Key = arduino.TemperaturePressureHumidity[i.ToString()].Id,
                    //        Selected = false,
                    //        myDevice = arduino.TemperaturePressureHumidity[i.ToString()]
                    //    });


                    //    //devicesList.Add((Int32.Parse(arduino.TemperaturePressureHumidity[i.ToString()].Id) + element).ToString(), new SplashyApp.Models.Device()
                    //    //{
                    //    //    Id = arduino.TemperaturePressureHumidity[i.ToString()].Id,
                    //    //    Name = arduino.TemperaturePressureHumidity[i.ToString()].Name,
                    //    //    Pin = arduino.TemperaturePressureHumidity[i.ToString()].Pin,
                    //    //    Type = arduino.TemperaturePressureHumidity[i.ToString()].Type
                    //    //});
                    //}
               // }
                element += TemperaturePressureHumidity.Count;

                Console.WriteLine(" FUNC  CreateDeviceList " + " Class:MqttServer line:184");

                for (int i = 0; i < Motors.Count; i++)
                {

                    if (Motors.ContainsKey(Motors.ElementAt(i).Key))
                    {
                        MyTemp.Add(new Temp()
                        {
                            Name = Motors[Motors.ElementAt(i).Key].Name,
                            Key = Motors[Motors.ElementAt(i).Key].Id,
                            Selected = false,
                            myDevice = Motors[Motors.ElementAt(i).Key],
                            MyRoom = Motors[Motors.ElementAt(i).Key].InRoom
                        });
                    }


                    //if (mainPage.arduino.Motors.ContainsKey(i.ToString()))
                    //{


                    //    MyTemp.Add(new Temp()
                    //    {
                    //        Name = arduino.Motors[i.ToString()].Name,
                    //        Key = arduino.Motors[i.ToString()].Id,
                    //        Selected = false,
                    //        myDevice = arduino.Motors[i.ToString()]
                    //    });

                    //    //devicesList.Add((Int32.Parse(arduino.Motors[i.ToString()].Id) + element).ToString(), new SplashyApp.Models.Device()
                    //    //{
                    //    //    Id = arduino.Motors[i.ToString()].Id,
                    //    //    Name = arduino.Motors[i.ToString()].Name,
                    //    //    Pin = arduino.Motors[i.ToString()].Pin,
                    //    //    Type = arduino.Motors[i.ToString()].Type
                    //    //});
                    //}
                }
                element += Motors.Count;

                Console.WriteLine(" FUNC  CreateDeviceList " + " Class:MqttServer line:225");
                for (int i = 0; i < Сurtains.Count; i++)
                {


                    if (Сurtains.ContainsKey(Сurtains.ElementAt(i).Key))
                    {
                        MyTemp.Add(new Temp()
                        {
                            Name = Сurtains[Сurtains.ElementAt(i).Key].Name,
                            Key = Сurtains[Сurtains.ElementAt(i).Key].Id,
                            Selected = false,
                            myDevice = Сurtains[Сurtains.ElementAt(i).Key],
                            MyRoom = Сurtains[Сurtains.ElementAt(i).Key].InRoom
                        });
                    }

                    //if (mainPage.arduino.Сurtains.ContainsKey(i.ToString()))
                    //{

                    //    MyTemp.Add(new Temp()
                    //    {
                    //        Name = arduino.Сurtains[i.ToString()].Name,
                    //        Key = arduino.Сurtains[i.ToString()].Id,
                    //        Selected = false,
                    //        myDevice = arduino.Сurtains[i.ToString()]
                    //    });


                    //    //devicesList.Add((Int32.Parse(arduino.Сurtains[i.ToString()].Id) + element).ToString(), new SplashyApp.Models.Device()
                    //    //{
                    //    //    Id = arduino.Сurtains[i.ToString()].Id,
                    //    //    Name = arduino.Сurtains[i.ToString()].Name,
                    //    //    Pin = arduino.Сurtains[i.ToString()].Pin,
                    //    //    Type = arduino.Сurtains[i.ToString()].Type
                    //    //});
                    //}
                }
                element += Сurtains.Count;

                Console.WriteLine(" FUNC  CreateDeviceList " + " Class:MqttServer line:265");

            }
            catch (Exception)
            {
                Console.WriteLine(" FUNC  CreateDeviceList " + " Class:MqttServer line:270");
                throw;
            }
            //  return devicesList;
        }






        public void DeleteFromTemp(string key) {
            for (int i = 0; i < MyTemp.Count; i++)
            {
                if (MyTemp[i].myDevice.Id == key)
                {
                    MyTemp.Remove(MyTemp[i]);
                }
            }
        }



        public void UpdateInTemp(string key , Device device)
        {
            for (int i = 0; i < MyTemp.Count; i++)
            {
                if (MyTemp[i].myDevice.Id == key)
                {
                    MyTemp[i].Name = device.Name;
                    MyTemp[i].Key = device.Id;
                    //selected
                    MyTemp[i].myDevice = device;
                    MyTemp[i].MyRoom = device.InRoom;
                }
            }
        }



        public void DeleteDevInRoom(Device lamp, MainPage mainPage )
        {
            try
            {
                //var el = (from e in MyTemp where e.Key == lamp.Id select e).FirstOrDefault();
                //if (el != null)
                //{
                //    if (lamp.Type.IndexOf("lamp") > -1)
                //    {
                //        var kitchen = (from e in mainPage.Rooms.Kitchens where e.Key == el.MyRoom.Key select e).FirstOrDefault();
                //        try
                //        {
                //            var deletedevice = (from e in kitchen.Value.RoomDevice where e.Key == el.Key select e).FirstOrDefault();
                //            if (deletedevice != null)
                //            {
                //                kitchen.Value.RoomDevice.Remove(deletedevice);
                //            }

                //        }
                //        catch (Exception ex)
                //        {

                //            throw;
                //        }
                //    }
                //}





















                //var el = (from e in MyTemp where e.Key == lamp.Id select e).FirstOrDefault();

                //if (el != null)
                //{
                //    string roomkey = el.MyRoom.Key;
                //    var room = (from r in mainPage.Rooms.MyRooms where r.Key == roomkey select r).FirstOrDefault();
                //    if (room != null)
                //    {
                //        for (int i = 0; i < room.myRoom.RoomDevice.Count; i++)
                //        {
                //            if (room.myRoom.RoomDevice[i].Key == el.Key)
                //            {
                //                //  room.myRoom.RoomDevice.Remove(el);
                //                room.myRoom.RoomDevice.RemoveAt(i);
                //            }
                //        }
                //    }
                //}
            }
            catch (Exception)
            {

               // throw;
            }

        }

    }
}
