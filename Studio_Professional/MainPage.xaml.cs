using Studio_Professional.Json;
using Studio_Professional.Popups;
using Studio_Professional.Views;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Phone.UI.Input;
using Windows.UI.Popups;
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
        private Storyboard flipFromFirstToSecond, flipFromSecondToFirst;
        private Storyboard flipFromSecondToThird, flipFromThirdToSecond;
        private Storyboard flipFromThirdToMain;

        private DialogStep PageState = DialogStep.Main;
        private CallRequest CallRequestData;
        
        private List<Button> categoryButtons;
        private List<Button> masterButtons;


        public MainPage()
        {
            this.InitializeComponent();

            Windows.UI.ViewManagement.StatusBar.GetForCurrentView().ForegroundColor = Windows.UI.Colors.Black;
            NavigationCacheMode = NavigationCacheMode.Required;
            
            flipFromMainToFirst = MainContentGrid.Resources["flipFromMainToFirst"] as Storyboard;
            flipFromFirstToMain = MainContentGrid.Resources["flipFromFirstToMain"] as Storyboard;
            flipFromFirstToSecond = RequestCallStepOne.Resources["flipFromFirstToSecond"] as Storyboard;
            flipFromSecondToFirst = RequestCallStepOne.Resources["flipFromSecondToFirst"] as Storyboard;
            flipFromSecondToThird = RequestCallStepTwo.Resources["flipFromSecondToThird"] as Storyboard;
            flipFromThirdToSecond = RequestCallStepTwo.Resources["flipFromThirdToSecond"] as Storyboard;
            flipFromThirdToMain = RequestCallStepThree.Resources["flipFromThirdToMain"] as Storyboard;

            TargetDatePicker.MinYear = DateTimeOffset.Now;
        }

        /// <summary>
        /// Вызывается перед отображением этой страницы во фрейме.
        /// </summary>
        /// <param name="e">Данные события, описывающие, каким образом была достигнута эта страница.
        /// Этот параметр обычно используется для настройки страницы.</param>
        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            HardwareButtons.BackPressed += HardwareButtons_BackPressed;
            CallRequestData = new CallRequest();

            if (!App.WebService.IsInternetAvailable())
            {
                Messages.ShowInternetAvailableMessage();
                return;
            }
            if(categoryButtons == null)
            {
                categoryButtons = new List<Button>();
                var response = await App.WebService.GetCategoriesJsonResponse();
                var categories = await App.Deserializer.Execute<CategoriesAnswer>(response.GetResponseStream());
                CategoryProgressRing.IsActive = false;

                foreach (var category in categories.Categories)
                {
                    var button = new Button
                    {
                        Style = CategoriesStack.Resources["ListItem"] as Style,
                        Content = category.Name
                    };
                    button.Click += async (sender, c) =>
                    {
                        if (PageState == DialogStep.FirstStep)
                        {
                            if (!App.WebService.IsInternetAvailable())
                            {
                                Messages.ShowInternetAvailableMessage();
                                return;
                            }
                            flipFromFirstToSecond.Begin();
                            PageState = DialogStep.SecondStep;
                            CallRequestData.CategoryId = category.Id;

                            MasterStack.Children.Clear();
                            masterButtons = new List<Button>(10);
                            var res = await App.WebService.GetMasterJsonResponse(CallRequestData.CategoryId);
                            var masters = await App.Deserializer.Execute<MastersAnswer>(res.GetResponseStream());
                            MasterProgressRing.IsActive = false;

                            foreach (var master in masters.Masters)
                            {
                                var btn = new Button
                                {
                                    Style = MasterStack.Resources["ListItem"] as Style,
                                    Content = master.Name
                                };

                                btn.Click += (senderc, cc) =>
                                {
                                    if (PageState == DialogStep.SecondStep)
                                    {
                                        if (!App.WebService.IsInternetAvailable())
                                        {
                                            Messages.ShowInternetAvailableMessage();
                                            return;
                                        }
                                        flipFromSecondToThird.Begin();
                                        PageState = DialogStep.ThirdStep;
                                        CallRequestData.MasterId = master.Id;
                                    }
                                };
                                masterButtons.Add(btn);
                                MasterStack.Children.Add(btn);
                            }
                        }
                    };
                    categoryButtons.Add(button);
                    CategoriesStack.Children.Add(button);
                }
            }
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            HardwareButtons.BackPressed -= HardwareButtons_BackPressed;
        }

        private void HardwareButtons_BackPressed(object sender, BackPressedEventArgs e)
        {
            if (PageState == DialogStep.FirstStep)
            {
                flipFromFirstToMain.Begin();
                PageState = DialogStep.Main;
                e.Handled = true;
            }
            if (PageState == DialogStep.SecondStep)
            {
                flipFromSecondToFirst.Begin();
                PageState = DialogStep.FirstStep;
                e.Handled = true;
            }
            if (PageState == DialogStep.ThirdStep)
            {
                flipFromThirdToSecond.Begin();
                PageState = DialogStep.SecondStep;
                e.Handled = true;
            }
        }

        private void GoToAboutButton_Click(object sender, RoutedEventArgs e)
        {
            if (!App.WebService.IsInternetAvailable())
            {
                Messages.ShowInternetAvailableMessage();
                return;
            }
            Frame.Navigate(typeof(AboutPage), null);
        }

        private void GoToDiscountButton_Click(object sender, RoutedEventArgs e)
        {
            if (!App.WebService.IsInternetAvailable())
            {
                Messages.ShowInternetAvailableMessage();
                return;
            }
            Frame.Navigate(typeof(DiscountPage), "MainPage navigated from");
        }

        private void GoToSpetialOffersButton_Click(object sender, RoutedEventArgs e)
        {
            if (!App.WebService.IsInternetAvailable())
            {
                Messages.ShowInternetAvailableMessage();
                return;
            }
            Frame.Navigate(typeof(SpecialOffersPage));
        }

        private void GoToGalleryButton_Click(object sender, RoutedEventArgs e)
        {
            if (!App.WebService.IsInternetAvailable())
            {
                Messages.ShowInternetAvailableMessage();
                return;
            }
            Frame.Navigate(typeof(Gallery));
        }

        private async void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            if (!App.WebService.IsInternetAvailable())
            {
                Messages.ShowInternetAvailableMessage();
                return;
            }
            if (PageState == DialogStep.ThirdStep)
            {
                if (!App.WebService.IsInternetAvailable())
                {
                    Messages.ShowInternetAvailableMessage();
                    return;
                }
                var responce = await App.WebService.SendJsonResponse(CallRequestData.MasterId, App.AppRepository.User.Data.Number,
                    DescriptionTextBox.Text, TargetDatePicker.Date.Date);
                var json = await App.Deserializer.Execute<SimpleAnswer>(responce.GetResponseStream());
                if (json.Answer == JsonAnswers.OK)
                {
                    flipFromThirdToMain.Begin();
                    PageState = DialogStep.Main;
                    MessageDialog msgBox = new MessageDialog("", "Заявка отправлена");
                    await msgBox.ShowAsync();
                }
                else
                {
                    flipFromThirdToMain.Begin();
                    PageState = DialogStep.Main;
                    MessageDialog msgBox = new MessageDialog(json.Answer, "Ошибка");
                    await msgBox.ShowAsync();
                }
            }
        }

        private async void RequesCall_Click(object sender, RoutedEventArgs e)
        {
            if (!App.WebService.IsInternetAvailable())
            {
                Messages.ShowInternetAvailableMessage();
                return;
            }
            if (categoryButtons == null)
            {
                categoryButtons = new List<Button>();
                var response = await App.WebService.GetCategoriesJsonResponse();
                var categories = await App.Deserializer.Execute<CategoriesAnswer>(response.GetResponseStream());
                CategoryProgressRing.IsActive = false;

                foreach (var category in categories.Categories)
                {
                    var button = new Button
                    {
                        Style = CategoriesStack.Resources["ListItem"] as Style,
                        Content = category.Name
                    };
                    button.Click += async (senderv, c) =>
                    {
                        if (PageState == DialogStep.FirstStep)
                        {
                            if (!App.WebService.IsInternetAvailable())
                            {
                                Messages.ShowInternetAvailableMessage();
                                return;
                            }
                            flipFromFirstToSecond.Begin();
                            PageState = DialogStep.SecondStep;
                            CallRequestData.CategoryId = category.Id;

                            MasterStack.Children.Clear();
                            masterButtons = new List<Button>(10);
                            var res = await App.WebService.GetMasterJsonResponse(CallRequestData.CategoryId);
                            var masters = await App.Deserializer.Execute<MastersAnswer>(res.GetResponseStream());
                            MasterProgressRing.IsActive = false;

                            foreach (var master in masters.Masters)
                            {
                                var btn = new Button
                                {
                                    Style = MasterStack.Resources["ListItem"] as Style,
                                    Content = master.Name
                                };

                                btn.Click += (senderc, cc) =>
                                {
                                    if (PageState == DialogStep.SecondStep)
                                    {
                                        if (!App.WebService.IsInternetAvailable())
                                        {
                                            Messages.ShowInternetAvailableMessage();
                                            return;
                                        }
                                        flipFromSecondToThird.Begin();
                                        PageState = DialogStep.ThirdStep;
                                        CallRequestData.MasterId = master.Id;
                                    }
                                };
                                masterButtons.Add(btn);
                                MasterStack.Children.Add(btn);
                            }
                        }
                    };
                    categoryButtons.Add(button);
                    CategoriesStack.Children.Add(button);
                }
            }
            if (PageState == DialogStep.Main)
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

    class CallRequest
    {
        public int CategoryId { get; set; }

        public int MasterId { get; set; }

        public DateTime Date { get; set; }

        public string Description { get; set; }
    }
}
