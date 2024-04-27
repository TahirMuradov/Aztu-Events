using Aztu_Events.Core.Helper.PageHelper;
using Aztu_Events.Core.Utilities.Results.Abstract;
using Aztu_Events.Entities.DTOs.Conferences;
using Aztu_Events.Entities.EnumClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aztu_Events.Business.Abstarct
{
    public interface IConfransService
    {
        Task<IResult> ConfrenceAddAsync(ConferenceCreateDto dto);
        Task<IResult> ConfrenceUpdateAsync(ConferenceUpdateDto dto);
        Task<IDataResult<PaginatedList<ConferenceGetAdminListDTO>>> ConferenceGetAdminList(FilterConferenceDto filter, string lang);
        Task<IDataResult<ConferenceGetAdminDTO>> ConferenceGetAdmin(Guid id, string lang);
        Task<IResult> ApproveConfrans(Guid id, ConferanceStatus status);
    }
}
