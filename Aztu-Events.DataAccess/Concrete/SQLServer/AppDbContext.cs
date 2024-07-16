using Aztu_Events.Entities.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Aztu_Events.DataAccess.Concrete.SQLServer
{
    public class AppDbContext: IdentityDbContext<User>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }




        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    ///*smarterasp connection string*/
        //    optionsBuilder.UseSqlServer("Data Source=SQL8006.site4now.net;Initial Catalog=db_aaa05b_aztuevents;User Id=db_aaa05b_aztuevents_admin;Password=4575865T@hir");
        //    /* soome.com connection string*/
        //    //optionsBuilder.UseSqlServer("workstation id=Aztu-Events.mssql.somee.com;packet size=4096;user id=TahirMuradov_SQLLogin_1;pwd=spdbt4zrvu;data source=Aztu-Events.mssql.somee.com;persist security info=False;initial catalog=Aztu-Events;TrustServerCertificate=True");
        //    // /*local connection string*/ optionsBuilder.UseSqlServer("Server = localhost; Database = Aztu_EventsDb; Trusted_Connection = True; MultipleActiveResultSets = True; TrustServerCertificate = True;");
        //}


        public DbSet<Confrans> Confrans { get; set; }
        public DbSet<Auditorium> Audutoriums { get; set; }
        public DbSet<Time> Times { get; set; }
        public DbSet<ConfranceLaunguage>ConfranceLaunguages { get; set; }
        public DbSet<SpecialGuest> SpecialGuests { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<CategoryLaunguage> CategoryLaunguages { get; set; }
        public DbSet<UserConfrance> UserConfrances { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<SavePdf> SavePdfs { get; set; }
        public DbSet<Alert> Alerts { get; set; }
        public DbSet<AlertLaunguage> AlertLaunguages { get; set; }
        public DbSet<EventTypeLaunguage> EventTypeLaunguages { get; set; }
        public DbSet<EventType> EventTypes { get; set; }



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
            builder.Entity<SavePdf>()
                .HasOne(x=>x.User)
                .WithMany(x=>x.Pdfs)
                .HasForeignKey(x=>x.UserId)
                .OnDelete(deleteBehavior: DeleteBehavior.Restrict);
            builder.Entity<Confrans>()
                .HasOne(x => x.EventType)
                .WithMany(x => x.Confrans)
                .HasForeignKey(x => x.EventTypeId)
                .OnDelete(DeleteBehavior.Restrict);

    
        }
    }
}
