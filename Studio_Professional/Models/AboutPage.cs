using Studio_Professional.Popups;
using System;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Media.Imaging;

namespace Studio_Professional.Models
{
    /// <summary>
    /// Модель данных страницы "О нас"
    /// </summary>
    public class AboutPage
    {
        public long Id { get; set; }
        
        public byte[] Image1 { get; set; }

        public byte[] Image2 { get; set; }

        public byte[] Image3 { get; set; }

        public byte[] Image4 { get; set; }

        public string TextHeader { get; set; }

        public string TextContent { get; set; }

        public string YouTubeId1 { get; set; }

        public string TextContent2 { get; set; }

        public byte[] Image5 { get; set; }

        public byte[] Image6 { get; set; }

        public string YouTubeId2 { get; set; }

        public string AdressHeader { get; set; }

        public string AdressText { get; set; }

        public string ContactHeader { get; set; }

        public string ContactText { get; set; }

        public string PhoneHeader { get; set; }

        public string PhoneText { get; set; }

        public string SocialLinkVk { get; set; }

        public string SocialLinkTw { get; set; }

        public string SocialLinkInst { get; set; }

        public string SocialLinkFb { get; set; }

        public string SocialLinkYb { get; set; }

        public double MapX { get; set; }

        public double MapY { get; set; }

        public DateTime Utd { get; set; }

        public static async Task<BitmapImage> ConvertBytesToBitmapImage(byte[] bytes)
        {
            try
            {
                var bitmapImage = new BitmapImage();
                var stream = new InMemoryRandomAccessStream();
                await stream.WriteAsync(bytes.AsBuffer());
                stream.Seek(0);

                bitmapImage.SetSource(stream);
                return bitmapImage;
            }
            catch(Exception e)
            {
                Messages.ShowErrorMessage(e.Message);
            }
            return null;
        }
    }
}
