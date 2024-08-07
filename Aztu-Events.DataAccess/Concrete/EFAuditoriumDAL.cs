﻿using Aztu_Events.Core.Utilities.Results.Abstract;
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
        private readonly AppDbContext _context;

        public EFAuditoriumDAL(AppDbContext context)
        {
            _context = context;
        }

        public IResult AddAuditorium(AddAuditoriumDTO addAudutoriumDTO)
        {
            try
            {
             
                    Auditorium auditorium = new Auditorium()
                    {
                        AuditoryCapacity = addAudutoriumDTO.AuditoryCapacity.Value,
                        AudutoriyaNumber = addAudutoriumDTO.AudutoriyaNumber
                    };
                    _context.Audutoriums.Add(auditorium);
                    _context.SaveChanges();


                
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
               
                
                    var Auditorium = _context.Audutoriums.Include(x => x.Times).FirstOrDefault(x => x.Id.ToString() == AuditoriumId);
                    if (Auditorium is null) return new ErrorResult(message: "Data Is NotFound");
                    _context.Times.RemoveRange(Auditorium.Times);
                    _context.Audutoriums.Remove(Auditorium);
                    _context.SaveChanges();


                
                return new SuccessResult();
            }
            catch (Exception ex)
            {

                return new ErrorResult(message: ex.Message);
            }
        }

        public IDataResult<List<GetAuditoriumDTO>> GetAllAuditorium()
        {
            try
            {
              
                return new SuccessDataResult<List<GetAuditoriumDTO>>(data: _context.Audutoriums.Include(x => x.Times).Select(x => new GetAuditoriumDTO
                {
                    AuditoriumCapacity = x.AuditoryCapacity,
                    Date = x.Times.Where(y => y.AuditoriumId == x.Id).Select(z => z.Date).ToList(),
                    StartedTime = x.Times.Where(y => y.AuditoriumId == x.Id).Select(z => z.StartedTime).ToList(),
                    EndTime = x.Times.Where(y => y.AuditoriumId == x.Id).Select(z => z.EndTime).ToList(),
                    AuditoriumId = x.Id,
                    AudutoriyaNumber = x.AudutoriyaNumber
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
                
                var Auditorium = _context.Audutoriums.Include(x => x.Times).Select(x => new GetAuditoriumDTO
                {
                    AuditoriumCapacity = x.AuditoryCapacity,
                    Date = x.Times.Where(y => y.AuditoriumId == x.Id).Select(z => z.Date).ToList(),
                    StartedTime = x.Times.Where(y => y.AuditoriumId == x.Id).Select(z => z.StartedTime).ToList(),
                    EndTime = x.Times.Where(y => y.AuditoriumId == x.Id).Select(z => z.EndTime).ToList(),
                    AuditoriumId = x.Id,
                    AudutoriyaNumber = x.AudutoriyaNumber
                }).FirstOrDefault(x => x.AuditoriumId.ToString() == AuditoriumId);
                if (Auditorium is null) return new ErrorDataResult<GetAuditoriumDTO>(message: "Data is NotFound");

                return new SuccessDataResult<GetAuditoriumDTO>(data: Auditorium);

            }
            catch (Exception ex)
            {

                return new ErrorDataResult<GetAuditoriumDTO>(message: ex.Message);
            }
        }

        public IResult UpdateAuditorium(UpdateAuditoriumDTO updateAuditoriumDTO)
        {
            try
            {
              
                    var data = _context.Audutoriums.FirstOrDefault(x => x.Id == updateAuditoriumDTO.AuditoriumId);
                    if (data == null) return new ErrorResult(message: "Auditorium is NotFound");
                    data.AudutoriyaNumber = updateAuditoriumDTO.AudutoriyaNumber;
                    data.AuditoryCapacity = updateAuditoriumDTO.AuditoriumCapacity.Value;
                    _context.Audutoriums.Update(data);
                    _context.SaveChanges();

                
                return new SuccessResult();
            }
            catch (Exception ex)
            {
                return new ErrorResult(ex.Message);
            }
        }
    }
}
