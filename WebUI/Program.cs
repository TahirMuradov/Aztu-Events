using Aztu_Events.DataAccess.Concrete.SQLServer;
using Aztu_Events.Entities.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.Options;
using System.Globalization;
using System.Reflection;
using WebUI.Services;
using Aztu_Events.Business.DependencyResolver;
using Aztu_Events.Business.Filters;
using FluentValidation.AspNetCore;
using Aztu_Events.Business.FluentValidation.AuthDTOValidator;
using FluentValidation;
using Aztu_Events.Business.CustomLanguageManager;


var builder = WebApplication.CreateBuilder(args);

#region Localizer
builder.Services.AddSingleton<LanguageService>();
builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");
builder.Services.AddMvc().AddMvcLocalization().AddDataAnnotationsLocalization(options => options.DataAnnotationLocalizerProvider = (type, factory) =>
{
    var assemblyName = new AssemblyName(typeof(SharedResource).GetTypeInfo().Assembly.FullName);
    return factory.Create(nameof(SharedResource), assemblyName.Name);
});
builder.Services.AddControllersWithViews().AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix);
builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    var supportCulture = new List<CultureInfo>
    {
        new CultureInfo("az"),
        new CultureInfo("en"),
        new CultureInfo("ru"),
    };
    options.DefaultRequestCulture = new RequestCulture(supportCulture[0], supportCulture[0]);
    options.SupportedCultures = supportCulture;
    options.SupportedUICultures = supportCulture;
    options.RequestCultureProviders.Insert(0, new QueryStringRequestCultureProvider());
});
#endregion

// Add services to the container.
builder.Services.AddControllersWithViews(options => options.ModelValidatorProviders.Clear());
#region Fluent Validation Registration add services to the container.
builder.Services/*.AddControllers(options => options.Filters.Add<ValidationFilters>())*/
    .AddFluentValidation(configuration =>
    {
        //configuration.RegisterValidatorsFromAssemblyContaining<LoginDTOValidation>();
        configuration.DisableDataAnnotationsValidation = true;
        configuration.LocalizationEnabled = true;
        configuration.DisableDataAnnotationsValidation = true;
        configuration.ValidatorOptions.LanguageManager = new CustomLanguageManager();
        configuration.ValidatorOptions.LanguageManager.Culture = new CultureInfo( Thread.CurrentThread.CurrentCulture.Name);
    })
 ;
#endregion
builder.Services.AddIdentity<User, IdentityRole>()
             .AddEntityFrameworkStores<AppDbContext>()
             .AddDefaultTokenProviders();
builder.Services.Run();

var app = builder.Build();
app.UseRequestLocalization(app.Services.GetRequiredService<IOptions<RequestLocalizationOptions>>().Value);
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
      name: "areas",
      pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
    );
});
app.Run();
