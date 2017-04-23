using DenunciasMunicipalesApp.Pages;
using DenunciasMunicipalesApp.ViewModels;
using System.Threading.Tasks;

namespace DenunciasMunicipalesApp.Services
{
    public class NavigationService
    {
        public async Task Navigate(string pageName)
        {
            switch (pageName)
            {
                case "NewComplaintPage":
                    var mainViewModel = MainViewModel.GetInstance();
                    mainViewModel.NewComplaint = new NewComplaintViewModel();
                    await App.Current.MainPage.Navigation.PushAsync(new NewComplaintPage());
                    break;
                default:
                    break;
            }
        }

        public async Task Back()
        {
            await App.Current.MainPage.Navigation.PopAsync();
        }
    }
}
