using Studio_Professional.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Phone.Devices.Notification;
using Windows.Phone.UI.Input;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
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
    public sealed partial class CodePage : Page
    {
        public CodePage()
        {
            this.InitializeComponent();
            Windows.UI.ViewManagement.StatusBar.GetForCurrentView().ForegroundColor = Windows.UI.Colors.Black;
        }

        /// <summary>
        /// Вызывается перед отображением этой страницы во фрейме.
        /// </summary>
        /// <param name="e">Данные события, описывающие, каким образом была достигнута эта страница.
        /// Этот параметр обычно используется для настройки страницы.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            HardwareButtons.BackPressed += HardwareButtons_BackPressed;
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
            if (Frame.CanGoBack)
            {
                e.Handled = true;
                Frame.GoBack();
            }
        }

        private async void PasswordBox_LostFocus(object sender, RoutedEventArgs e)
        {
            int code;
            if(!int.TryParse(PasswordTextBox.Password, out code)) {
                MessageDialog msg = new MessageDialog("Пароль должен состоять из цифр", "Ошибка");
                await msg.ShowAsync();
                return;
            }
            var response = await App.WebService.AddSaleJsonResponse(App.AppRepository.User.Data.Number, PasswordTextBox.Password);
            var json = await App.Deserializer.Execute<SimpleAnswer>(response.GetResponseStream());

            if(json.Answer == JsonAnswers.OK)
            {
                VibrationDevice vibration = VibrationDevice.GetDefault();
                vibration.Vibrate(TimeSpan.FromMilliseconds(30));
                Frame.GoBack();

                var response1 = await App.WebService.UserSaleJsonResponse(App.AppRepository.User.Data.Number);
                var json1 = await App.Deserializer.Execute<SimpleAnswer>(response1.GetResponseStream());

                if (json1.Answer == JsonAnswers.OK)
                {
                    App.AppRepository.User.Data.Discount = json1.Answer;
                    return;
                }
            }
            else
            {
                MessageDialog msg = new MessageDialog(json.Answer, "Ошибка");
                await msg.ShowAsync();
            }
        }
    }
}
