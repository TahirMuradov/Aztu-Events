using Aztu_Events.Business.Abstarct;
using Aztu_Events.Business.FluentValidation.ConferanceValidator;
using Aztu_Events.Entities.DTOs.Conferences;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace WebUI.Areas.Dashboard.Controllers
{
    [Area(nameof(Dashboard))]
    public class ConferanceForUserController : Controller
    {
        private readonly IConfransService _confransService;
        private readonly IAuditoriumService _auditoriumService;
        private readonly IHttpContextAccessor _contextAccessor;
        public ConferanceForUserController(IConfransService confransService, IHttpContextAccessor contextAccessor, IAuditoriumService auditoriumService)
        {
            _confransService = confransService;
            _contextAccessor = contextAccessor;
            _auditoriumService = auditoriumService;
        }

        public IActionResult Index()
        {
            string currentCultur = Thread.CurrentThread.CurrentCulture.Name;
          var currentUserId = _contextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var result = _confransService.GetAllConferanceForUser(currentUserId, currentCultur);
            return View(result.Data);
        }
        [HttpGet]
        public IActionResult Create()
        {
            var CurrentUserId = _contextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var auditorium=_auditoriumService.GetAllAuditorium();

            return View( new ConferenceAddDTO
            {
               UserId = CurrentUserId,
            });
        }
        [HttpPost]
        public async Task< IActionResult> Create(ConferenceAddDTO conferenceCreateDto, IFormFile Photo)
        {
            var currentCulture=Thread.CurrentThread.CurrentCulture.Name;
            ConferanceAddDTOValidator validationRules=new ConferanceAddDTOValidator(currentCulture);
            var resultValidator=validationRules.Validate(conferenceCreateDto);
            if (!resultValidator.IsValid)
            {
                foreach (var Error in resultValidator.Errors)
                {
                    ModelState.AddModelError("Error", Error.ErrorMessage);
                    return View(conferenceCreateDto);
                }
            }

            var result=await _confransService.ConfrenceAddAsync(conferenceCreateDto);
            if (!result.IsSuccess)
            {
                if (currentCulture=="az")
                {
                    ModelState.AddModelError("Error", "Konfrans Yaradila Bilmədi!Səhifəni yenidən yükləyərək Təkrar Yoxlayın");
                } else if (currentCulture == "en")
                {
                    ModelState.AddModelError("Error", "Failed to create conference! Please reload the page and try again.");
                }
                else
                {
                    ModelState.AddModelError("Error", "Не удалось создать конференцию! Пожалуйста, перезагрузите страницу и попробуйте снова.");
                }

            return View(conferenceCreateDto);
            }
            return RedirectToAction("Index");
        }
    }
}
