using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfCursovaya
{
    class Shedule
    {
        public int sheduleId { get; set; }

        private string shedule, placeShedule;
        private string data;

        public string SheduleName
        {
            get { return shedule; }
            set { shedule = value; }
        }

        public string PlaceShedule
        {
            get { return placeShedule; }
            set { placeShedule = value; }
        }
        public string Data
        {
            get { return data; }
            set { data = value; }
        }
        

        public Shedule()
        {

        }
        public Shedule(string shedule, string date, string placeshedule)
        {
            this.shedule = shedule;
            this.placeShedule = placeshedule;
            this.data = date;
        }
    }
}
