using DenunciasMunicipalesApp.Classes;
using DenunciasMunicipalesApp.Models;
using DenunciasMunicipalesApp.Services;
using GalaSoft.MvvmLight.Command;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace DenunciasMunicipalesApp.ViewModels
{
    public class NewComplaintViewModel : Complaint, INotifyPropertyChanged
    {
        #region Attributes
        private ApiService apiService;

        private DialogService dialogService;

        private NavigationService navigationService;

        private GeolocatorService geolocatorService;

        private bool isRunning;

        private bool isEnabled;

        private ImageSource imageSource;

        private MediaFile file;

        public ObservableCollection<Pin> Pins { get; set; }
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

        public ImageSource ImageSource
        {
            set
            {
                if (imageSource != value)
                {
                    imageSource = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ImageSource"));
                }
            }
            get
            {
                return imageSource;
            }
        }

        public ObservableCollection<ComplaintTypesItemViewModel> ComplaintTypes { get; set; }

        #endregion

        #region Constructors
        public NewComplaintViewModel()
        { 

            ComplaintTypes = new ObservableCollection<ComplaintTypesItemViewModel>();

            apiService = new ApiService();
            dialogService = new DialogService();
            navigationService = new NavigationService();
            geolocatorService = new GeolocatorService();

            IsEnabled = true;
            LoadComplaintTypes();

            Pins = new ObservableCollection<Pin>();

        }
        #endregion

        #region Methods
        private async void LoadComplaintTypes()
        {
            var complaintTypes = new List<ComplaintType>();
            complaintTypes = await apiService.Get<ComplaintType>("http://denunciasmunicipalesbackend2.azurewebsites.net", "/api", "/ComplaintTypes");
            ReloadComplaintTypes(complaintTypes);
        }

        private void ReloadComplaintTypes(List<ComplaintType> complaintTypes)
        {
            ComplaintTypes.Clear();

            foreach (var complaintType in complaintTypes.OrderBy(ct => ct.Description))
            {
                ComplaintTypes.Add(new ComplaintTypesItemViewModel
                {
                    ComplaintTypeId = complaintType.ComplaintTypeId,
                    Description = complaintType.Description,
                });
            }
        }

        #endregion

        #region Events
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Commands

        public ICommand TakePictureCommand { get { return new RelayCommand(TakePicture); } }


        public ICommand ClickCommand { get { return new RelayCommand(Click); } }

        private async void Click()
        {

        }



        private async void TakePicture()
        {
            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                await dialogService.ShowMessage("No hay cámara ", ":( No hay cámara disponible");
            }

            file = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions
            {
                Directory = "Sample",
                Name = "test.jpg",
                PhotoSize = PhotoSize.Small,
            });

            IsRunning = true;

            if (file != null)
            {
                ImageSource = ImageSource.FromStream(() =>
                {
                    var stream = file.GetStream();
                    return stream;
                });
            }
        }

        public ICommand TakeVideoCommand { get { return new RelayCommand(TakeVideo); } }

        private async void TakeVideo()
        {
            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakeVideoSupported)
            {
                await dialogService.ShowMessage("No hay cámara ", ":( No hay cámara disponible");
                return;
            }

            var file = await CrossMedia.Current.TakeVideoAsync(new StoreVideoOptions
            {
                Name = "video.mp4",
                Directory = "DefaultVideos",
            });

            if (file == null)
            {
                return;
            }

            await dialogService.ShowMessage("Vídeo grabado en:" + file.Path, "Entendido");

            file.Dispose();
        }

        public ICommand NewComplaintCommand { get { return new RelayCommand(NewComplaint); } }
        private async void NewComplaint()
        {
            if (string.IsNullOrEmpty(Description))
            {
                await dialogService.ShowMessage("Error", "Debe ingresar una descripción");
                return;
            }

            if (string.IsNullOrEmpty(CaseAddress))
            {
                await dialogService.ShowMessage("Error", "Debe ingresar una dirección del caso");
                return;
            }

            var imageArray = FilesHelper.ReadFully(file.GetStream());
            file.Dispose();

            await geolocatorService.GetLocationAsync();

            var complaint = new Complaint
            {
                Description = Description,
                CaseAddress = CaseAddress,
                Date = DateTime.Today,
                CreatedBy = "Alfredo Martinez",
                ImageArray = imageArray,
                ComplaintTypeId = ComplaintTypeId,
                Latitude = geolocatorService.Latitude,
                Longitude = geolocatorService.Longitude,
            };

            IsRunning = true;
            IsEnabled = false;
            var response = await apiService.Post("http://denunciasmunicipalesbackend0.azurewebsites.net", "/api", "/Complaints", complaint);
            IsRunning = false;
            IsEnabled = true;

            if (!response.IsSuccess)
            {
                await dialogService.ShowMessage("Error", response.Message);
                return;
            }

            await dialogService.ShowMessage("Información", "Su denuncia será atendida");
            await navigationService.Back();
        }
        #endregion
    }
}