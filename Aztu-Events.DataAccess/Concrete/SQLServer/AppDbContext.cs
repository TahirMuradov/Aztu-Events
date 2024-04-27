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
            optionsBuilder.UseSqlServer("Data Source=localhost\\SQLEXPRESS;Initial Catalog=Aztu_EventsDb;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
        }


        public DbSet<Confrans> Confrans { get; set; }
        public DbSet<Auditorium> Audutoria { get; set; }
        public DbSet<Time> Times { get; set; }
        public DbSet<ConfranceLaunguage>ConfranceLaunguages { get; set; }
        public DbSet<AuditorimTime> AudutorimTimes { get; set; }



        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<User>().ToTable("Users");
            builder.Entity<IdentityRole>().ToTable("Roles");
    
        }
    }
}
