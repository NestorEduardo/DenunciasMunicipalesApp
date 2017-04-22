using DenunciasMunicipalesApp.Services;
using GalaSoft.MvvmLight.Command;
using System.Windows.Input;
using System;

namespace DenunciasMunicipalesApp.ViewModels
{
    public class MainViewModel
    {
        #region Attributes

        private ApiService apiService;

        #endregion

        #region Properties

        #endregion

        #region Constructors

        public MainViewModel()
        {
            apiService = new ApiService();
        }

        #endregion

        #region Methods

        #endregion

        #region Commands

        public ICommand AddComplaintCommand { get { return new RelayCommand(AddComplaint); } }

        private void AddComplaint()
        {
            throw new NotImplementedException();
        }

        #endregion

    }
}
