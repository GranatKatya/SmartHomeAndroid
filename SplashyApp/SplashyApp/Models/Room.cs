using SplashyApp.Views.AboutUS;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace SplashyApp.Models
{
   public class Room : INotifyPropertyChanged
    {
       // public string Name { get; set; }
        private string name;
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


        private string count;
        public string Count
        {
            get
            {
                return count;
            }
            set
            {

                count = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("Count"));
            }

        }

        private string type;
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

        private string key;
        public string Key
        {
            get
            {
                return key;
            }
            set
            {

                key = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("Key"));
            }

        }

      


        public ObservableCollection<Temp> RoomDevice { get; set; }
        public ObservableCollection<Temp> AadNewRooms { get; set; }

        public Room()
        {
            RoomDevice = new ObservableCollection<Temp>();
            AadNewRooms = new ObservableCollection<Temp>();
        }

       

        public override string ToString()
        {
            return "Name: " + Name + " Type: " + Type;
        }


        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }



        // public int UpdateConfig(string result, string topic, ObservableCollection<Temp> MyTemp, string type)
        public int UpdateConfig(string result, string topic, ObservableCollection<Temp> MyTemp, string type , Dictionary<string, string> NeedLoadedRoomsAndDevices)
        {
            try
            {
                string[] parsedmessage = ParseMessage(result);

                Name = parsedmessage[0];

                if (parsedmessage[1].IndexOf("delete") == -1)// name pin/delete
                {
                    Count = parsedmessage[1];

                    if (parsedmessage.Length > 2)
                    {
                        string[] topicword = topic.Split(new string[] { "/" }, StringSplitOptions.RemoveEmptyEntries);

                        for (int i = 0; i < parsedmessage.Length - 2; i++)
                        {
                            string parsedmessage2 = parsedmessage[i + 2];
                            if (parsedmessage[i + 2].IndexOf("delete") > -1)
                            {
                                string res =   parsedmessage2.Remove(parsedmessage2.Length-6, 6);//6 = delete
                                Temp t = (from o in MyTemp where o.Key == res select o).FirstOrDefault();
                                if (t!=null)
                                {                                   
                                    RoomDevice.Remove(t);
                                    t.isHaveRooms = false;
                                    t.MyRoom = new Room();
                                }
                                
                            }
                            else
                            {
                                Temp t = (from o in MyTemp where o.Key == parsedmessage[i + 2] select o).FirstOrDefault();
                                if (t != null)
                                {


                                    t.MyRoom = new Room() { Name = parsedmessage[0], Count = parsedmessage[1], Key = topicword[2], Type = type, RoomDevice = new ObservableCollection<Temp>() { t } };
                                    t.myDevice.InRoom = new Room() { Name = parsedmessage[0], Count = parsedmessage[1], Key = topicword[2], Type = type, RoomDevice = new ObservableCollection<Temp>() { t } };

                                    Temp t1 = (from o in RoomDevice where o.Key == parsedmessage[i + 2] select o).FirstOrDefault();
                                    if (t1 == null)
                                    {
                                        RoomDevice.Add(t);
                                    }

                                    if (t.MyRoom.RoomDevice.Count > 0)
                                    {
                                        t.isHaveRooms = true;
                                    }

                                }
                                else
                                {



                                    //KeyValuePair<string, string> item = (from o in NeedLoadedRoomsAndDevices where o.Key.Contains(parsedmessage[2]) select o).FirstOrDefault();
                                    ////  message = result;
                                    //try
                                    //{
                                    //    if (!String.IsNullOrEmpty(item.Key) && !String.IsNullOrEmpty(item.Value))
                                    //    {
                                    //        string topic1 = item.Key;
                                    //       string mss = item.Value + "|"+ topicword[2];
                                    //        NeedLoadedRoomsAndDevices.Remove(item.Key);
                                    //        NeedLoadedRoomsAndDevices.Add(topic1, mss);
                                    //    }
                                    //}
                                    //catch (Exception ex)
                                    //{

                                    //   // throw;
                                    //}

                                    if (!NeedLoadedRoomsAndDevices.ContainsKey(topic))
                                    {
                                        NeedLoadedRoomsAndDevices.Add(topic, result);
                                    }

                                }
                            }
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < RoomDevice.Count; i++)
                    {
                       
                        //Temp t = (from o in RoomDevice where o.Key == RoomDevice[i].Key select o).FirstOrDefault();
                        if (RoomDevice[i] != null)
                        {
                           
                            RoomDevice[i].isHaveRooms = false;
                            RoomDevice[i].MyRoom = new Room();
                            RoomDevice.Remove(RoomDevice[i]);
                            i--;
                        }

                    }

                    return 1;
                }
                return 0;

            }
            catch (Exception)
            {

                throw;
            }
        }

        public string[] ParseMessage(string result)
        {

            string[] separators = { "|" };
            return result.Split(separators, StringSplitOptions.RemoveEmptyEntries);
        }
    }

    public class Kitchen : Room {

    }
    public class Toilet : Room { }

    public  class Hallway : Room
    {   //прихожая
    }
    public  class LivingRoom : Room { }
    public  class Bedroom : Room { }
    public class Balcony : Room { }
    public class Porch : Room
    { //крыльцо веранда
    }





}
