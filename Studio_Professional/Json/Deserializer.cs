using System.IO;
using System.Runtime.Serialization.Json;
using System.Threading.Tasks;

namespace Studio_Professional.Json
{
    /// <summary>
    /// Десериализатор json формата
    /// </summary>
    public class Deserializer
    {
        /// <summary>
        /// Асинхронно выполняет десериализацию json в обект
        /// </summary>
        /// <typeparam name="T">Класс объекта в который будет записан json</typeparam>
        /// <param name="jsonStream">Поток содержащий json</param>
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
