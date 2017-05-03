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

        public NewUserViewModel NewUser { get; set; }

        public LoginViewModel LoginUser { get; set; }

        public ComplaintsViewModel Complaints { get; set; }
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

     //   public ICommand ComaplintsCommand { get { return new RelayCommand(Complaints); } }

        private async void AddComplaint()
        {
            await navigationService.Navigate("NewComplaintPage");
        }

        private async void AddUser()
        {
            await navigationService.Navigate("NewUserPage");
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
