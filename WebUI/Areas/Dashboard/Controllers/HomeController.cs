using Aztu_Events.Business.Abstarct;
using Aztu_Events.Core.Helper.FileHelper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Areas.Dashboard.Controllers
{
    [Area(nameof(Dashboard))]
    [Authorize(Roles = "Admin,User,SuperAdmin")]
    public class HomeController : Controller
    {
        private readonly IConfransService _confransService;
        private readonly ICommentService _commentService;

        public HomeController(IConfransService confransService, ICommentService commentService)
        {
            _confransService = confransService;
            _commentService = commentService;
        }

        public IActionResult Index()
        {
            List<string> photos = _confransService.GetAllConferanceForAdmin("az").Data.Select(x => x.ImgUrl).ToList();
            FileHelper.AutoRemove(photos);
            return View();
        }
        public IActionResult AlertSeenChangeAll()
        {
            
            var result1 = _commentService.AlertSeen();
            if (!result1.IsSuccess)
                return BadRequest();

            
            var result2 = _confransService.AlertSeen();
            if (!result2.IsSuccess) return BadRequest();
            return Ok();
        }
    }
}
