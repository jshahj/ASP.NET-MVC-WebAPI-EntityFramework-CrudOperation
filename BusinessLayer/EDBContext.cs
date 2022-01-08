using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.UserDB;

namespace BusinessLayer
{
    public class EDBContext : DbContext
    {

        public EDBContext() : base("dbcon")
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }



        public DbSet<UserMaster> UserMasters { get; set; }

    }
}
