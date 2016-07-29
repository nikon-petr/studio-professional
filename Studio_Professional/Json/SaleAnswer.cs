using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Studio_Professional.Json
{
    [DataContract]
    public class SaleAnswer : SimpleAnswer
    {
        [DataMember(Name = "content")]
        public string Content { get; set; }
    }
}
