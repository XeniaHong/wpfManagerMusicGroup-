using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfCursovaya
{
    class Group
    {
        public int groupId { get; set; }

        private string nameGroup, fandomName;
        private int countMember;
        private int year;

        public string NameGroup
        {
            get { return nameGroup; }
            set { nameGroup = value; }
        }
        public string FandomName
        {
            get { return fandomName; }
            set { fandomName = value; }
        }
        public int CountMember
        {
            get { return countMember; }
            set { countMember = value; }
        }

        
        public int Year
        {
            get { return year; }
            set { year = value; }
        }

        public Group()
        {

        }

        public Group(string name, int years, string fandom, int count)
        {
            this.nameGroup = name;
            this.year = years;
            this.fandomName = fandom;
            this.countMember = count;
        }   
    }
}
