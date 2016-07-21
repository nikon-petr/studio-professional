using System;
using System.Runtime.Serialization;

namespace Studio_Professional.Json
{
    /// <summary>
    /// Ответ Api содержащий инфорацию для страницы "О нас"
    /// </summary>
    [DataContract]
    public class AboutAnswer : SimpleAnswer
    {
        [DataMember(Name = "Image1-1")]
        public byte[] Image1 { get; set; }

        [DataMember(Name = "Image1-2")]
        public byte[] Image2 { get; set; }

        [DataMember(Name = "Image1-3")]
        public byte[] Image3 { get; set; }

        [DataMember(Name = "Image1-4")]
        public byte[] Image4 { get; set; }

        [DataMember(Name = "textBlock1Header")]
        public string TextHeader { get; set; }

        [DataMember(Name = "textBlock1Content")]
        public string TextContent { get; set; }

        [DataMember(Name = "YoutubeId1")]
        public string YouTubeId1 { get; set; }

        [DataMember(Name = "textBlock2Content")]
        public string TextContent2 { get; set; }

        [DataMember(Name = "Image2-1")]
        public byte[] Image5 { get; set; }

        [DataMember(Name = "Image2-2")]
        public byte[] Image6 { get; set; }

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

        [DataMember(Name = "Map-x")]
        public double MapX { get; set; }

        [DataMember(Name ="Map-y")]
        public double MapY { get; set; }

        [DataMember]
        public DateTime Utd { get; set; }
    }
}
