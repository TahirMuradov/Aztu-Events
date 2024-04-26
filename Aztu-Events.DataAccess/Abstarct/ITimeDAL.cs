using Aztu_Events.Core.Utilities.Results.Abstract;
using Aztu_Events.Entities.DTOs.TimeDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Aztu_Events.DataAccess.Abstarct
{
    public interface ITimeDAL
    {
        IResult AddTime(AddTimeDTO addTimeDTO);
        IResult DeleteTime(string TimeId);
        IDataResult<List<GetTimeDTO>> GetAllTime();
        IDataResult<GetTimeDTO> GetTime(string TimeId);
    }
}
