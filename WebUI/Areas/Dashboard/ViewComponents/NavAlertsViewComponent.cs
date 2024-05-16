using Aztu_Events.Business.Abstarct;
using Aztu_Events.Entities.DTOs.Conferences;
using Aztu_Events.Entities.EnumClass;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace WebUI.Areas.Dashboard.ViewComponents
{
    public class NavAlertsViewComponent:ViewComponent
    {
        private readonly ICommentService _commentService;
        private readonly IConfransService _confransService;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IUserService _userService;
        private readonly IRoleService _roleService;
        public NavAlertsViewComponent(IConfransService confransService, IHttpContextAccessor contextAccessor, IUserService userService, IRoleService roleService, ICommentService commentService)
        {
            _confransService = confransService;
            _contextAccessor = contextAccessor;
            _userService = userService;
            _roleService = roleService;
            _commentService = commentService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var currentCulture = Thread.CurrentThread.CurrentCulture.Name;
            var currentUserId = _contextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var user=await _userService.GetUserAsync(LangCode:currentCulture,UserId:currentUserId);
            List<GetALLConferenceUserDTO> conferance = null;

            if (User.IsInRole("Admin")||User.IsInRole("SuperAdmin"))
            {
                conferance = _confransService.GetAllConferanceForUser(UserId: currentUserId, LangCode: currentCulture).Data.Where(x => x.Status == ConferanceStatus.Gözlənilir && !x.AlertSeen).ToList();
                ViewBag.CommentAlert = _commentService.GetAllCommentsForAmin(currentCulture).Data.Where(x =>!x.AlertSeen);
            }
            else
            {

            conferance = _confransService.GetAllConferanceForUser(UserId: currentUserId, LangCode: currentCulture).Data.Where(x => x.Status != ConferanceStatus.Gözlənilir && !x.AlertSeen).ToList();
            }
           
           
            return View("NavAlerts",conferance);
        }
    }
}
