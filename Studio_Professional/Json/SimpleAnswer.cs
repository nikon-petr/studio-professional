using System.Runtime.Serialization;

namespace Studio_Professional.Json
{
    /// <summary>
    /// Простой ответ Api содержащий только одну переменную: answer
    /// </summary>
    [DataContract]
    public class SimpleAnswer
    {
        [DataMember(Name = "answer", IsRequired = false)]
        public string Answer;
    }
}
