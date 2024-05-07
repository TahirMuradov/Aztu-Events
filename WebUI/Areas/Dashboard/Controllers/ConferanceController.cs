using Aztu_Events.Business.Abstarct;
using Aztu_Events.Entities.DTOs.Conferences;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Areas.Dashboard.Controllers
{
    [Area(nameof(Dashboard))]
    [Authorize(Roles = "Admin")]
    public class ConferanceController : Controller
    {
        private readonly IConfransService _confransService;

        public ConferanceController(IConfransService confransService)
        {
            _confransService = confransService;
        }

        public IActionResult Index()
        {
            var currenCulture=Thread.CurrentThread.CurrentCulture.Name;
            var result = _confransService.GetAllConferanceForAdmin(currenCulture);
            return View(result.Data);
        }
        [HttpGet]
        public async Task<IActionResult> ChangeStatus(string id)
        {
            
            if(string.IsNullOrEmpty(id)) return Redirect("/dashboard/Conferance/index");
            var currenCulture = Thread.CurrentThread.CurrentCulture.Name;
            var result = await _confransService.ConferenceGetDetailForAdminAsync(Guid.Parse(id) ,currenCulture);
            return View(result.Data);
        }
        [HttpPost]
        public async Task< IActionResult> ChangeStatus(ConferenceGetAdminDTO conferenceGetAdminDTO,string responseMessage)
        {
            if (string.IsNullOrEmpty(conferenceGetAdminDTO.Id.ToString())) return Redirect("/dashboard/Conferance/index");
            var result = await _confransService.ApproveConfransAsync(conferenceGetAdminDTO.Id, conferenceGetAdminDTO.Status, responseMessage);
            return Redirect("/dashboard/Conferance/index");
        }
        [HttpDelete]
        public IActionResult Delete(string id)
        {
            if (string.IsNullOrEmpty(id))return BadRequest();
            var result = _confransService.ConfrenceRemove(id);

            return result.IsSuccess ?Ok():BadRequest();
        }
        [HttpGet]
        public async Task< IActionResult> Detail(string id) {
            var currenCulture = Thread.CurrentThread.CurrentCulture.Name;
            var result = await _confransService.ConferenceGetDetailForAdminAsync(id: Guid.Parse(id), lang: currenCulture);
            return View(result.Data);
        }
    
    
    }
}
