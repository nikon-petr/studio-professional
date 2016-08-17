using Studio_Professional.Json;
using Studio_Professional.Popups;
using System;
using Windows.Phone.Devices.Notification;
using Windows.Phone.UI.Input;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// Документацию по шаблону элемента пустой страницы см. по адресу http://go.microsoft.com/fwlink/?LinkID=390556

namespace Studio_Professional.Views
{
    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class SpecialOffersDetailsPage : Page
    {
        public SpecialOffersDetailsPage()
        {
            this.InitializeComponent();
            Windows.UI.ViewManagement.StatusBar.GetForCurrentView().ForegroundColor = Colors.Black;
        }

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
            var loadingRing = new ProgressRing
            {
                IsActive = true,
                Width = 60,
                Height = 60,
                Foreground = new SolidColorBrush(color: Colors.Black),
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center,
                Background = new SolidColorBrush(color: Colors.Transparent)
            };
            CoverBorder.Child = loadingRing;

            var response = await App.WebService.ItemPromoJsonResponse((string)e.Parameter);
            var jsonItem = await App.Deserializer.Execute<SpecialOffersAnswer>(response.GetResponseStream());
            response = await App.WebService.GetPromoJsonResponse(jsonItem.Id);
            var jsonDetails = await App.Deserializer.Execute<SpecialOfferDetailsAnswer>(response.GetResponseStream());

            var image = new BitmapImage { UriSource = new Uri(jsonItem.Image) };
            HeaderImage.Source = image;
            image.ImageOpened += (ev, sender) =>
            {
                loadingRing.IsActive = false;
                Windows.UI.ViewManagement.StatusBar.GetForCurrentView().ForegroundColor = Colors.White;
                CoverBorder.Visibility = Visibility.Collapsed;
            };
            HeaderTextBlock.Text = jsonItem.Header;
            TimePeriodTextBlock.Text = jsonDetails.DateOpen.Replace('-','.') + " - " + jsonDetails.DateClose.Replace('-', '.');
            DescriptionTextBlock.Text = jsonDetails.Description;
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
            if (!App.WebService.IsInternetAvailable())
            {
                Frame.BackStack.Clear();
                Frame.Navigate(typeof(MainPage));
                e.Handled = true;
                return;
            }
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
    }
}
