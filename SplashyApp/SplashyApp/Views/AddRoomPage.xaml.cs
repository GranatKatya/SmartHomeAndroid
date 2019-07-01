using SplashyApp.Models;
using SplashyApp.Views.AboutUS;
using SplashyApp.Views.Rooms;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;




namespace SplashyApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddRoomPage : ContentPage
    {

        protected override bool OnBackButtonPressed()
        {
            if (roomsPage!=null)
            {
                if (roomsPage.selectedItem!=null)
                {
                    roomsPage.selectedItem.Selected = false;
                    roomsPage.selectedItem = null;
                }
            }
          

            base.OnBackButtonPressed();
            return false;




            //int i = 0;
            //if (i==0)
            //{
            //    //   OnClosePageRequested();

            //}
            //else
            //{
            //    base.OnBackButtonPressed();
            //    return false;
            //}
        }


        //async void OnClosePageRequested()
        //{
        //    var tdvm = (TaskDetailsViewModel)BindingContext;
        //    if (tdvm.CanSaveTask())
        //    {
        //        var result = await DisplayAlert("Wait", "You have unsaved changes! Are you sure you want to go back?", "Discard changes", "Cancel");

        //        if (result)
        //        {
        //            tdvm.DiscardChanges();
        //            await Navigation.PopAsync(true);
        //        }
        //    }
        //    else
        //    {
        //        await Navigation.PopAsync(true);
        //    }
        //}







        private RoomsPage roomsPage = null;
        //   public AddDwvicePageViewModel AddDevicePageViewModel { get; set; } = new AddDwvicePageViewModel();// { Name="kk"};
        private bool isEdit = false;
        Dictionary<Temp, string> AllRooms;
        //   public ObservableCollection<Temp> BindingList { get; set; }

        public bool IsClick { get; set; } = false;

        public AddRoomPage(RoomsPage rp, bool isedit = false)
        {



            InitializeComponent();
            roomsPage = rp;
            isEdit = isedit;
          

            AllRooms = new Dictionary<Temp, string>();


            for (int i = 0; i < roomsPage.mainPage.arduino.MyTemp.Count; i++)
            {
                // if (roomsPage.mainPage.arduino.MyTemp[i].MyRoom == null)

                if (roomsPage.mainPage.arduino.MyTemp[i].isHaveRooms == false)
                {
                    // Temp t = roomsPage.mainPage.arduino.MyTemp[i];
                    AllRooms.Add(new Temp()
                    {
                        Key = roomsPage.mainPage.arduino.MyTemp[i].Key,
                        Name = roomsPage.mainPage.arduino.MyTemp[i].Name,
                        isHaveRooms = roomsPage.mainPage.arduino.MyTemp[i].isHaveRooms,
                        myDevice = new Models.Device()
                        {
                            Name = roomsPage.mainPage.arduino.MyTemp[i].myDevice.Name,
                            Id = roomsPage.mainPage.arduino.MyTemp[i].myDevice.Id,
                            Pin = roomsPage.mainPage.arduino.MyTemp[i].myDevice.Pin,
                            Type = roomsPage.mainPage.arduino.MyTemp[i].myDevice.Type,
                            InRoom = new Room()
                            {
                                Type = roomsPage.mainPage.arduino.MyTemp[i].myDevice.InRoom.Type,
                                Name = roomsPage.mainPage.arduino.MyTemp[i].myDevice.InRoom.Name,
                                Count = roomsPage.mainPage.arduino.MyTemp[i].myDevice.InRoom.Count,
                                Key = roomsPage.mainPage.arduino.MyTemp[i].myDevice.InRoom.Key,
                                RoomDevice = new ObservableCollection<Temp>(roomsPage.mainPage.arduino.MyTemp[i].myDevice.InRoom.RoomDevice)
                            }
                        },

                        MyRoom = new Room() { Name = roomsPage.mainPage.arduino.MyTemp[i].MyRoom.Name, Type = roomsPage.mainPage.arduino.MyTemp[i].MyRoom.Type, Count = roomsPage.mainPage.arduino.MyTemp[i].MyRoom.Count, Key = roomsPage.mainPage.arduino.MyTemp[i].MyRoom.Key, RoomDevice = new ObservableCollection<Temp>(roomsPage.mainPage.arduino.MyTemp[i].MyRoom.RoomDevice) },

                        Selected = false
                    }, roomsPage.mainPage.arduino.MyTemp[i].MyDeviceString);
                }
            }


            //   BindingList =  roomsPage.selectedItem.myRoom.RoomDevice;


            // this.BindingContext = this;
            DevicesList.ItemsSource = roomsPage.selectedItem.myRoom.RoomDevice;

            RoomTypePicker.ItemsSource = AvailableRooms.myAvailableRooms;
            DevicePicker.ItemsSource = AllRooms.Values.ToList();

            if (isedit)
            {
                //  Name.Text = mainPage.addElementName;
                //  Pin.Text = mainPage.addElementPin;

                Name.Text = roomsPage.selectedItem.myRoom.Name;
                Count.Text = roomsPage.selectedItem.myRoom.Count.ToString();

                int idx = AvailableRooms.myAvailableRooms.IndexOf(roomsPage.selectedItem.myRoom.Type);
                RoomTypePicker.SelectedIndex = idx + 1;


                RoomTypePicker.SelectedItem = AvailableRooms.myAvailableRooms[idx];
                RoomTypePicker.IsEnabled = false;

            }
            else {
                //DeleteDeviceButton.IsEnabled = false;

             //   roomsPage.selectedItem = new SelectedRoom() { myRoom = new Room() { Count = 0.ToString() } };
              ///  DeleteDeviceButton.IsVisible = false;
            }
        }

        //protected override bool OnBackButtonPressed() {
        //    roomsPage.selectedItem = null;
        //    return true;
        //}

        //protected override void OnDisappearing() {
        //    roomsPage.selectedItem = null;
        //}

        private async void Add(object sender, EventArgs e)
        {
            if (!IsClick)
            {
                IsClick = true;
                addbutton.IsEnabled = false;

                if (isEdit == false)
                {
                    if (!String.IsNullOrEmpty(Name.Text) && !String.IsNullOrEmpty(Count.Text) && (!String.IsNullOrEmpty(roomsPage.selectedItem.myRoom.Type)))
                    {

                        roomsPage.selectedItem.myRoom.Name = Name.Text;
                        try
                        {
                            roomsPage.selectedItem.myRoom.Count = (roomsPage.selectedItem.myRoom.RoomDevice.Count).ToString();
                            //roomsPage.selectedItem.myRoom.Count = Count.Text;
                        }
                        catch (Exception)
                        {

                            DisplayAlert("Error", "You can enter only number", "ok");
                            Count.Text = "";
                            return;
                        }

                        try
                        {
                            roomsPage.selectedItem.IsSended = true;
                            MessagingCenter.Send<Page>(this, "RoomAdd");



                            //  roomsPage.selectedItem = null;
                            await Navigation.PopAsync();

                        }
                        catch (Exception)
                        {
                            await DisplayAlert("Error", "Тo changes were made", "Ok");
                        }

                    }
                    else
                    {
                        DisplayAlert("Warning", "You can't send empty name, pin or type", "Ok");
                    }
                }
                else
                {//edit               

                    if (!String.IsNullOrEmpty(Name.Text) && !String.IsNullOrEmpty(Count.Text))
                    {
                        roomsPage.selectedItem.myRoom.Name = Name.Text;
                        try
                        {
                            roomsPage.selectedItem.myRoom.Count = (roomsPage.selectedItem.myRoom.RoomDevice.Count).ToString();// roomsPage.selectedItem.myRoom.Count;  //Count.Text;
                        }
                        catch (Exception)
                        {

                            DisplayAlert("Error", "You can enter only number", "ok");
                            Count.Text = "";
                            return;
                        }


                        try
                        {
                            roomsPage.selectedItem.IsSended = true;
                            MessagingCenter.Send<Page>(this, "RoomEdit");
                            //   roomsPage.selectedItem = null;
                            await Navigation.PopAsync();

                        }
                        catch (Exception)
                        {
                            await DisplayAlert("Error", "Тo changes were made", "Ok");
                        }

                    }
                    else
                    {
                        DisplayAlert("Warning", "You can't send empty name, pin or type", "Ok");
                    }

                }

            }
        }

        private void SelectedIndexChanged(object sender, EventArgs e)
        {
            string typedev = RoomTypePicker.Items[RoomTypePicker.SelectedIndex];
            roomsPage.selectedItem.myRoom.Type = typedev;
        }


        private void SelectedDevChanged(object sender, EventArgs e)
        {

            // }

        }


        private async void phonesList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                if (selectedItem != null)
                {
                    selectedItem.Selected = false;
                }

                Temp t = DevicesList.SelectedItem as Temp;
                selectedItem = t;
                t.Selected = true;


            }
        }





        private void AddDevice(object sender, EventArgs e)
        {            //isHaveRooms
            if (DevicePicker.SelectedItem != null)
            {



                if (AllRooms.Keys.ToList()[DevicePicker.SelectedIndex].isHaveRooms != false)
                {
                    DisplayAlert("Warning", "This device already in room", "Ok");
                    return;
                }
                // DevicePicker.ItemsSource = AllRooms.Values.ToList();
                //  string typedev = DevicePicker.Items[DevicePicker.SelectedIndex] ;


                try
                {
                    AllRooms.Keys.ToList()[DevicePicker.SelectedIndex].MyRoom = new Room();   //????????????????????????????
                                                                                              //  AllRooms.Remove(AllRooms.Keys.ToList()[DevicePicker.SelectedIndex]);
                    AllRooms.Keys.ToList()[DevicePicker.SelectedIndex].isHaveRooms = true;

                    //AllRooms.Keys.ToList()[DevicePicker.SelectedIndex].Selected = true;
                    roomsPage.selectedItem.temp = AllRooms.Keys.ToList()[DevicePicker.SelectedIndex];
                    roomsPage.selectedItem.temp.isHaveRooms = true;
                    // roomsPage.selectedItem.myRoom.RoomDevice.Add(AllRooms.Keys.ToList()[DevicePicker.SelectedIndex]);




                    roomsPage.selectedItem.myRoom.Count = (Int32.Parse(roomsPage.selectedItem.myRoom.Count) + 1).ToString();


                    Temp element = (from el in roomsPage.selectedItem.myRoom.RoomDevice where el.Key == AllRooms.Keys.ToList()[DevicePicker.SelectedIndex].Key select el).FirstOrDefault();

                    if (element == null)
                    {
                        roomsPage.selectedItem.myRoom.RoomDevice.Add(AllRooms.Keys.ToList()[DevicePicker.SelectedIndex]);
                        roomsPage.selectedItem.myRoom.AadNewRooms.Add(AllRooms.Keys.ToList()[DevicePicker.SelectedIndex]);
                    }

                }
                catch (Exception ex)
                {


                    //  throw;
                }
            }
            else
            {
                DisplayAlert("Warning", "You need to choose item", "Ok");
            }
        }

        private async void DeleteDevice(object sender, EventArgs e)
        {
            if (DevicesList.SelectedItem != null)
            {
                // Temp seleteditemINRoomDevice  = (from el in roomsPage.selectedItem.myRoom.RoomDevice where el.Key == AllRooms.Keys.ToList()[DevicePicker.SelectedIndex].Key select el).FirstOrDefault();
                //Temp seletediteminAadNewRooms = (from el in roomsPage.selectedItem.myRoom.AadNewRooms where el.Key == AllRooms.Keys.ToList()[DevicePicker.SelectedIndex].Key select el).FirstOrDefault();


                var action = await DisplayAlert("Delete?", "Are you sure to delete this item", "Yes", "No");
                if (action)
                {

                    if (isEdit == false)
                    {//add
                        Temp t = DevicesList.SelectedItem as Temp;
                        Temp seleteditemINRoomDevice = (from el in roomsPage.selectedItem.myRoom.RoomDevice where el.Key == t.Key select el).FirstOrDefault();
                        Temp seletediteminAadNewRooms = (from el in roomsPage.selectedItem.myRoom.AadNewRooms where el.Key == t.Key select el).FirstOrDefault();


                        if (seletediteminAadNewRooms != null && seleteditemINRoomDevice != null)
                        {
                            seleteditemINRoomDevice.isHaveRooms = false;
                            seletediteminAadNewRooms.isHaveRooms = false;
                            roomsPage.selectedItem.myRoom.RoomDevice.Remove(seleteditemINRoomDevice);
                            roomsPage.selectedItem.myRoom.AadNewRooms.Remove(seletediteminAadNewRooms);
                        }


                    }
                    else
                    {//edit

                        Temp t = DevicesList.SelectedItem as Temp;
                        Temp seleteditemINRoomDevice = (from el in roomsPage.selectedItem.myRoom.RoomDevice where el.Key == t.Key select el).FirstOrDefault();
                        Temp seletediteminAadNewRooms = (from el in roomsPage.selectedItem.myRoom.AadNewRooms where el.Key == t.Key select el).FirstOrDefault();


                        if (seletediteminAadNewRooms != null && seleteditemINRoomDevice != null)
                        {
                            seleteditemINRoomDevice.isHaveRooms = false;
                            seletediteminAadNewRooms.isHaveRooms = false;
                            roomsPage.selectedItem.myRoom.RoomDevice.Remove(seleteditemINRoomDevice);
                            roomsPage.selectedItem.myRoom.AadNewRooms.Remove(seletediteminAadNewRooms);
                        }
                        else if (seleteditemINRoomDevice != null && seletediteminAadNewRooms == null)
                        {

                            if (roomsPage.selectedItem.myRoom.RoomDevice.Count != 0)
                            {


                                //    var rooms = (from r in roomsPage.mainPage.Rooms.Kitchens where r.Key == roomsPage.selectedItem.Key select r).FirstOrDefault();
                                //    try
                                //    {
                                //        var room = rooms.Value;
                                //        if (room.RoomDevice.Count > 0)
                                //        {
                                //            var deletedevice = (from d in room.RoomDevice where d.Key == seleteditemINRoomDevice.Key select d).FirstOrDefault();
                                //            if (deletedevice != null)
                                //            {
                                //                room.RoomDevice.Remove(deletedevice);
                                //            }
                                //        }

                                //    }
                                //    catch (Exception)
                                //    {

                                //        //  throw;
                                //    }

                                //     seleteditemINRoomDevice.isHaveRooms = false;
                                //     seletediteminAadNewRooms.isHaveRooms = false;




                                /////////////////   MessagingCenter.Send<Page>(this, "DeleteDeviceInRoom");






                                //for (int i = 0; i < roomsPage.selectedItem.myRoom.RoomDevice.Count; i++)
                                //{
                                //    if (seleteditemINRoomDevice.Key == roomsPage.selectedItem.myRoom.RoomDevice[i].Key)
                                //    {
                                //        //delete by mqtt
                                //      //  roomsPage.mainPage.mqttConnection.Publish(topic, roomsPage.selectedItem.myRoom.Name + "|" + (Int32.Parse(roomsPage.selectedItem.myRoom.Count) - 1).ToString() + alldevices, true);

                                //        continue;
                                //    }
                                //    alldevices += "|" + roomsPage.selectedItem.myRoom.RoomDevice[i].Key;
                                //}







                                string alldevices = "";

                                for (int i = 0; i < roomsPage.selectedItem.myRoom.RoomDevice.Count; i++)
                                {
                                    if (seleteditemINRoomDevice.Key == roomsPage.selectedItem.myRoom.RoomDevice[i].Key)
                                    {
                                      //  delete by mqtt
                                      //    roomsPage.mainPage.mqttConnection.Publish(topic, roomsPage.selectedItem.myRoom.Name + "|" + (Int32.Parse(roomsPage.selectedItem.myRoom.Count) - 1).ToString() + alldevices, true);
                                        alldevices += "|" + roomsPage.selectedItem.myRoom.RoomDevice[i].Key + "delete";
                                        continue;
                                    }
                                    alldevices += "|" + roomsPage.selectedItem.myRoom.RoomDevice[i].Key;
                                }

                                string topic = "rooms/" + roomsPage.selectedItem.myRoom.Type + "s/" + roomsPage.selectedItem.myRoom.Key + "/config";
                                try
                                {
                                    roomsPage.mainPage.mqttConnection.Publish(topic, roomsPage.selectedItem.myRoom.Name + "|" + (Int32.Parse(roomsPage.selectedItem.myRoom.Count) - 1).ToString() + alldevices, true);
                                }
                                catch (Exception)
                                {
                                    DisplayAlert("Error", "Item not delete", "Ok");
                                }








                                roomsPage.selectedItem.myRoom.RoomDevice.Remove(seleteditemINRoomDevice);
                                roomsPage.selectedItem.myRoom.AadNewRooms.Remove(seletediteminAadNewRooms);

                            }
                        }
                    }
                }
                else
                {
                    DisplayAlert("Warning", "This item already deleted", "Ok");
                }
            }


            else
            {
                DisplayAlert("Warning", "You need to choose item", "Ok");
            }

        }


        public Temp selectedItem { get; set; } = null;
        private bool isInfo = false;
        private async void Info(object sender, EventArgs e)
        {
            try
            {
                if (!isInfo)
                {
                    isInfo = true;

                    if (DevicesList.SelectedItem != null)
                    {
                        Temp t = DevicesList.SelectedItem as Temp;
                        selectedItem = t;

                        await Navigation.PushAsync(new InfoDevice(this));

                        selectedItem.Selected = false;
                        selectedItem = null;
                        DevicesList.SelectedItem = null;
                    }
                    else { DisplayAlert("Warning", "You need to choose item ", "Ok"); }
                    isInfo = false;
                }
            }
            catch (Exception)
            {
                DisplayAlert("Error", "the object is not changed", "Ok");
            }

        }
    }



    public class AddRoomPageViewModel : INotifyPropertyChanged
    {
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




        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}   