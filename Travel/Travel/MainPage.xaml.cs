using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Travel
{
	public partial class MainPage : ContentPage
	{
        public MainPage()
        {
            InitializeComponent();
            var assembly = typeof(MainPage);
            iconImage.Source = ImageSource.FromResource("Travel.Assest.Images.logo.png", assembly);

        }
        private void LoginButton_Clicked(object sender, EventArgs e)
        {
            bool isEmailEmpty = string.IsNullOrEmpty(emailEntry.Text);
            bool isPasswordEmpty = string.IsNullOrEmpty(passwordEntry.Text);

            if (isEmailEmpty || isPasswordEmpty)
            {

            }
            else
            {
                Navigation.PushAsync(new HomePage());
            }
        }
    }
}
