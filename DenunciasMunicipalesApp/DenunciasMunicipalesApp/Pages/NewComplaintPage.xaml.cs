using Plugin.Media;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using DenunciasMunicipalesApp.ViewModels;
using Plugin.Geolocator;
using Xamarin.Forms.Maps;

namespace DenunciasMunicipalesApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewComplaintPage : ContentPage
    {
        public NewComplaintPage()
        {
            InitializeComponent();

            var mainViewModel = new MainViewModel();
            mainViewModel.GetGeolotation();
            foreach (Pin item in mainViewModel.Pins)
            {
                MyMap.Pins.Add(item);
            }

            Locator();



            pickVideo.Clicked += async (sender, args) =>
        {
            if (!CrossMedia.Current.IsPickVideoSupported)
            {
                DisplayAlert("Videos Not Supported", ":( Permission not granted to videos.", "OK");
                return;
            }
            var file = await CrossMedia.Current.PickVideoAsync();

            if (file == null)
                return;

            DisplayAlert("Video Selected", "Location: " + file.Path, "OK");
            file.Dispose();
        };
        }

        private async void Locator()
        {
            var locator = CrossGeolocator.Current;
            locator.DesiredAccuracy = 50;

            var location = await locator.GetPositionAsync(timeoutMilliseconds: 10000);
            var position = new Position(location.Latitude, location.Longitude);
            MyMap.MoveToRegion(MapSpan.FromCenterAndRadius(position, Distance.FromMiles(.3)));
        }
    }
}
