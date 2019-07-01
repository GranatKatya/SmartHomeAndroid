using SplashyApp.Models;
using SplashyApp.ViewModel;
using SplashyApp.Views.AboutUS;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SplashyApp.Views.Rooms
{



    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RoomsPage : ContentPage
    {
        // MainPage mainPage = null;
        public MainPage mainPage { get; set; }
        public SelectedRoom selectedItem = null;

        public RoomsPage(MainPage mp)
        {
            InitializeComponent();
            mainPage = mp;

            

            //  for (int i = 0; i < mainPage.Rooms.MyRooms.Count; i++)
            //{
            //    mainPage.Rooms.MyRooms[i].Selected = false;

            //}
            //  DevicesList.SelectedItem = null;

            // DevicesList.SelectedItem = null;

            //  selectedItem.Selected = false;
            //selectedItem = null;

            this.BindingContext = mainPage.Rooms.MyRooms;
            //RoomsPicker.ItemsSource = new List<string>(AvailableRooms.myAvailableRooms);
            Subscribe();
            // mainPage.Rooms.CreateRoomList();
                
         //   UpdateNewDevicesInRooms();
        }



        protected override bool OnBackButtonPressed()
        {

            if (selectedItem != null)
            {
                DevicesList.SelectedItem = null;
                selectedItem.Selected = false;
                selectedItem = null;
            }
            

            base.OnBackButtonPressed();
            return false;          
        }



        void UpdateNewDevicesInRooms()
        {
            for (int i = 0; i < mainPage.NeedLoadedRoomsAndDevices.Count; i++)
            {              

                string topic = (mainPage.NeedLoadedRoomsAndDevices.ElementAt(i).Key);
                string[] topicword = topic.Split(new string[] { "/" }, StringSplitOptions.RemoveEmptyEntries);
                string type = topicword[1];
                if (type.IndexOf("kitchen")>-1)
                {
                    type = "kitchen";
                }
                else if (type.IndexOf("toilet") > -1)
                {
                    type = "toilet";
                }
                else if (type.IndexOf("hallway") > -1)
                {
                    type = "hallway";
                }
                else if (type.IndexOf("livingroom") > -1)
                {
                    type = "livingroom";
                }
                else if (type.IndexOf("bedroom") > -1)
                {
                    type = "bedroom";
                }
                else if (type.IndexOf("balcony") > -1)
                {
                    type = "balcony";
                }
                else if (type.IndexOf("porch") > -1)
                {
                    type = "porch";
                }

                SelectedRoom item = (from change in mainPage.Rooms.MyRooms where change.Key == topicword[2] select change).FirstOrDefault();
                if (item != null)
                {
                    item.myRoom.UpdateConfig(mainPage.NeedLoadedRoomsAndDevices.ElementAt(i).Value, mainPage.NeedLoadedRoomsAndDevices.ElementAt(i).Key, mainPage.arduino.MyTemp, type, mainPage.NeedLoadedRoomsAndDevices);
                    mainPage.NeedLoadedRoomsAndDevices.Remove(mainPage.NeedLoadedRoomsAndDevices.ElementAt(i).Key);
                    i--;
                }               
            }

        }
        private void Subscribe()
        {

           // DevicesList.SelectedItem = null;


            MessagingCenter.Subscribe<Page>(
                this, // кто подписывается на сообщения
                "RoomAdd",   // название сообщения
                (sender) =>
                {
                    try
                    {
                        if (selectedItem != null )
                        {
                            if (selectedItem.myRoom != null)
                            {
                                if (!String.IsNullOrEmpty(selectedItem.myRoom.Name) && !String.IsNullOrEmpty(selectedItem.myRoom.Count) && !String.IsNullOrEmpty(selectedItem.myRoom.Type))
                                {
                                    if (selectedItem.myRoom.RoomDevice.Count != 0)
                                    {
                                        if (String.IsNullOrEmpty(selectedItem.Key) && selectedItem.IsSended)
                                        {
                                            string alldevices = "";
                                            for (int i = 0; i < selectedItem.myRoom.RoomDevice.Count; i++)
                                            {

                                                alldevices += "|" + selectedItem.myRoom.RoomDevice[i].Key;
                                            }

                                            int lampindex = Guid.NewGuid().GetHashCode();
                                            string topic = "rooms/" + selectedItem.myRoom.Type + "s/" + lampindex.ToString() + "/config";
                                            mainPage.mqttConnection.Publish(topic, selectedItem.myRoom.Name + "|" + selectedItem.myRoom.Count + alldevices, true);

                                            selectedItem.IsSended = false;
                                        }
                                    }
                                    else {//==0 empty room

                                       if (String.IsNullOrEmpty(selectedItem.Key) && selectedItem.IsSended)
                                        {
                                            int lampindex = Guid.NewGuid().GetHashCode();
                                            string topic = "rooms/" + selectedItem.myRoom.Type + "s/" + lampindex.ToString() + "/config";
                                            mainPage.mqttConnection.Publish(topic, selectedItem.myRoom.Name + "|" + selectedItem.myRoom.Count, true);

                                            selectedItem.IsSended = false;
                                        }
                                       
                                    }



                                    //int lampindex = Guid.NewGuid().GetHashCode();
                                    //mainPage.Rooms.MyRooms.Add(new SelectedRoom() {Key = lampindex.ToString(), Selected= false,
                                    //    myRoom = new Room() { Key = lampindex.ToString(), Count= selectedItem.myRoom.Count, Name = selectedItem.myRoom.Name, Type= selectedItem.myRoom.Type } });


                                    DevicesList.SelectedItem = null;
                                    selectedItem.Selected = false;
                                    selectedItem = null;
                                }
                            }
                        }
                    }
                    catch (Exception)
                    {

                        throw;
                    }
                });    // вызываемое действие


            MessagingCenter.Subscribe<Page>(
            this, // кто подписывается на сообщения
            "RoomEdit",   // название сообщения
            (sender) =>
            {
                try
                {
                    if (selectedItem != null)
                    {
                        if (selectedItem.myRoom != null)
                        {
                            if (!String.IsNullOrEmpty(selectedItem.myRoom.Name) && !String.IsNullOrEmpty(selectedItem.myRoom.Count.ToString()) && !String.IsNullOrEmpty(selectedItem.myRoom.Type) && !String.IsNullOrEmpty(selectedItem.myRoom.Key))
                            {
                                if (selectedItem.myRoom.RoomDevice.Count != 0)
                                {
                                    if (selectedItem.IsSended)//чтобы не отправлялось 2 раза
                                    {

                                        string alldevices = "";
                                        for (int i = 0; i < selectedItem.myRoom.RoomDevice.Count; i++)
                                        {

                                            alldevices += "|" + selectedItem.myRoom.RoomDevice[i].Key;
                                        }

                                        string topic = "rooms/" + selectedItem.myRoom.Type + "s/" + selectedItem.myRoom.Key + "/config";
                                        mainPage.mqttConnection.Publish(topic, selectedItem.myRoom.Name + "|" + selectedItem.myRoom.Count + alldevices, true);

                                        selectedItem.IsSended = false;
                                    }
                                }

                                else
                                {//==0 empty room
                                    if (selectedItem.IsSended)//чтобы не отправлялось 2 раза
                                    {
                                        string topic = "rooms/" + selectedItem.myRoom.Type + "s/" + selectedItem.myRoom.Key + "/config";
                                        mainPage.mqttConnection.Publish(topic, selectedItem.myRoom.Name + "|" + selectedItem.myRoom.Count, true);

                                        selectedItem.IsSended = false;
                                    }
                                }

                                DevicesList.SelectedItem = null;
                                selectedItem.Selected = false;
                                selectedItem = null;
                            }
                            else
                            {
                                DisplayAlert("Warning", "You can't send empty name, count or type", "Ok");
                            }
                        }
                    }

                }
                catch (Exception)
                {

                    throw;
                }

            });    // вызываемое действие


        }


        //protected override bool OnBackButtonPressed()
        //{
        //    if (_browser.CanGoBack)
        //    {
        //        _browser.GoBack();
        //        return true;
        //    }
        //    else
        //    {
        //        //await Navigation.PopAsync(true);
        //        base.OnBackButtonPressed();
        //        return true;
        //    }
        //}

        //  protected override void ondisappearing()
        // {
        // selecteditem = new selectedroom() { myroom = new room() { count = 0.tostring() } };
        //  deviceslist.selecteditem = null;
        // selecteditem = new selectedroom() { myroom = new room() { count = 0.tostring() } };
        //  }


        private bool isEdit = false;
        private async void EditRoom(object sender, EventArgs e)
        {
            try
            {
                if (!isEdit)
                {
                    isEdit = true;
                    Edit.IsEnabled = false;

                    if (DevicesList.SelectedItem != null)
                    {
                        SelectedRoom r = DevicesList.SelectedItem as SelectedRoom;
                        // selectedItem = r;
                       // DevicesList.SelectedItem = null;

                        selectedItem = (SelectedRoom)r.Clone();
                        selectedItem.myRoom = new Room() { Name = r.myRoom.Name, Key = r.myRoom.Key, Count = r.myRoom.Count, Type = r.myRoom.Type, AadNewRooms = new System.Collections.ObjectModel.ObservableCollection<Temp>(),
                            RoomDevice = new System.Collections.ObjectModel.ObservableCollection<Temp>(r.myRoom.RoomDevice) };





                        await Navigation.PushAsync(new AddRoomPage(this, true));
                    }
                    else { DisplayAlert("Warning", "You need to choose item ", "Ok"); }
                    isEdit = false;
                    Edit.IsEnabled = true;
                }
            }
            catch (Exception)
            {
                DisplayAlert("Error", "the object is not changed", "Ok");
            }
        }


        private bool isAdd = false;
        private async void AddRoom(object sender, EventArgs e)
        {
            try
            {
                if (!isAdd)
                {
                    isAdd = true;
                    Add.IsEnabled = false;

                                                  //   DevicesList.SelectedItem = null;
                    selectedItem = new SelectedRoom() {  myRoom = new Room() { Count=0.ToString()} };
                    await Navigation.PushAsync(new AddRoomPage(this));
                    isAdd = false;
                    Add.IsEnabled = true;
                }

              
            }
            catch (Exception ex)
            {
                DisplayAlert("Error", "Message don't send", "Ok");
            }
        }


        private bool isdelete = false;
        private void DeleteRoom(object sender, EventArgs e)
        {
            try
            {
                if (!isdelete)
                {
                    isdelete = true;
                    Delete.IsEnabled = false;

                    if (DevicesList.SelectedItem != null)
                    {
                        SelectedRoom t = DevicesList.SelectedItem as SelectedRoom;

                        string topic = "rooms/" + t.myRoom.Type + "s/" + t.myRoom.Key + "/config";
                        mainPage.mqttConnection.Publish(topic, t.myRoom.Name + "|delete", true);

                        // selectedItem.Selected = false;
                        selectedItem = null;
                        Delete.IsEnabled = true;
                        DevicesList.SelectedItem = null;

                    }

                    else { DisplayAlert("Warning", "You need to choose item ", "Ok"); }
                    isdelete = false;
                    Delete.IsEnabled = true;
                }
            }
            catch (Exception)
            {

                throw;
            }
          
        }

        private bool isInfo = false;
        private async void InfoRoom(object sender, EventArgs e)
        {
            try
            {
                if (!isInfo)
                {
                    isInfo = true;
                    Info.IsEnabled = false;
                    if (DevicesList.SelectedItem != null)
                    {
                        SelectedRoom t = DevicesList.SelectedItem as SelectedRoom;
                        selectedItem = t;

                        await Navigation.PushAsync(new InfoRooms(this));

                        selectedItem.Selected = false;
                        selectedItem = null;
                    }
                    else { DisplayAlert("Warning", "You need to choose item ", "Ok"); }
                    isInfo = false;
                    Info.IsEnabled = true;
                }
            }
            catch (Exception)
            {
                DisplayAlert("Error", "the object is not changed", "Ok");
            }
        }

        private void phonesList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                if (selectedItem != null)
                {
                    selectedItem.Selected = false;
                }


                SelectedRoom t = DevicesList.SelectedItem as SelectedRoom;
                selectedItem = t;
                t.Selected = true;
            }
        }
    }





    public class SelectedRoom : INotifyPropertyChanged , ICloneable
    {
        public string Key { get; set; }
      //  public SplashyApp.Models.Room myRoom{ get; set; }
        private SplashyApp.Models.Room myyRoom { get; set; }

        public bool IsSended { get; set; } = false;

        public SplashyApp.Models.Room myRoom
        {
            get
            {
                return myyRoom;
            }
            set
            {

                myyRoom = value;
                MyRoomString = myRoom.ToString();
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("myRoom"));
            }

        }

        public Temp temp { get; set; }

        public string myRoomString;//{ get { return myRoom.ToString(); } }
        public string MyRoomString
        {
            get
            {
                return  myRoom.ToString(); 
            }
            set
            {

                myRoomString = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("MyRoomString"));
                  ///  PropertyChanged(this, new PropertyChangedEventArgs("BackgroundColor"));
                }
            }

        }


        public Color BackgroundColor
        {
            get
            {
                if (Selected)
                    return Color.CornflowerBlue;
                else
                    return Color.WhiteSmoke;
            }
        }

        private Boolean _selected;
        public Boolean Selected
        {
            get
            {
                return _selected;
            }
            set
            {

                _selected = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("BackgroundColor"));
            }

        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}