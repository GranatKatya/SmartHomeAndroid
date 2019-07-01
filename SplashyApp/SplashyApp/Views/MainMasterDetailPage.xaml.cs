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
    public partial class MainMasterDetailPage : MasterDetailPage
    {
        public MainMasterDetailPage()
        {
            InitializeComponent();
            navigationDrawerList.ItemsSource = GetMasterPageLists();
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
               // Detail = new NavigationPage(new HomeTabbedPage());
                IsPresented = false;    
            }
        }
        List<MasterPageList> GetMasterPageLists()
        {
            return new List<MasterPageList>
            {
                new MasterPageList() { Title = "Games", Icon = "home.png" },
                new MasterPageList() { Title = "Movies", Icon = "admin.png" },
                new MasterPageList() { Title = "Setting", Icon = "setting.png" }
            };
        }
    }


    //This class used for binding ListView. We can move it to other separate files as well   
    public class MasterPageList
    {
        public string Title { get; set; }
        public string Icon { get; set; }
    }
}