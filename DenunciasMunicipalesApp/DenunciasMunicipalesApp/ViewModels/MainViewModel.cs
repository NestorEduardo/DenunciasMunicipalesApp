using DenunciasMunicipalesApp.Services;
using GalaSoft.MvvmLight.Command;
using System.Windows.Input;

namespace DenunciasMunicipalesApp.ViewModels
{
    public class MainViewModel
    {
        #region Attributes
        private ApiService apiService;

        private NavigationService navigationService;
        #endregion

        #region Properties
        public NewComplaintViewModel NewComplaint{ get; set; }
        #endregion

        #region Constructors
        public MainViewModel()
        {
            // Services
            apiService = new ApiService();
            navigationService = new NavigationService();
        }
        #endregion

        #region Methods

        #endregion

        #region Commands
        public ICommand AddComplaintCommand { get { return new RelayCommand(AddComplaint); } }

        private async void AddComplaint()
        {
            await navigationService.Navigate("NewComplaintPage");
        }
        #endregion
    }
}
