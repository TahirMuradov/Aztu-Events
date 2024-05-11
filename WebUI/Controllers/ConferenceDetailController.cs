using Aztu_Events.Business.Abstarct;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers
{
    public class ConferenceDetailController : Controller
    {
        private readonly IConfransService _confransService;
public ConferenceDetailController(IConfransService confransService)
        {
            _confransService = confransService;
        }

        public async Task<IActionResult> Index(string id)
        {
            if (string.IsNullOrEmpty(id)) return Redirect("/conferences");
            var currentCulture=Thread.CurrentThread.CurrentCulture.Name;
            var data = await _confransService.ConferenceGetDetailForAdminAsync(Guid.Parse(id), currentCulture);
            if (!data.IsSuccess || data.Data is null) return Redirect("/conferences");
            return View(data.Data);
        }
    }
}
