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

        private DialogService dialogService;
        #endregion

        #region Properties
        public NewComplaintViewModel NewComplaint{ get; set; }
        #endregion

        #region Constructors
        public MainViewModel()
        {
            // Singleton
            instance = this;

            // Services
            apiService = new ApiService();
            navigationService = new NavigationService();
            dialogService = new DialogService();
        }
        #endregion

        #region Methods

        #endregion

        #region Commands
        public ICommand AddComplaintCommand { get { return new RelayCommand(AddComplaint); } }

        public ICommand AddUserCommand { get { return new RelayCommand(AddUser); } }

        private async void AddComplaint()
        {
            await navigationService.Navigate("NewComplaintPage");
        }

        private async void AddUser()
        {
            await navigationService.Navigate("AddUserPage");
        }
        #endregion

        #region Singleton
        private static MainViewModel instance;

        public static MainViewModel GetInstance()
        {
            if (instance == null)
            {
                instance = new MainViewModel();
            }

            return instance;
        }
        #endregion
    }
}
