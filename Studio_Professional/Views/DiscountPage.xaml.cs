﻿using Studio_Professional.Json;
using Studio_Professional.Popups;
using System;
using Windows.Phone.Devices.Notification;
using Windows.Phone.UI.Input;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// Документацию по шаблону элемента пустой страницы см. по адресу http://go.microsoft.com/fwlink/?LinkID=390556

namespace Studio_Professional.Views
{
    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class DiscountPage : Page
    {
        public DiscountPage()
        {
            this.InitializeComponent();
            Windows.UI.ViewManagement.StatusBar.GetForCurrentView().ForegroundColor = Windows.UI.Colors.Black;
            NavigationCacheMode = NavigationCacheMode.Disabled;
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
            LoadingRing.IsActive = true;
            LoadingRingBackground.Visibility = Visibility.Visible;
            var response = await App.WebService.UserSaleJsonResponse(App.AppRepository.User.Data.Number);
            var json = await App.Deserializer.Execute<SaleAnswer>(response.GetResponseStream());

            if (json.Answer == JsonAnswers.WRONGNUMBER || json.Answer == JsonAnswers.NODATA)
            {
                await new MessageDialog(json.Answer, "Ошибка").ShowAsync();
                return;
            }
            DiscountTextBlock.Text = json.Answer;
            LoadingRing.IsActive = false;
            LoadingRingBackground.Visibility = Visibility.Collapsed;
            ContentTextBlock.Text = json.Content;

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

        private void GetDiscountButton_Click(object sender, RoutedEventArgs e)
        {
            if (!App.WebService.IsInternetAvailable())
            {
                Messages.ShowInternetAvailableMessage();
                return;
            }
            Frame.Navigate(typeof(CodePage));
        }
    }
}
