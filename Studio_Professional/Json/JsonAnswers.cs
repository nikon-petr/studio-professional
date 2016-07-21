using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Studio_Professional.Json
{
    /// <summary>
    /// Возможные стандартные ответы в json, возвращаемые Api
    /// </summary>
    public sealed class JsonAnswers
    {
        public static string OK { get; } = "OK";
        public static string WRONGNUMBER { get; } = "WRONGNUMBER";
        public static string ALREADYHAVE { get; } = "ALREADYHAVE";
        public static string NODATA { get; } = "NODATA";
        public static string INCORRECTCODE { get; } = "INCORRECTCODE";
        public static string WRONGCODE { get; } = "WRONGCODE";
        public static string MAXSALE { get; } = "MAXSALE";
        public static string NaN { get; } = "NaN";
        public static string NOTFOUND { get; } = "NOTFOUND";

        private JsonAnswers() { }
    }
}
