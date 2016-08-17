using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Studio_Professional.Json
{
    [DataContract]
    public class MastersAnswer : SimpleAnswer
    {
        [DataMember(Name = "count")]
        public int Count { get; set; }

        [DataMember(Name = "1")]
        public Master master1;
        [DataMember(Name = "2")]
        public Master master2;
        [DataMember(Name = "3")]
        public Master master3;
        [DataMember(Name = "4")]
        public Master master4;
        [DataMember(Name = "5")]
        public Master master5;
        [DataMember(Name = "6")]
        public Master master6;
        [DataMember(Name = "7")]
        public Master master7;
        [DataMember(Name = "8")]
        public Master master8;
        [DataMember(Name = "9")]
        public Master master9;
        [DataMember(Name = "10")]
        public Master master10;

        private List<Master> masters;

        public List<Master> Masters
        {
            get
            {
                if(masters == null)
                {
                    masters = new List<Master>(10);
                    if (master1 != null) masters.Add(master1);
                    if (master2 != null) masters.Add(master2);
                    if (master3 != null) masters.Add(master3);
                    if (master4 != null) masters.Add(master4);
                    if (master5 != null) masters.Add(master5);
                    if (master6 != null) masters.Add(master6);
                    if (master7 != null) masters.Add(master7);
                    if (master8 != null) masters.Add(master8);
                    if (master9 != null) masters.Add(master9);
                    if (master10 != null) masters.Add(master10);
                }
                return masters;
            }
        }
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
