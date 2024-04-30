using Aztu_Events.Business.Abstarct;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace WebUI.Areas.Dashboard.Controllers
{
    [Area(nameof(Dashboard))]
    public class ConferanceForUserController : Controller
    {
        private readonly IConfransService _confransService;
        private readonly IHttpContextAccessor _contextAccessor;
        public ConferanceForUserController(IConfransService confransService, IHttpContextAccessor contextAccessor)
        {
            _confransService = confransService;
            _contextAccessor = contextAccessor;
        }

        public IActionResult Index()
        {
            string currentCultur = Thread.CurrentThread.CurrentCulture.Name;
          var currentUserId = _contextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var result = _confransService.GetAllConferanceForUser(currentUserId, currentCultur);
            return View(result.Data);
        }
    }
}
