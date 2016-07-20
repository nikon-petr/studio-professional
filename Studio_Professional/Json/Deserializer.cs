using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace Studio_Professional.Json
{
    public class Deserializer
    {
        public async Task<T> Execute<T>(Stream jsonStream)
        {
            return await Task.Run(() =>
            {
                using (jsonStream)
                {
                    DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(T));
                    return (T)serializer.ReadObject(jsonStream);
                }
            });
        }
    }
}
