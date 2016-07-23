﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;

namespace Studio_Professional.Models
{
    /// <summary>
    /// Модель данных страницы "О нас"
    /// </summary>
    public class AboutPage
    {
        public int Id { get; set; }
        
        public BitmapImage Image1 { get; set; }

        public BitmapImage Image2 { get; set; }

        public BitmapImage Image3 { get; set; }

        public BitmapImage Image4 { get; set; }

        public string TextHeader { get; set; }

        public string TextContent { get; set; }

        public string YouTubeId1 { get; set; }

        public BitmapImage Image5 { get; set; }

        public BitmapImage Image6 { get; set; }

        public string YouTubeId2 { get; set; }

        public string AdressHeader { get; set; }

        public string AdresText { get; set; }

        public string ContactHeader { get; set; }

        public string ContactText { get; set; }

        public string PhoneNumber { get; set; }

        public string PhoneText { get; set; }

        public string SocialLinkVk { get; set; }

        public string SocialLinkTw { get; set; }

        public string SocialLinkInst { get; set; }

        public string SocialLinkFb { get; set; }

        public double MapX { get; set; }

        public double MapY { get; set; }

        public DateTime Utd { get; set; }
    }
}
