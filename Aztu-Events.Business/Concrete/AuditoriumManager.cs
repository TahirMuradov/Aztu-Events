using Aztu_Events.Business.Abstarct;
using Aztu_Events.Core.Utilities.Results.Abstract;
using Aztu_Events.DataAccess.Abstarct;
using Aztu_Events.Entities.DTOs.AudutoriumDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aztu_Events.Business.Concrete
{
    public class AuditoriumManager : IAuditoriumService
    {
        private readonly IAudutoriumDAL _audutoriumDAL;

        public AuditoriumManager(IAudutoriumDAL audutoriumDAL)
        {
            _audutoriumDAL = audutoriumDAL;
        }

        public IResult AddAuditorium(AddAuditoriumDTO addAudutoriumDTO)
        {
            return _audutoriumDAL.AddAuditorium(addAudutoriumDTO);
        }

        public IResult DeleteAuditorium(string AuditoriumId)
        {
            return _audutoriumDAL.DeleteAuditorium(AuditoriumId);
        }

        public IDataResult<List<GetAuditoriumDTO>> GetAllAuditorium()
        {
           return _audutoriumDAL.GetAllAuditorium();
        }

        public IDataResult<GetAuditoriumDTO> GetAuditorium(string AuditoriumId)
        {
            return _audutoriumDAL.GetAuditorium(AuditoriumId);
        }

        public IResult UpdateAuditorium(UpdateAuditoriumDTO updateAuditoriumDTO)
        {
      return _audutoriumDAL.UpdateAuditorium(updateAuditoriumDTO);
        }
    }
}
