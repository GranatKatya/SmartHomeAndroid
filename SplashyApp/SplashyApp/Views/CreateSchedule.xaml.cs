using SplashyApp.Models;
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
	public partial class CreateSchedule : ContentPage
	{
		public CreateSchedule ()
		{
			InitializeComponent ();
            

            //CronExpressionPicker.Items.Add("sadasd");
            //CronExpressionPicker.Items.Add("111");
            //CronExpressionPicker.Items.Add("sa222dasd");


        
            CronExpressionPicker.ItemsSource = new List<string>(CronExpression.cronExpressions.Keys);

        }

        private void SelectedIndexChanged(object sender, EventArgs e)
        {
          var name =   CronExpressionPicker.Items[CronExpressionPicker.SelectedIndex];

          //  DisplayAlert(name, "Selected value", "ok");
        }

        private void Ok(object sender, EventArgs e)
        {

        }

        private void Cancel(object sender, EventArgs e)
        {

        }
    }
}