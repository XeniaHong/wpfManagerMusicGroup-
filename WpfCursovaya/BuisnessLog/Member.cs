using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfCursovaya
{
    class Member
    {
        public int memberId { get; set; }

        private string nickName, name, lastName, position;
        private int weight, height;
        private int birth;

        public string NickName
        {
            get { return nickName; }
            set { nickName = value; }
        }
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public string LastName
        {
            get { return lastName; }
            set { lastName = value; }
        }
        public string Position
        {
            get { return position; }
            set { position = value; }
        }

        public int Weight
        {
            get { return weight; }
            set { weight = value; }
        }
        public int Height
        {
            get { return height; }
            set { height = value; }
        }

        public int Birth
        {
            get { return birth; }
            set { birth = value; }
        }

        public Member()
        {

        }

        public Member(string nick, string name, string lastName, string position, int weight, int height, int birth)
        {
            this.nickName = nick;
            this.name = name;
            this.lastName = lastName;
            this.birth = birth;
            this.weight = weight;
            this.height = height;
            this.position = position;
        }
    }
}
