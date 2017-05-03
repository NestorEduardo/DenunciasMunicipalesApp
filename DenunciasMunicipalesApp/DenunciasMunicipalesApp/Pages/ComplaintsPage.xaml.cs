using DenunciasMunicipalesApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DenunciasMunicipalesApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ComplaintsPage : ContentPage
    {
        public ComplaintsPage()
        {
            var mainViewModel = MainViewModel.GetInstance();
            mainViewModel.Complaints = new ComplaintsViewModel();
            InitializeComponent();
        }
    }
}
