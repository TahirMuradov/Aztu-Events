using Aztu_Events.Business.Abstarct;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Areas.Dashboard.Controllers
{
    [Area(nameof(Dashboard))]
    [Authorize(Roles = "Admin,SuperAdmin")]
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
    
        [HttpPut]
        public IActionResult ApporiveComment(string Id)
        {
            if (string.IsNullOrEmpty(Id)) return BadRequest();
            var data = _commentService.ApporiveComment(Id);
            
            return data.IsSuccess?Ok():BadRequest();
        }
        [HttpDelete]
        public IActionResult DeleteComment(string Id)
        {
            if (string.IsNullOrEmpty(Id)) return BadRequest();
            var resul = _commentService.DeleteComment(Id);
            return resul.IsSuccess?Ok():BadRequest();
        }
    }
}
