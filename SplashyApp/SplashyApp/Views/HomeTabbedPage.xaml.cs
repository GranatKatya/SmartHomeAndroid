using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SplashyApp.Views
{
	//[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class HomeTabbedPage : TabbedPage
    {
        MainPage mainPage = null;
        public HomeTabbedPage(MainPage mp)  
        {
            InitializeComponent();
            mainPage = mp;


            this.Title = "TabbedPage";
            this.Children.Add(new NavigationPage(new GamePage(mainPage)));
            this.Children.Add(new NavigationPage(new GamePage(mainPage)));
           // this.Children.Add(new NavigationPage(new GamePage()));
            this.Children.Add(new NavigationPage (new MoviePage()));
            this.Children.Add(new NavigationPage(new CreateSchedule()));

            this.Children.Add( new NavigationPage( new ContentPage
            {
                Title = "Blue",
                Content = new BoxView
                {
                    Color = Color.Blue,
                    HeightRequest = 100f,
                    VerticalOptions = LayoutOptions.Center
                },
            })
            );
            this.Children.Add(new NavigationPage( new ContentPage
            {
                Title = "Blue and Red",
                Content = new StackLayout
                {
                    Children = {
                    new BoxView { Color = Color.Blue },
                    new BoxView { Color = Color.Red}
                }
                }
            }));




            if (Application.Current.Properties.ContainsKey("MenuName"))
            {
                var menuName = Application.Current.Properties["MenuName"].ToString();
                if (menuName == "Movies")
                {
                    this.CurrentPage = Children[1];
                }
            }
        }
    }
}