using SplashyApp.Views;
using SplashyApp.Views.Rooms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SplashyApp.ViewModel
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class InfoRooms : ContentPage
	{

        RoomsPage roomsPage = null;
        public AddRoomPageViewModel AddRoomPageViewModel { get; set; } = new AddRoomPageViewModel();
        public InfoRooms(RoomsPage rp)
		{
			InitializeComponent ();
            roomsPage = rp;

            AddRoomPageViewModel.Name = roomsPage.selectedItem.myRoom.Name;
            AddRoomPageViewModel.Count = roomsPage.selectedItem.myRoom.Count;
            AddRoomPageViewModel.Type = roomsPage.selectedItem.myRoom.Type;
            AddRoomPageViewModel.Key = roomsPage.selectedItem.myRoom.Key;

            this.BindingContext = AddRoomPageViewModel;
            DevicesList.BindingContext = roomsPage.selectedItem.myRoom.RoomDevice;
            
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