using Aztu_Events.Core.Helper.PageHelper;
using Aztu_Events.Core.Utilities.Results.Abstract;
using Aztu_Events.Entities.DTOs.Conferences;
using Aztu_Events.Entities.EnumClass;

namespace Aztu_Events.Business.Abstarct
{
    public interface IConfransService
    {
        public IResult AlertSeen(string CurrentUserId);
        IResult ConfrenceRemove(string id);
        Task<IResult> ConfrenceAddAsync(ConferenceAddDTO dto);
        Task<IResult> ConfrenceUpdateAsync(ConferenceUpdateDto dto);
        IDataResult<ConferenceGetDetailForUIDTO> GetConferenceDetailForUI(string ConferenceId, string LangCode);
        Task<IDataResult<PaginatedList<ConferenceGetAdminListDTO>>> ConferenceGetListFilterAsync(FilterConferenceDto filter, string lang);
        Task<IDataResult<ConferenceGetAdminDTO>> ConferenceGetDetailForAdminAsync(Guid id, string lang);
        Task<IResult> ApproveConfransAsync(Guid id, ConferanceStatus status, string ResponseMessage = null, bool IsFeatured=false);
        IDataResult<List<ConferenceGetAdminListDTO>> GetAllConferanceForAdmin(string LangCode);
        IDataResult<List<GetALLConferenceUserDTO>> GetAllConferanceForUser(string UserId, string LangCode);
        IDataResult<GetConferenceUserDTO> GetConferanceDetailForUser(string UserId, string ConfranceId, string LangCode);
        IDataResult<ConferenceUpdateDto> GetConferenceForUpdateUser(string UserId, string ConferenceId);

    }
}
