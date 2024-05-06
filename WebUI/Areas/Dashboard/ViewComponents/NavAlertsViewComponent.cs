using Aztu_Events.Business.Abstarct;
using Aztu_Events.Entities.EnumClass;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace WebUI.Areas.Dashboard.ViewComponents
{
    public class NavAlertsViewComponent:ViewComponent
    {
        private readonly IConfransService _confransService;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IUserService _userService;
        private readonly IRoleService _roleService;
        public NavAlertsViewComponent(IConfransService confransService, IHttpContextAccessor contextAccessor, IUserService userService, IRoleService roleService)
        {
            _confransService = confransService;
            _contextAccessor = contextAccessor;
            _userService = userService;
            _roleService = roleService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var currentCulture = Thread.CurrentThread.CurrentCulture.Name;
            var currentUserId = _contextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var user=await _userService.GetUserAsync(LangCode:currentCulture,UserId:currentUserId);
            var conferance = _confransService.GetAllConferanceForUser(UserId: currentUserId, LangCode: currentCulture).Data.Where(x => x.Status != ConferanceStatus.Gözlənilir).ToList();
           
           
            return View("NavAlerts",conferance);
        }
    }
}
