using Aztu_Events.Core.Utilities.Results.Abstract;
using Aztu_Events.Entities.DTOs.CategoryDTOs;

namespace Aztu_Events.DataAccess.Abstarct
{
    public interface ICategoryDAL
    {
        public IResult AddCategory(CategoryAddDTO categoryAddDTO);
        public IResult RemoveCategory(string Id);
        public IDataResult<CategoryUpdateDTO> CategoryGetForUpdate(string Id);
        public IDataResult<CategoryGetDTO> GetCategory(string id,string langCode);
        public IDataResult<List< CategoryGetDTO>> GetAllCategory( string langCode);
        public IResult UpdateCategory(CategoryUpdateDTO categoryUpdateDTO);


    }
}
