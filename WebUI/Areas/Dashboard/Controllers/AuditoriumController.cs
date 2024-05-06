﻿using Aztu_Events.Business.Abstarct;
using Aztu_Events.Entities.DTOs.AudutoriumDTOs;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Areas.Dashboard.Controllers
{
    [Area(nameof(Dashboard))]
    public class AuditoriumController : Controller
    {
        private readonly IAuditoriumService _auditoriumService;
public AuditoriumController(IAuditoriumService auditoriumService)
        {
            _auditoriumService = auditoriumService;
        }

        public IActionResult Index()
        {
            var result=_auditoriumService.GetAllAuditorium();
            return View(result.Data);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(AddAuditoriumDTO addAuditoriumDTO)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var result = _auditoriumService.AddAuditorium(addAuditoriumDTO);

            return  Redirect("/dashboard/auditorium/index");
        }
        [HttpGet]
        public IActionResult Edit(string id)
        {
           if (string.IsNullOrEmpty(id)) return RedirectToAction("Index");
            var result = _auditoriumService.GetAuditorium(id);
            return View(new UpdateAuditoriumDTO
            {
                AuditoriumId=result.Data.AuditoriumId,
                 AuditoriumCapacity=result.Data.AuditoriumCapacity,
                 AudutoriyaNumber=result.Data.AudutoriyaNumber
            });  
        }
        [HttpPost]
        public IActionResult Edit(UpdateAuditoriumDTO updateAuditoriumDTO)
        {
            if (!ModelState.IsValid)
            {
                var result1 = _auditoriumService.GetAuditorium(updateAuditoriumDTO.AuditoriumId.ToString());
                return View(new UpdateAuditoriumDTO
                {
                    AuditoriumId = result1.Data.AuditoriumId,
                    AuditoriumCapacity = result1.Data.AuditoriumCapacity,
                    AudutoriyaNumber = result1.Data.AudutoriyaNumber
                });
            }
            var result = _auditoriumService.UpdateAuditorium(updateAuditoriumDTO);
            return Redirect("/dashboard/auditorium/index");
        }
        [HttpPost]
        public IActionResult Delete(string id)
        {
            if (string.IsNullOrEmpty(id)) return BadRequest();
            var result=_auditoriumService.DeleteAuditorium(id);
            return result.IsSuccess ? Ok():BadRequest() ;
        }
    }
}