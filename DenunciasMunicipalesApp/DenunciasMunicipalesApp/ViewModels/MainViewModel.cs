using DenunciasMunicipalesApp.Services;
using GalaSoft.MvvmLight.Command;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms.Maps;

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
        public NewComplaintViewModel NewComplaint { get; set; }

        public NewUserViewModel NewUser { get; set; }

        public LoginViewModel LoginUser { get; set; }

        public ComplaintsViewModel Complaints { get; set; }

        public ObservableCollection<Pin> Pins { get; set; }
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

            Pins = new ObservableCollection<Pin>();
        }
        #endregion

        #region Methods
        public void GetGeolotation()
        {
            var position1 = new Position(6.2652880, -75.5098530);
            var pin1 = new Pin
            {
                Type = PinType.Place,
                Position = position1,
                Label = "Pin1",
                Address = "prueba pin1"
            };
            Pins.Add(pin1);

            var position2 = new Position(6.2652880, -75.4598530);
            var pin2 = new Pin
            {
                Type = PinType.Place,
                Position = position2,
                Label = "Pin2",
                Address = "prueba pin2"
            };
            Pins.Add(pin2);

            var position3 = new Position(6.2652880, -75.4898530);
            var pin3 = new Pin
            {
                Type = PinType.Place,
                Position = position3,
                Label = "Pin3",
                Address = "prueba pin3"
            };
            Pins.Add(pin3);
        }
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
