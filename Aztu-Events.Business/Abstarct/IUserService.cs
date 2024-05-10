using Aztu_Events.Core.Utilities.Results.Abstract;
using Aztu_Events.Entities.DTOs.UserDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aztu_Events.Business.Abstarct
{
    public interface IUserService
    {
        public Task<IResult> UpdateUserAsync (UserUpdateDTO userUpdateDTO);
        public Task< IDataResult<GetUsersDTO>> GetUserAsync(string UserId, string LangCode);
        public Task< IDataResult<List<GetUsersDTO>>> GetAllUserAsync( string LangCode);
        public Task< IResult> DeleteUserAsync(string UserId);
        public Task< IResult> AssignRoleToUserAsnyc(string RoleId, string UserId);
        public Task<IResult> UserDeleteRoleAsync(string UserId, List<string> RoleNames);

    }
}
