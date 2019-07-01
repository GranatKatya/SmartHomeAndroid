using SplashyApp.Views.Rooms;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace SplashyApp.Models
{
    public class Rooms
    {
        public Dictionary<string, Kitchen> Kitchens { get; set; }
        public Dictionary<string, Toilet> Toilets { get; set; }
        public Dictionary<string, Hallway> Hallwaies { get; set; }
        public Dictionary<string, LivingRoom> LivingRooms { get; set; }
        public Dictionary<string, Bedroom> Bedrooms { get; set; }
        public Dictionary<string, Balcony> Balconies { get; set; }
        public Dictionary<string, Porch> Porches { get; set; }

        public ObservableCollection<SelectedRoom> MyRooms { get; set; }

        public Rooms()
        {
            Kitchens = new Dictionary<string, Kitchen>();
            Toilets = new Dictionary<string, Toilet>();
            Hallwaies = new Dictionary<string, Hallway>();
            LivingRooms = new Dictionary<string, LivingRoom>();
            Bedrooms = new Dictionary<string, Bedroom>();
            Balconies = new Dictionary<string, Balcony>();
            Porches = new Dictionary<string, Porch>();

            MyRooms = new ObservableCollection<SelectedRoom>();
            
        }




        public void DeleteFromRoomList(string key)
        {
            for (int i = 0; i < MyRooms.Count; i++)
            {
                if (MyRooms[i].myRoom.Key == key)
                {
                    MyRooms.Remove(MyRooms[i]);
                }
            }
        }

        public void UpdateInRoomList(string key, Room room)
        {
            for (int i = 0; i < MyRooms.Count; i++)
            {
                if (MyRooms[i].myRoom.Key == key)
                {
                    MyRooms[i].Key = room.Key;                           
                    MyRooms[i].myRoom = room;
                }
            }
        }


        public void CreateRoomList()
        {
            try
            {               
                MyRooms.Clear();             

                int element = 0;
                for (int i = 0; i < Kitchens.Count; i++)
                {
                    if (Kitchens.ContainsKey(Kitchens.ElementAt(i).Key))
                    {
                        MyRooms.Add(new SelectedRoom()
                        {
                          //  Name = Kitchens[Kitchens.ElementAt(i).Key].Name,
                            Key = Kitchens[Kitchens.ElementAt(i).Key].Key,
                            Selected = false,
                            myRoom = Kitchens[Kitchens.ElementAt(i).Key]
                        });
                    }                 
                }
                element = Kitchens.Count;

                for (int i = 0; i < Toilets.Count; i++)
                {
                    if (Toilets.ContainsKey(Toilets.ElementAt(i).Key))
                    {
                        MyRooms.Add(new SelectedRoom()
                        {
                          //  Name = Toilets[Toilets.ElementAt(i).Key].Name,
                            Key = Toilets[Toilets.ElementAt(i).Key].Key,
                            Selected = false,
                            myRoom = Toilets[Toilets.ElementAt(i).Key]
                        });
                    }                    
                }
                element += Toilets.Count;

                for (int i = 0; i < Hallwaies.Count; i++)
                {
                    if (Hallwaies.ContainsKey(Hallwaies.ElementAt(i).Key))
                    {
                        MyRooms.Add(new SelectedRoom()
                        {
                         //   Name = Hallwaies[Hallwaies.ElementAt(i).Key].Name,
                            Key = Hallwaies[Hallwaies.ElementAt(i).Key].Key,
                            Selected = false,
                            myRoom = Hallwaies[Hallwaies.ElementAt(i).Key]
                        });
                    }
                }
                element += Hallwaies.Count;


                for (int i = 0; i < LivingRooms.Count; i++)
                {
                    if (LivingRooms.ContainsKey(LivingRooms.ElementAt(i).Key))
                    {
                        MyRooms.Add(new SelectedRoom()
                        {                          
                            Key = LivingRooms[LivingRooms.ElementAt(i).Key].Key,
                            Selected = false,
                            myRoom = LivingRooms[LivingRooms.ElementAt(i).Key]
                        });
                    }                    
                }
                element += LivingRooms.Count;

                for (int i = 0; i < Bedrooms.Count; i++)
                {
                    if (Bedrooms.ContainsKey(Bedrooms.ElementAt(i).Key))
                    {
                        MyRooms.Add(new SelectedRoom()
                        {
                           // Name = Сurtains[Сurtains.ElementAt(i).Key].Name,
                            Key = Bedrooms[Bedrooms.ElementAt(i).Key].Key,
                            Selected = false,
                            myRoom = Bedrooms[Bedrooms.ElementAt(i).Key]
                        });
                    }
                }
                element += Bedrooms.Count;            

                for (int i = 0; i < Balconies.Count; i++)
                {
                    if (Balconies.ContainsKey(Balconies.ElementAt(i).Key))
                    {
                        MyRooms.Add(new SelectedRoom()
                        {
                            Key = Balconies[Balconies.ElementAt(i).Key].Key,
                            Selected = false,
                            myRoom = Balconies[Balconies.ElementAt(i).Key]
                        });
                    }
                }
                element += Balconies.Count;

                for (int i = 0; i < Porches.Count; i++)
                {
                    if (Porches.ContainsKey(Porches.ElementAt(i).Key))
                    {
                        MyRooms.Add(new SelectedRoom()
                        {
                            Key = Porches[Porches.ElementAt(i).Key].Key,
                            Selected = false,
                            myRoom = Porches[Porches.ElementAt(i).Key]
                        });
                    }
                }
                element += Porches.Count;

            }
            catch (Exception)
            {
                throw;
            }
            //  return devicesList;
        }
    }

    public class AvailableRooms{

        public static List<string> myAvailableRooms = new List<string>() { "kitchen", "toilet", "hallway", "livingroom", "bedroom", "balcony", "porch" };
    }
}
