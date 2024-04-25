using Aztu_Events.Core.Helper.EmailHelper;
using Aztu_Events.Core.Helper.EmailHelper.Concrete;
using Aztu_Events.DataAccess.Concrete.SQLServer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Aztu_Events.Entities.Concrete;
namespace Aztu_Events.Business.DependencyResolver
{
    public static class ServiceRegistration
    {
        public static void Run(this IServiceCollection services)
        {
            services.AddScoped<AppDbContext>();
            services.AddScoped<IEmailHelper, EmailHelper>();
    
            services.Configure<IdentityOptions>(options =>
            {
                options.SignIn.RequireConfirmedEmail = false;

                // Default Password settings.
                options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+/ ";
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 3;
                options.Password.RequiredUniqueChars = 1;
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);

            });
        }
    }
}
