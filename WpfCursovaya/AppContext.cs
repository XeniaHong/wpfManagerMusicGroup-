using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace WpfCursovaya
{
    class AppContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Manager> Managers { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<Shedule> Shedules { get; set; }
        public DbSet<Tour> Tours { get; set; }

        public AppContext() : base("DefaultConnection") { }
    }
}
