using Aztu_Events.Core.Utilities.Results.Abstract;
using Aztu_Events.Core.Utilities.Results.Concrete.ErrorResults;
using Aztu_Events.Core.Utilities.Results.Concrete.SuccessResults;
using Aztu_Events.DataAccess.Abstarct;
using Aztu_Events.DataAccess.Concrete.SQLServer;
using Aztu_Events.Entities.Concrete;
using Aztu_Events.Entities.DTOs.EventTypeDTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aztu_Events.DataAccess.Concrete
{
    public class EFEventTypeDAL : IEventTypeDAL
    {
        private readonly AppDbContext _appDbContext;

        public EFEventTypeDAL(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public IResult AddEventType(AddEventTypeDTO addEventTypeDTO)
        {
            try
            {
                EventType eventType = new ();
                _appDbContext.EventTypes.Add(eventType);
                for (int i = 0; i < addEventTypeDTO.LangCode.Count; i++)
                {
                    EventTypeLaunguage eventTypeLaunguage = new()
                    {

                        LangCode = addEventTypeDTO.LangCode[i],
                        TypeContent = addEventTypeDTO.Content[i],
                        EventTypeId = eventType.Id
                    };
                    _appDbContext.EventTypeLaunguages.Add(eventTypeLaunguage);
                }
                _appDbContext.SaveChanges();
                return new SuccessResult();
            }
            catch (Exception ex)
            {

                return new ErrorResult(message:ex.ToString());
            }
        }

        public IDataResult<List<GetEventTypeDTO>> GetAllEventType(string LangCode)
        {
            try
            {

                var datas=_appDbContext.EventTypes.Include(x=>x.EventTypeLaunguage).Select(x=>new GetEventTypeDTO
                {
                    EventTypeId=x.Id.ToString(),
                    Content=x.EventTypeLaunguage.FirstOrDefault(y=>y.LangCode==LangCode).TypeContent
                }).ToList();
                return new SuccessDataResult<List<GetEventTypeDTO>>(data:datas);
            }
            catch (Exception ex)
            {

                return new ErrorDataResult<List<GetEventTypeDTO>>(message: ex.Message);
            }
        }

        public IDataResult<GetEventTypeDTO> GetEventType(string EventTypeId, string LangCode)
        {
            try
            {

                var data = _appDbContext.EventTypes.Include(x => x.EventTypeLaunguage).Select(x => new GetEventTypeDTO
                {
                    EventTypeId = x.Id.ToString(),
                    Content = x.EventTypeLaunguage.FirstOrDefault(y => y.LangCode == LangCode).TypeContent
                }).FirstOrDefault(x=>x.EventTypeId==EventTypeId);
                return new SuccessDataResult<GetEventTypeDTO>(data:data);
            }
            catch (Exception ex)
            {

                return new ErrorDataResult<GetEventTypeDTO>(message: ex.Message);
            }
        }

        public IDataResult<GetEventTypeForUpdate> GetEventTypeForUpate(string id)
        {
            try
            {

                var data = _appDbContext.EventTypes.Include(x => x.EventTypeLaunguage).Select(x => new GetEventTypeForUpdate
                {
                    EventTypeId = x.Id.ToString(),
                    Content = x.EventTypeLaunguage.Select(y => y.TypeContent).ToList(),
                    LangCode=x.EventTypeLaunguage.Select(y=>y.LangCode).ToList(),
                }).FirstOrDefault(x => x.EventTypeId == id);
                if (data == null) return new ErrorDataResult<GetEventTypeForUpdate>(message:"EventType Is NotFound!");
                return new SuccessDataResult<GetEventTypeForUpdate>(data: data);
            }
            catch (Exception ex)
            {

                return new ErrorDataResult<GetEventTypeForUpdate>(message: ex.Message);
            }
        }

        public IResult RemoveEventType(string EventTypeId)
        {
            try
            {
                var data = _appDbContext.EventTypes.Include(x => x.EventTypeLaunguage).FirstOrDefault(x => x.Id.ToString() == EventTypeId);
                if (data is null) return new ErrorResult(message: "EventType is NotFound!");
                _appDbContext.EventTypeLaunguages.RemoveRange(data.EventTypeLaunguage);
                _appDbContext.EventTypes.Remove(data);
                _appDbContext.SaveChanges();
                return new SuccessResult();
            }
            catch (Exception ex)
            {

               return new ErrorResult(message: ex.Message);
            }
        }

        public IResult UpdateEventType(UpdateEventTypeDTO updateEventTypeDTO)
        {
            try
            {
                var data = _appDbContext.EventTypes.Include(x => x.EventTypeLaunguage).FirstOrDefault(x => x.Id.ToString() == updateEventTypeDTO.Id);
                if (data is null) return new ErrorResult(message: "EventType is Notfound!");
                for (int i = 0; i < updateEventTypeDTO.LangCode.Count; i++)
                {
                    var content = data.EventTypeLaunguage.FirstOrDefault(x => x.LangCode == updateEventTypeDTO.LangCode[i]);
                    if (content is null) continue;
                    content.TypeContent = updateEventTypeDTO.Content[i];
                    _appDbContext.EventTypeLaunguages.Update(content);
                }
                _appDbContext.SaveChanges();
                return new SuccessResult();
            }
            catch (Exception ex)
            {

                return new ErrorResult(message: ex.Message);
            }
        }
    }
}
