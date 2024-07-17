using Aztu_Events.Business.Concrete;
using Aztu_Events.Business.FluentValidation.AuthDTOValidator;
using Aztu_Events.Core.Helper.EmailHelper;
using Aztu_Events.Entities.Concrete;
using Aztu_Events.Entities.DTOs.AuthDTOs;
using Aztu_Events.Entities.DTOs.UserDTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;


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
        public async Task<IActionResult> Login(LoginDTO loginDTO)
        {
         
            var currentCulture = Thread.CurrentThread.CurrentCulture.Name;
            LoginDTOValidation validator = new LoginDTOValidation(currentCulture);
            var result = validator.Validate(loginDTO);
            if (!result.IsValid)
            {
                for (int i = 0; i < result.Errors.Count; i++)
                {
                    if (i % 2 != 0)
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
         var UserRoles=   await _userManager.GetRolesAsync(checkEmail);
            if (UserRoles == null|| UserRoles.Count==0||UserRoles.Contains(null)||!(UserRoles.Contains("Admin") || UserRoles.Contains("SuperAdmin") || UserRoles.Contains("User") || UserRoles.Contains("User2")))
            {
                if (currentCulture == "az")
                {
                    ModelState.AddModelError("Error", "Sizin Profil Təsdiqlənməyib!Admin Tərəfindən Təsdiqləndikdən sonra daxil ola bilərsiniz!");
                }
                else if (currentCulture == "ru")
                {
                    ModelState.AddModelError("Error", "Ваш профиль не подтвержден! Вы можете войти в систему после подтверждения администратором!");
                }
                else
                {
                    ModelState.AddModelError("Error", "Your Profile Has Not Been Confirmed! You can log in after being confirmed by Admin!");
                }

                return View();
            }
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
            if (User.IsInRole("Admin")||User.IsInRole("SuperAdmin"))
            {

            return Redirect("/dashboard");
            }
            return Redirect("/");
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
            ViewBag.Roles=_roleManager.Roles.ToList();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterDTO registerDTO)
        {
            var currentCulture = Thread.CurrentThread.CurrentCulture.Name;
            RegisterDTOValidator validator = new RegisterDTOValidator(currentCulture);
            var ValidatorResult = validator.Validate(registerDTO);
            if (!ValidatorResult.IsValid)
            {
                ViewBag.Roles = _roleManager.Roles.ToList();
                for (int i = 0; i < ValidatorResult.Errors.Count; i++)
                {


                    ModelState.AddModelError("Error", ValidatorResult.Errors[i].ErrorMessage);



                }
                return View(registerDTO);
            }
            var checekEmail = await _userManager.FindByEmailAsync(registerDTO.Email);
            if (checekEmail != null)
            {
                ViewBag.Roles = _roleManager.Roles.ToList();
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

            IdentityRole role = null;
            if (!string.IsNullOrEmpty(registerDTO.RoleId))
            {
                role = await _roleManager.FindByIdAsync(registerDTO.RoleId);
                if (role is null || role.Name == "Admin" || role.Name == "SuperAdmin" || role.Name == "User" || role.Name == "User2")
                {
                    ViewBag.Roles = _roleManager.Roles.ToList();
                    if (currentCulture == "az")
                    {
                        ModelState.AddModelError("Error", "Vəzifə Tapilmadı!");
                    }
                    else if (currentCulture == "en")
                    {
                        ModelState.AddModelError("Error", "Position Not Found!");
                    }
                    else
                    {
                        ModelState.AddModelError("Error", "Позиция не найдена!");
                    }

                    return View();
                }
            }
            registerDTO.UserName = registerDTO.Firstname + registerDTO.Lastname + Guid.NewGuid().ToString().Substring(0, 5);
            User newUser = new User
            {
                UserName = registerDTO.UserName,
                Email = registerDTO.Email,
                FirstName = registerDTO.Firstname,
                LastName = registerDTO.Lastname,
                PhoneNumber = registerDTO.PhoneNumber,
                };
            var result = await _userManager.CreateAsync(newUser, registerDTO.Password);


            if (result.Succeeded)
            {
                var user = await _userManager.FindByEmailAsync(registerDTO.Email);
                if (_userManager.Users.Count() == 1)
                {


                    await _roleManager.CreateAsync(new IdentityRole
                    {
                        Name = "Admin"
                    });
                    await _roleManager.CreateAsync(new IdentityRole
                    {
                        Name = "SuperAdmin"
                    });


             
                    await _roleManager.CreateAsync(
                        new IdentityRole
                        {
                            Name = "Student"
                        }
                        );
                    await _roleManager.CreateAsync(
                     new IdentityRole
                     {
                         Name = "Teacher"
                     }
                     );
                    await _roleManager.CreateAsync(
                 new IdentityRole
                 {
                     Name = "User"
                 }
                 ); await _roleManager.CreateAsync(
                 new IdentityRole
                 {
                     Name = "User2"
                 }
                 );
                    await _roleManager.CreateAsync( new IdentityRole
               {
                   Name = "Organizer"
               }
               );

                    await _userManager.AddToRoleAsync(user, "SuperAdmin");
                }
                if (role is not null)
                {

                    await _userManager.AddToRoleAsync(user, role.Name);
                }
                string token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                var confirmLink = Url.ActionLink(controller: "Auth",
        action: "ConfirmEmail",
        host: Request.Host.Value,
        values: new { email = user.Email, token });
                var emailResult = await _EmailHelper.SendEmailAsync(userEmail: user.Email, confirmationLink: confirmLink, UserName: user.UserName);
                if (emailResult.IsSuccess)
                {
                    ViewBag.Roles = _roleManager.Roles.ToList();
                    if (currentCulture == "az")
                    {
                        ModelState.AddModelError("Error", "Emailinize Tesdiqleme Linki Gonderirldi!");
                        ModelState.AddModelError("Error", "Təsdiqləmə Linkinə Keçid Etdikdən Sonra Sizin Məlumatlariniza Baxilacaq Və Profiliniz Təsdiqlənəndən Sonra Daxil Ola Biləcəksiniz!");
                    }
                    else if (currentCulture == "ru")
                    {
                        ModelState.AddModelError("Error", "Ссылка для подтверждения отправлена на вашу электронную почту!");
                        ModelState.AddModelError("Error", "После перехода по ссылке подтверждения ваши данные будут проверены, и вы сможете войти после подтверждения профиля!");

                    }
                    else
                    {
                        ModelState.AddModelError("Error", "Confirmation link has been sent to your email!");
                        ModelState.AddModelError("Error", "After clicking the confirmation link, your information will be verified, and you will be able to log in after your profile is confirmed!");

                    }

                    return View();
                }


                await _userManager.DeleteAsync(user);
                if (currentCulture == "az")
                {
                    ViewBag.Roles = _roleManager.Roles.ToList();
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
                foreach (var error in result.Errors)
                {

                ModelState.AddModelError("Error", error.Description);
                }
                ViewBag.Roles = _roleManager.Roles.ToList();
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
            var currentCulture = Thread.CurrentThread.CurrentCulture.Name;
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
        public async Task<IActionResult> ConfirmEmail(string email, string token)
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
