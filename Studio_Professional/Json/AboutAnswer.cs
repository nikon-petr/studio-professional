using Studio_Professional.Models;
using System;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Studio_Professional.Json
{
    /// <summary>
    /// Ответ Api содержащий инфорацию для страницы "О нас"
    /// </summary>
    [DataContract]
    public class AboutAnswer : SimpleAnswer
    {
        [DataMember(Name = "Image1-1")]
        public string Image1Uri { get; set; }

        [DataMember(Name = "Image1-2")]
        public string Image2Uri { get; set; }

        [DataMember(Name = "Image1-3")]
        public string Image3Uri { get; set; }

        [DataMember(Name = "Image1-4")]
        public string Image4Uri { get; set; }

        [DataMember(Name = "textBlock1Header")]
        public string TextHeader { get; set; }

        [DataMember(Name = "textBlock1Content")]
        public string TextContent { get; set; }

        [DataMember(Name = "YoutubeId1")]
        public string YouTubeId1 { get; set; }

        [DataMember(Name = "textBlock2Content")]
        public string TextContent2 { get; set; }

        [DataMember(Name = "Image2-1")]
        public string Image5Uri { get; set; }

        [DataMember(Name = "Image2-2")]
        public string Image6Uri { get; set; }

        [DataMember(Name = "YoutubeId2")]
        public string YouTubeId2 { get; set; }

        [DataMember]
        public string AdressHeader { get; set; }

        [DataMember]
        public string AdressText { get; set; }

        [DataMember]
        public string ContactHeader { get; set; }

        [DataMember]
        public string ContactText { get; set; }

        [DataMember]
        public string PhoneHeader { get; set; }

        [DataMember]
        public string PhoneText { get; set; }

        [DataMember]
        public string SocialLinkVk { get; set; }

        [DataMember]
        public string SocialLinkTw { get; set; }

        [DataMember]
        public string SocialLinkInst { get; set; }

        [DataMember]
        public string SocialLinkFb { get; set; }

        [DataMember]
        public string SocialLinkYb { get; set; }

        [DataMember(Name = "Map-x")]
        public double MapX { get; set; }

        [DataMember(Name = "Map-y")]
        public double MapY { get; set; }

        [DataMember(Name = "Utd")]
        public string DateString { get; set; }

        public async Task<AboutPage> GetModel()
        {
            return await Task.Run(async () =>
            {
                var aboutPage = new AboutPage
                {
                    Id = 42,
                    Image1 = await App.WebService.DownloadImage(Image1Uri),
                    Image2 = await App.WebService.DownloadImage(Image2Uri),
                    Image3 = await App.WebService.DownloadImage(Image3Uri),
                    Image4 = await App.WebService.DownloadImage(Image4Uri),
                    Image5 = await App.WebService.DownloadImage(Image5Uri),
                    Image6 = await App.WebService.DownloadImage(Image6Uri),
                    TextHeader = TextHeader,
                    TextContent = TextContent,
                    TextContent2 = TextContent2,
                    AdressHeader = AdressHeader,
                    AdressText = AdressText,
                    ContactHeader = ContactHeader,
                    ContactText = ContactText,
                    PhoneHeader = PhoneHeader,
                    PhoneText = PhoneText,
                    YouTubeId1 = YouTubeId1,
                    YouTubeId2 = YouTubeId2,
                    SocialLinkFb = SocialLinkFb,
                    SocialLinkInst = SocialLinkInst,
                    SocialLinkTw = SocialLinkTw,
                    SocialLinkVk = SocialLinkVk,
                    MapX = MapX,
                    MapY = MapY
                };
                try
                {
                    aboutPage.Utd = DateTime.ParseExact(DateString, "dd.MM.yyyy HH:mm", CultureInfo.InvariantCulture);
                }
                catch(FormatException e)
                {
                    Debug.WriteLine(e.Message);
                }
                return aboutPage;
            });
        }
    }
}
