using Aztu_Events.Core.Utilities.Results.Abstract;
using Aztu_Events.Core.Utilities.Results.Concrete.ErrorResults;
using Aztu_Events.Core.Utilities.Results.Concrete.SuccessResults;
using Aztu_Events.DataAccess.Abstarct;
using Aztu_Events.DataAccess.Concrete.SQLServer;
using Aztu_Events.Entities.Concrete;
using Aztu_Events.Entities.DTOs.AudutoriumDTOs;
using Microsoft.EntityFrameworkCore;

namespace Aztu_Events.DataAccess.Concrete
{
    public class EFAuditoriumDAL : IAudutoriumDAL
    {
        public IResult AddAuditorium(AddAuditoriumDTO addAudutoriumDTO)
        {
            try
            {
                using (var context=new AppDbContext())
                {
                    Auditorium auditorium = new Auditorium()
                    {
                        AuditoryCapacity=addAudutoriumDTO.AuditoryCapacity,
                        AudutoriyaNumber=addAudutoriumDTO.AudutoriyaNumber
                    };
                    context.Audutoria.Add(auditorium);
                    context.SaveChanges();


                }
                return new SuccessResult();
            }
            catch (Exception ex)
            {

                return new ErrorResult(message: ex.Message);
            }
        }

        public IResult DeleteAuditorium(string AuditoriumId)
        {
            try
            {
                using (var context=new AppDbContext())
                {
                    var Auditorium = context.Audutoria.Include(x=>x.AudutorimTimes).FirstOrDefault(x => x.Id.ToString() == AuditoriumId);
                    if (Auditorium is null) return new ErrorResult(message: "Data Is NotFound");
                    context.AudutorimTimes.RemoveRange(Auditorium.AudutorimTimes);
                    context.Audutoria.Remove(Auditorium);
                    context.SaveChanges();
                  

                }
                return new SuccessResult();
            }
            catch (Exception ex)
            {

                return new ErrorResult(message:ex.Message);
            }
        }

        public IDataResult<List<GetAuditoriumDTO>> GetAllAuditorium()
        {
            try
            {
                using var context=new AppDbContext();
                return new SuccessDataResult<List<GetAuditoriumDTO>>(data:context.Audutoria.Include(x=>x.AudutorimTimes).Select(x=>new GetAuditoriumDTO
                {
                    AuditoriumCapacity=x.AuditoryCapacity,
                    AuditoriumFreeTimes=x.AudutorimTimes.Select(y=>y.Time.DateTime).ToList(),
                    AuditoriumId=x.Id,
                    AudutoriumNumber=x.AudutoriyaNumber
                }).ToList());

            }
            catch (Exception ex)
            {

                return new ErrorDataResult<List<GetAuditoriumDTO>>(message: ex.Message);
            }
        }

        public IDataResult<GetAuditoriumDTO> GetAuditorium(string AuditoriumId)
        {
            try
            {
                using var context = new AppDbContext();
                var Auditorium = context.Audutoria.Include(x=>x.AudutorimTimes).Select(x=>new GetAuditoriumDTO
                {
                    AuditoriumCapacity = x.AuditoryCapacity,
                    AuditoriumFreeTimes=x.AudutorimTimes.Select(y=>y.Time.DateTime).ToList(),
                    AuditoriumId = x.Id,
                    AudutoriumNumber=x.AudutoriyaNumber
                }).FirstOrDefault(x => x.AuditoriumId.ToString() == AuditoriumId);
                if (Auditorium is null) return new ErrorDataResult<GetAuditoriumDTO>(message:"Data is NotFound");

                return new SuccessDataResult<GetAuditoriumDTO>(data: Auditorium);

            }
            catch (Exception ex)
            {

                return new ErrorDataResult<GetAuditoriumDTO>(message: ex.Message);
            }
        }
    }
}
