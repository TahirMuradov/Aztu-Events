﻿using Aztu_Events.Business.Abstarct;
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

        public async Task<IResult> ApproveConfransAsync(Guid id, ConferanceStatus status)
        {
            return await _confrenceDal.ApproveConfransAsync(id, status);
        }

        public async Task<IResult> ApproveConfransAsync(Guid id, ConferanceStatus status, string ResponseMessage = null)
        {
           return await _confrenceDal.ApproveConfransAsync(id, status, ResponseMessage);
        }

        public async Task<IDataResult<ConferenceGetAdminDTO>> ConferenceGetDetailForAdminAsync(Guid id, string lang)
        {
            return await _confrenceDal.ConferenceGetDetailForAdminAsync(id, lang);
        }

        public async Task<IDataResult<PaginatedList<ConferenceGetAdminListDTO>>> ConferenceGetListFilterAsync(FilterConferenceDto filter, string lang)
        {
            return await _confrenceDal.ConferenceGetListFilterAsync(filter, lang);
        }

        public async Task<IResult> ConfrenceAddAsync(ConferenceAddDTO dto)
        {
            return await _confrenceDal.ConfrenceAddAsync(dto);
        }

        public IResult ConfrenceRemove(string id)
        {
            return  _confrenceDal.ConfrenceRemove(id);
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

        public IDataResult<ConferenceUpdateDto> GetConferenceForUpdateUser(string UserId, string ConferenceId)
        {
           return _confrenceDal.GetConferenceForUpdateUser(UserId:UserId,ConferenceId:ConferenceId);
        }
    }
}