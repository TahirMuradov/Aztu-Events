using Aztu_Events.Core.Utilities.Results.Abstract;
using Aztu_Events.Entities.DTOs.EventTypeDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aztu_Events.Business.Abstarct
{
    public interface IEventTypeService
    {
        public IResult AddEventType(AddEventTypeDTO addEventTypeDTO);
        public IResult RemoveEventType(string EventTypeId);
        public IDataResult<GetEventTypeDTO> GetEventType(string EventTypeId, string LangCode);
        public IDataResult<List<GetEventTypeDTO>> GetAllEventType(string LangCode);
        public IResult UpdateEventType(UpdateEventTypeDTO updateEventTypeDTO);
        public IDataResult<GetEventTypeForUpdate> GetEventTypeForUpate(string id);
    }
}
