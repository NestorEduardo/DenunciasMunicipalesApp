using DenunciasMunicipalesApp.Models;
using DenunciasMunicipalesApp.Services;
using GalaSoft.MvvmLight.Command;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Windows.Input;

namespace DenunciasMunicipalesApp.ViewModels
{
    public class LoginViewModel : User, INotifyPropertyChanged
    {
        #region Attributes
        private ApiService apiService;

        private DialogService dialogService;

        private NavigationService navigationService;

        private bool isRunning;

        private bool isEnabled;
        #endregion

        #region Properties
        public bool IsRunning
        {
            set
            {
                if (isRunning != value)
                {
                    isRunning = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("IsRunning"));
                }
            }
            get
            {
                return isRunning;
            }
        }

        public bool IsEnabled
        {
            set
            {
                if (isEnabled != value)
                {
                    isEnabled = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("IsEnabled"));
                }
            }
            get
            {
                return isEnabled;
            }
        }
        #endregion

        #region Constructors
        public LoginViewModel()
        {
            apiService = new ApiService();
            dialogService = new DialogService();
            navigationService = new NavigationService();

            IsEnabled = true;
        }
        #endregion

        #region Events
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Commands
        public ICommand LoginCommand { get { return new RelayCommand(LoginUser); } }

        private async void LoginUser()
        {
            if (string.IsNullOrEmpty(Email))
            {
                await dialogService.ShowMessage("Error", "Debe ingresar un correo electrónico");
                return;
            }

            if (!IsAValidEmail(Email))
            {
                await dialogService.ShowMessage("Error", "Debe ingresar un correo electrónico válido");
                return;
            }

            if (string.IsNullOrEmpty(Password))
            {
                await dialogService.ShowMessage("Error", "Debe ingresar una contraseña");
                return;
            }

            var user = new User
            {
                Email = Email,
                Password = Password,
            };

            IsRunning = true;
            IsEnabled = false;
            var users = await apiService.Get<User>("http://denunciasmunicipalesbackend0.azurewebsites.net/", "/api", "/Users");
            IsRunning = false;
            IsEnabled = true;

            foreach (var userItem in users)
            {
                if (user.Email == userItem.Email)
                {
                    if (user.Password == userItem.Password)
                    {
                        await navigationService.Navigate("ComplaintsPage");
                        return;
                    }
                }
            }

            await dialogService.ShowMessage("Error", "Correo electrónico o contraseña inválida");
            return;
        }

        public ICommand AddUserCommand { get { return new RelayCommand(AddUser); } }

        private async void AddUser()
        {
            await navigationService.Navigate("NewUserPage");
            return;
        }
        #endregion

        #region Methods
        Regex EmailRegex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
        public bool IsAValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            return EmailRegex.IsMatch(email);
        }
        #endregion
    }
}
