using SplashyApp.Models;
using SplashyApp.Views.AboutUS;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;



namespace SplashyApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AddDevicePage : ContentPage
	{






        protected override bool OnBackButtonPressed()
        {
            if (mainPage != null)
            {
                if (mainPage.selectedItem != null)
                {
                    mainPage.selectedItem.Selected = false;
                    mainPage.selectedItem = null;
                }
            }
            base.OnBackButtonPressed();
            return false;
        }




        public bool isclick { get; set; } = false;
        private AboutUSPage mainPage = null;
        public AddDwvicePageViewModel AddDevicePageViewModel { get; set; } = new AddDwvicePageViewModel();// { Name="kk"};

        private bool isEdit = false;
        public AddDevicePage (AboutUSPage mp, bool isedit = false)
		{
			InitializeComponent ();
            mainPage = mp;
            isEdit = isedit;

            DeviceTypePicker.ItemsSource = DevicesTypes.DevicesType;

            if (isedit)
            {
                //  Name.Text = mainPage.addElementName;
                //  Pin.Text = mainPage.addElementPin;

                Name.Text = mainPage.device.myDevice.Name;
                Name.Text = mainPage.device.Name;
                Pin.Text = mainPage.device.myDevice.Pin;

                int idx = DevicesTypes.DevicesType.IndexOf(mainPage.device.myDevice.Type);
                DeviceTypePicker.SelectedIndex = idx+1;

                DeviceTypePicker.SelectedItem = DevicesTypes.DevicesType[idx];
              //  DeviceTypePicker.ItemDisplayBinding = DevicesTypes.DevicesType[idx];
                DeviceTypePicker.IsEnabled = false;

            }

          
        }

        private async void Add(object sender, EventArgs e)
        {

            //  this.Navigation.RemovePage(this.Navigation.NavigationStack[this.Navigation.NavigationStack.Count - 2]);
            // await Navigation.PushAsync (new AboutUSPage(mainPage, Name.Text, Pin.Text));


            //  Navigation.PushAsync(new AboutUS(Name.Text, Pin.Text));

            // отправляем сообщение
            //  MessagingCenter.Send<AddDevicePage>(this, "LabelChange");


            if (!isclick)
            {
                isclick = true;
                ADDBUTTON.IsEnabled = false;


                if (isEdit == false)
                {
                    if (!String.IsNullOrEmpty(Name.Text) && !String.IsNullOrEmpty(Pin.Text) && (!String.IsNullOrEmpty(mainPage.device.myDevice.Type)))
                    {

                        if (mainPage.device.myDevice.Type.IndexOf("temperature") > -1)
                        {

                        }

                        // mainPage.addElementName = Name.Text;
                        //mainPage.addElementPin = Pin.Text;
                        mainPage.device.myDevice.Name = Name.Text;
                        mainPage.device.Name = Name.Text;
                        mainPage.device.myDevice.Pin = Pin.Text;




                        try
                        {
                            MessagingCenter.Send<Page>(this, "LabelChange");
                            Navigation.PopAsync();
                        }
                        catch (Exception ex)
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

                    mainPage.device.myDevice.Name = Name.Text;
                    mainPage.device.Name = Name.Text;
                    mainPage.device.myDevice.Pin = Pin.Text;

                    // mainPage.addElementName = Name.Text;
                    //mainPage.addElementPin = Pin.Text;
                    // mainPage.addElementId =  mainPage.addElementId;

                    if (!String.IsNullOrEmpty(Name.Text) && !String.IsNullOrEmpty(Pin.Text))
                    {


                        try
                        {
                            MessagingCenter.Send<Page>(this, "DeviceEdit");
                            await Navigation.PopAsync();
                        }
                        catch (Exception ex)
                        {

                            DisplayAlert("Error", "Тo changes were made", "Ok");
                        }
                    }
                    else
                    {
                        DisplayAlert("Warning", "You can't send empty name, pin or type", "Ok");
                    }

                }
            }

            //  await    Navigation.PopAsync();
        }

        private void SelectedIndexChanged(object sender, EventArgs e)
        {
            string typedev = DeviceTypePicker.Items[DeviceTypePicker.SelectedIndex];
            mainPage.device.myDevice.Type = typedev;
        }



        //public bool A(int a) {

        //    return true;
        //}
    }


    public class AddDwvicePageViewModel : INotifyPropertyChanged
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

        private string myRooms;
        public string MyRooms
        {
            get
            {
                return myRooms;
            }
            set
            {

                myRooms = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("MyRooms"));
            }
        }


        private string pin;
        public string Pin
        {
            get
            {
                return pin;
            }
            set
            {

                pin = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("Pin"));
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

        private string id;
        public string Id
        {
            get
            {
                return id;
            }
            set
            {

                id = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("Id"));
            }

        }



        private string temperature;
        public string Temperature
        {
            get
            {
                return temperature;
            }
            set
            {

                temperature = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("Temperature"));
            }
        }

        private string pressure;
        public string Pressure
        {
            get
            {
                return pressure;
            }
            set
            {

                pressure = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("Pressure"));
            }
        }

        private string humidity;
        public string Humidity
        {
            get
            {
                return humidity;
            }
            set
            {

                humidity = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("Humidity"));
            }
        }

        private string altitude;
        public string Altitude
        {
            get
            {
                return altitude;
            }
            set
            {

                altitude = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("Altitude"));
            }
        }



        public string MyRoomToString()
        {
            return myRoom.ToString();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }


    public class DevicesTypes
    {
        public static  List<string> DevicesType { get; set; } = new List<string>() { "lamp", "motionsensor", "temperature", "motor", "curtain" };


    }
}