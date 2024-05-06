using Aztu_Events.Business.Abstarct;
using Aztu_Events.Core.Helper.FileHelper;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Areas.Dashboard.Controllers
{
    [Area(nameof(Dashboard))]
    public class HomeController : Controller
    {
        private readonly IConfransService _confransService;

        public HomeController(IConfransService confransService)
        {
            _confransService = confransService;
        }

        public IActionResult Index()
        {
            List<string> photos = _confransService.GetAllConferanceForAdmin("az").Data.Select(x=>x.ImgUrl).ToList();
            FileHelper.AutoRemove(photos);
            return View();
        }
    }
}
