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
        [Authorize(Roles = "Admin,User,SuperAdmin")]
        public IActionResult Index()
        {
            List<string> photos = _confransService.GetAllConferanceForAdmin("az").Data.Select(x => x.ImgUrl).ToList();
            FileHelper.AutoRemove(photos);
            return View();
        }
        [Authorize(Roles = "Admin,SuperAdmin")]
        public IActionResult AlertSeenChangeForAdmin(string CurrentUserId)
        {

           
            var result1 = _commentService.AlertSeen();
            if (!result1.IsSuccess)
                return BadRequest();

            
            var result2 = _confransService.AlertSeen(CurrentUserId);
            if (!result2.IsSuccess) return BadRequest();
            return Ok();
        }
        [Authorize(Roles = "Admin,User,SuperAdmin")]
        public IActionResult AlertSeenChangeForUser(string CurrentUserId)
        {

      


            var result2 = _confransService.AlertSeen(CurrentUserId);
            if (!result2.IsSuccess) return BadRequest();
            return Ok();
        }
    }
}
