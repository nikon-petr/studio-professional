using Studio_Professional.Json;
using Studio_Professional.Popups;
using System;
using System.Linq;
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
    public sealed partial class SpecialOffersPage : Page
    {
        private int Count;

        public SpecialOffersPage()
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
            for (int i = 0; i < Count; i++, even = !even)
            {
                response = await App.WebService.ItemPromoJsonResponse((i + 1).ToString());
                var jsonItem = await App.Deserializer.Execute<SpecialOffersAnswer>(response.GetResponseStream());

                if (jsonItem.Answer == JsonAnswers.NOTFOUND || jsonItem.Answer == JsonAnswers.NaN)
                {
                    // TODO
                }
                if (even)
                {
                    ContentGrid.RowDefinitions.Add(new RowDefinition());
                }
                var image = new BitmapImage(new Uri(jsonItem.Image));
                var button = new Button
                {
                    Style = even ? leftButtonStyle : rightButtonStyle,
                    Background = new ImageBrush { ImageSource = image },
                    Content = new TextBlock { Style = textBlockStyle, Text = jsonItem.Header },
                    Tag = (i + 1).ToString()
                };
                //var border = new Border
                //{
                //    IsHitTestVisible = false,
                //    Child = new TextBlock { Style = textBlockStyle, Text = jsonItem.Header },
                //    Background = new SolidColorBrush(color: new Color { A = 127, R = 0, B = 0, G = 0 }),
                //    Margin = button.Margin,
                //    VerticalAlignment = VerticalAlignment.Center,
                //    Width = button.Width,
                //    Height = button.Height,
                //    HorizontalAlignment = button.HorizontalAlignment
                //};
                button.Click += (sender, ev) =>
                {
                    Frame.Navigate(typeof(SpecialOffersDetailsPage), (sender as Button).Tag as string);
                };
                //Canvas.SetZIndex(border, 1);
                Grid.SetRow(button, ContentGrid.RowDefinitions.Count() - 1);
                //Grid.SetRow(border, ContentGrid.RowDefinitions.Count() - 1);
                //Grid.SetColumn(border, even ? 0 : 1);
                ContentGrid.Children.Add(button);
                //ContentGrid.Children.Add(border);
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
