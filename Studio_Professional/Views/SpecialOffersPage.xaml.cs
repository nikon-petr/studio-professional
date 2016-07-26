using Studio_Professional.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Phone.Devices.Notification;
using Windows.Phone.UI.Input;
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
    public sealed partial class SpecialOffersPage : Page
    {
        private int Count;

        public SpecialOffersPage()
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
            var response = await App.WebService.CountPromoJsonResponse();
            var json = await App.Deserializer.Execute<SimpleAnswer>(response.GetResponseStream());
            if (int.TryParse(json.Answer, out Count))
            {
                // TODO
            }
            var leftButtonStyle = this.Resources["LeftButton"] as Style;
            var rightButtonStyle = this.Resources["RightButton"] as Style;
            var textBlockStyle = this.Resources["ButtonContentTextBLock"] as Style;
            var even = true;
            Debug.WriteLine(Count);
            for (int i = 0; i < Count; i++, even = !even)
            {
                response = await App.WebService.ItemPromoJsonResponse(i + 1);
                var jsonItem = await App.Deserializer.Execute<SpecialOffersAnswer>(response.GetResponseStream());
                if(jsonItem.Answer == JsonAnswers.NOTFOUND || jsonItem.Answer == JsonAnswers.NaN)
                {
                    // TODO
                }
                if (even)
                {
                    ContentGrid.RowDefinitions.Add(new RowDefinition());
                }
                var button = new Button
                {
                    Style = even ? leftButtonStyle : rightButtonStyle,
                    Background = new ImageBrush { ImageSource = new BitmapImage(new Uri(jsonItem.Image)) },
                    Content = new TextBlock { Style = textBlockStyle, Text = jsonItem.Header }
                };
                button.Click += (sender, ev) =>
                {
                    Frame.Navigate(typeof(SpecialOffersDetailsPage), jsonItem.Id + 1);
                };
                Grid.SetRow(button, ContentGrid.RowDefinitions.Count() - 1);
                Debug.WriteLine(ContentGrid.RowDefinitions.Count() - 1);
                ContentGrid.Children.Add(button);
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

        private void b1_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(SpecialOffersDetailsPage));
        }
    }
}
