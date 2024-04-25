using Aztu_Events.Entities.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Aztu_Events.DataAccess.Concrete.SQLServer
{
    public class AppDbContext: IdentityDbContext<User>
    {

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server = localhost; Database = Aztu_EventsDb; Trusted_Connection = True; MultipleActiveResultSets = True; TrustServerCertificate = True;");
        }


        public DbSet<Confrans> Confrans { get; set; }
        public DbSet<Audutorium> Audutoria { get; set; }
        public DbSet<Time> Times { get; set; }
        public DbSet<AudutorimTime> AudutorimTimes { get; set; }



        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<User>().ToTable("Users");
            builder.Entity<IdentityRole>().ToTable("Roles");
    
        }
    }
}
