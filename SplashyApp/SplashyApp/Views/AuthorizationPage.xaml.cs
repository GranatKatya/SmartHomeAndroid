using SplashyApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SplashyApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AuthorizationPage : ContentPage
    {
        private CheckLogin checkLogin = new CheckLogin();
        public AuthorizationPage()
        {
            InitializeComponent();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            //Navigation.PushAsync(new NavigationPage(new MainPage()));
            // Detail = new NavigationPage((Page)Activator.CreateInstance(typeof(HomePage)));
            //  Detail.Navigation.PushAsync(new SettingPage());
            //  Detail = new NavigationPage(new MainPage());

            //  await Navigation.PushAsync(new ProfileSettingsPage());

            bool res = checkLogin.LoginAsync(Login.Text, Password.Text, ConfirmPass.Text);

            if (res)
            {
                DisplayAlert("Sing in", "Done", "OK");


                // await Navigation.PushAsync((new MainPage()));
                Navigation.PushAsync((new MainPage()));


                this.Navigation.RemovePage(this.Navigation.NavigationStack[this.Navigation.NavigationStack.Count - 2]);

            }
            else
            {
                DisplayAlert("Sing in", "You entered non exists data", "OK");
            }

        }
    }



    

}