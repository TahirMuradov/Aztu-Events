using Aztu_Events.Business.Abstarct;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace WebUI.Areas.Dashboard.ViewComponents
{
    public class UserInformationViewComponent:ViewComponent
    {
        private readonly IUserService _userService;
        private readonly IHttpContextAccessor _contextAccessor;
        public UserInformationViewComponent(IUserService userService, IHttpContextAccessor contextAccessor)
        {
            _userService = userService;
            _contextAccessor = contextAccessor;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var currentCulture = Thread.CurrentThread.CurrentCulture.Name;
            var currentUserId = _contextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var user = await _userService.GetUserAsync(LangCode: currentCulture, UserId: currentUserId);
            return View("UserInformation",user.Data);
        }
    }
}
