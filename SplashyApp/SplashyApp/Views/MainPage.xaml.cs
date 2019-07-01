#undef WINDOWS_APP

using SplashyApp.Models;
using SplashyApp.Views.AboutUS;

using SplashyApp.Views.Configuration;

using SplashyApp.Views.Home;
using SplashyApp.Views.Profile_Settings;
using SplashyApp.Views.Rooms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using uPLibrary.Networking.M2Mqtt;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SplashyApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : MasterDetailPage
    {
        public List<MasterPageItem> menuList { get; set; }

        //  public  MQTTConnection mqttClientMobile;
        //  public static MqttServer mqttClientArduino;

        public MQTTConnection mqttConnection;
        public   Arduino arduino;
        public  List<MobileDevice> mobileDevices;
        //   public List<SplashyApp.Models.Device> listDevices;
        public Models.Rooms Rooms { get; set; }
        public Dictionary<string, string> NeedLoadedRoomsAndDevices;



        public MainPage()
        {

            InitializeComponent();

            //Content = new HeaderView();
            menuList = new List<MasterPageItem>();


            //Fot Android / IOS icons
            var page1 = new MasterPageItem() {id = 1, Title = "Home", Icon = "Home.png" };
            var page2 = new MasterPageItem() {id = 2,  Title = "Devices", Icon = "About.png" };
            var page3 = new MasterPageItem() {id = 3 , Title = "Configuration", Icon = "Configuration.png" };
            var page4 = new MasterPageItem() {id = 4, Title = "Profile", Icon = "ProfileSetting.png" };
            var page5 = new MasterPageItem() { id = 5, Title = "Configuration", Icon = "Configuration.png" };
            var page6 = new MasterPageItem() { id = 6, Title = "Profile Settings", Icon = "ProfileSetting.png" };
            var page7 = new MasterPageItem() { id = 7, Title = "Rooms", Icon = "Home.png" };

            menuList.Add(page1);
            menuList.Add(page2);
            menuList.Add(page7);
            menuList.Add(page3);
            menuList.Add(page4);
            menuList.Add(page5);
            menuList.Add(page6);

          


            navigationDrawerList.ItemsSource = menuList;

            //  Detail = new NavigationPage((Page)Activator.CreateInstance(typeof(HomePage)));



            // Remove page before Edit Page
            //     this.Navigation.RemovePage(this.Navigation.NavigationStack[this.Navigation.NavigationStack.Count ]);

            arduino = new Arduino();
            mobileDevices = new List<MobileDevice>();
            mqttConnection = new MQTTConnection("", "", "", this);
            // listDevices = new  List<SplashyApp.Models.Device>();
            Rooms = new Models.Rooms() { };
            NeedLoadedRoomsAndDevices = new Dictionary<string, string>();

            // MqttClient client = new MqttClient("35.158.109.193", MqttSettings.MQTT_BROKER_DEFAULT_PORT, false, null, null, MqttSslProtocols.None);           
            //int  code = client.Connect("A", "server", "ri0Xohsuozahr2aB", true, 150);


            Detail = new HomeTabbedPage(this);
        }


        private void OnMenuSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = (MasterPageList)e.SelectedItem;

            if (item.Title == "Setting")
            {
                Detail.Navigation.PushAsync(new SettingPage());
                IsPresented = false;
            }
            else
            {
                Application.Current.Properties["MenuName"] = item.Title;
                Detail = new NavigationPage(new HomeTabbedPage(this));
                IsPresented = false;
            }
        }


        void UpdateNewDevicesInRooms()
        {
            for (int i = 0; i < NeedLoadedRoomsAndDevices.Count; i++)
            {

                string topic = (NeedLoadedRoomsAndDevices.ElementAt(i).Key);
                string[] topicword = topic.Split(new string[] { "/" }, StringSplitOptions.RemoveEmptyEntries);
                string type = topicword[1];
                if (type.IndexOf("kitchen") > -1)
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

                SelectedRoom item = (from change in Rooms.MyRooms where change.Key == topicword[2] select change).FirstOrDefault();
                if (item != null)
                {
                    item.myRoom.UpdateConfig(NeedLoadedRoomsAndDevices.ElementAt(i).Value, NeedLoadedRoomsAndDevices.ElementAt(i).Key, arduino.MyTemp, type, NeedLoadedRoomsAndDevices);
                    NeedLoadedRoomsAndDevices.Remove(NeedLoadedRoomsAndDevices.ElementAt(i).Key);
                    i--;
                }
            }

        }

        async private void Handle_ItemTapped(object sender, Xamarin.Forms.ItemTappedEventArgs e)
        {
            UpdateNewDevicesInRooms();

            var myselecteditem = e.Item as MasterPageItem;

            switch (myselecteditem.id)
            {

                case 1:
                    await Navigation.PushAsync(new HomePage());
                   
                   
                    break;
                case 2:
                    await Navigation.PushAsync(new AboutUSPage(this));
                 
                    break;
                case 3:
                    await Navigation.PushAsync(new ConfigurationPage());
                  
                    break;
                case 4:
                    await Navigation.PushAsync(new ProfileSettingsPage());

                    break;
                case 5:
                    await Navigation.PushAsync(new ProfileSettingsPage());

                    break;
                case 6:
                    await Navigation.PushAsync(new ProfileSettingsPage());

                    break;
                case 7:
                    await Navigation.PushAsync(new RoomsPage(this));

                    break;


            }
            ((ListView)sender).SelectedItem = null;
            IsPresented = false;


        }
    }

 
 

  
    }
