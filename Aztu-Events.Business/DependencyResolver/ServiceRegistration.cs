using Aztu_Events.Core.Helper.EmailHelper;
using Aztu_Events.Core.Helper.EmailHelper.Concrete;
using Aztu_Events.DataAccess.Concrete.SQLServer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Aztu_Events.Entities.Concrete;
using Aztu_Events.DataAccess.Abstarct;
using Aztu_Events.DataAccess.Concrete;
using Aztu_Events.Business.Abstarct;
using Aztu_Events.Business.Concrete;
using FluentValidation;
using System;
using Aztu_Events.Entities.DTOs.AuthDTOs;
using Aztu_Events.Business.FluentValidation.AuthDTOValidator;
namespace Aztu_Events.Business.DependencyResolver
{
    public static class ServiceRegistration
    {
        public static void Run(this IServiceCollection services)
        {
            services.AddScoped<AppDbContext>();
            services.AddScoped<IEmailHelper, EmailHelper>();
            services.AddScoped<IUserService, UserManager>();
            services.AddScoped<IRoleService, RoleManager>();
            services.AddScoped<IAudutoriumDAL, EFAuditoriumDAL>();
            services.AddScoped<IAuditoriumService, AuditoriumManager>();
            services.AddScoped<IConfrenceDal, EFConferenceDAL>();
            services.AddScoped<IConfransService, ConfransManager>();
            services.AddScoped<ITimeDAL, EFTimeDAL>();
            services.AddScoped<ITimeService, TimeManager>();
            services.AddScoped<ICategoryDAL, EFCategoryDAL>();
            services.AddScoped<ICategoryService, CategoryManager>();
            services.AddScoped<ICommentDAL, EFCommentDAL>();
            services.AddScoped<ICommentService, CommentManager>();
      
            services.Configure<IdentityOptions>(options =>
            {
                options.SignIn.RequireConfirmedEmail = true;

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
          services.ConfigureApplicationCookie(option =>
            {
                
                option.LoginPath = "/Auth/Login";
            });
        }
    }
}
