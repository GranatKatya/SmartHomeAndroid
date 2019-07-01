using SplashyApp.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace SplashyApp.ViewModel
{
    public class LoginViewModel
    {
        private CheckLogin checkLogin = new CheckLogin();

        public string UserName { get; set; }
        public string Password { get; set; }
        public string CPassword { get; set; }

        public ICommand LoginCommand
        {
            get
            {
                return new Command(() =>
                {
                    //проверка Й!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
                   bool res =  checkLogin.LoginAsync(UserName, Password, CPassword);


                   

                    //  MessagingCenter.Send<MainPage>(this, "Hi");

                    //  DisplayAlert("Alert", "You have been alerted", "OK");

                });
            }
        }
    }



    public class CheckLogin
    {
        public bool LoginAsync(string login, string password, string cpassword)
        {

            if (login == "katya" && (password == "1111" && cpassword== password))
            {
                return true;
            }return false;
        }
    }
}
