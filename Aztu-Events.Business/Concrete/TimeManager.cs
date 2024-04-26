using Aztu_Events.Business.Abstarct;
using Aztu_Events.Core.Utilities.Results.Abstract;
using Aztu_Events.DataAccess.Abstarct;
using Aztu_Events.Entities.DTOs.TimeDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aztu_Events.Business.Concrete
{
    public class TimeManager : ITimeService
    {
        private readonly ITimeDAL _timeDAL;

        public TimeManager(ITimeDAL timeDAL)
        {
            _timeDAL = timeDAL;
        }

        public IResult AddTime(AddTimeDTO addTimeDTO)
        {
           return _timeDAL.AddTime(addTimeDTO);
        }

        public IResult DeleteTime(string TimeId)
        {
            return _timeDAL.DeleteTime(TimeId);
        }

        public IDataResult<List<GetTimeDTO>> GetAllTime()
        {
           return _timeDAL.GetAllTime();
        }

        public IDataResult<GetTimeDTO> GetTime(string TimeId)
        {
            return _timeDAL.GetTime(TimeId);
        }
    }
}
