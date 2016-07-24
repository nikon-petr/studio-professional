using Studio_Professional.Views;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Phone.UI.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

// Документацию по шаблону элемента "Пустая страница" см. по адресу http://go.microsoft.com/fwlink/?LinkId=391641

namespace Studio_Professional
{
    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private Storyboard flipFromMainToFirst, flipFromFirstToMain;

        private DialogStep PageState = DialogStep.Main;

        public MainPage()
        {
            this.InitializeComponent();

            Windows.UI.ViewManagement.StatusBar.GetForCurrentView().ForegroundColor = Windows.UI.Colors.Black;
            NavigationCacheMode = NavigationCacheMode.Required;


            flipFromMainToFirst = MainContentGrid.Resources["flipFromMainToFirst"] as Storyboard;
            flipFromFirstToMain = MainContentGrid.Resources["flipFromFirstToMain"] as Storyboard;
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

        private void HardwareButtons_BackPressed(object sender, BackPressedEventArgs e)
        {
            if(PageState == DialogStep.FirstStep)
            {
                flipFromFirstToMain.Begin();
                PageState = DialogStep.Main;
                e.Handled = true;
            }
        }

        private void GoToAboutButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(AboutPage), null);
        }

        private void GoToDiscountButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(DiscountPage));
        }

        private void GoToSpetialOffersButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(SpecialOffersPage));
        }

        private void GoToGalleryButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Gallery));
        }

        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void RequesCall_Click(object sender, RoutedEventArgs e)
        {
            if(PageState == DialogStep.Main)
            {
                flipFromMainToFirst.Begin();
                PageState = DialogStep.FirstStep;
            }
        }
    }

    enum DialogStep
    {
        Main,
        FirstStep,
        SecondStep,
        ThirdStep
    }
}
