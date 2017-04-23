using DenunciasMunicipalesApp.Services;
using GalaSoft.MvvmLight.Command;
using System;
using System.ComponentModel;
using System.Windows.Input;

namespace DenunciasMunicipalesApp.ViewModels
{
    public class NewComplaintViewModel : INotifyPropertyChanged
    {
        #region Attributes
        private ApiService apiService;

        private DialogService dialogService;

        private NavigationService navigationService;

        private string description;

        private decimal caseAddress;

        private DateTime date { get; set; }

        private string createdBy { get; set; }

        private bool isRunning;

        private bool isEnabled;
        #endregion

        #region Properties

        public string Descrpition
        {
            set
            {
                if (description != value)
                {
                    description = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Descrpition"));
                }
            }
            get
            {
                return description;
            }
        }

        public decimal CaseAddress
        {
            set
            {
                if (caseAddress != value)
                {
                    caseAddress = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("CaseAddress"));
                }
            }
            get
            {
                return caseAddress;
            }
        }

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
        public NewComplaintViewModel()
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
        public ICommand NewComplaintCommand { get { return new RelayCommand(NewComplaint); } }

        private async void NewComplaint()
        {
            if (string.IsNullOrEmpty(Descrpition))
            {
                await dialogService.ShowMessage("Error", "Debe ingresar una descripción");
                return;
            }

            if (string.IsNullOrEmpty(Descrpition))
            {
                await dialogService.ShowMessage("Error", "Debe ingresar una dirección del caso");
                return;
            }
        }
        #endregion
    }
}