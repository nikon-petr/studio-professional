using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Studio_Professional.Models
{
    /// <summary>
    /// Иоель данных пользователя
    /// </summary>
    public class User
    {
        public string Name { get; set; }

        public string Number { get; set; }

        public DateTime LastLogin { get; set; }
    }
}
