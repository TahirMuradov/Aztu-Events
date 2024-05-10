using Aztu_Events.Business.Abstarct;
using Aztu_Events.Core.Utilities.Results.Abstract;
using Aztu_Events.Core.Utilities.Results.Concrete;
using Aztu_Events.Core.Utilities.Results.Concrete.ErrorResults;
using Aztu_Events.Core.Utilities.Results.Concrete.SuccessResults;
using Aztu_Events.Entities.Concrete;
using Aztu_Events.Entities.DTOs.UserDTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Aztu_Events.Business.Concrete
{
    public class UserManager : IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserManager(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<IResult> AssignRoleToUserAsnyc(string RoleId, string UserId)
        {
            try
            {
                User user = await _userManager.FindByIdAsync(UserId);
                if (user == null) return new ErrorResult(message: "User is NotFound");
                IdentityRole role = await _roleManager.FindByIdAsync(RoleId);
                if (role == null) return new ErrorResult(message: "Rol is NotFound!");
                IdentityResult result = await _userManager.AddToRoleAsync(user, role.Name);
                if (result.Succeeded) return new SuccessResult();
                return new ErrorResult(message:result.Succeeded.ToString());
                
            }
            catch (Exception ex)
            {
                return new ErrorResult(message:ex.Message);
            }
   
        }

        public async Task< IResult> DeleteUserAsync(string UserId)
        {
            try
            {
                User user = await _userManager.FindByIdAsync(UserId);
                if (user == null) return new ErrorResult(message: "User is NotFound");
              IdentityResult result= await _userManager.DeleteAsync(user);
                if (result.Succeeded)
                return new SuccessResult();
                return new ErrorResult(message:result.Errors.ToString());


            }
            catch (Exception ex)
            {

                return new ErrorResult(message:ex.Message);

            }
        }

        public async Task< IDataResult<List<GetUsersDTO>>> GetAllUserAsync(string LangCode)
        {
            try
            {
                var User = _userManager.Users.Include(x => x.Confrans).ThenInclude(x => x.ConfranceLaunguages).ToList();
                var usersDTO = new List<GetUsersDTO>();

                foreach (var user in User)
                {
                    var roles = await _userManager.GetRolesAsync(user);

                    usersDTO.Add(new GetUsersDTO
                    {
                        UserName = user.UserName,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        Email = user.Email,
                        PhoneNumber = user.PhoneNumber,
                        Id = user.Id,
                        Conferances = user.Confrans.Where(x => x.Status == Entities.EnumClass.ConferanceStatus.Təsdiq).Select(y => y.ConfranceLaunguages.FirstOrDefault(z => z.LangCode == LangCode).ConfransName).ToList(),
                        Roles = roles.ToList()
                    });
                }

                return new SuccessDataResult<List<GetUsersDTO>>(data: usersDTO);
            }
            catch (Exception ex)
            {

                return new ErrorDataResult<List<GetUsersDTO>>(message: ex.Message.ToString());
            }
        }

        public async Task< IDataResult<GetUsersDTO>> GetUserAsync(string UserId, string LangCode)
        {
            try
            {
                
                var user = await _userManager.FindByIdAsync(UserId);

                if (user == null)                                 
                    return new ErrorDataResult<GetUsersDTO>("User not found");
                

                // Kullanıcının rollerini al
                var roles = await _userManager.GetRolesAsync(user);

              
                var conferances = user.Confrans?.Where(x=>x.Status==Entities.EnumClass.ConferanceStatus.Təsdiq)
                    .Select(y => y.ConfranceLaunguages?.FirstOrDefault(z => z.LangCode == LangCode)?.ConfransName)
                    .ToList();

                // Kullanıcı bilgilerini DTO'ya dönüştür
                var userDTO = new GetUsersDTO
                {
                    UserName = user.UserName,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber,
                    Id = user.Id,
                    Conferances = conferances,
                    Roles = roles.ToList()
                };

               
                return new SuccessDataResult<GetUsersDTO>(userDTO);
            }
            catch (Exception ex)
            {
                
                return new ErrorDataResult<GetUsersDTO>(message: ex.Message);
            }
        }

        public async Task<IResult> UpdateUserAsync(UserUpdateDTO userUpdateDTO)
        {
            try
            {
                var User = await _userManager.FindByIdAsync(userUpdateDTO.UserId);
                if (User == null) return new ErrorResult(message:"User Is Not Found!");
                var UserNameChecked=await _userManager.FindByNameAsync(userUpdateDTO.UserName);

                if (UserNameChecked != null &&User.Id!=UserNameChecked.Id) return new ErrorResult(message: "This is UserName Not Empty!");
                var result = await _userManager.CheckPasswordAsync(User, userUpdateDTO.Password);
                    if (!result)
                    {
                        return new ErrorResult(message: "password is not correct");
                    }
                   
                
                User.PhoneNumber = userUpdateDTO.PhoneNumber != null ? userUpdateDTO.PhoneNumber : User.PhoneNumber;
                User.FirstName = userUpdateDTO.Firstname != null ? userUpdateDTO.Firstname : User.FirstName;
                User.UserName = userUpdateDTO.UserName ?? User.UserName;
                User.LastName = userUpdateDTO.Lastname != null ? userUpdateDTO.Lastname : User.LastName;
                if (userUpdateDTO.NewPassword is not null)
                {

                    var changePassword = await _userManager.ChangePasswordAsync(User, userUpdateDTO.Password, userUpdateDTO.NewPassword);
                }
                await _userManager.UpdateAsync(User);
                return new SuccessResult();
            }
            catch (Exception ex)
            {

                return new ErrorResult(message: ex.Message);
            }
        }

        public async Task<IResult> UserDeleteRoleAsync(string UserId, List<string> RoleName)
        {
            try
            {

                var user = await _userManager.FindByIdAsync(UserId);
                if (user is null) return new ErrorResult();
                IdentityResult Result = await _userManager.RemoveFromRolesAsync(user, RoleName);
                if (Result.Succeeded)
                {
                    return new SuccessResult();
                }
                return new ErrorResult();
            }
            catch (Exception ex)
            {
                return new ErrorResult(ex.Message);


            }
        }
    }
}
