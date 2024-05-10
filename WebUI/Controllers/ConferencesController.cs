using Aztu_Events.Business.Abstarct;
using Aztu_Events.Core.Helper.PageHelper;
using Aztu_Events.Core.Utilities.Results.Abstract;
using Aztu_Events.Entities.DTOs.Conferences;
using Aztu_Events.Entities.EnumClass;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers
{
    public class ConferencesController : Controller
    {
        private readonly IConfransService _confransService;
        private readonly ICategoryService _categoryService;

        public ConferencesController(IConfransService confransService, ICategoryService categoryService)
        {
            _confransService = confransService;
            _categoryService = categoryService;
        }

        public async Task<IActionResult> Index(int page=1)
        {
            
            string currentCulture=Thread.CurrentThread.CurrentCulture.Name;
            if (page == 0)
           page = 1;
           IDataResult< PaginatedList<ConferenceGetAdminListDTO>> data = null;
          

           data = await _confransService.ConferenceGetListFilterAsync(new FilterConferenceDto
            {
                Page = page,
                Status=ConferanceStatus.Təsdiq,
                CategoryId=null,
                PageSize=9
                
                
                
            }, lang: currentCulture);
                if (data.Data.Data is null||data.Data.Data.Count==0)
                {
                    page = 1;
                data = await _confransService.ConferenceGetListFilterAsync(new FilterConferenceDto
                {
                    Page = page,
                    Status = ConferanceStatus.Təsdiq,
                    CategoryId = null,
                    PageSize = 9



                }, lang: currentCulture);

            }
            
          
            ViewBag.Category = _categoryService.GetAllCategory(currentCulture).Data;
            ViewBag.CurrentPage = page;
            return View(data.Data);
        }
    }
}
