using Aztu_Events.Business.Abstarct;
using Aztu_Events.Business.FluentValidation.CategoryDTOValidation;
using Aztu_Events.Entities.DTOs.CategoryDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Areas.Dashboard.Controllers
{
    [Area(nameof(Dashboard))]
    [Authorize(Roles = "Admin,SuperAdmin")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public IActionResult Index()
        {
            var currentCulture = Thread.CurrentThread.CurrentCulture.Name;
            var datas = _categoryService.GetAllCategory(currentCulture);
            return View(datas.Data);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(CategoryAddDTO categoryAddDTO)
        {
            var currentCulture = Thread.CurrentThread.CurrentCulture.Name;
            CategoryAddDTOValidator validationRules = new CategoryAddDTOValidator(currentCulture);
            var resultValidate = validationRules.Validate(categoryAddDTO);
            if (!resultValidate.IsValid)
            {
                for (int i = 0; i < resultValidate.Errors.Count; i++)
                {
                    ModelState.AddModelError("Error", resultValidate.Errors[i].ErrorMessage);
                }
                return View(categoryAddDTO);
            }
            var result = _categoryService.AddCategory(categoryAddDTO);
            if (result.IsSuccess)
                return Redirect("/dashboard/category/index");
            return View(categoryAddDTO);
        }
        [HttpDelete]
        public IActionResult Delete(string Id)
        {
            if (string.IsNullOrEmpty(Id))
                return BadRequest();
            var result = _categoryService.RemoveCategory(Id);
            return result.IsSuccess ? Ok() : BadRequest();
        }
        [HttpGet]
        public IActionResult Edit(string Id)
        {
            if (string.IsNullOrEmpty(Id))
                return Redirect("/dashboard/category/index");
            var data = _categoryService.CategoryGetForUpdate(Id);
            return View(data.Data);
        }
        [HttpPost]
        public IActionResult Edit(CategoryUpdateDTO categoryUpdateDTO)
        {
            var currentCulture=Thread.CurrentThread.CurrentCulture.Name;
            CategoryUpdateDTOValidator validationRules = new CategoryUpdateDTOValidator(currentCulture);
            var ValidatorResult= validationRules.Validate(categoryUpdateDTO);
            if (!ValidatorResult.IsValid)
            {
                for (int i = 0; i < ValidatorResult.Errors.Count; i++)
                {
                    ModelState.AddModelError("Error", ValidatorResult.Errors[i].ErrorMessage);
                }
                return View(categoryUpdateDTO);
            }
            var result = _categoryService.UpdateCategory(categoryUpdateDTO);
            if (!result.IsSuccess)
            {
                ModelState.AddModelError("Error", result.Message);
            return View();
            }
            return Redirect("/dashboard/category/index");
        }
    }
}
