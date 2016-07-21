using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Studio_Professional.Json
{
    /// <summary>
    /// Простой ответ Api содержащий только одну переменную: answer
    /// </summary>
    [DataContract]
    public class AnswerTemplate
    {
        [DataMember(Name = "answer")]
        public string Answer;
    }
}
