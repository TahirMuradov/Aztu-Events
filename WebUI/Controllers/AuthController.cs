using Aztu_Events.Core.Helper.EmailHelper;
using Aztu_Events.Entities.Concrete;
using Aztu_Events.Entities.DTOs.AuthDTOs;
using Aztu_Events.Entities.DTOs.UserDTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;


namespace WebUI.Controllers
{
    public class AuthController : Controller
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly IEmailHelper _EmailHelper;
        public AuthController(SignInManager<User> signInManager, UserManager<User> userManager, IEmailHelper emailHelper)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _EmailHelper = emailHelper;
        }
        public IActionResult Login()
        {

            if (User.Identity.IsAuthenticated)
            {
                return Redirect("/dashboard");
            }

            return View();
        }
        [HttpPost]
        public async Task< IActionResult> Login(LoginDTO loginDTO)
        {
            if (!ModelState.IsValid)
            {
            
                return View();
            }
            var checkEmail = await _userManager.FindByEmailAsync(loginDTO.Email);

            if (checkEmail == null)
            {


                ModelState.AddModelError("Error", "Email və ya Parol Səhvdir!");
                return View();
            }
            //var CurrentUserRole = await _userManager.GetRolesAsync(checkEmail);
            //if (CurrentUserRole.Count == 0)
            //{
            //    return Redirect("/error/index");
            //}

            Microsoft.AspNetCore.Identity.SignInResult signInResult = await _signInManager.PasswordSignInAsync(checkEmail, loginDTO.Password, loginDTO.RememberMe, true);
            if (!signInResult.Succeeded)
            {
                ModelState.AddModelError("Error", "Email və ya Parol Səhvdir!");

                return View();
            }
            return Redirect("/dashboard");
        }

        public async Task<IActionResult> LogOut()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("login");
            }
            await _signInManager.SignOutAsync();
            return RedirectToAction("login");

        }
    
        [HttpGet]
        public async Task<IActionResult> Register()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction(controllerName: "home", actionName: "index");
            }

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterDTO registerDTO)
        {
            if (!ModelState.IsValid) return View();
            if (!Regex.IsMatch(registerDTO.PhoneNumber, "^(?!0+$)(\\+\\d{1,3}[- ]?)?(?!0+$)\\d{10,15}$"))
            {
                ModelState.AddModelError("Error", "Əlaqə Nömrəsini düzgün qeyd edin!");
                return View();
            }
            var checekEmail = await _userManager.FindByEmailAsync(registerDTO.Email);
            if (checekEmail != null)
            {
                ModelState.AddModelError("Error", "Bu Emailde Istifadeci var");
                return View();
            }
            registerDTO.UserName = registerDTO.Firstname + registerDTO.Lastname + Guid.NewGuid().ToString().Substring(0, 5);

            var result = await _userManager.CreateAsync(new User
            {
                UserName = registerDTO.UserName,
                Email = registerDTO.Email,
                FirstName = registerDTO.Firstname,
                LastName = registerDTO.Lastname,
                PhoneNumber = registerDTO.PhoneNumber

            }, registerDTO.Password);
            if (result.Succeeded)
            {
                var user = await _userManager.FindByEmailAsync(registerDTO.Email);

               string token= await _userManager.GenerateEmailConfirmationTokenAsync(user);
                var confirmLink = Url.ActionLink(controller: "Auth",
        action: "ConfirmEmail",
        host: Request.Host.Value,
        values: new { email = user.Email,token });
          var emailResult=    await  _EmailHelper.SendEmailAsync(userEmail: user.Email, confirmationLink: confirmLink, UserName: user.UserName);
                if (emailResult.IsSuccess)
                {

                ModelState.AddModelError("Error", "Emailinize Tesdiqleme Linki Gonderirldi!");
                return View();
                }


               await _userManager.DeleteAsync(user);
                ModelState.AddModelError("Error", "Qeydiyyat Uğursuz oldu yenidən cəhd edin!");
                return View();


            }





            if (!result.Succeeded)
            {
                ModelState.AddModelError("Error", result.Errors.ToString());
                return View();
            }

            return RedirectToAction("Login");


        }

        [HttpGet]
        public IActionResult ForgotPassword()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction(controllerName: "home", actionName: "index");
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ForgotPassword(string Email)
        {

            var user = await _userManager.FindByEmailAsync(email: Email);
            if (user == null)
            {
                ModelState.AddModelError("Error", "Bu Elektron Poctda Istifadeci tapilmadi!");
                return View();
            }
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var confirmLink = Url.ActionLink(controller: "Auth",
                action: "ForgotPasswordConfirm",
                host: Request.Host.Value,
                values: new { token, email = user.Email });
            var result = await _EmailHelper.SendEmailAsync(user.Email, confirmLink, user.UserName);
            if (result.IsSuccess)
                ModelState.AddModelError("Error", "Elektron Poctunuzu yoxlayin tesdiqleme linki gonderildi!");
            return View();

        }
        [HttpGet]
        public async Task<IActionResult> ForgotPasswordConfirm(string token, string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user is null)
                return Redirect("/auth/login");

            bool tokenResult = await _userManager.VerifyUserTokenAsync(
            user: user,
            tokenProvider: _userManager.Options.Tokens.PasswordResetTokenProvider,
            purpose: UserManager<User>.ResetPasswordTokenPurpose,
            token: token
                           );
            if (!tokenResult)
            {
                return Redirect("/auth/login");
            }
            return View(new UserResetPasswordDTO
            {
                UserId = user.Id,
                Token = token,


            });
        }
        [HttpPost]
        public async Task<IActionResult> ForgotPasswordConfirm(UserResetPasswordDTO userResetPasswordDTO)
        {
            var user = await _userManager.FindByIdAsync(userResetPasswordDTO.UserId);
            if (user is null)
                return Redirect("/auth/login");
            var tokenResult = await _userManager.ResetPasswordAsync(user, userResetPasswordDTO.Token, userResetPasswordDTO.NewPassword);

            if (tokenResult.Succeeded)
            {
                return Redirect("/auth/login");
            }
            return View(new UserResetPasswordDTO
            {
                UserId = user.Id,
                Token = userResetPasswordDTO.Token

            });
        }
        public async Task<IActionResult> ConfirmEmail(string email,string token)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(token))
            {
                return RedirectToAction("login");
            }
            var user = await _userManager.FindByEmailAsync(email);
            if (user is null) return RedirectToAction("login");
            IdentityResult result = await _userManager.ConfirmEmailAsync(user, token);
            if (result.Succeeded)
            {
                return View();
            }
            return RedirectToAction("index");
        }
    }
}
