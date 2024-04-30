using Aztu_Events.Business.Abstarct;
using Aztu_Events.Entities.DTOs.Conferences;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Areas.Dashboard.Controllers
{
    [Area(nameof(Dashboard))]
    public class ConferanceController : Controller
    {
        private readonly IConfransService _confransService;

        public ConferanceController(IConfransService confransService)
        {
            _confransService = confransService;
        }

        public IActionResult Index()
        {
            var currenCulture=Thread.CurrentThread.CurrentCulture.Name;
            var result = _confransService.GetAllConferanceForAdmin(currenCulture);
            return View(result.Data);
        }
    
    }
}
