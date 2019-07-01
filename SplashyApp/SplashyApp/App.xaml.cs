﻿using DLToolkit.Forms.Controls;
using SplashyApp.Views;

using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace SplashyApp
{
    public partial class App : Application
    {
        
        public App()
        {
            InitializeComponent();
            // MainPage = new NavigationPage(new AuthorizationPage());
           MainPage = new NavigationPage(new MainPage()); //new NavigationPage(new AuthorizationPage());// new NavigationPage(new MainPage()); //new MainMasterDetailPage(); //new NavigationPage(new MainPage());// new MainMasterDetailPage();  //new NavigationPage(new MainPage());
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
