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

        public async Task<IDataResult<ConferenceGetAdminDTO>> ConferenceGetAdmin(Guid id, string lang)
        {
            return await _confrenceDal.ConferenceGetAdmin(id, lang);
        }

        public async Task<IDataResult<PaginatedList<ConferenceGetAdminListDTO>>> ConferenceGetAdminList(FilterConferenceDto filter, string lang)
        {
            return await _confrenceDal.ConferenceGetAdminList(filter, lang);
        }

        public async Task<IResult> ConfrenceAddAsync(ConferenceCreateDto dto)
        {
            return await _confrenceDal.ConfrenceAddAsync(dto);
        }

        public async Task<IResult> ConfrenceUpdateAsync(ConferenceUpdateDto dto)
        {
            return await _confrenceDal.ConfrenceUpdateAsync(dto);
        }
    }
}
