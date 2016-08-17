using System.Runtime.Serialization;

namespace Studio_Professional.Json
{
    [DataContract]
    public class SaleAnswer : SimpleAnswer
    {
        [DataMember(Name = "content")]
        public string Content { get; set; }
    }
}
