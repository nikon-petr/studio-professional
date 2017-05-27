using MyToolkit.Multimedia;
using Studio_Professional.Popups;
using System;
using System.Diagnostics;
using Windows.Devices.Geolocation;
using Windows.Phone.Devices.Notification;
using Windows.Phone.UI.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Maps;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// Документацию по шаблону элемента пустой страницы см. по адресу http://go.microsoft.com/fwlink/?LinkID=390556

namespace Studio_Professional.Views
{
    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class AboutPage : Page
    {
        public AboutPage()
        {
            this.InitializeComponent();
            Windows.UI.ViewManagement.StatusBar.GetForCurrentView().ForegroundColor = Windows.UI.Colors.Black;
        }

        private string YouTubeId1, YouTubeId2;

        /// <summary>
        /// Вызывается перед отображением этой страницы во фрейме.
        /// </summary>
        /// <param name="e">Данные события, описывающие, каким образом была достигнута эта страница.
        /// Этот параметр обычно используется для настройки страницы.</param>
        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            HardwareButtons.BackPressed += HardwareButtons_BackPressed;

            if (!App.WebService.IsInternetAvailable())
            {
                Messages.ShowInternetAvailableMessage();
                return;
            }

            Image1.Source = await Models.AboutPage.ConvertBytesToBitmapImage(App.AppRepository.AboutPage.Content.Image1);
            Image2.Source = await Models.AboutPage.ConvertBytesToBitmapImage(App.AppRepository.AboutPage.Content.Image2);
            Image3.Source = await Models.AboutPage.ConvertBytesToBitmapImage(App.AppRepository.AboutPage.Content.Image3);
            Image4.Source = await Models.AboutPage.ConvertBytesToBitmapImage(App.AppRepository.AboutPage.Content.Image4);
            TextHeader.Text = App.AppRepository.AboutPage.Content.TextHeader;
            YouTubeId1 = App.AppRepository.AboutPage.Content.YouTubeId1;
            TextContent.Text = App.AppRepository.AboutPage.Content.TextContent;
            TextContent2.Text = App.AppRepository.AboutPage.Content.TextContent2;
            Image5.Source = await Models.AboutPage.ConvertBytesToBitmapImage(App.AppRepository.AboutPage.Content.Image5);
            Image6.Source = await Models.AboutPage.ConvertBytesToBitmapImage(App.AppRepository.AboutPage.Content.Image6);
            YouTubeId2 = App.AppRepository.AboutPage.Content.YouTubeId2;
            AdressHeader.Text = App.AppRepository.AboutPage.Content.AdressHeader;
            AddressText.Text = App.AppRepository.AboutPage.Content.AdressText;
            ContactHeader.Text = App.AppRepository.AboutPage.Content.ContactHeader;
            ContactsText.Text = App.AppRepository.AboutPage.Content.ContactText;
            PhoneHeader.Text = App.AppRepository.AboutPage.Content.PhoneHeader;
            PhoneText.Text = App.AppRepository.AboutPage.Content.PhoneText;
            GoToVkButton.Tag = new Uri(App.AppRepository.AboutPage.Content.SocialLinkVk);
            GoToTwButton.Tag = new Uri(App.AppRepository.AboutPage.Content.SocialLinkTw);
            GoToFbButton.Tag = new Uri(App.AppRepository.AboutPage.Content.SocialLinkFb);
            GoToInstButton.Tag = new Uri(App.AppRepository.AboutPage.Content.SocialLinkInst);
            
            var mapIcon = new MapIcon();
            var position = new Geopoint(new BasicGeoposition
            {
                Latitude = App.AppRepository.AboutPage.Content.MapX,
                Longitude = App.AppRepository.AboutPage.Content.MapY
            });
            mapIcon.Location = position;
            Map.Center = position;
            Map.MapElements.Add(mapIcon);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            HardwareButtons.BackPressed -= HardwareButtons_BackPressed;
        }

        private void GoToBackButton_Click(object sender, RoutedEventArgs e)
        {
            VibrationDevice vibration = VibrationDevice.GetDefault();
            vibration.Vibrate(TimeSpan.FromMilliseconds(20));
            if (Frame.CanGoBack)
            {
                Frame.GoBack();
            }
        }

        private void HardwareButtons_BackPressed(object sender, BackPressedEventArgs e)
        {
            Frame frame = Window.Current.Content as Frame;
            if (frame == null)
            {
                return;
            }

            if (frame.CanGoBack)
            {
                frame.GoBack();
                e.Handled = true;
            }
        }

        private async void SecondVideo_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if (!App.WebService.IsInternetAvailable())
            {
                return;
            }
            if (SecondVideo.Source == null)
            {
                var url = await YouTube.GetVideoUriAsync(YouTubeId2, YouTubeQuality.Quality360P);
                SecondVideo.Source = url.Uri;
                SecondVideo.AutoPlay = true;
            }
            //if (SecondVideo.CanPause)
            //{
            //    SecondVideo.Pause();
            //}
            //else
            //{
            //    SecondVideo.Play();
            //}
        }

        private async void GoToVkButton_Click(object sender, RoutedEventArgs e)
        {
            var success = await Windows.System.Launcher.LaunchUriAsync(GoToVkButton.Tag as Uri);

            if (!success)
            {
                Messages.ShowLaunchUriErrorMessage();
            }
        }

        private async void GoToFbButton_Click(object sender, RoutedEventArgs e)
        {
            var success = await Windows.System.Launcher.LaunchUriAsync(GoToFbButton.Tag as Uri);

            if (!success)
            {
                Messages.ShowLaunchUriErrorMessage();
            }
        }

        private async void GoToTwButton_Click(object sender, RoutedEventArgs e)
        {
            var success = await Windows.System.Launcher.LaunchUriAsync(GoToTwButton.Tag as Uri);

            if (!success)
            {
                Messages.ShowLaunchUriErrorMessage();
            }

        }

        private async void GoToInstButton_Click(object sender, RoutedEventArgs e)
        {
            var success = await Windows.System.Launcher.LaunchUriAsync(GoToInstButton.Tag as Uri);

            if (!success)
            {
                Messages.ShowLaunchUriErrorMessage();
            }
        }

        private async void FirstVideo_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if (!App.WebService.IsInternetAvailable())
            {
                return;
            }
            if (FirstVideo.Source == null)
            {
                var url = await YouTube.GetVideoUriAsync(YouTubeId1, YouTubeQuality.Quality360P);
                FirstVideo.Source = url.Uri;
                FirstVideo.AutoPlay = true;
            }
            //if (FirstVideo.CanPause)
            //{
            //    FirstVideo.Pause();
            //}
            //else
            //{
            //    FirstVideo.Play();
            //}
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if (!App.WebService.IsInternetAvailable())
            {
                return;
            }
            FirstVideo.Width = this.ActualWidth - 30;
            FirstVideo.Height = (this.ActualWidth - 30) / 16 * 9;
            FirstVideo.PosterSource = new BitmapImage { UriSource = new Uri("http://img.youtube.com/vi/" + YouTubeId1 + "/mqdefault.jpg") };

            SecondVideo.Width = this.ActualWidth - 30;
            SecondVideo.Height = (this.ActualWidth - 30) / 16 * 9;
            SecondVideo.PosterSource = new BitmapImage { UriSource = new Uri("http://img.youtube.com/vi/" + YouTubeId2 + "/mqdefault.jpg") };
        }
    }
}
