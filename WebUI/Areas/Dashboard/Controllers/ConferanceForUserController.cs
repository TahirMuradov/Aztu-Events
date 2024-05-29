using Aztu_Events.Business.Abstarct;
using Aztu_Events.Business.FluentValidation.ConferanceValidator;
using Aztu_Events.Core.Helper;
using Aztu_Events.Core.Helper.FileHelper;
using Aztu_Events.Entities.DTOs.Conferences;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;

namespace WebUI.Areas.Dashboard.Controllers
{
    [Area(nameof(Dashboard))]
    [Authorize(Roles = "Admin,User,SuperAdmin")]
    public class ConferanceForUserController : Controller
    {
        private readonly IConfransService _confransService;
        private readonly IAuditoriumService _auditoriumService;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly ITimeService _timeService;
        private readonly ICategoryService _categoryService;
        private readonly IEventTypeService _eventTypeService;

        public ConferanceForUserController(IConfransService confransService, IHttpContextAccessor contextAccessor, IAuditoriumService auditoriumService, ITimeService timeService, ICategoryService categoryService, IEventTypeService eventTypeService)
        {
            _confransService = confransService;
            _contextAccessor = contextAccessor;
            _auditoriumService = auditoriumService;
            _timeService = timeService;
            _categoryService = categoryService;
            _eventTypeService = eventTypeService;
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
            var currentCulture = Thread.CurrentThread.CurrentCulture.Name;
            var CurrentUserId = _contextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var auditorium = _auditoriumService.GetAllAuditorium();
            ViewBag.Auditorium = auditorium.Data;
            var category = _categoryService.GetAllCategory(currentCulture);
            var type=_eventTypeService.GetAllEventType(currentCulture);
            ViewBag.Type = type.Data;
            ViewBag.Category = category.Data;
            return View(new ConferenceAddDTO
            {
                UserId = CurrentUserId,
            });
        }
        [HttpPost]
        public async Task<IActionResult> Create(ConferenceAddDTO conferenceCreateDto, IFormFile Photo,IFormFile file)
        {
            var currentCulture = Thread.CurrentThread.CurrentCulture.Name;
            ConferanceAddDTOValidator validationRules = new ConferanceAddDTOValidator(currentCulture);
            var resultValidator = validationRules.Validate(conferenceCreateDto);
            if (!resultValidator.IsValid)
            {
                for (int i = 0; i < resultValidator.Errors.Count; i++)
                {
                    if (resultValidator.Errors[i].ErrorMessage == "Conference start time cannot be empty." && (currentCulture == "az" || currentCulture == "ru"))
                        continue;
                    ModelState.AddModelError("Error", resultValidator.Errors[i].ErrorMessage);

                }
                var auditorium = _auditoriumService.GetAllAuditorium();
                ViewBag.Auditorium = auditorium.Data;
                var category = _categoryService.GetAllCategory(currentCulture);
                ViewBag.Category = category.Data;
                var type = _eventTypeService.GetAllEventType(currentCulture);
                ViewBag.Type = type.Data;
                var CurrentUserId = _contextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
                conferenceCreateDto.UserId = CurrentUserId;
                return View(conferenceCreateDto);
            }
            var AllTimes = _timeService.GetAllTime().Data;

            if (AllTimes.Any(x => (x.Date == conferenceCreateDto.Day && x.AuditoriumId == conferenceCreateDto.AudutoriumId) && !(x.EndTime <= conferenceCreateDto.StartedDate || x.StartedTime >= conferenceCreateDto.EndDate)))
            {
                if (currentCulture == "az")
                {
                    ModelState.AddModelError("Error", "Auditoriyada Həmin tarix-də konferans var.Zəhmət olmasa basqa tarixi seçin!");
                }
                else if (currentCulture == "en")
                {
                    ModelState.AddModelError("Error", "There is a conference on the same date in the auditorium. Please select another date!");
                }
                else
                {
                    ModelState.AddModelError("Error", "В аудитории на ту же дату запланирована конференция. Пожалуйста, выберите другую дату!");
                }
                var auditorium = _auditoriumService.GetAllAuditorium();
                ViewBag.Auditorium = auditorium.Data;
                var category = _categoryService.GetAllCategory(currentCulture);
                ViewBag.Category = category.Data;
                var CurrentUserId = _contextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
                conferenceCreateDto.UserId = CurrentUserId;
                return View(conferenceCreateDto);
            }
            if (file is null)
            {
                if (currentCulture == "az")
                {
                    ModelState.AddModelError("Error", "Təqdim Edəcəyiniz Slyadı Pdf Formatında Əlavə edin!");
                }
                else if (currentCulture == "en")
                {
                    ModelState.AddModelError("Error", "Add the Slide You Will Present in Pdf Format!");
                }
                else
                {
                    ModelState.AddModelError("Error", "Добавьте слайд, который вы представите в формате PDF!");
                }
                var auditorium = _auditoriumService.GetAllAuditorium();
                ViewBag.Auditorium = auditorium.Data;
                var category = _categoryService.GetAllCategory(currentCulture);
                ViewBag.Category = category.Data;
                var type = _eventTypeService.GetAllEventType(currentCulture);
                ViewBag.Type = type.Data;
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
                var category = _categoryService.GetAllCategory(currentCulture);
                ViewBag.Category = category.Data;
                var type = _eventTypeService.GetAllEventType(currentCulture);
                ViewBag.Type = type.Data;
                var CurrentUserId = _contextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
                conferenceCreateDto.UserId = CurrentUserId;
                return View(conferenceCreateDto);
            }
            var imgUrl = await FileHelper.SaveFileAsync(Photo, WWWRootGetPaths.GetwwwrootPath);
            var pdfUrl = await FileHelper.SavePdfAsync(file, WWWRootGetPaths.GetwwwrootPath);
            conferenceCreateDto.ImgUrl = imgUrl;
            conferenceCreateDto.PdfUrl = pdfUrl;
            var result = await _confransService.ConfrenceAddAsync(conferenceCreateDto);


            if (!result.IsSuccess)
            {
                if (!string.IsNullOrEmpty(result.Message))
                {

                    ModelState.AddModelError("Error", result.Message);


                }
                if (currentCulture == "az")
                {
                    ModelState.AddModelError("Error", "Konfrans Yaradila Bilmədi!Səhifəni yenidən yükləyərək Təkrar Yoxlayın");
                }
                else if (currentCulture == "en")
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
        public async Task<IActionResult> Detail(string Id)
        {
            if (string.IsNullOrEmpty(Id))
            {
                return Redirect("/dashboard/ConferanceForUser/index");
            }
            var CurrentUserId = _contextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var culture = Thread.CurrentThread.CurrentCulture.Name;
            var data = await _confransService.GetConferanceDetailForUserAsync(UserId: CurrentUserId, ConfranceId: Id, LangCode: culture);
            if (data.Data is null || !data.IsSuccess)
            {
                return Redirect("/dashboard/ConferanceForUser/index");
            }
            return View(data.Data);
        }
        [HttpGet]
        public IActionResult Update(string Id)
        {
            if (string.IsNullOrEmpty(Id)) return Redirect("/dashboard/ConferanceForUser/Index");
            var CurrentUserId = _contextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var result = _confransService.GetConferenceForUpdateUser(UserId: CurrentUserId, Id);
            if (!result.IsSuccess) return Redirect("/dashboard/ConferanceForUser/Index");
            var auditorium = _auditoriumService.GetAllAuditorium();
            ViewBag.Auditorium = auditorium.Data;
            var currentCulture = Thread.CurrentThread.CurrentCulture.Name;
            var category = _categoryService.GetAllCategory(currentCulture);
            ViewBag.Category = category.Data;
            return View(result.Data);
        }
        [HttpPost]
        public async Task<IActionResult> Update(ConferenceUpdateDto conferenceUpdateDto,IFormFile Photo)
        {
            var currentCulture = Thread.CurrentThread.CurrentCulture.Name;
            ConferenceUpdateDTOValidator validationRules = new ConferenceUpdateDTOValidator(currentCulture);
            var ValidatorResult = validationRules.Validate(conferenceUpdateDto);
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
                var category = _categoryService.GetAllCategory(currentCulture);
                ViewBag.Category = category.Data;
                for (int i = 0; i < ValidatorResult.Errors.Count; i++)
                {
                    ModelState.AddModelError("Error", ValidatorResult.Errors[i].ErrorMessage);
                }
                return View(result.Data);
            }
            if (conferenceUpdateDto.ImgUrl is  null && Photo is null)
            {
                var CurrentUserId = _contextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
                var result = _confransService.GetConferenceForUpdateUser(UserId: CurrentUserId, conferenceUpdateDto.Id.ToString());
                if (result.Data is null)
                {
                    return Redirect("/dashboard/ConferanceForUser/Index");
                }
                var auditorium = _auditoriumService.GetAllAuditorium();
                ViewBag.Auditorium = auditorium.Data;
                var category = _categoryService.GetAllCategory(currentCulture);
                ViewBag.Category = category.Data;
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
                return View(result.Data);
            }
            if (Photo is not null)
            {
            string url=    await FileHelper.SaveFileAsync(Photo, WWWRootGetPaths.GetwwwrootPath);
                conferenceUpdateDto.ImgUrl = url;
            
            }
            
            
            var resulUpdate = await _confransService.ConfrenceUpdateAsync(conferenceUpdateDto);
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
                var category = _categoryService.GetAllCategory(currentCulture);
                ViewBag.Category = category.Data;
                return View(result.Data);
            }
            return Redirect("/dashboard/ConferanceForUser/Index");
        }

        [HttpGet]
        public IActionResult GetConfranceTime(string AuditoriumId)
        {
            if (string.IsNullOrEmpty(AuditoriumId)) return BadRequest();

            var auditorium = _auditoriumService.GetAuditorium(AuditoriumId);
            var serialaizer = JsonConvert.SerializeObject(auditorium.Data);
            return Json(serialaizer);
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteRegistrationUser(string UserId,string ConferenceId) 
        {
           if (string.IsNullOrEmpty(UserId) ||string.IsNullOrEmpty(ConferenceId)) return BadRequest();
           var result= await _confransService.DeleteRegistretionUserAsync(UserId,ConferenceId);
            return result.IsSuccess? Ok():BadRequest();
        }
    
    }
}
