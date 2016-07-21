using Studio_Professional.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Phone.Devices.Notification;
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
    public sealed partial class RegistrationPage : Page
    {
        private bool IsNumberValidated { get; set; } = false;
        private bool IsNameValidated { get; set; } = false;

        public RegistrationPage()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Вызывается перед отображением этой страницы во фрейме.
        /// </summary>
        /// <param name="e">Данные события, описывающие, каким образом была достигнута эта страница.
        /// Этот параметр обычно используется для настройки страницы.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            if (!IsNameValidated && !IsNumberValidated)
            {
                VibrationDevice vibration = VibrationDevice.GetDefault();
                vibration.Vibrate(TimeSpan.FromMilliseconds(30));
                return;
            }
            App.UserRepositiry.Insert(new User
            {
                Name = NameTextBox.Text,
                Number = NumberTextBox.Text,
                LastLogin = DateTime.UtcNow
            });
            Frame.Navigate(typeof(TestPage));
        }

        private void NameTextBox_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == Windows.System.VirtualKey.Enter)
            {
                NumberTextBox.Focus(FocusState.Keyboard);
            }
        }

        private void NumberTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (NumberTextBox.Text.Length != 11)
            {
                VibrationDevice vibration = VibrationDevice.GetDefault();
                vibration.Vibrate(TimeSpan.FromMilliseconds(30));
                PhoneValidationMessage.Text = "Номер набран не полностью";
                PhoneValidationMessage.Visibility = Visibility.Visible;
                IsNumberValidated = false;
            }
            else if (NumberTextBox.Text[0] != '7' && NumberTextBox.Text[1] != '9')
            {
                VibrationDevice vibration = VibrationDevice.GetDefault();
                vibration.Vibrate(TimeSpan.FromMilliseconds(30));
                PhoneValidationMessage.Text = "Неверный формат ввода номера";
                PhoneValidationMessage.Visibility = Visibility.Visible;
                IsNumberValidated = false;
            }
            else
            {
                PhoneValidationMessage.Text = String.Empty;
                PhoneValidationMessage.Visibility = Visibility.Collapsed;
                IsNumberValidated = true;
            }
            if (NumberTextBox.Text.Length == 0)
            {
                PhoneGrid.ColumnDefinitions[1].Width = new GridLength(0);
                NumberTextBox.Padding = NameTextBox.Padding;
                PlusSymbloTextBox.Visibility = Visibility.Collapsed;
            }
        }

        private void NumberTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            PhoneGrid.ColumnDefinitions[1].Width = new GridLength(0.07, GridUnitType.Star);
            NumberTextBox.Padding = new Thickness(-2, 0, 0, 0);
            PlusSymbloTextBox.Visibility = Visibility.Visible;
        }

        private void NameTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if(NameTextBox.Text.Length == 0)
            {
                VibrationDevice vibration = VibrationDevice.GetDefault();
                vibration.Vibrate(TimeSpan.FromMilliseconds(30));
                IsNameValidated = false;
                NameValidationMessage.Text = "Введите имя";
                NameValidationMessage.Visibility = Visibility.Visible;
            }
            else
            {
                IsNameValidated = true;
                NameValidationMessage.Text = String.Empty;
                NameValidationMessage.Visibility = Visibility.Collapsed;
            }
        }
    }
}
