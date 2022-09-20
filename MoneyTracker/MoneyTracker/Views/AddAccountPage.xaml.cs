using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MoneyTracker.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddAccountPage : ContentPage
    {
        public string Name { get; set; }
        public float InitialBalance { get; set; }

        public AddAccountPage()
        {
            InitializeComponent();
            BindingContext = this;
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            if (Name == null || Name == "" || InitialBalance == null)
            {
                await DisplayAlert("Alert!", "Fill all fields", "Ok");
                return;
            }

            if (await Services.DatabaseConnection.VerifyIfAccExist(Name))
            {
                await Services.DatabaseConnection.AddAccount(
                    new Models.Account
                    {
                        Name = Name,
                        Balance = InitialBalance
                    });
                await DisplayAlert("Success!", "Wallet Added", "Ok");
                await Navigation.PushAsync(new MainTabbedPage());

            }
            else
                await DisplayAlert("Alert!", "Wallet Exist, try another Name", "Ok");

            accountName.Text = "";
            accountBalance.Text = "";

            Name = "";
            InitialBalance = 0;
        }

        private async void Button_Clicked_1(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MainTabbedPage());
        }
    }
}