using Aztu_Events.Business.Abstarct;
using Aztu_Events.Core.Helper.PageHelper;
using Aztu_Events.Core.Utilities.Results.Abstract;
using Aztu_Events.DataAccess.Abstarct;
using Aztu_Events.Entities.DTOs.Conferences;
using Aztu_Events.Entities.EnumClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aztu_Events.Business.Concrete
{
    public class ConfransManager : IConfransService
    {
        private readonly IConfrenceDal _confrenceDal;

        public ConfransManager(IConfrenceDal confrenceDal)
        {
            _confrenceDal = confrenceDal;
        }

        public async Task<IResult> ApproveConfrans(Guid id, ConferanceStatus status)
        {
            return await _confrenceDal.ApproveConfrans(id, status);
        }

        public Task<IResult> ApproveConfrans(Guid id, ConferanceStatus status, string ResponseMessage = null)
        {
           return _confrenceDal.ApproveConfrans(id, status, ResponseMessage);
        }

        public async Task<IDataResult<ConferenceGetAdminDTO>> ConferenceGetDetailForAdmin(Guid id, string lang)
        {
            return await _confrenceDal.ConferenceGetDetailForAdmin(id, lang);
        }

        public async Task<IDataResult<PaginatedList<ConferenceGetAdminListDTO>>> ConferenceGetListFilter(FilterConferenceDto filter, string lang)
        {
            return await _confrenceDal.ConferenceGetListFilter(filter, lang);
        }

        public async Task<IResult> ConfrenceAddAsync(ConferenceAddDTO dto)
        {
            return await _confrenceDal.ConfrenceAddAsync(dto);
        }

        public async Task<IResult> ConfrenceUpdateAsync(ConferenceUpdateDto dto)
        {
            return await _confrenceDal.ConfrenceUpdateAsync(dto);
        }

        public IDataResult<List<ConferenceGetAdminListDTO>> GetAllConferanceForAdmin(string LangCode)
        {
           return _confrenceDal.GetAllConferanceForAdmin(LangCode);

        }

        public IDataResult<List<GetALLConferenceUserDTO>> GetAllConferanceForUser(string UserId, string LangCode)
        {
        return _confrenceDal.GetAllConferanceForUser(UserId:UserId,LangCode: LangCode);
        }

        public IDataResult<GetConferenceUserDTO> GetConferanceDetailForUser(string UserId, string ConfranceId, string LangCode)
        {return _confrenceDal.GetConferanceDetailForUser( UserId: UserId,ConfranceId: ConfranceId,LangCode: LangCode);
        }
    }
}
