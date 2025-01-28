using Aztu_Events.Business.Abstarct;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebUI.Models;

namespace WebUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfransService _confransService;
        public HomeController(ILogger<HomeController> logger, IConfransService confransService)
        {
            _logger = logger;
            _confransService = confransService;
        }

        public IActionResult Index()
        {
            if (Request.Cookies[CookieRequestCultureProvider.DefaultCookieName] == null)
            {
                Response.Cookies.Append(CookieRequestCultureProvider.DefaultCookieName, CookieRequestCultureProvider.MakeCookieValue(new RequestCulture("az")),
                              new CookieOptions
                              {
                                  Expires = DateTimeOffset.UtcNow.AddYears(1)
                              }
                              );
                return RedirectToAction("Index");
            }
            var currentCulture=Thread.CurrentThread.CurrentCulture.Name;
            var currentDate = DateOnly.FromDateTime(DateTime.UtcNow.ToLocalTime());
            var currentTime = TimeOnly.FromDateTime(DateTime.UtcNow.ToLocalTime());
            var data = _confransService.GetAllConferanceForAdmin(currentCulture).Data?.Where(x=>x.IsFeatured&& (x.Day > currentDate || (x.Day == currentDate && x.StartedDate > currentTime))).ToList();
            return View(data);
        }
        public IActionResult ChangeLanguage(string culture)
        {
            Response.Cookies.Append(CookieRequestCultureProvider.DefaultCookieName, CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions
                {
                    Expires = DateTimeOffset.UtcNow.AddYears(1)
                }
                );
            return Redirect(Request.Headers["Referer"].ToString());
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
