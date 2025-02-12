﻿using Studio_Professional.Json;
using Studio_Professional.Repository;
using Studio_Professional.Views;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;
using System.Net;
using OneSignalSDK_WP_WNS;
using Studio_Professional.Models;
using Studio_Professional.Popups;

// Документацию по шаблону "Пустое приложение" см. по адресу http://go.microsoft.com/fwlink/?LinkId=391641

namespace Studio_Professional
{
    /// <summary>
    /// Обеспечивает зависящее от конкретного приложения поведение, дополняющее класс Application по умолчанию.
    /// </summary>
    public sealed partial class App : Application
    {
        private TransitionCollection transitions;

        public static AppRepository AppRepository { get; } = new AppRepository();
        public static Web.WebService WebService { get; } = new Web.WebService();
        public static Json.Deserializer Deserializer { get; } = new Json.Deserializer();

        /// <summary>
        /// Инициализирует одноэлементный объект приложения.  Это первая выполняемая строка разрабатываемого
        /// кода; поэтому она является логическим эквивалентом main() или WinMain().
        /// </summary>
        public App()
        {
            this.InitializeComponent();
            this.Suspending += this.OnSuspending;
            this.RequestedTheme = ApplicationTheme.Light;
        }

        /// <summary>
        /// Вызывается при обычном запуске приложения пользователем.  Будут использоваться другие точки входа,
        /// если приложение запускается для открытия конкретного файла, отображения
        /// результатов поиска и т. д.
        /// </summary>
        /// <param name="e">Сведения о запросе и обработке запуска.</param>
        protected override async void OnLaunched(LaunchActivatedEventArgs e)
        {
#if DEBUG
            if (System.Diagnostics.Debugger.IsAttached)
            {
                this.DebugSettings.EnableFrameRateCounter = true;
            }
#endif
            OneSignal.Init("2baab644-8886-4090-82ea-8aac8dfcb9ac", e);

            if (App.WebService.IsInternetAvailable())
            {
                WebResponse response = await WebService.AboutContentJsonResponse();
                var json = await Deserializer.Execute<AboutAnswer>(response.GetResponseStream());
                 Debug.WriteLine(json.SocialLinkYb == null);
                var model = await json.GetModel();
                if (AppRepository.AboutPage.Content == null)
                {
                    AppRepository.AboutPage.Insert(model);
                }
                if (AppRepository.AboutPage.Content.Utd != model.Utd)
                {
                    AppRepository.AboutPage.UpdatePageContent(model);
                }
            }

            Frame rootFrame = Window.Current.Content as Frame;

            // Не повторяйте инициализацию приложения, если в окне уже имеется содержимое,
            // только обеспечьте активность окна
            if (rootFrame == null)
            {
                // Создание фрейма, который станет контекстом навигации, и переход к первой странице
                rootFrame = new Frame();

                // TODO: Измените это значение на размер кэша, подходящий для вашего приложения
                rootFrame.CacheSize = 0;

                // Задайте язык по умолчанию
                rootFrame.Language = Windows.Globalization.ApplicationLanguages.Languages[0];

                if (e.PreviousExecutionState == ApplicationExecutionState.Terminated)
                {
                    // TODO: Загрузить состояние из ранее приостановленного приложения
                }

                // Размещение фрейма в текущем окне
                Window.Current.Content = rootFrame;
            }

            if (rootFrame.Content == null)
            {
                // Удаляет турникетную навигацию для запуска.
                if (rootFrame.ContentTransitions != null)
                {
                    this.transitions = new TransitionCollection();
                    foreach (var c in rootFrame.ContentTransitions)
                    {
                        this.transitions.Add(c);
                    }
                }

                rootFrame.ContentTransitions = null;
                rootFrame.Navigated += this.RootFrame_FirstNavigated;

                // Если стек навигации не восстанавливается для перехода к первой странице,
                // настройка новой страницы путем передачи необходимой информации в качестве параметра
                // навигации
                if (AppRepository.User.Data == null)
                {
                    if (!rootFrame.Navigate(typeof(RegistrationPage), e.Arguments))
                    {
                        throw new Exception("Failed to create initial page");
                    }
                }
                else
                {
                    if (!rootFrame.Navigate(typeof(MainPage), e.Arguments))
                    {
                        throw new Exception("Failed to create initial page");
                    }
                }
            }

            // Обеспечение активности текущего окна
            Window.Current.Activate();
        }

        /// <summary>
        /// Восстанавливает переходы содержимого после запуска приложения.
        /// </summary>
        /// <param name="sender">Объект, где присоединен обработчик.</param>
        /// <param name="e">Сведения о событии перехода.</param>
        private void RootFrame_FirstNavigated(object sender, NavigationEventArgs e)
        {
            var rootFrame = sender as Frame;
            rootFrame.ContentTransitions = this.transitions ?? new TransitionCollection() { new NavigationThemeTransition() };
            rootFrame.Navigated -= this.RootFrame_FirstNavigated;
        }

        /// <summary>
        /// Вызывается при приостановке выполнения приложения.  Состояние приложения сохраняется
        /// без учета информации о том, будет ли оно завершено или возобновлено с неизменным
        /// содержимым памяти.
        /// </summary>
        /// <param name="sender">Источник запроса приостановки.</param>
        /// <param name="e">Сведения о запросе приостановки.</param>
        private void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();

            // Сохранение времени последнего входа в приложение
            AppRepository.User.UpdateDate();

            // TODO: Сохранить состояние приложения и остановить все фоновые операции
            deferral.Complete();
        }
    }
}