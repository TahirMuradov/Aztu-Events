using Aztu_Events.Business.Abstarct;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace WebUI.Areas.Dashboard.Controllers
{
    [Area(nameof(Dashboard))]
    public class AlertController : Controller
    {
        private readonly IAlertService _alertService;
        private readonly IHttpContextAccessor _contextAccessor;

        public AlertController(IAlertService alertService, IHttpContextAccessor contextAccessor)
        {
            _alertService = alertService;
            _contextAccessor = contextAccessor;
        }
        [HttpDelete]
        public IActionResult DeleteAlert(string alertId)
        {
            if (string.IsNullOrWhiteSpace(alertId)) return BadRequest();
            var currentUserId= _contextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var result = _alertService.DeleteAlert(alertId: alertId, currentUserId);
            return result.IsSuccess? Ok() : BadRequest(result.Message);
        }
        [HttpDelete]
        public IActionResult DeleteAllAlert()
        {
            var currentUserId = _contextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var result = _alertService.DeleteAllAlert(currentUserId);
            return result.IsSuccess ? Ok() : BadRequest(result.Message);
        }
    }
}
