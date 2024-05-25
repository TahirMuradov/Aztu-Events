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
        public IActionResult DeleteAlertForAdmin(string alertId)
        {
            if (string.IsNullOrWhiteSpace(alertId)||!(User.IsInRole("SuperAdmin")||User.IsInRole("Admin"))) return BadRequest();
     
            var result = _alertService.DeleteAlert(alertId: alertId,null);
            return result.IsSuccess ? Ok() : BadRequest(result.Message);
        }
        [HttpDelete]
        public IActionResult DeleteAllAlertForAdmin()
        {
            var currentUserId = _contextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            if (User.IsInRole("Admin")||User.IsInRole("SuperAdmin"))
            {
                var result1 = _alertService.DeleteAllAlert(currentUserId);
                if (!result1.IsSuccess) return BadRequest();
            }
            var result = _alertService.DeleteAllAlert(null);
            return result.IsSuccess ? Ok() : BadRequest(result.Message);
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
