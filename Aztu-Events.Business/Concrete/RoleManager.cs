using Aztu_Events.Business.Abstarct;
using Aztu_Events.Core.Utilities.Results.Abstract;
using Aztu_Events.Core.Utilities.Results.Concrete.ErrorResults;
using Aztu_Events.Core.Utilities.Results.Concrete.SuccessResults;
using Aztu_Events.Entities.DTOs.RoleDTOs;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aztu_Events.Business.Concrete
{
    public class RoleManager:IRoleService
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public RoleManager(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task<IResult> AddRoleAsync(string RoleName)
        {
            try
            {
                if (RoleName is null)
                    return new ErrorResult("Rol Daxil  Edin");
                var checkedRoleName = _roleManager.Roles.FirstOrDefault(x => x.Name == RoleName);
                if (checkedRoleName != null) return new ErrorResult("Rol Yaradilib");
                var result = await _roleManager.CreateAsync(new IdentityRole
                {
                    Name = RoleName,
                });
                if (!result.Succeeded)
                    return new ErrorResult("Rol Yardila Bilmedi");
                return new SuccessResult();
            }
            catch (Exception ex)
            {

                return new ErrorResult(ex.Message);

            }
        }

        public async Task<IResult> DeletRoleAsync(string RoleId)
        {
            try
            {
                var checkedRole = await _roleManager.FindByIdAsync(RoleId);
                if (checkedRole is null)
                    return new ErrorResult("Rol Tapilmadi");
                var result = await _roleManager.DeleteAsync(checkedRole);
                if (!result.Succeeded) return new ErrorResult();
                return new SuccessResult("Rol Silindi");
            }
            catch (Exception ex)
            {

                return new ErrorResult(ex.Message);
            }
        }

        public IDataResult<List<GetRolesDTO>> GetRoles()
        {
            try
            {
                var result = _roleManager.Roles.Select(x => new GetRolesDTO
                {
                    id = x.Id,
                    RoleName = x.Name
                }).ToList();
                return new SuccessDataResult<List<GetRolesDTO>>(result);
            }
            catch (Exception ex)
            {

                return new ErrorDataResult<List<GetRolesDTO>>(ex.Message);
            }
        }
        public IDataResult<GetRolesDTO> GetRole(string Id)
        {
            try
            {
                if (string.IsNullOrEmpty(Id))
                    return new ErrorDataResult<GetRolesDTO>("Id Bos Ola Bilmez");
                var result = _roleManager.Roles.FirstOrDefault(x => x.Id == Id);
                if (result is null)
                    return new ErrorDataResult<GetRolesDTO>("Rol Tapilmadi");
                return new SuccessDataResult<GetRolesDTO>(new GetRolesDTO
                {
                    id = result.Id,
                    RoleName = result.Name
                });


            }
            catch (Exception ex)
            {

                return new ErrorDataResult<GetRolesDTO>(ex.Message);

            }
        }
    }
}
