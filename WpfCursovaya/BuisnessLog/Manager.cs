using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfCursovaya
{
    class Manager
    {
        public int managerId { get; set; }

        private string  name, lastName;
        private int   userLogin;
        private int birth;

        
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
   
        public int UserLogin
        {
            get { return userLogin; }
            set { userLogin = value; }
        }

        public int Birth
        {
            get { return birth; }
            set { birth = value; }
        }

        public Manager()
        {

        }

        public Manager( string name, string lastName, int userLogin, int birth)
        {
            this.name = name;
            this.lastName = lastName;
            this.birth = birth;
            this.userLogin = userLogin;
        }
    }
}
