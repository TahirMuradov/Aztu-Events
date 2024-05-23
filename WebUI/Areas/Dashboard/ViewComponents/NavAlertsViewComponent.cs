using Aztu_Events.Business.Abstarct;
using Aztu_Events.Entities.DTOs.AlertDTOs;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace WebUI.Areas.Dashboard.ViewComponents
{
    public class NavAlertsViewComponent : ViewComponent
    {
        private readonly ICommentService _commentService;
        private readonly IConfransService _confransService;
        private readonly IHttpContextAccessor _contextAccessor;


        public NavAlertsViewComponent(IConfransService confransService, IHttpContextAccessor contextAccessor, ICommentService commentService)
        {
            _confransService = confransService;
            _contextAccessor = contextAccessor;
            _commentService = commentService;

        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var currentCulture = Thread.CurrentThread.CurrentCulture.Name;
            var currentUserId = _contextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;



            IQueryable<GetAlertDTO> alertsConference = null;
            if (User.IsInRole("SuperAdmin") || User.IsInRole("Admin")) 
            {
            ViewBag.ConferenceAdmin = _confransService.GetAlertsForConference(CurrentUserId: null, langCode: currentCulture).Data;

            }
            alertsConference = _confransService.GetAlertsForConference(CurrentUserId: currentUserId, langCode: currentCulture).Data;

            if (User.IsInRole("SuperAdmin") || User.IsInRole("Admin"))
            {
                IQueryable<GetAlertDTO> AlertsComment = _commentService.GetAlertsForComment(currentCulture).Data;
              
                ViewBag.CommentAlert = AlertsComment;
            }
     


            return View("NavAlerts", alertsConference);
        }
    }
}
