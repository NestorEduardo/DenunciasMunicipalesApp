using Plugin.Geolocator;

namespace DenunciasMunicipalesApp.Classes
{
    public class GeolocatorService
    {
        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public async System.Threading.Tasks.Task GetLocationAsync()
        {
            var locator = CrossGeolocator.Current;
            locator.DesiredAccuracy = 50;
            var location = await locator.GetPositionAsync(timeoutMilliseconds: 10000);
            Latitude = location.Latitude;
            Longitude = location.Longitude;
        }
    }
}
