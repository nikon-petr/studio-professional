using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Studio_Professional.Models
{
    /// <summary>
    /// Моель данных пользователя
    /// </summary>
    public class User : INotifyPropertyChanged
    {
        public string Name { get; set; }

        public string Number { get; set; }

        public DateTime LastLogin { get; set; }

        private string discount;

        public string Discount
        {
            get
            {
                return discount;
            }
            set
            {
                discount = value;
                OnPropertyChanged("Discount");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

    }
}
