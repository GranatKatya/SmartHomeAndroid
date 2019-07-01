using SplashyApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SplashyApp.Views.AboutUS
{
   
   
  
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AboutUSPage : ContentPage
	{
        MainPage mainPage = null;
      //  public Dictionary<string, SplashyApp.Models.Device> devicesList = null;
     //   public string[] Values { get; set; }


     //   public ObservableCollection<Temp> MyTemp { get; set; } = new ObservableCollection<Temp>();                                  // { new Temp() { Name ="111", Selected = false},
     //   public ObservableCollection<string> MyList { get; set; }
        public Temp selectedItem;


        //public string addElementName = null;
        //public string addElementPin = null;
        //public string addElementType = null;
        //public string addElementId = null;
        public Temp device { get; set; } = null;

        public AboutUSPage (MainPage mp)
		{
			InitializeComponent ();

          //  addElementName = addelementname;
           // addElementPin = addelementpin;
            mainPage = mp;



            //CreateDeviceList( mainPage.arduino);





            // MyList = mainPage.arduino.MyList;


            //this.BindingContext = mainPage.arduino.MyList; 
            this.BindingContext = mainPage.arduino.MyTemp;

            //  devicesList = CreateDeviceList(devicesList, mainPage.arduino);// new Dictionary<string, SplashyApp.Models.Device>();
            // Values = devicesList.Values.Select(z => z.Type).ToArray();


            // this.BindingContext = MyList;
            SendCommand sc = new SendCommand();
            SendMessagePicker.ItemsSource = new List<string>( sc.SendCommands);
          //  this.BindingContext = this;


            //      DevicesList.ItemsSource = Values;//new List<string>() { "lamp1", "lamp2", "lamp3" };

            // this.DevicesList.BackgroundColor = Color.Lime;
            //  this.DevicesList.fo = Color.Lime;
            // устанавливаем подписку на сообщения
            Subscribe();
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



        public AboutUSPage()
        {
            InitializeComponent();

            //  addElementName = addelementname;
            // addElementPin = addelementpin;
//mainPage = mp;



            //CreateDeviceList( mainPage.arduino);





            // MyList = mainPage.arduino.MyList;


            //this.BindingContext = mainPage.arduino.MyList; 
         //   this.BindingContext = mainPage.arduino.MyTemp;

            //  devicesList = CreateDeviceList(devicesList, mainPage.arduino);// new Dictionary<string, SplashyApp.Models.Device>();
            // Values = devicesList.Values.Select(z => z.Type).ToArray();


            // this.BindingContext = MyList;
          //  SendCommand sc = new SendCommand();
           // SendMessagePicker.ItemsSource = new List<string>(sc.SendCommands);
            //  this.BindingContext = this;


            //      DevicesList.ItemsSource = Values;//new List<string>() { "lamp1", "lamp2", "lamp3" };

            // this.DevicesList.BackgroundColor = Color.Lime;
            //  this.DevicesList.fo = Color.Lime;
            // устанавливаем подписку на сообщения
           // Subscribe();
        }

        // установка подписки на сообщения
        private void Subscribe()
        {
            MessagingCenter.Subscribe<Page>(
                this, // кто подписывается на сообщения
                "LabelChange",   // название сообщения
                (sender) =>
                {
                    try
                    {
                        if (device != null)
                        {

                            //   if (!String.IsNullOrEmpty(addElementName) && !String.IsNullOrEmpty(addElementPin) && !String.IsNullOrEmpty(addElementType))
                            if (!String.IsNullOrEmpty(device.myDevice.Name) && !String.IsNullOrEmpty(device.myDevice.Pin) && !String.IsNullOrEmpty(device.myDevice.Type))
                            {


                                if (String.IsNullOrEmpty(device.Key))
                                {
                                    if (device.myDevice.Type.IndexOf("temperature") > -1)
                                    {
                                        int lampindex = Guid.NewGuid().GetHashCode();
                                        mainPage.mqttConnection.Publish("devices/" + device.myDevice.Type + "s/" + lampindex.ToString() + "/config", device.myDevice.Name + "|" + device.myDevice.Pin + "|1|1|1", true);


                                    }
                                    else
                                    {
                                        int lampindex = Guid.NewGuid().GetHashCode();
                                        mainPage.mqttConnection.Publish("devices/" + device.myDevice.Type + "s/" + lampindex.ToString() + "/config", device.myDevice.Name + "|" + device.myDevice.Pin, true);

                                    }
                                }

                                device.Selected = false;
                                device = null;
                                //addElementName = null;
                                //addElementPin = null;
                                //addElementType = null;


                            }
                            else
                            {
                                DisplayAlert("Warning", "You can't send empty name, pin or type", "Ok");
                            }

                        }
                    }
                    catch (Exception)
                    {

                        throw;
                    }

                    /*stackLabel.Text = "Получено сообщение"; */
                });    // вызываемое действие




            //MessagingCenter.Subscribe<Page>(
            //  this, // кто подписывается на сообщения
            //  "UpdateMyTemp",   // название сообщения
            //  (sender) =>
            //  {
            //      //CreateDeviceList(mainPage.arduino);
            //  });    // вызываемое действие

            MessagingCenter.Subscribe<Page>(
            this, // кто подписывается на сообщения
            "DeviceEdit",   // название сообщения
            (sender) =>
            {
                try
                {

                    if (device != null)
                    {

                        //  if (!String.IsNullOrEmpty(addElementName) && !String.IsNullOrEmpty(addElementPin) && !String.IsNullOrEmpty(addElementType) && !String.IsNullOrEmpty(addElementId))
                        if (!String.IsNullOrEmpty(device.myDevice.Name) && !String.IsNullOrEmpty(device.myDevice.Pin) && !String.IsNullOrEmpty(device.myDevice.Type) && !String.IsNullOrEmpty(device.myDevice.Id))
                        {
                            //  Temp t = DevicesList.SelectedItem as Temp;
                            //mainPage.mqttConnection.Publish("devices/" + t.myDevice.Type + "s/" + t.myDevice.Id + "/config", "NEWNAME|2", true);

                            if (device.myDevice.Type.IndexOf("temperature") > -1)
                            {
                                string topic = "devices/" + device.myDevice.Type + "s/" + device.myDevice.Id + "/config";
                                mainPage.mqttConnection.Publish(topic, device.myDevice.Name + "|" + device.myDevice.Pin + "|1|1|1", true);
                            }
                            else
                            {
                                string topic = "devices/" + device.myDevice.Type + "s/" + device.myDevice.Id + "/config";
                                mainPage.mqttConnection.Publish(topic, device.myDevice.Name + "|" + device.myDevice.Pin, true);



                            }

                            device.Selected = false;
                            device = null;
                            //addElementName = null;
                            //addElementPin = null;
                            //addElementType = null;
                            //addElementId = null;

                        }
                        else
                        {
                            DisplayAlert("Warning", "You can't send empty name, pin or type", "Ok");
                        }

                    }

                }
                catch (Exception)
                {

                    throw;
                }
            });    // вызываемое действие
        }





        private bool isAdd = false;
        private   async void AddDevice(object sender, EventArgs e)
        {
          
            try
            {
                //  mainPage.mqttConnection.Publish("devices/lamps/0/command", "WTF");



                //   AddDevicePage ad = new AddDevicePage(this);

                //  ad.Disappearing += SDSDSDSD;
                //ad.BindingContext = addElementName;
                if (!isAdd)
                {
                    isAdd = true;

                    device = new Temp() { myDevice = new Models.Device() };
                    await Navigation.PushAsync(new AddDevicePage(this));
                    isAdd = false;
                }

              

                // bool res =  ad.A(1);
            }
            catch (Exception)
            {
                DisplayAlert("Error", "Message don't send", "Ok");
            }               
        }


        private bool isDelete= false;
        private void Delete_Devicce(object sender, EventArgs e)
        {
            try
            {

                if (!isDelete)
                {
                    isDelete = true;

                    if (DevicesList.SelectedItem != null)
                    {
                        Temp t = DevicesList.SelectedItem as Temp;
                        //    mainPage.mqttConnection.Publish("devices/lamps/0/command", "WTF");
                        mainPage.mqttConnection.Publish("devices/" + t.myDevice.Type + "s/" + t.myDevice.Id + "/config", t.myDevice.Name + "|delete", true);

                        DevicesList.SelectedItem = null;

                    }
                    isDelete = false;
                    //   Task.Delay(1000);
                    //  MyTemp = null;
                    //CreateDeviceList(mainPage.arduino);
                }
                else { DisplayAlert("Warning", "You need to choose item ", "Ok"); }
            }
            catch (Exception)
            {

                throw;
            }

        }

        private bool isEdie= false;
        private async void EditDevice(object sender, EventArgs e)
        {
            try
            {
                if (!isEdie)
                {
                    isEdie = true;
                    if (DevicesList.SelectedItem != null)
                    {
                        Temp t = DevicesList.SelectedItem as Temp;

                        device = t;
                        //addElementName = t.myDevice.Name;
                        //addElementPin = t.myDevice.Pin;
                        //addElementType = t.myDevice.Type;
                        //addElementId = t.myDevice.Id;
                        await Navigation.PushAsync(new AddDevicePage(this, true));
                    }
                    isEdie = false;
                }
                else { DisplayAlert("Warning", "You need to choose item ", "Ok"); }
            }
            catch (Exception)
            {
                DisplayAlert("Error", "the object is not changed", "Ok");
            }
        }


        private bool isSend= false;
        private void SendDevice(object sender, EventArgs e)
        {
          //  mainPage.mqttConnection.Publish("devices/lamps/0/command", "WTF");
            try
            {
                if (!isSend)
                {
                    isSend = true;

                    //  string mess = SendMessage.Text;
                    if (DevicesList.SelectedItem != null && SendMessagePicker.SelectedItem != null)//!string.IsNullOrEmpty(mess))//(mess != "" || mess != null || mess !="(null)"))
                    {
                        string mess = SendMessagePicker.Items[SendMessagePicker.SelectedIndex];
                        Temp t = DevicesList.SelectedItem as Temp;
                        //  mainPage.mqttConnection.Publish("devices/lamps/0/command", "WTF");
                        string topic = "devices/" + t.myDevice.Type + "s/" + t.myDevice.Id + "/command";
                        mainPage.mqttConnection.Publish(topic, mess);

                        SendMessagePicker.SelectedItem = null;
                        // SendMessage.Text = "";
                    }

                    else
                    {

                        if (SendMessagePicker.SelectedItem == null)//(string.IsNullOrEmpty(mess))
                        {
                            DisplayAlert("Warning", "You can't send empty message", "Ok");

                        }
                        else
                        {
                            DisplayAlert("Warning", "You need to choose item ", "Ok");
                        }

                    }
                    isSend = false;
                }
              
            }
            
            catch (Exception)
            {
                DisplayAlert("Error", "Message don't send","Ok");
              
            }
        }

        private void phonesList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null) {

                if (selectedItem !=null)
                {
                    selectedItem.Selected = false;
                }
               

                Temp t =  DevicesList.SelectedItem as Temp;
                selectedItem = t;
                t.Selected = true;


              //  ((ListView)sender).
              // ((ListView)sender).SelectedItem = null;
            }
            //  selected.Text = e.SelectedItem.ToString();
        }

        private bool isInfo= false;
        private async void InfoDevice(object sender, EventArgs e)
        {

            try
            {

                if (!isInfo)
                {
                    isInfo = true;
                    if (DevicesList.SelectedItem != null)
                    {
                        Temp t = DevicesList.SelectedItem as Temp;

                        device = t;
                        //addElementName = t.myDevice.Name;
                        //addElementPin = t.myDevice.Pin;
                        //addElementType = t.myDevice.Type;
                        //addElementId = t.myDevice.Id;
                        await Navigation.PushAsync(new InfoDevice(this));

                        device.Selected = false;
                        device = null;
                        DevicesList.SelectedItem = null;
                        //device = null;
                        //addElementName = "";
                        //addElementPin = "";
                        //addElementType = "";
                        //addElementId = "";

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




    public class Temp : INotifyPropertyChanged
    {
        public string Key { get; set; }
        public SplashyApp.Models.Device myDev;// { get; set; }
        public SplashyApp.Models.Device myDevice
        {
            get
            {
                return myDev;
            }
            set
            {

                myDev = value;
                MyDeviceString = myDevice.ToString();


                Lamp l = myDev as Lamp;
                if (l != null)
                {
                    if (String.IsNullOrEmpty(l.State))
                    {
                        Status = "off";
                         Switch = false;
                    }
                    else
                    {
                        Status = l.State;
                        Switch = l.boolState;
                    }
                }


                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("myDevice"));
            }

        }

        public string myDeviceString;//{ get { return myDevice.ToString(); } }
        public string MyDeviceString
        {
            get
            {
                return myDevice.ToString();
            }
            set
            {

                myDeviceString = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("MyDeviceString"));
            }

        }





        public string Name { get; set; }
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

        public bool isHaveRooms { get; set; } = false;
        private Room myRoom;
        public Room MyRoom
        {
            get
            {
                return myRoom;
            }
            set
            {

                myRoom = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("MyRoom"));
            }

        }

       // private string switc;
        public string Status
        {
            get
            {
                Lamp l = myDev as Lamp;
                if (l != null)
                {
                    return l.State;
                }
                else
                {
                    return "none";
                }               
            }
            set
            {

                // status = value;
                Lamp l = myDev as Lamp;
                if (l != null)
                {
                    l.State = value;
                }

                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("Status"));
            }
        }

       // private bool myswitch;
        public bool Switch
        {
            get
            {
                Lamp l = myDev as Lamp;
                if (l != null)
                {
                   return  l.boolState;
                    //if (l.State.IndexOf("on") > -1)
                    //{
                    //    return true;
                    //}
                    //else if (l.State.IndexOf("off") > -1) {
                    //    return false;
                    //}
                    //else if (l.State.IndexOf("none") > -1)
                    //{
                    //    return false;
                    //}                   
                }
                else
                {
                    return false;
                }
            }
            set
            {
                // status = value;
                Lamp l = myDev as Lamp;
                if (l != null)
                {
                    l.boolState = value;
                }

                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("Switch"));
            }
        }

        




        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        public int UpdateConfig(string result)
        {
            string[] parsedmessage = ParseMessage(result);

            Name = parsedmessage[0];
            myDevice.Name = parsedmessage[0];
            if (parsedmessage[1].IndexOf("delete") == -1)// name pin/delete
            {
                myDevice.Pin = parsedmessage[1];

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




        //public Color ColorText
        //{
        //    get
        //    {
        //        if (SelectedTextColor)
        //            return Color.Red;
        //        else
        //            return Color.Black;
        //    }

        //}
        //private Boolean _selectedTextColor;
        //public Boolean SelectedTextColor
        //{
        //    get
        //    {
        //        return _selectedTextColor;
        //    }
        //    set
        //    {
        //        _selectedTextColor = value;
        //        if (PropertyChanged != null)
        //            PropertyChanged(this, new PropertyChangedEventArgs("SelectedTextColor"));
        //    }
        //}
    }




    public class SendCommand {

         public List<string> SendCommands { get; set; } = new List<string>() { "on", "off"};
    }
}