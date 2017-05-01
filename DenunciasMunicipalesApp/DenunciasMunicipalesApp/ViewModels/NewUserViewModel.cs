using DenunciasMunicipalesApp.Classes;
using DenunciasMunicipalesApp.Models;
using DenunciasMunicipalesApp.Services;
using GalaSoft.MvvmLight.Command;
using System.ComponentModel;
using System.Windows.Input;

namespace DenunciasMunicipalesApp.ViewModels
{
    public class NewUserViewModel : User, INotifyPropertyChanged
    {
        #region Attributes
        private ApiService apiService;

        private DialogService dialogService;

        private NavigationService navigationService;

        private bool isRunning;

        private bool isEnabled;

        private string passwordConfirmation;
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

        public string PasswordConfirmation
        {
            set
            {
                if (PasswordConfirmation != value)
                {
                    passwordConfirmation = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("PasswordConfirmation"));
                }
            }
            get
            {
                return passwordConfirmation;
            }
        }
        #endregion

        #region Constructors
        public NewUserViewModel()
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
        public ICommand NewUserCommand { get { return new RelayCommand(NewUser); } }
        private async void NewUser()
        {
            if (string.IsNullOrEmpty(FullName))
            {
                await dialogService.ShowMessage("Error", "Debe ingresar un nombre");
                return;
            }

            if (FullName.Length < 3)
            {
                await dialogService.ShowMessage("Error", "El nombre debe ser mayor o igual a tres caracteres");
                return;
            }

            if (string.IsNullOrEmpty(Email))
            {
                await dialogService.ShowMessage("Error", "Debe ingresar un correo electrónico");
                return;
            }

            if (Email.Length < 3)
            {
                await dialogService.ShowMessage("Error", "El correo electrónico debe ser mayor o igual a tres caracteres");
                return;
            }

            if (string.IsNullOrEmpty(Password))
            {
                await dialogService.ShowMessage("Error", "Debe ingresar una contraseña");
                return;
            }

            if (Password.Length < 4)
            {
                await dialogService.ShowMessage("Error", "La contraseña debe ser mayor o igual a cuatro caracteres");
                return;
            }

            if (string.IsNullOrEmpty(PasswordConfirmation))
            {
                await dialogService.ShowMessage("Error", "Debe ingresar una contraseña");
                return;
            }

            if (Password != PasswordConfirmation)
            {
                await dialogService.ShowMessage("Error", "La contraseña y la confirmación no coinciden");
                return;
            }

            var user = new User
            {
                Email = Email,
                FullName = FullName,
                Password = Password,
                Points = 0,
            };

            IsRunning = true;
            IsEnabled = false;
            var users = await apiService.Get<User>("http://denunciasmunicipalesbackend2.azurewebsites.net", "/api", "/Users");

            foreach (var userItem in users)
            {
                if (userItem.Email == user.Email)
                {
                    await dialogService.ShowMessage("Error", "Ya existe un usuario registrado con ese correo electrónico");
                    IsRunning = false;
                    IsEnabled = true;
                    return;
                }
            }

            var response = await apiService.Post("http://denunciasmunicipalesbackend2.azurewebsites.net", "/api", "/Users", user);


            if (!response.IsSuccess)
            {
                await dialogService.ShowMessage("Error", response.Message);
                return;
            }

            await dialogService.ShowMessage("Información", "Gracias por registrarte");
            await navigationService.Navigate("NewComplaintPage");
        }

        #endregion
    }
}
