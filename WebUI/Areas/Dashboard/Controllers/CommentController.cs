using Aztu_Events.Business.Abstarct;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Areas.Dashboard.Controllers
{
    [Area(nameof(Dashboard))]
    public class CommentController : Controller
    {
        private readonly ICommentService _commentService;

        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        public IActionResult Index()
        {
           var currentCulture=Thread.CurrentThread.CurrentCulture.Name;
            var data = _commentService.GetAllCommentsForAmin(currentCulture);
            return View(data.Data);
        }
    }
}
