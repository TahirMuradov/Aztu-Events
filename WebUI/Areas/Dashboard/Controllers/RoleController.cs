using Aztu_Events.Business.Abstarct;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Areas.Dashboard.Controllers
{
    [Area(nameof(Dashboard))]
    [Authorize(Roles ="Admin")]
    public class RoleController : Controller
    {
        private readonly IRoleService _roleService;


        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        public IActionResult Index()
        {
            var result=_roleService.GetRoles();
            return View(result.Data);
        }
    }
}
