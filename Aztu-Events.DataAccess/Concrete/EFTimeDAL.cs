using Aztu_Events.Core.Utilities.Results.Abstract;
using Aztu_Events.Core.Utilities.Results.Concrete.ErrorResults;
using Aztu_Events.Core.Utilities.Results.Concrete.SuccessResults;
using Aztu_Events.DataAccess.Abstarct;
using Aztu_Events.DataAccess.Concrete.SQLServer;
using Aztu_Events.Entities.DTOs.TimeDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aztu_Events.DataAccess.Concrete
{
    public class EFTimeDAL : ITimeDAL
    {

        public IDataResult<List<GetTimeDTO>> GetAllTime()
        {
            try
            {
                using var context = new AppDbContext();
                return new SuccessDataResult<List<GetTimeDTO>>(data:

                    context.Times.Select(x => new GetTimeDTO
                    {
                        AuditoriumId = x.AuditoriumId,
                        Date = x.Date,
                        EndTime = x.EndTime,
                        StartedTime = x.StartedTime,
                        TimeId = x.Id.ToString()
                    }).ToList()

                    );

            }
            catch (Exception ex)
            {

                return new ErrorDataResult<List<GetTimeDTO>>(message: ex.Message);
            }
        }
    }
}
