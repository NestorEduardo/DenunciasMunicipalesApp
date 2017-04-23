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
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
            accederButton.Clicked += accederButton_Clicked;
        }

        private async void registrarButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new AddUserPage());
        }

        private async void accederButton_Clicked(object sender, EventArgs e)
        {

            Navigation.PushAsync(new ComplaintsPage());
            
            /*if (string.IsNullOrEmpty(txtcorreo.Text))
            {
                await DisplayAlert("Error", "Debe ingregar un correo válido", "Aceptar");
                txtcorreo.Focus();
                txtcorreo.Text = string.Empty;
                return;
            }

            if (string.IsNullOrEmpty(txtcontraseña.Text))
            {
                await DisplayAlert("Error", "Debe ingregar una contraseña válida", "Aceptar");
                txtcontraseña.Focus();
                txtcontraseña.Text = string.Empty;
                return;

            }*/
        }

    }
}
