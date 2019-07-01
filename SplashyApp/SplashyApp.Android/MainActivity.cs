using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms;
using System.Linq;

namespace SplashyApp.Droid
{
    [Activity(Label = "SplashyApp", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
          
           
            LoadApplication(new App());
        }

        //public override bool OnOptionsItemSelected(IMenuItem item)
        //    //Итак, как я упоминал ранее, когда пользователь нажимает на что-либо на навигационной панели Android по умолчанию, 
        //    //запускается вышеупомянутая OnOptionsItemSelected (), где, как мы проверим идентификатор выделенного элемента и проверим 
        //    //идентификатор по умолчанию кнопки возврата. Да, идентификатор кнопки возврата по умолчанию совпадает с целым числом 16908332
        //    //в приложениях Xamarin Forms - Android.
        //{
        //    // check if the current item id 
        //    // is equals to the back button id
        //    if (item.ItemId == 16908332)
        //    {
        //        // retrieve the current xamarin forms page instance
        //        var currentpage = (CoolContentPage)
        //        Xamarin.Forms.Application.
        //        Current.MainPage.Navigation.
        //        NavigationStack.LastOrDefault();

        //        // check if the page has subscribed to 
        //        // the custom back button event
        //        if (currentpage?.CustomBackButtonAction != null)
        //        {
        //            // invoke the Custom back button action
        //            currentpage?.CustomBackButtonAction.Invoke();
        //            // and disable the default back button action
        //            return false;
        //        }

        //        // if its not subscribed then go ahead 
        //        // with the default back button action
        //        return base.OnOptionsItemSelected(item);
        //    }
        //    else
        //    {
        //        // since its not the back button 
        //        //click, pass the event to the base
        //        return base.OnOptionsItemSelected(item);
        //    }
        //}

        //public override void OnBackPressed()
        //{
        //    // this is not necessary, but in Android user 
        //    // has both Nav bar back button and
        //    // physical back button its safe 
        //    // to cover the both events

        //    // retrieve the current xamarin forms page instance
        //    var currentpage = (CoolContentPage)
        //    Xamarin.Forms.Application.
        //    Current.MainPage.Navigation.
        //    NavigationStack.LastOrDefault();

        //    // check if the page has subscribed to 
        //    // the custom back button event
        //    if (currentpage?.CustomBackButtonAction != null)
        //    {
        //        currentpage?.CustomBackButtonAction.Invoke();
        //    }
        //    else
        //    {
        //        base.OnBackPressed();
        //    }
        //}


    }
}


//namespace SplashyApp.Droid
//{
//    public class BetterPickerRenderer : PickerRenderer
//    {
//        protected override void OnElementChanged(ElementChangedEventArgs<Picker> e)
//        {
//            base.OnElementChanged(e);

//            if (Control != null)
//            {
//                Control.Gravity = GravityFlags.CenterHorizontal;
//            }
//        }
//    }
//}