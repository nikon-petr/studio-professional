using Studio_Professional.Json;
using Studio_Professional.Popups;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Phone.Devices.Notification;
using Windows.Phone.UI.Input;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// Документацию по шаблону элемента пустой страницы см. по адресу http://go.microsoft.com/fwlink/?LinkID=390556

namespace Studio_Professional.Views
{
    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class Gallery : Page
    {
        public Gallery()
        {
            this.InitializeComponent();
            Windows.UI.ViewManagement.StatusBar.GetForCurrentView().ForegroundColor = Windows.UI.Colors.Black;
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
            var response = await App.WebService.CountSlidesJsonResponse();
            var jsonCount = await App.Deserializer.Execute<SimpleAnswer>(response.GetResponseStream());

            var images = new List<Image>();

            for (int i = 0; i < int.Parse(jsonCount.Answer); i++)
            {
                response = await App.WebService.ImageSlideJsonResponse(i + 1);
                var jsonLink = await App.Deserializer.Execute<SimpleAnswer>(response.GetResponseStream());
                var progressRing = new ProgressRing
                {
                    IsActive = true,
                    Width = 60,
                    Height = 60,
                    Background = new SolidColorBrush(color: Colors.Transparent),
                    Foreground = new SolidColorBrush(color: Colors.Black),
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center
                };
                var image = new Image
                {
                    Source = new BitmapImage { UriSource = new Uri(jsonLink.Answer) },
                    HorizontalAlignment = HorizontalAlignment.Center,
                    Stretch = Stretch.Uniform,
                    Tag = i
                };
                var grid = new Grid();
                grid.Children.Add(progressRing);
                grid.Children.Add(image);
                image.ImageOpened += (sv, ev) =>
                {
                    progressRing.IsActive = false;
                };
                image.Tapped += (sv, ev) =>
                {
                    if (ev.GetPosition(image).X < this.ActualWidth / 2)
                    {
                        if (GalleryPivot.SelectedIndex == 0)
                        {
                            GalleryPivot.SelectedIndex = GalleryPivot.Items.Count - 1;
                            return;
                        }
                        GalleryPivot.SelectedIndex--;
                    }
                    else
                    {
                        if (GalleryPivot.SelectedIndex == GalleryPivot.Items.Count - 1)
                        {
                            GalleryPivot.SelectedIndex = 0;
                            return;
                        }
                        GalleryPivot.SelectedIndex++;
                    }
                };
                GalleryPivot.Items.Add(new PivotItem
                {
                    Content = grid,
                    Margin = new Thickness(0)
                });
            }
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
    }
}
