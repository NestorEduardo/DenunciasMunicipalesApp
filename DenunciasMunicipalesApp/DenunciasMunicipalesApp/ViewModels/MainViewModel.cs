using DenunciasMunicipalesApp.Services;

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
    }
}
