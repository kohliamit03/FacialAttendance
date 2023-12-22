namespace FacialRecognition
{
    public partial class MainPage : ContentPage
    {
        //int count = 0;
        private static string? selectedDeviceId = null;
        public MainPage()
        {
            InitializeComponent();
            //try
            //{ 
            //    DsDevice[] captureDevices = DsDevice.GetDevicesOfCat(FilterCategory.VideoInputDevice);
            //    var devices = new List<DsDevice>(DsDevice.GetDevicesOfCat(FilterCategory.VideoInputDevice));
            //    var cameraNames = new List<string>();
            //    foreach (var device in devices)
            //    {
            //        camera_devices.Items.Add(device.Name);
            //    }
            //}
            //catch (Exception ex)
            //{
            //}
        }

        private async Task GetPhoto()
        {
            if (MediaPicker.IsCaptureSupported)
            {
                FileResult photo = await MediaPicker.CapturePhotoAsync();
                if (photo != null)
                {
                    // save the file into local storage
                    string localFilePath = Path.Combine(FileSystem.CacheDirectory, photo.FileName);

                    using Stream sourceStream = await photo.OpenReadAsync();
                    using FileStream localFileStream = File.OpenWrite(localFilePath);

                    await sourceStream.CopyToAsync(localFileStream);
                }
            }
        }

        private void cameraView_CameraLoaded(object sender, EventArgs e)
        {
            var cameraList = cameraView.Cameras;
            cameraView.MirroredImage = false;
            foreach (var cams in cameraList)
            {
                camera_devices.Items.Add(cams.Name);
            }
        }

        private void camera_SelectionChanged(object sender, EventArgs e)
        {
            //if(camera_devices.SelectedIndex == 0)
            //{
            //    selectedDeviceId = null;
            //    MainThread.BeginInvokeOnMainThread(async () =>
            //    {
            //        await cameraView.StopCameraAsync();
            //    });
            //}
            //else
            //{
            selectedDeviceId = cameraView.Cameras[((Picker)sender).SelectedIndex].DeviceId;
            MainThread.BeginInvokeOnMainThread(async () =>
            {
                await cameraView.StopCameraAsync();
                cameraView.Camera = cameraView.Cameras[((Picker)sender).SelectedIndex];

                await cameraView.StartCameraAsync();
            });
            //}
            //
            //else
            //{
            //    MainThread.BeginInvokeOnMainThread(async () =>
            //    {
            //        await cameraView.StopCameraAsync();
            //        selectedDeviceId = 
            //     });
            //}
        }
    }
}
