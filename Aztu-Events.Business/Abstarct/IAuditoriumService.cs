using Aztu_Events.Core.Utilities.Results.Abstract;
using Aztu_Events.Entities.DTOs.AudutoriumDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aztu_Events.Business.Abstarct
{
    public interface IAuditoriumService
    {
        IResult AddAuditorium(AddAuditoriumDTO addAudutoriumDTO);
        IResult DeleteAuditorium(string AuditoriumId);
        IDataResult<List<GetAuditoriumDTO>> GetAllAuditorium();
        IDataResult<GetAuditoriumDTO> GetAuditorium(string AuditoriumId);
    }
}
