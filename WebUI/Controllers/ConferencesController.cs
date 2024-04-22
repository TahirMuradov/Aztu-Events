using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers
{
    public class ConferencesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
