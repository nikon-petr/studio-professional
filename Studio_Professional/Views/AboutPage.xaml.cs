﻿using Studio_Professional.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Devices.Geolocation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Phone.Devices.Notification;
using Windows.Phone.UI.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Maps;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
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

        /// <summary>
        /// Вызывается перед отображением этой страницы во фрейме.
        /// </summary>
        /// <param name="e">Данные события, описывающие, каким образом была достигнута эта страница.
        /// Этот параметр обычно используется для настройки страницы.</param>
        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            HardwareButtons.BackPressed += HardwareButtons_BackPressed;

            Image1.Source = await Models.AboutPage.ConvertBytesToBitmapImage(App.AppRepository.AboutPage.Content.Image1);
            Image2.Source = await Models.AboutPage.ConvertBytesToBitmapImage(App.AppRepository.AboutPage.Content.Image2);
            Image3.Source = await Models.AboutPage.ConvertBytesToBitmapImage(App.AppRepository.AboutPage.Content.Image3);
            Image4.Source = await Models.AboutPage.ConvertBytesToBitmapImage(App.AppRepository.AboutPage.Content.Image4);
            TextHeader.Text = App.AppRepository.AboutPage.Content.TextHeader;
            TextContent.Text = App.AppRepository.AboutPage.Content.TextContent;
            TextContent2.Text = App.AppRepository.AboutPage.Content.TextContent2;
            Image5.Source = await Models.AboutPage.ConvertBytesToBitmapImage(App.AppRepository.AboutPage.Content.Image5);
            Image6.Source = await Models.AboutPage.ConvertBytesToBitmapImage(App.AppRepository.AboutPage.Content.Image6);
            AdressHeader.Text = App.AppRepository.AboutPage.Content.AdressHeader;
            AddressText.Text = App.AppRepository.AboutPage.Content.AdressText;
            ContactHeader.Text = App.AppRepository.AboutPage.Content.ContactHeader;
            ContactsText.Text = App.AppRepository.AboutPage.Content.ContactText;
            PhoneHeader.Text = App.AppRepository.AboutPage.Content.PhoneHeader;
            PhoneText.Text = App.AppRepository.AboutPage.Content.PhoneText;

            var mapIcon = new MapIcon();
            var position = new Geopoint(new BasicGeoposition
            {
                Latitude = App.AppRepository.AboutPage.Content.MapX,
                Longitude = App.AppRepository.AboutPage.Content.MapY
            });
            mapIcon.Location = position;
            Map.Center = position;
            Map.ZoomLevel = 14;
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
    }
}
