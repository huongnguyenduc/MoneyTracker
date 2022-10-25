using MoneyTracker.Views;
using System.Globalization;
using System.Windows.Input;
using Xamarin.Forms;

namespace MoneyTracker.ViewModels
{
    public class AddAccountViewModel : BaseViewModel
    {
        private string _name;
        private float _initialBalance;

        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }
        public float InitialBalance
        {
            get { return _initialBalance; }
            set
            {
                _initialBalance = value;
                OnPropertyChanged(nameof(InitialBalance));
            }
        }

        public string CurrencySymbol
        {
            // TODO: Help user self-change Currency
            get { return NumberFormatInfo.CurrentInfo.CurrencySymbol; }
        }

        public ICommand AddWalletCommand => new Command(AddWallet);
        async void AddWallet()
        {
            if (Name == null || Name == "" || InitialBalance == null)
            {
                await Application.Current.MainPage.DisplayAlert("Alert!", "Fill all fields", "Ok");
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
                await Application.Current.MainPage.DisplayAlert("Success!", "Wallet Added", "Ok");
                await Application.Current.MainPage.Navigation.PushAsync(new MainTabbedPage());

            }
            else
                await Application.Current.MainPage.DisplayAlert("Alert!", "Wallet Exist, try another Name", "Ok");
            Name = "";
            InitialBalance = 0;
        }
    }
}
