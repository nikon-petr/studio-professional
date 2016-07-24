using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Studio_Professional.Json
{
    [DataContract]
    public class CategoriesAnswer : SimpleAnswer
    {
        [DataMember(Name = "count")]
        int Count { get; set; }

        [DataMember]
        List<Category> categories { get; set; }
    }

    [DataContract]
    public class Category
    {
        [DataMember(Name = "id")]
        public int Id { get; set; }

        [DataMember(Name = "catName")]
        public string Name { get; set; }
    }
}
