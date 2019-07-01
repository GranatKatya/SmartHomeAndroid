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
	public partial class GamePage : ContentPage
	{
        MainPage mainPage = null;
        public GamePage() { InitializeComponent(); }
        public GamePage (MainPage mp)
		{
            InitializeComponent();
            mainPage = mp;
           this.BindingContext = mainPage.arduino.MyTemp;
        }   

        private void Switch_Toggled(object sender, ToggledEventArgs e)
        { 

           ViewCell cell = (sender as Switch).Parent.Parent.Parent.Parent as ViewCell;

           Temp t = cell.BindingContext as Temp;

            //// Temp t = sender as Temp;
            if (t != null)
            {

                string topic = "devices/" + t.myDevice.Type + "s/" + t.myDevice.Id + "/command";
                Lamp l = t.myDevice as Lamp;
                if (l.boolState)
                {
                    l.State = "on";
                }else {
                    l.State = "off"; }
               
                if (l != null)
                {
                    mainPage.mqttConnection.Publish(topic, l.State, true);
                }
            }
        }
    }
}

