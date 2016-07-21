using System.Runtime.Serialization;

namespace Studio_Professional.Json
{
    /// <summary>
    /// Ответ Api содержащий инфорацию для страницы "Акции"
    /// </summary>
    [DataContract]
    public class SpecialOffersAnswer : SimpleAnswer
    {
        [DataMember(Name = "id")]
        public string Id { get; set; }

        [DataMember(Name = "img")]
        public string Image { get; set; }

        [DataMember(Name = "header")]
        public string Header { get; set; }
    }
}
