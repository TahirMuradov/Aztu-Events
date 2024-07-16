using Aztu_Events.Core.Utilities.Results.Abstract;
using Aztu_Events.Entities.DTOs.AudutoriumDTOs;

namespace Aztu_Events.DataAccess.Abstarct
{
    public interface IAudutoriumDAL
    {
        IResult AddAuditorium(AddAuditoriumDTO addAudutoriumDTO);
        IResult DeleteAuditorium(string AuditoriumId);
        IDataResult<List<GetAuditoriumDTO>> GetAllAuditorium();
        IDataResult<GetAuditoriumDTO> GetAuditorium(string AuditoriumId);
        IResult UpdateAuditorium(UpdateAuditoriumDTO updateAuditoriumDTO);
    }
}
