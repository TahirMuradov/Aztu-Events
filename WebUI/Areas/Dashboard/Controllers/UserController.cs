using Aztu_Events.Business.Abstarct;
using Aztu_Events.Entities.DTOs.UserDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Areas.Dashboard.Controllers
{
    [Area(nameof(Dashboard))]
    [Authorize(Roles ="Admin")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly IRoleService _roleService;
        public UserController(IUserService userService, IRoleService roleService)
        {
            _userService = userService;
            _roleService = roleService;
        }
        [HttpGet]

        public async Task<IActionResult> Index()
        {

            var currentCulture = Thread.CurrentThread.CurrentCulture.Name;
           var result= await _userService.GetAllUserAsync(LangCode: currentCulture);

            return View(result.Data);
        }
        [HttpPost]
        public  async Task< IActionResult> Delete(string id)
        {
            if (id == null) return BadRequest();

        var result= await   _userService.DeleteUserAsync(id);
            return Ok(result);
        }
        [HttpGet]
        public async Task<IActionResult> AddRole(string id)
        {
            if (id is null)
          RedirectToAction("Index");
            var roles = _roleService.GetRoles().Data;
            var userRole = await _userService.GetUserAsync(id, "az");
            if (userRole is null)
                RedirectToAction("Index");
            if (userRole.Data.Roles.Contains("Admin"))
                return Redirect("/dashboard/user/index");
            ViewBag.Roles = roles.Where(x => !userRole.Data.Roles.Contains(x.RoleName)).ToList();

            if (string.IsNullOrEmpty(id)) { return RedirectToAction("Index"); }
            return View(new UserAddRoleDTO
            {
                UserId = id
            });

        }
        [HttpPost]
        public async Task<IActionResult> AddRole(UserAddRoleDTO userAddRoleDTO)
        {
            var result = await _userService.AssignRoleToUserAsnyc(UserId: userAddRoleDTO.UserId, RoleId: userAddRoleDTO.RoleId);
            if (!result.IsSuccess)
                return Redirect($"/dashboard/user/addrole/{userAddRoleDTO.UserId}");
            return Redirect("/dashboard/user/index");

        }
        [HttpGet]
        public async Task<IActionResult> UserDeleteRole(string id)

        {
            if (string.IsNullOrEmpty(id)) Redirect("/dashboard/user/index");
            var role = await _userService.GetUserAsync(UserId: id,LangCode:"az");

            if (!role.IsSuccess) return Redirect("/dashboard/user/index");

            if (role.Data.Roles.Count == 0) return Redirect("/dashboard/user/index");
            if (!role.Data.Roles.Contains("Admin")) return Redirect("/dashboard/user/index");

            return View(new UserDeleteRoleDTO
            {
                UserId = id,
                Roles = role.Data.Roles

            });

        }
        [HttpPost]
        public async Task<IActionResult> UserDeleteRole(UserDeleteRoleDTO userDeleteRoleDTO)
        {
         

               


            var result = await _userService.UserDeleteRoleAsync(UserId: userDeleteRoleDTO.UserId, RoleNames: userDeleteRoleDTO.Roles);
            return result.IsSuccess ?
                Redirect("/dashboard/user/index")
                     :
               View(result.Message);

        }
    }
}
