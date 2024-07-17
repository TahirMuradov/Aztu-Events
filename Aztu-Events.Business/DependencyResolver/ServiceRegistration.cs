using Aztu_Events.Business.Abstarct;
using Aztu_Events.Business.Concrete;
using Aztu_Events.Core.Helper.EmailHelper;
using Aztu_Events.Core.Helper.EmailHelper.Concrete;
using Aztu_Events.DataAccess.Abstarct;
using Aztu_Events.DataAccess.Concrete;
using Aztu_Events.DataAccess.Concrete.SQLServer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
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
            services.AddScoped<IRegisterConferenceDAL, EFRegisterConferenceDAL>();
            services.AddScoped<IRegisterConferenceService, RegisterConferenceManager>();
            services.AddScoped<IAlertDAL, EFAlertDAL>();
            services.AddScoped<IAlertService, AlertManager>();
            services.AddScoped<IEventTypeDAL, EFEventTypeDAL>();
            services.AddScoped<IEventTypeService, EventTypeManager>();
            services.Configure<IdentityOptions>(options =>
            {
                options.SignIn.RequireConfirmedEmail = true;
                options.User.RequireUniqueEmail = true;
                
                // Default Password settings.
                options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzçşıəöğiABCDEƏFGHIİJKLMNOÖĞŞÇPQRSTUVWXYZ0123456789-._@+/ ";
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
