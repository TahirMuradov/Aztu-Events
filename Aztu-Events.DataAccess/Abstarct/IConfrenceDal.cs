using Aztu_Events.Core.Helper.PageHelper;
using Aztu_Events.Core.Utilities.Results.Abstract;
using Aztu_Events.Entities.Concrete;
using Aztu_Events.Entities.DTOs.Conferences;
using Aztu_Events.Entities.EnumClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aztu_Events.DataAccess.Abstarct
{
    public interface IConfrenceDal
    {
        Task<IResult> ConfrenceAddAsync(ConferenceAddDTO dto);
        Task<IResult> ConfrenceUpdateAsync(ConferenceUpdateDto dto);
        Task<IDataResult<PaginatedList<ConferenceGetAdminListDTO>>> ConferenceGetListFilter(FilterConferenceDto filter, string lang);
        Task<IDataResult<ConferenceGetAdminDTO>> ConferenceGetDetailForAdmin(Guid id, string lang);
        Task<IResult> ApproveConfrans(Guid id, ConferanceStatus status,string ResponseMessage=null);
       IDataResult<List<ConferenceGetAdminListDTO>> GetAllConferanceForAdmin(string LangCode);
        IDataResult<List<GetALLConferenceUserDTO>> GetAllConferanceForUser(string UserId, string LangCode);
        IDataResult<GetConferenceUserDTO> GetConferanceDetailForUser(string UserId,string ConfranceId, string LangCode);
        IDataResult<ConferenceUpdateDto> GetConferenceForUpdateUser(string UserId, string ConferenceId);

    }
}
