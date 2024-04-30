using Aztu_Events.Business.FluentValidation.AuthDTOValidator;
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
        private readonly RoleManager<IdentityRole> _roleManager;
        public AuthController(SignInManager<User> signInManager, UserManager<User> userManager, IEmailHelper emailHelper, RoleManager<IdentityRole> roleManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _EmailHelper = emailHelper;
            _roleManager = roleManager;
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
            var currentCulture=Thread.CurrentThread.CurrentCulture.Name;
            LoginDTOValidation validator = new LoginDTOValidation(currentCulture);
            var result=validator.Validate(loginDTO);
            if (!result.IsValid)
            {
                for (int i=0;i<result.Errors.Count;i++)
                {
                    if (i%2!=0)
                    {

                    ModelState.AddModelError("Error", result.Errors[i].ErrorMessage);
                    }
            

                }
                return View();    
            }


            var checkEmail = await _userManager.FindByEmailAsync(loginDTO.Email);

            if (checkEmail == null)
            {

                if (currentCulture == "az")
                {
                    ModelState.AddModelError("Error", "Email və ya Parol Səhvdir!");
                }
                else if (currentCulture == "ru")
                {
                    ModelState.AddModelError("Error", "Неверный адрес электронной почты или пароль!");
                }
                else 
                {
                    ModelState.AddModelError("Error", "Incorrect email or password!");
                }

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

                if (currentCulture == "az")
                {
                    ModelState.AddModelError("Error", "Email və ya Parol Səhvdir!");
                }
                else if (currentCulture == "ru")
                {
                    ModelState.AddModelError("Error", "Неверный адрес электронной почты или пароль!");
                }
                else
                {
                    ModelState.AddModelError("Error", "Incorrect email or password!");
                }

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
          var currentCulture=Thread.CurrentThread.CurrentCulture.Name;
           RegisterDTOValidator validator=new RegisterDTOValidator(currentCulture);
            var ValidatorResult=validator.Validate(registerDTO);
            if (!ValidatorResult.IsValid)
            {
                for (int i = 0; i < ValidatorResult.Errors.Count; i++)
                {
                  

                        ModelState.AddModelError("Error", ValidatorResult.Errors[i].ErrorMessage);
                    


                }
                return View(registerDTO);
            }
            var checekEmail = await _userManager.FindByEmailAsync(registerDTO.Email);
            if (checekEmail != null)
            {
                if (currentCulture == "az")
                {
                    ModelState.AddModelError("Error", "Bu Emailde Istifadeci var");
                }
                else if (currentCulture == "ru")
                {
                    ModelState.AddModelError("Error", "Пользователь с этим адресом электронной почты уже существует!");
                }
                else
                {
                    ModelState.AddModelError("Error", "User with this email already exists!");
                }
               
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
            var role = await _roleManager.FindByNameAsync("Admin");
            if (role is null)
            {
                await _roleManager.CreateAsync(new IdentityRole
                {
                    Name = "Admin"
                });
            }
            var role2 = await _roleManager.FindByNameAsync("User");
            if (role2 is null)
            {
                await _roleManager.CreateAsync(new IdentityRole
                {
                    Name = "User"
                });
            }

            if (result.Succeeded)
            {
                var user = await _userManager.FindByEmailAsync(registerDTO.Email);
                if (_userManager.Users.Count() == 1)
                {
                  await  _userManager.AddToRoleAsync(user, "Admin");
                }
                else
                {
                    await _userManager.AddToRoleAsync(user, "User");
                }
                string token= await _userManager.GenerateEmailConfirmationTokenAsync(user);
                var confirmLink = Url.ActionLink(controller: "Auth",
        action: "ConfirmEmail",
        host: Request.Host.Value,
        values: new { email = user.Email,token });
          var emailResult=    await  _EmailHelper.SendEmailAsync(userEmail: user.Email, confirmationLink: confirmLink, UserName: user.UserName);
                if (emailResult.IsSuccess)
                {
                    if (currentCulture == "az")
                    {
                        ModelState.AddModelError("Error", "Emailinize Tesdiqleme Linki Gonderirldi!");
                    }
                    else if (currentCulture == "ru")
                    {
                        ModelState.AddModelError("Error", "Ссылка для подтверждения отправлена на вашу электронную почту!");
                    }
                    else
                    {
                        ModelState.AddModelError("Error", "Confirmation link has been sent to your email!");
                    }
                  
                return View();
                }


               await _userManager.DeleteAsync(user);
                if (currentCulture == "az")
                {
                    ModelState.AddModelError("Error", "Qeydiyyat Uğursuz oldu yenidən cəhd edin!");
                }
                else if (currentCulture == "ru")
                {
                    ModelState.AddModelError("Error", "Регистрация не удалась, пожалуйста, попробуйте еще раз!");
                }
                else
                {
                    ModelState.AddModelError("Error", "Registration failed, please try again!");
                }
       
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
            var currentCulture=Thread.CurrentThread.CurrentCulture.Name;
            if (string.IsNullOrEmpty(Email))
            {

                if (currentCulture == "az")
                {
                    ModelState.AddModelError("Error", "Email Boş ola bilməz!");
                }
                else if (currentCulture == "ru")
                {
                    ModelState.AddModelError("Error", "Поле электронной почты не может быть пустым!");
                }
                else
                {
                    ModelState.AddModelError("Error", "Email field cannot be empty!");
                }
                return View();
            }
            var user = await _userManager.FindByEmailAsync(email: Email);
            if (user == null)
            {
                if (currentCulture == "az")
                {
                    ModelState.AddModelError("Error", "Bu Elektron Poctda Istifadeci tapilmadi!");
                }
                else if (currentCulture == "ru")
                {
                    ModelState.AddModelError("Error", "Пользователь с этим адресом электронной почты не найден!");
                }
                else
                {
                    ModelState.AddModelError("Error", "No user found with this email!");
                }
            
                return View();
            }
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var confirmLink = Url.ActionLink(controller: "Auth",
                action: "ForgotPasswordConfirm",
                host: Request.Host.Value,
                values: new { token, email = user.Email });
            var result = await _EmailHelper.SendEmailAsync(user.Email, confirmLink, user.UserName);
            if (result.IsSuccess)
            {
                if (currentCulture == "az")
                {
                    ModelState.AddModelError("Error", "Emailinize Tesdiqleme Linki Gonderirldi!");
                }
                else if (currentCulture == "ru")
                {
                    ModelState.AddModelError("Error", "Ссылка для подтверждения отправлена на вашу электронную почту!");
                }
                else
                {
                    ModelState.AddModelError("Error", "Confirmation link has been sent to your email!");
                }
            }    
               
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
