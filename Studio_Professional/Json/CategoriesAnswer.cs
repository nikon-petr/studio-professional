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
        public int Count { get; set; }

        [DataMember(Name = "1")]
        public Category category1;
        [DataMember(Name = "2")]
        public Category category2;
        [DataMember(Name = "3")]
        public Category category3;
        [DataMember(Name = "4")]
        public Category category4;
        [DataMember(Name = "5")]
        public Category category5;
        [DataMember(Name = "6")]
        public Category category6;
        [DataMember(Name = "7")]
        public Category category7;
        [DataMember(Name = "8")]
        public Category category8;
        [DataMember(Name = "9")]
        public Category category9;
        [DataMember(Name = "10")]
        public Category category10;

        private List<Category> categories;

        public List<Category> Categories
        {
            get
            {
                if (categories == null)
                {
                    categories = new List<Category>(10);
                    if (category1 != null) categories.Add(category1);
                    if (category2 != null) categories.Add(category2);
                    if (category3 != null) categories.Add(category3);
                    if (category4 != null) categories.Add(category4);
                    if (category5 != null) categories.Add(category5);
                    if (category6 != null) categories.Add(category6);
                    if (category7 != null) categories.Add(category7);
                    if (category8 != null) categories.Add(category8);
                    if (category9 != null) categories.Add(category9);
                    if (category10 != null) categories.Add(category10);
                }
                return categories;
            }
        }
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
