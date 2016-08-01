using System;
using Windows.UI.Popups;

namespace Studio_Professional.Popups
{
    public class Messages
    {
        /// <summary>
        /// Показывает сообщение о ошибке в интернет соединении
        /// </summary>
        public static async void ShowInternetAvailableMessage()
        {
            await new MessageDialog("Интернет соединене не доступно", "Ошибка! ;(").ShowAsync();
        }

        /// <summary>
        /// Показывает сообщение о ошибке при десериализации ответа сервера
        /// </summary>
        public static async void ShowJsonDeserializationErrorMessage()
        {
            await new MessageDialog("Неизвестный ответ сервера", "Ошибка! ;(").ShowAsync();
        }

        /// <summary>
        /// Показывает сообщение о ошибке при отправке запроса серверу
        /// </summary>
        public static async void ShowWebRequestErrorMessage()
        {
            await new MessageDialog("Ошибка при отправке запроса", "Ошибка! ;(").ShowAsync();
        }

        /// <summary>
        /// Показывает сообщение о ошибке при работе с базой данных
        /// </summary>
        public static async void ShowDatabaseErrorMessage()
        {
            await new MessageDialog("Ошибка в базе данных", "Ошибка! ;(").ShowAsync();
        }

        /// <summary>
        /// ПОказывает сообщение об ошибке
        /// </summary>
        /// <param name="title">Заголовок</param>
        /// <param name="content">Текст сообщения</param>
        public static async void ShowErrorMessage(string title, string content)
        {
            await new MessageDialog(content, title).ShowAsync();
        }

        public static async void ShowErrorMessage(string content)
        {
            await new MessageDialog(content, "Непредвиденная ошибка!").ShowAsync();
        }
    }
}
