using Aztu_Events.Business.Abstarct;
using Aztu_Events.Business.FluentValidation.ConferanceValidator;
using Aztu_Events.Core.Helper;
using Aztu_Events.Core.Helper.FileHelper;
using Aztu_Events.Entities.Concrete;
using Aztu_Events.Entities.DTOs.Conferences;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Security.Claims;

namespace WebUI.Areas.Dashboard.Controllers
{
    [Area(nameof(Dashboard))]
    public class ConferanceForUserController : Controller
    {
        private readonly IConfransService _confransService;
        private readonly IAuditoriumService _auditoriumService;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly ITimeService _timeService;

        public ConferanceForUserController(IConfransService confransService, IHttpContextAccessor contextAccessor, IAuditoriumService auditoriumService, ITimeService timeService)
        {
            _confransService = confransService;
            _contextAccessor = contextAccessor;
            _auditoriumService = auditoriumService;
            _timeService = timeService;
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
            ViewBag.Auditorium= auditorium.Data;
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
                for (int i=0;i<resultValidator.Errors.Count;i++)
                {
                    if (!resultValidator.Errors[i].ErrorMessage.Contains('"'))
                    {

                        ModelState.AddModelError("Error", resultValidator.Errors[i].ErrorMessage);
                    }
                }
                var auditorium = _auditoriumService.GetAllAuditorium();
                ViewBag.Auditorium = auditorium.Data;
                var CurrentUserId = _contextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
                conferenceCreateDto.UserId = CurrentUserId;
                return View(conferenceCreateDto);
            }
            var AllTimes=_timeService.GetAllTime().Data;
            
            if (AllTimes.Any(x=>(x.Date==conferenceCreateDto.Day&&x.AuditoriumId==conferenceCreateDto.AudutoriumId)&&!(x.EndTime <= conferenceCreateDto.StartedDate || x.StartedTime >= conferenceCreateDto.EndDate)))
            {
                if (currentCulture == "az")
                {
                    ModelState.AddModelError("Error", "Auditoriyada Həmin tarix-də konferans var.Zəhmət olmasa basqa tarixi seçin!");
                }
                else if (currentCulture=="en")
                {
                    ModelState.AddModelError("Error", "There is a conference on the same date in the auditorium. Please select another date!");
                }
                else
                {
                    ModelState.AddModelError("Error", "В аудитории на ту же дату запланирована конференция. Пожалуйста, выберите другую дату!");
                }
                var auditorium = _auditoriumService.GetAllAuditorium();
                ViewBag.Auditorium = auditorium.Data;
                var CurrentUserId = _contextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
                conferenceCreateDto.UserId = CurrentUserId;
                return View(conferenceCreateDto);
            }
            if (Photo is null)
            {
                if (currentCulture == "az")
                {
                    ModelState.AddModelError("Error", "Şəkil Əlavə edin!");
                }
                else if (currentCulture == "en")
                {
                    ModelState.AddModelError("Error", "Add a picture!");
                }
                else
                {
                    ModelState.AddModelError("Error", "Добавьте изображение!");
                }
                var auditorium = _auditoriumService.GetAllAuditorium();
                ViewBag.Auditorium = auditorium.Data;
                var CurrentUserId = _contextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
                conferenceCreateDto.UserId = CurrentUserId;
                return View(conferenceCreateDto);
            }
            var imgUrl = await FileHelper.SaveFileAsync(Photo, WWWRootGetPaths.GetwwwrootPath);
            conferenceCreateDto.ImgUrl = imgUrl;
            var result=await _confransService.ConfrenceAddAsync(conferenceCreateDto);


            if (!result.IsSuccess)
            {
                    if (!string.IsNullOrEmpty(result.Message)) 
                    {

                        ModelState.AddModelError("Error", result.Message);


                    }
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
            return Redirect("/dashboard/ConferanceForUser/Index");
        }
        [HttpGet]
    public IActionResult Detail(string Id)
        {
            if (string.IsNullOrEmpty(Id))
            {
                return Redirect("/dashboard/ConferanceForUser/index");
            }
            var CurrentUserId = _contextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var culture=Thread.CurrentThread.CurrentCulture.Name;
            var data = _confransService.GetConferanceDetailForUser(UserId: CurrentUserId, ConfranceId: Id, LangCode: culture);
            return View(data.Data);
        }
        [HttpGet]
        public IActionResult Update(string Id)
        {
            if (string.IsNullOrEmpty(Id)) return Redirect("/dashboard/ConferanceForUser/Index");
            var CurrentUserId = _contextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var result = _confransService.GetConferenceForUpdateUser(UserId: CurrentUserId,Id);
            if (!result.IsSuccess) return Redirect("/dashboard/ConferanceForUser/Index");
            var auditorium = _auditoriumService.GetAllAuditorium();
            ViewBag.Auditorium = auditorium.Data;
            return View(result.Data);
        }
        [HttpPost]
        public async Task<IActionResult> Update(ConferenceUpdateDto conferenceUpdateDto)
        {
            var currentCulture = Thread.CurrentThread.CurrentCulture.Name;
            ConferenceUpdateDTOValidator validationRules = new ConferenceUpdateDTOValidator(currentCulture);
            var ValidatorResult=validationRules.Validate(conferenceUpdateDto);
            if (!ValidatorResult.IsValid)
            {
                var CurrentUserId = _contextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
                var result = _confransService.GetConferenceForUpdateUser(UserId: CurrentUserId, conferenceUpdateDto.Id.ToString());
                if (result.Data is null)
                {
                    return Redirect("/dashboard/ConferanceForUser/Index");
                }
                var auditorium = _auditoriumService.GetAllAuditorium();
                ViewBag.Auditorium = auditorium.Data;
                for (int i = 0; i < ValidatorResult.Errors.Count; i++)
                {
                    ModelState.AddModelError("Error",  ValidatorResult.Errors[i].ErrorMessage);
                }
                return View(result.Data);
            }
         
            var resulUpdate=await _confransService.ConfrenceUpdateAsync(conferenceUpdateDto);
            if (!resulUpdate.IsSuccess)
            {
                var CurrentUserId = _contextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
                var result = _confransService.GetConferenceForUpdateUser(UserId: CurrentUserId, conferenceUpdateDto.Id.ToString());
                if (result.Data is null)
                {
                    return Redirect("/dashboard/ConferanceForUser/Index");
                }
                var auditorium = _auditoriumService.GetAllAuditorium();
                ViewBag.Auditorium = auditorium.Data;
                return View(result.Data);
            }
            return Redirect("/dashboard/ConferanceForUser/Index");
        }
    }
}
