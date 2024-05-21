using Aztu_Events.Business.Abstarct;
using Aztu_Events.Entities.Concrete;
using Aztu_Events.Entities.DTOs.CommentDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace WebUI.Controllers
{
    public class ConferenceDetailController : Controller
    {
        private readonly IConfransService _confransService;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly ICommentService _commentService;
        private readonly IRegisterConferenceService _registerConferenceService;
        public ConferenceDetailController(IConfransService confransService, IHttpContextAccessor contextAccessor, ICommentService commentService, IRegisterConferenceService registerConferenceService)
        {
            _confransService = confransService;
            _contextAccessor = contextAccessor;
            _commentService = commentService;
            _registerConferenceService = registerConferenceService;
        }
        [HttpGet]
        public  IActionResult Index(string id)
        {
            if (string.IsNullOrEmpty(id)) return Redirect("/conferences");
            var currentCulture=Thread.CurrentThread.CurrentCulture.Name;
            var data =  _confransService.GetConferenceDetailForUI(id,currentCulture);
            if (User.Identity.IsAuthenticated)
            {

            var currentUserId = _contextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            ViewBag.CurrentUserId = currentUserId;
            }
            if (!data.IsSuccess) return Redirect("/conferences");
            return View(data.Data);
        }
        [HttpPost]
        [Authorize(Roles = "Admin,SuperAdmin,User,User2")]
        public IActionResult AddComment(AddCommentDTO addCommentDTO)
        {
            if (string.IsNullOrEmpty(addCommentDTO.UserId)||string.IsNullOrEmpty(addCommentDTO.ConferenceId)||string.IsNullOrEmpty(addCommentDTO.Content))
       return BadRequest("Istifadeci Id-si ,Konferans Id veya Yorum Mezmunu Bos Ola Bilmez!");
           var result=_commentService.AddComment(addCommentDTO);
            return result.IsSuccess?Ok(true):BadRequest(false);
        }
        [HttpPost]
        [Authorize(Roles = "Admin,SuperAdmin,User,User2")]
        public async Task< IActionResult> RegisterConference(string ConferenceId)
        {
            if (string.IsNullOrEmpty(ConferenceId) )
                return BadRequest("Konferans Id Bos Ola Bilmez!");
            var currentUserId = _contextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var result = await _registerConferenceService.RegisterConferenceAsync(ConferenceId,currentUserId);

            return result.IsSuccess ? Ok() : NotFound();
        }

    }
}
