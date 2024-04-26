using Aztu_Events.Core.Utilities.Results.Abstract;
using Aztu_Events.Core.Utilities.Results.Concrete.ErrorResults;
using Aztu_Events.Core.Utilities.Results.Concrete.SuccessResults;
using Aztu_Events.DataAccess.Abstarct;
using Aztu_Events.DataAccess.Concrete.SQLServer;
using Aztu_Events.Entities.Concrete;
using Aztu_Events.Entities.DTOs.TimeDTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aztu_Events.DataAccess.Concrete
{
    public class EFTimeDAL : ITimeDAL
    {
        public IResult AddTime(AddTimeDTO addTimeDTO)
        {
            try
            {
                using (var context=new AppDbContext())
                {
                    var Auditorium = context.Audutoria.Include(x=>x.AudutorimTimes).FirstOrDefault(x => x.Id.ToString() == addTimeDTO.AuditoriumId);
                    if (Auditorium == null) return new ErrorResult(message: "Data Is NotFound");
                    var checekdTime = Auditorium.AudutorimTimes.Where(x => x.Time.DateTime == addTimeDTO.Time);
                    if (checekdTime is not null) return new ErrorResult(message: "This Auditorium has this date");
                    Time time = new Time()
                    {
                        DateTime=addTimeDTO.Time
                    };
                    context.Times.Add(time);
                    context.SaveChanges();
                    AuditorimTime auditorimTime = new AuditorimTime()
                    {
                        AudutoriumId=Guid.Parse( addTimeDTO.AuditoriumId),
                        TimeId=time.Id
                    };
                    context.AudutorimTimes.Add(auditorimTime);
                    context.SaveChanges();
                }
                return new SuccessResult();
            }
            catch (Exception ex)
            {

                return new ErrorResult(message: ex.Message);
            }
        }

        public IResult DeleteTime(string TimeId)
        {
            try
            {
                using (var context=new AppDbContext())
                {
                    var data = context.Times.FirstOrDefault(x => x.Id.ToString() == TimeId);
                    if (data == null) return new ErrorResult(message:"Data Is NotFound");
                    var AudutorimTime = context.AudutorimTimes.Where(x => x.TimeId == data.Id);
                    context.AudutorimTimes.RemoveRange(AudutorimTime);
                    context.Times.Remove(data);
                    return new SuccessResult();
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public IDataResult<List<GetTimeDTO>> GetAllTime()
        {
            try
            {
                using var context = new AppDbContext();
                return new SuccessDataResult<List<GetTimeDTO>>(data:context.Times.Select(x=>new GetTimeDTO
                {
                    Time=x.DateTime,
                    TimeId=x.Id.ToString()
                }).ToList());
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<List<GetTimeDTO>>(message:ex.Message);
            }
         
        }

        public IDataResult<GetTimeDTO> GetTime(string TimeId)
        {
            try
            {
                using var context = new AppDbContext();
                var data = context.Times.Select(x => new GetTimeDTO
                {
                    Time = x.DateTime,
                    TimeId = x.Id.ToString()
                }).FirstOrDefault(x => x.TimeId == TimeId);
                if (data is null) return new ErrorDataResult<GetTimeDTO>(message:"Data is NotFound");

                return new SuccessDataResult<GetTimeDTO>(data:data );
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<GetTimeDTO>(message: ex.Message);
            }
        }
    }
}
