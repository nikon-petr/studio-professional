using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Studio_Professional.Json
{
    [DataContract]
    public class MastersAnswer : SimpleAnswer
    {
        [DataMember(Name = "count")]
        public int Count { get; set; }

        [DataMember]
        public List<Master> masters { get; set; }
    }

    [DataContract]
    public class Master
    {
        [DataMember(Name = "id")]
        public int Id { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }
    }
}
