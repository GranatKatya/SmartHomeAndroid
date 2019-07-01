using SplashyApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SplashyApp.Views.Home
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : ContentPage
    {

        public HomePage()
        {
           // InitializeComponent();


            Label header = new Label
            {
                Text = "Список моделей",
                FontSize = Xamarin.Forms.Device.GetNamedSize(NamedSize.Large, typeof(Label)),
                HorizontalOptions = LayoutOptions.Center
            };

            string[] phones = new string[] { "iPhone 7", "Samsung Galaxy S8", "Huawei P10", "LG G6" };

            ListView listView = new ListView();
            // определяем источник данных
            listView.ItemsSource = phones;
            this.Content = new StackLayout { Children = { header, listView } };

        }

    }
}