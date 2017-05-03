using DenunciasMunicipalesApp.Pages;
using DenunciasMunicipalesApp.ViewModels;
using System.Threading.Tasks;

namespace DenunciasMunicipalesApp.Services
{
    public class NavigationService
    {
        public async Task Navigate(string pageName)
        {
            var mainViewModel = MainViewModel.GetInstance();

            switch (pageName)
            {
                case "NewComplaintPage":
                    mainViewModel.NewComplaint = new NewComplaintViewModel();
                    await App.Current.MainPage.Navigation.PushAsync(new NewComplaintPage());
                    break;
                case "NewUserPage":
                    mainViewModel.NewUser = new NewUserViewModel();
                    await App.Current.MainPage.Navigation.PushAsync(new NewUserPage());
                    break;
                case "ComplaintsPage":
                    mainViewModel.Complaints = new ComplaintsViewModel();
                    await App.Current.MainPage.Navigation.PushAsync(new ComplaintsPage());
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
