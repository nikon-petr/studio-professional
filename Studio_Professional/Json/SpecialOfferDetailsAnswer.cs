using System;
using System.Runtime.Serialization;

namespace Studio_Professional.Json
{
    /// <summary>
    /// Ответ Api содержащий инфорацию для страницы отдельной акции
    /// </summary>
    [DataContract]
    class SpecialOfferDetailsAnswer : SimpleAnswer
    {
        [DataMember(Name = "dateOpen")]
        public string DateOpen { get; set; }

        [DataMember(Name = "dateClose")]
        public string DateClose { get; set; }

        [DataMember(Name = "description")]
        public string Description { get; set; }
    }
}
