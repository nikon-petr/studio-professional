using System;
using System.Net;
using System.Threading.Tasks;

namespace Studio_Professional.Web
{
    /// <summary>
    /// Предоставляет функционал для взаимодействия с Api
    /// </summary>
    public class WebService
    {
        private static string Scheme { get; } = "http";
        private static string Domain { get; } = "flexed.ru";
        private static string UserPath { get; } = "studioProfessional/Api/Users/";
        private static string ContentPath { get; } = "studioProfessional/Api/Content/";
        private static string PromoPath { get; } = "studioProfessional/Api/Promo/";
        private static string MasterPath { get; set; } = "studioProfessional/Api/Masters/";

        /// <summary>
        /// Асинхронный метод создает запрос с указанным Uri
        /// </summary>
        /// <returns>Возвращает задачу с загружаемым ответом сервера</returns>
        private async Task<WebResponse> MakeRequest(Uri uri)
        {
            try
            {
                WebRequest request = WebRequest.Create(uri);
                return await request.GetResponseAsync();
            }
            catch (TaskCanceledException e)
            {
                // TODO
            }
            catch (WebException e)
            {
                // TODO
            }
            catch (Exception e)
            {
                // TODO
            }
            return null;
        }

        /// <summary>
        /// Асинхронный метод инициирует запрос к скриту RegUser.php
        /// </summary>
        /// <param name="number">Номер телефона</param>
        /// <param name="name">Имя</param>
        public async Task<WebResponse> UserRegistrJsonResponse(string number, string name)
        {
            return await MakeRequest(
                new UriBuilder
                {
                    Scheme = Scheme,
                    Host = Domain,
                    Path = UserPath + "RegUser.php/?",
                    Query = "number=" + number + "&" + "name=" + name
                }
                .Uri);
        }

        /// CHECKED
        /// <summary>
        /// Асинхронный метод инициирует запрос к скриту GetMySale.php
        /// </summary>
        /// <param name="number">Номер телефона</param>
        public async Task<WebResponse> UserSaleJsonResponse(string number)
        {
            return await MakeRequest(
                new UriBuilder
                {
                    Scheme = Scheme,
                    Host = Domain,
                    Path = UserPath + "GetMySale.php/?",
                    Query = "number=" + number
                }
                .Uri
            );
        }

        /// <summary>
        /// Асинхронный метод инициирует запрос к скриту AddSale.php
        /// </summary>
        /// <param name="number">Номер телефона</param>
        /// <param name="code">Код на скидку</param>
        public async Task<WebResponse> AddSaleJsonResponse(string number, string code)
        {
            return await MakeRequest(
                new UriBuilder
                {
                    Scheme = Scheme,
                    Host = Domain,
                    Path = UserPath + "AddSale.php/?",
                    Query = "number=" + number + "&" + "code=" + code
                }
                .Uri);
        }

        /// <summary>
        /// Асинхронный метод инициирует запрос к скриту GetAboutContent.php
        /// </summary>
        public async Task<WebResponse> AboutContentJsonResponse()
        {
            return await MakeRequest(
                new UriBuilder
                {
                    Scheme = Scheme,
                    Host = Domain,
                    Path = ContentPath + "GetAboutContent.php"
                }
                .Uri
            );
        }

        /// <summary>
        /// Асинхронный метод инициирует запрос к скриту GetAboutContentUtd.php
        /// </summary>
        public async Task<WebResponse> AboutContentUtdJsonResponse()
        {
            return await MakeRequest(
                new UriBuilder
                {
                    Scheme = Scheme,
                    Host = Domain,
                    Path = ContentPath + "GetAboutContentUtd.php"
                }
                .Uri
            );
        }

        /// <summary>
        /// Асинхронный метод инициирует запрос к скриту GetCountSlides.php
        /// </summary>
        public async Task<WebResponse> CountSlidesJsonResponse()
        {
            return await MakeRequest(
                new UriBuilder
                {
                    Scheme = Scheme,
                    Host = Domain,
                    Path = ContentPath + "GetCountSlides.php"
                }
                .Uri
            );
        }

        /// <summary>
        /// Асинхронный метод инициирует запрос к скриту GetSlidesImage.php
        /// </summary>
        /// <param name="n">Порядковый номер, начиная с 1</param>
        public async Task<WebResponse> ImageSlideJsonResponse(int n)
        {
            return await MakeRequest(
                new UriBuilder
                {
                    Scheme = Scheme,
                    Host = Domain,
                    Path = ContentPath + "GetSlidesImage.php",
                    Query = "n=" + n.ToString()
                }
                .Uri
            );
        }

        /// <summary>
        /// Асинхронный метод инициирует запрос к скриту GetCountPromo.php
        /// </summary>
        public async Task<WebResponse> CountPromoJsonResponse()
        {
            return await MakeRequest(
                new UriBuilder
                {
                    Scheme = Scheme,
                    Host = Domain,
                    Path = PromoPath + "GetCountPromo.php"
                }
                .Uri
            );
        }

        /// <summary>
        /// Асинхронный метод инициирует запрос к скриту GetListItemPromo.php
        /// </summary>
        /// <param name="n">Порядковый номер, начиная с 1</param>
        public async Task<WebResponse> ItemPromoJsonResponse(int n)
        {
            return await MakeRequest(
                new UriBuilder
                {
                    Scheme = Scheme,
                    Host = Domain,
                    Path = PromoPath + "GetListItemPromo.php",
                    Query = "n=" + n.ToString()
                }
                .Uri
            );
        }

        /// <summary>
        /// Асинхронный метод инициирует запрос к скриту GetPromo.php
        /// </summary>
        /// <param name="id">id акции(не совпадает с порядковым номером)</param>
        public async Task<WebResponse> GetPromoJsonResponse(int id)
        {
            return await MakeRequest(
                new UriBuilder
                {
                    Scheme = Scheme,
                    Host = Domain,
                    Path = PromoPath + "GetPromo.php",
                    Query = "id=" + id.ToString()
                }
                .Uri
            );
        }

        /// <summary>
        /// Асинхронный метод инициирует запрос к скриту GetCatMasters.php
        /// </summary>
        public async Task<WebResponse> GetCategoriesJsonResponse()
        {
            return await MakeRequest(
                new UriBuilder
                {
                    Scheme = Scheme,
                    Host = Domain,
                    Path = MasterPath + "GetCatMasters.php"
                }
                .Uri
            );
        }

        /// <summary>
        /// Асинхронный метод инициирует запрос к скриту GetMasters.php
        /// </summary>
        /// <param name="id">Id категории</param>
        public async Task<WebResponse> GetMasterJsonResponse(int id)
        {
            return await MakeRequest(
                new UriBuilder
                {
                    Scheme = Scheme,
                    Host = Domain,
                    Path = MasterPath + "GetMasters.php",
                    Query = "catId=" + id.ToString()
                }
                .Uri
            );
        }

        /// <summary>
        /// Асинхронный метод инициирует запрос к скриту Send.php
        /// </summary>
        /// <param name="id">Id мастера</param>
        /// <param name="phone">Телефон пользователя</param>
        /// <param name="description">Описание</param>
        /// <param name="date">Дата</param>
        public async Task<WebResponse> SendJsonResponse(int id, string phone, string description, DateTime date)
        {
            return await MakeRequest(
                new UriBuilder
                {
                    Scheme = Scheme,
                    Host = Domain,
                    Path = MasterPath + "Send.php",
                    Query = "masterId=" + id.ToString() + "&phone=" + phone + "&desc=" + description + "&date=" + date.Date.ToString("d.MM.yyyy") 
                }
                .Uri
            );
        }
    }
}
