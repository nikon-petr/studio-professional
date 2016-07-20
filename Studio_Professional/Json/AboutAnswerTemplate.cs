using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Studio_Professional.Json
{
    [DataContract]
    public class AboutAnswerTemplate : AnswerTemplate
    {
        [DataMember]
        public byte[] Image1 { get; }
        [DataMember]
        public byte[] Image2 { get; }
        [DataMember]
        public byte[] Image3 { get; }
        [DataMember]
        public byte[] Image4 { get; }
        [DataMember]
        public string TextHeader { get; }
        [DataMember]
        public string TextContent { get; }
        [DataMember]
        public string YouTubeId1 { get; }
        [DataMember]
        public byte[] Image5 { get; }
        [DataMember]
        public byte[] Image6 { get; }
        [DataMember]
        public string YouTubeId2 { get; }
        [DataMember]
        public string AdressHeader { get; }
        [DataMember]
        public string AdressText { get; }
        [DataMember]
        public string ContactHeader { get; }
        [DataMember]
        public string ContactText { get; }
        [DataMember]
        public string PhoneHeader { get; }
        [DataMember]
        public string PhoneText { get; }
        [DataMember]
        public string SocialLinkVk { get; }
        [DataMember]
        public string SocialLinkTw { get; }
        [DataMember]
        public string SocialLinkInst { get; }
        [DataMember]
        public string SocialLinkFb { get; }
        [DataMember]
        public double MapX { get; }
        [DataMember]
        public double MapY { get; }
        [DataMember]
        public DateTime Utd { get; }
    }
}
