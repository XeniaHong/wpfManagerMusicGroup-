using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfCursovaya
{
    class Tour
    {
        public int TourId { get; set; }

        private string country, town, placeConcert;
        private int data;
        private int costTicket;

        public string Country
        {
            get { return country; }
            set { country = value; }
        }
        public string Town
        {
            get { return town; }
            set { town = value; }
        }
        public string PlaceConcert
        {
            get { return placeConcert; }
            set { placeConcert = value; }
        }

        public int Data
        {
            get { return data; }
            set { data = value; }
        }
        
        public int CostTicket
        {
            get { return costTicket; }
            set { costTicket = value; }
        }

        public Tour()
        {

        }

        public Tour(string country, string place, string town, int cost, int data)
        {
            this.country = country;
            this.placeConcert = place;
            this.town = town;
            this.data = data;
            this.costTicket = cost;
        }
    }
}
