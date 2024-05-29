using Aztu_Events.Business.Abstarct;
using Aztu_Events.Business.FluentValidation.EventTypeDTOValidation;
using Aztu_Events.Entities.DTOs.EventTypeDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace WebUI.Areas.Dashboard.Controllers
{
    [Area(nameof(Dashboard))]
    [Authorize(Roles = "Admin,SuperAdmin")]
    public class EventTypeController : Controller
    {
        private readonly IEventTypeService _eventTypeService;

        public EventTypeController(IEventTypeService eventTypeService)
        {
            _eventTypeService = eventTypeService;
        }

        public IActionResult Index()
        {
            var currentCulture = Thread.CurrentThread.CurrentCulture.Name;
            var data = _eventTypeService.GetAllEventType(currentCulture);
            return View(data.Data);
        }
        [HttpGet]
        public IActionResult Create()
        {

            return View();
        }
        [HttpPost]
        public IActionResult Create(AddEventTypeDTO addEventTypeDTO)
        {
            var CurrentCulture=Thread.CurrentThread.CurrentCulture.Name;
            AddEventTypeDTOValidation validationRules = new AddEventTypeDTOValidation(CurrentCulture);
            var ValidationResult=validationRules.Validate(addEventTypeDTO);
            if (!ValidationResult.IsValid) 
            {
                foreach (var error in ValidationResult.Errors)
                {
                    ModelState.AddModelError("Error", error.ErrorMessage);
                }
            
            return View(addEventTypeDTO);
            }
            var result=_eventTypeService.AddEventType(addEventTypeDTO);
            if (!result.IsSuccess)
                return View();
            return Redirect("/dashboard/eventtype/index");
        }
        [HttpDelete]
        public IActionResult  Delete(string Id)
        {
            if (string.IsNullOrEmpty(Id))
                return BadRequest();
            var result=_eventTypeService.RemoveEventType(Id);

            return result.IsSuccess ?Ok():BadRequest();
        }
        [HttpGet]
        public IActionResult Edit(string Id)
        {
            if (string.IsNullOrEmpty(Id)) return Redirect("/dashboard/eventtype/index");
            var data=_eventTypeService.GetEventTypeForUpate(Id);
            
            if (!data.IsSuccess)
                return Redirect("/dashboard/eventtype/index");
            return View(new UpdateEventTypeDTO
            {
                Content=data.Data.Content,
                Id=data.Data.EventTypeId,
                LangCode=data.Data.LangCode,
            });
        }
        [HttpPost]
        public IActionResult Edit(UpdateEventTypeDTO updateEventTypeDTO) 
        {
            var culutre=Thread.CurrentThread.CurrentCulture.Name;
            UpdateEventTypeDTOValidation validationRules = new UpdateEventTypeDTOValidation(culutre);
            var ValidatonResult=validationRules.Validate(updateEventTypeDTO);
            if (!ValidatonResult.IsValid)
            {
                foreach (var error in ValidatonResult.Errors)
                {
                    ModelState.AddModelError("Errror", error.ErrorMessage);
                }
                if (updateEventTypeDTO.Id is null)
                {
                    return Redirect("/dashboard/eventtype/index");
                }
                var data = _eventTypeService.GetEventTypeForUpate(updateEventTypeDTO.Id);

                if (!data.IsSuccess)
                    return Redirect("/dashboard/eventtype/index");
                return View(new UpdateEventTypeDTO
                {
                    Content = data.Data.Content,
                    Id = data.Data.EventTypeId,
                    LangCode = data.Data.LangCode,
                });
            }

            var result = _eventTypeService.UpdateEventType(updateEventTypeDTO);


             return Redirect("/dashboard/eventtype/index");
        }
    }
}
