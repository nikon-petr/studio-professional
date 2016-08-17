using System.Collections.Generic;
using System.Runtime.Serialization;

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
        [DataMember(Name = "11")]
        public Category category11;
        [DataMember(Name = "12")]
        public Category category12;
        [DataMember(Name = "13")]
        public Category category13;
        [DataMember(Name = "14")]
        public Category category14;
        [DataMember(Name = "15")]
        public Category category15;
        [DataMember(Name = "16")]
        public Category category16;
        [DataMember(Name = "17")]
        public Category category17;
        [DataMember(Name = "18")]
        public Category category18;
        [DataMember(Name = "19")]
        public Category category19;
        [DataMember(Name = "20")]
        public Category category20;
        [DataMember(Name = "21")]
        public Category category21;
        [DataMember(Name = "22")]
        public Category category22;
        [DataMember(Name = "23")]
        public Category category23;
        [DataMember(Name = "24")]
        public Category category24;
        [DataMember(Name = "25")]
        public Category category25;
        [DataMember(Name = "26")]
        public Category category26;
        [DataMember(Name = "27")]
        public Category category27;
        [DataMember(Name = "28")]
        public Category category28;
        [DataMember(Name = "29")]
        public Category category29;
        [DataMember(Name = "30")]
        public Category category30;

        private List<Category> categories;

        public List<Category> Categories
        {
            get
            {
                if (categories == null)
                {
                    categories = new List<Category>(30);
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
                    if (category11 != null) categories.Add(category11);
                    if (category12 != null) categories.Add(category12);
                    if (category13 != null) categories.Add(category13);
                    if (category14 != null) categories.Add(category14);
                    if (category15 != null) categories.Add(category15);
                    if (category16 != null) categories.Add(category16);
                    if (category17 != null) categories.Add(category17);
                    if (category18 != null) categories.Add(category18);
                    if (category19 != null) categories.Add(category19);
                    if (category20 != null) categories.Add(category20);
                    if (category21 != null) categories.Add(category21);
                    if (category22 != null) categories.Add(category22);
                    if (category23 != null) categories.Add(category23);
                    if (category24 != null) categories.Add(category24);
                    if (category25 != null) categories.Add(category25);
                    if (category26 != null) categories.Add(category26);
                    if (category27 != null) categories.Add(category27);
                    if (category28 != null) categories.Add(category28);
                    if (category29 != null) categories.Add(category29);
                    if (category30 != null) categories.Add(category30);
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
