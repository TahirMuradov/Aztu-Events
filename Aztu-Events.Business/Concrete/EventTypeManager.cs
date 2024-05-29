using Aztu_Events.Business.Abstarct;
using Aztu_Events.Core.Utilities.Results.Abstract;
using Aztu_Events.DataAccess.Abstarct;
using Aztu_Events.Entities.DTOs.EventTypeDTOs;

namespace Aztu_Events.Business.Concrete
{
    public class EventTypeManager : IEventTypeService
    {
        private readonly IEventTypeDAL _eventTypeDAL;

        public EventTypeManager(IEventTypeDAL eventTypeDAL)
        {
            _eventTypeDAL = eventTypeDAL;
        }

        public IResult AddEventType(AddEventTypeDTO addEventTypeDTO)
        {
           return _eventTypeDAL.AddEventType(addEventTypeDTO);
        }

        public IDataResult<List<GetEventTypeDTO>> GetAllEventType(string LangCode)
        {
           return _eventTypeDAL.GetAllEventType(LangCode);
        }

        public IDataResult<GetEventTypeDTO> GetEventType(string EventTypeId, string LangCode)
        {
            return GetEventType(EventTypeId, LangCode);
        }

        public IDataResult<GetEventTypeForUpdate> GetEventTypeForUpate(string id)
        {
            return _eventTypeDAL.GetEventTypeForUpate(id);
        }

        public IResult RemoveEventType(string EventTypeId)
        {
            return _eventTypeDAL.RemoveEventType(EventTypeId);
        }

        public IResult UpdateEventType(UpdateEventTypeDTO updateEventTypeDTO)
        {
            return _eventTypeDAL.UpdateEventType(updateEventTypeDTO);
        }
    }
}
