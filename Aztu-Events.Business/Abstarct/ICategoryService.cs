using Aztu_Events.Core.Utilities.Results.Abstract;
using Aztu_Events.Entities.DTOs.CategoryDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aztu_Events.Business.Abstarct
{
    public interface ICategoryService
    {
        public IResult AddCategory(CategoryAddDTO categoryAddDTO);
        public IDataResult<CategoryUpdateDTO> CategoryGetForUpdate(string Id);

        public IResult RemoveCategory(string Id);
        public IDataResult<CategoryGetDTO> GetCategory(string id, string langCode);
        public IDataResult<List<CategoryGetDTO>> GetAllCategory(string langCode);
        public IResult UpdateCategory(CategoryUpdateDTO categoryUpdateDTO);
    }
}
