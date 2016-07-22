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
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

// Документацию по шаблону элемента пустой страницы см. по адресу http://go.microsoft.com/fwlink/?LinkID=390556

namespace Studio_Professional.Views
{
    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class RegistrationPage : Page
    {
        private Storyboard NameMessageFlipStoryboard;
        private Storyboard NameMessageFlipBackStoyboard;
        private Storyboard NumberMessageFlipStoryboard;
        private Storyboard NumberMessageFlipBackStoyboard;

        private bool IsNumberValidated { get; set; } = false;
        private bool IsNameValidated { get; set; } = false;

        public RegistrationPage()
        {
            this.InitializeComponent();

            NameMessageFlipStoryboard = NameValidationMessage.Resources["flipStoryBoard"] as Storyboard;
            NameMessageFlipBackStoyboard = NameValidationMessage.Resources["flipBackStoryBoard"] as Storyboard;
            NumberMessageFlipStoryboard = PhoneValidationMessage.Resources["flipStoryBoard"] as Storyboard;
            NumberMessageFlipBackStoyboard = PhoneValidationMessage.Resources["flipBackStoryBoard"] as Storyboard;
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
                Storyboard storyboard = NumberMessageFlipStoryboard;
                storyboard.Begin();
                VibrationDevice vibration = VibrationDevice.GetDefault();
                vibration.Vibrate(TimeSpan.FromMilliseconds(30));
                PhoneValidationMessage.Text = "Номер набран не полностью";
                IsNumberValidated = false;
            }
            else if (NumberTextBox.Text[0] != '7' || NumberTextBox.Text[1] != '9')
            {
                Storyboard storyboard = NumberMessageFlipStoryboard;
                storyboard.Begin();
                VibrationDevice vibration = VibrationDevice.GetDefault();
                vibration.Vibrate(TimeSpan.FromMilliseconds(30));
                PhoneValidationMessage.Text = "Неверный формат ввода номера";
                IsNumberValidated = false;
            }
            else
            {
                Storyboard storyboard = NumberMessageFlipBackStoyboard;
                storyboard.Begin();
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
                Storyboard storyboard = NameMessageFlipStoryboard;
                storyboard.Begin();
                VibrationDevice vibration = VibrationDevice.GetDefault();
                vibration.Vibrate(TimeSpan.FromMilliseconds(30));
                IsNameValidated = false;
                NameValidationMessage.Text = "Введите имя";
            }
            else
            {
                Storyboard storyboard = NameMessageFlipBackStoyboard;
                storyboard.Begin();
                IsNameValidated = true;
            }
        }
    }
}
