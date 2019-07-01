using SplashyApp.Models;
using SplashyApp.Views.AboutUS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SplashyApp.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class InfoDevice : ContentPage
	{
        AboutUSPage aboutUSPage = null;
        public AddDwvicePageViewModel AddDevicePageViewModel { get; set; } = new AddDwvicePageViewModel();
       
        public InfoDevice (AboutUSPage p)
		{
			InitializeComponent ();
            aboutUSPage = p;
            AddDevicePageViewModel.Name = aboutUSPage.device.myDevice.Name;
            AddDevicePageViewModel.Pin = aboutUSPage.device.myDevice.Pin;
            AddDevicePageViewModel.Type = aboutUSPage.device.myDevice.Type;
            AddDevicePageViewModel.Id = aboutUSPage.device.myDevice.Id;
            AddDevicePageViewModel.MyRoom =  aboutUSPage.device.MyRoom;

            if (aboutUSPage.device.myDevice.Type.IndexOf("temperature") > -1)
            {
                TemperaturePressureHumidity t = aboutUSPage.device.myDevice as TemperaturePressureHumidity;
                AddDevicePageViewModel.Temperature = t.Temperature;
                AddDevicePageViewModel.Pressure = t.Pressure;
                AddDevicePageViewModel.Humidity = t.Humidity;
                AddDevicePageViewModel.Altitude = t.Altitude;

                stack1.IsVisible = true;
                stack2.IsVisible = true;
            }

            if (aboutUSPage.device.MyRoom.ToString().IndexOf("Name:  Type: ") >-1)
            {
                AddDevicePageViewModel.MyRooms = "free";
            }
            else
            {
                AddDevicePageViewModel.MyRooms = aboutUSPage.device.MyRoom.ToString();
            }
           
            //AddDevicePageViewModel.Name = aboutUSPage.addElementName;
            //AddDevicePageViewModel.Pin = aboutUSPage.addElementPin;
            //AddDevicePageViewModel.Type = aboutUSPage.addElementType;
            //AddDevicePageViewModel.Id = aboutUSPage.addElementId;


            this.BindingContext = AddDevicePageViewModel;
        }

        public InfoDevice(AddRoomPage p)
        {
            InitializeComponent();
            aboutUSPage = new AboutUSPage() { device = p.selectedItem };
            AddDevicePageViewModel.Name = aboutUSPage.device.myDevice.Name;
            AddDevicePageViewModel.Pin = aboutUSPage.device.myDevice.Pin;
            AddDevicePageViewModel.Type = aboutUSPage.device.myDevice.Type;
            AddDevicePageViewModel.Id = aboutUSPage.device.myDevice.Id;
            AddDevicePageViewModel.MyRoom = aboutUSPage.device.MyRoom;
            if (aboutUSPage.device.MyRoom.ToString().IndexOf("Name:  Type: ") > -1)
            {
                AddDevicePageViewModel.MyRooms = "free";
            }
            else
            {
                AddDevicePageViewModel.MyRooms = aboutUSPage.device.MyRoom.ToString();
            }
            //AddDevicePageViewModel.Name = aboutUSPage.addElementName;
            //AddDevicePageViewModel.Pin = aboutUSPage.addElementPin;
            //AddDevicePageViewModel.Type = aboutUSPage.addElementType;
            //AddDevicePageViewModel.Id = aboutUSPage.addElementId;


            this.BindingContext = AddDevicePageViewModel;
        }

        private async void Back(object sender, EventArgs e)
        {
            try
            {
                await Navigation.PopAsync();
            }
            catch (Exception ex)
            {
               // DisplayAlert("Error", "Тo changes were made", "Ok");
            }
          
        }
    }
}