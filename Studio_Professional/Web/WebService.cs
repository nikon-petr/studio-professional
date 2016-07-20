using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Studio_Professional.Web
{
    public class WebService
    {
        private static string UriDomain { get; } = "http://flexed.ru/studioProfessional/Api/";
        private static string UserCategory { get; } = "Users/";
        private static string ContentCategory { get; } = "Content/";
        private static string PromoCategory { get; } = "Promo/";

        private async Task<Stream> MakeGetRequest(string Uri)
        {
            try
            {
                WebRequest request = WebRequest.Create(Uri);
                WebResponse response = await request.GetResponseAsync();
                return response.GetResponseStream();
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

        public async Task<Stream> RegistrationRequest(string number, string name)
        {
            string requestId = "RegUser.php/?";
            string parametrs = "number=" + number + "&" + "name" + name;
            return await MakeGetRequest(UriDomain + UserCategory + requestId + parametrs);
        }

        public async Task<Stream> GetUserSale(string number)
        {
            string requestId = "GetMySale.php/?";
            string parametrs = "number=" + number;
            return await MakeGetRequest(UriDomain + UserCategory + requestId + parametrs);
        }
    }
}
