using SplashyApp.Views.AboutUS;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace SplashyApp
{

    public class PersonDataTemplateSelector : DataTemplateSelector
    {
        public DataTemplate lampTemplate { get; set; }
        public DataTemplate InvalidTemplate { get; set; }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
        //    Temp t = item as Temp;
        //    return t?.myDevice?.Type == "lamp" ? ValidTemplate : InvalidTemplate;
            // return ((Temp)item)?.myDevice?.Type == "lamp" ? ValidTemplate : InvalidTemplate;
            if (item != null)
            {
                return ((Temp)item)?.myDevice.Type == "lamp" ? lampTemplate : InvalidTemplate;
            
            }
            else {
                return lampTemplate; }
          
        }
    }
}
