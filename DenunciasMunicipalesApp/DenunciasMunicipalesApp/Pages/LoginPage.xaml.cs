using DenunciasMunicipalesApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DenunciasMunicipalesApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            var mainViewModel = MainViewModel.GetInstance();
            mainViewModel.LoginUser = new LoginViewModel();
            InitializeComponent();
        }
    }
}