
using Aztu_Events.Business.Abstarct;
using Aztu_Events.Business.FluentValidation.UserDTOValidator;
using Aztu_Events.Entities.DTOs.UserDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace WebUI.Areas.Dashboard.Controllers
{
    [Area(nameof(Dashboard))]
    [Authorize(Roles ="SuperAdmin")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly IRoleService _roleService;
        private readonly IHttpContextAccessor _contextAccessor;
        public UserController(IUserService userService, IRoleService roleService, IHttpContextAccessor contextAccessor)
        {
            _userService = userService;
            _roleService = roleService;
            _contextAccessor = contextAccessor;
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
            if (userRole.Data.Roles.Contains("SuperAdmin"))
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
            var role = _roleService.GetRole(userAddRoleDTO.RoleId);
            if (!role .IsSuccess && role.Data is null) return Redirect("/dashboard/user/index");
            if (role.Data.RoleName=="SuperAdmin")
                Redirect("/dashboard/user/index");
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
            if (!role.Data.Roles.Contains("SuperAdmin")) return Redirect("/dashboard/user/index");

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
        [HttpGet]
    public async Task< IActionResult> Settings(string id)
        {
            if (string.IsNullOrEmpty(id))
                Redirect("/dashboard/home/index");
            var currentUserId= _contextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            if (string.IsNullOrEmpty(currentUserId))
                Redirect("/");
            if (id.ToLower()!=currentUserId.ToLower())
                Redirect("/dashboard/home/index");
            var user = await _userService.GetUserAsync(UserId:currentUserId, "az");
            if (user.Data is null)
                Redirect("/dashboard/home/index");

            return View(new UserUpdateDTO
            {
                UserId= user.Data.Id,
                Firstname = user.Data.FirstName,
                Lastname = user.Data.LastName,
                PhoneNumber = user.Data.PhoneNumber,
                UserName = user.Data.UserName
                
            });
        }
        [HttpPost]
        public async Task<IActionResult> Settings (UserUpdateDTO userUpdateDTO)
        { 
            var currentCulture=Thread.CurrentThread.CurrentCulture.Name;
            var currentUserId = _contextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            if (string.IsNullOrEmpty(currentUserId))
                Redirect("/");
            if (userUpdateDTO.UserId.ToLower() != currentUserId.ToLower())
                Redirect("/dashboard/home/index");
            UserUpdateDTOValidator validationRules =new UserUpdateDTOValidator(currentCulture);
            var ValidatorResult=validationRules.Validate(userUpdateDTO);
            if (!ValidatorResult.IsValid)
            {
                for (int i = 0; i < ValidatorResult.Errors.Count; i++)
                {
                    ModelState.AddModelError("Error", ValidatorResult.Errors[i].ErrorMessage);
                }
                return View(userUpdateDTO);
            }
            var result=await _userService.UpdateUserAsync(userUpdateDTO);
            if (!result.IsSuccess&& result.Message == "This is UserName Not Empty!")
            {
                if (currentCulture == "az")
                {
                    ModelState.AddModelError("Error", "Bu İstifadəçi Adında Artıq İstifadəçi Var!");
                }
                else if (currentCulture == "en")
                {
                    ModelState.AddModelError("Error", "There is already a user with this Username!");
                }
                else
                {
                    ModelState.AddModelError("Error", "Уже есть пользователь с таким именем пользователя!");
                }
            }
            if (!result.IsSuccess && result.Message=="password is not correct")
            {
                if (currentCulture == "az")
                {
                    ModelState.AddModelError("Error", "Cari Şifrə Yanlışdır!");
                }
                else if (currentCulture == "en")
                {
                    ModelState.AddModelError("Error", "Current Password is incorrect!");
                }
                else
                {
                    ModelState.AddModelError("Error", "Неверный текущий пароль!");
                }
            return View(userUpdateDTO);
            }
      
            return Redirect("/dashboard/home/index"); 
        
        }



    }
}
