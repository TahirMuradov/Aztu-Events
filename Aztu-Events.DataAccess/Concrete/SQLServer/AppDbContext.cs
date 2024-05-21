using Aztu_Events.Entities.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Asn1.Esf;
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
        public DbSet<Auditorium> Audutoriums { get; set; }
        public DbSet<Time> Times { get; set; }
        public DbSet<ConfranceLaunguage>ConfranceLaunguages { get; set; }
        public DbSet<SpecialGuest> SpecialGuests { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<CategoryLaunguage> CategoryLaunguages { get; set; }
        public DbSet<UserConfrance> UserConfrances { get; set; }
        public DbSet<Comment> Comments { get; set; }
        //public DbSet<Alert> Alerts { get; set; }
        //public DbSet<AlertLaunguage> AlertLaunguages { get; set; }



        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<User>().ToTable("Users");
            builder.Entity<IdentityRole>().ToTable("Roles");
            builder.Entity<Confrans>()
                .HasOne(x => x.Time)
                .WithOne(x => x.Confrans)
                .HasForeignKey<Confrans>(x => x.TimeId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.Entity<Confrans>()
                .HasOne(x=>x.Category)
                .WithMany(x => x.Confrans)
                .HasForeignKey(x => x.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.Entity<Comment>()
                .HasOne(x=>x.User)
                .WithMany(x=>x.Comments)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.Entity<UserConfrance>()
                .HasOne(x=>x.User)
                .WithMany(x=>x.userConfrances)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Restrict);

    
        }
    }
}
