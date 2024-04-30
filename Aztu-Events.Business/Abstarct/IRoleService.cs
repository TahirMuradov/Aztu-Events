using Aztu_Events.Core.Utilities.Results.Abstract;
using Aztu_Events.Entities.DTOs.RoleDTOs;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aztu_Events.Business.Abstarct
{
    public interface IRoleService
    {
        public Task<IResult> AddRoleAsync(string RoleName);
        public Task<IResult> DeletRoleAsync(string RoleId);
        public IDataResult<GetRolesDTO> GetRole(string Id);
        public IDataResult<List<GetRolesDTO>> GetRoles();

    }
}
