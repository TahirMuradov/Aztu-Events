using Aztu_Events.Business.Abstarct;
using Aztu_Events.Core.Utilities.Results.Abstract;
using Aztu_Events.DataAccess.Abstarct;
using Aztu_Events.Entities.DTOs.CategoryDTOs;
using Azure.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aztu_Events.Business.Concrete
{
    public class CategoryManager : ICategoryService
    {
        private readonly ICategoryDAL _categoryDAL;

        public CategoryManager(ICategoryDAL categoryDAL)
        {
            _categoryDAL = categoryDAL;
        }

        public IResult AddCategory(CategoryAddDTO categoryAddDTO)
        {
            return _categoryDAL.AddCategory(categoryAddDTO);
        }

        public IDataResult<CategoryUpdateDTO> CategoryGetForUpdate(string Id)
        {
            return _categoryDAL.CategoryGetForUpdate(Id);
        }

        public IDataResult<List<CategoryGetDTO>> GetAllCategory(string langCode)
        {
           return _categoryDAL.GetAllCategory(langCode);
        }

        public IDataResult<CategoryGetDTO> GetCategory(string id, string langCode)
        {
            return _categoryDAL.GetCategory(id, langCode);
        }

        public IResult RemoveCategory(string Id)
        {
          return _categoryDAL.RemoveCategory(Id);
        }

        public IResult UpdateCategory(CategoryUpdateDTO categoryUpdateDTO)
        {
           return _categoryDAL.UpdateCategory(categoryUpdateDTO);
        }
    }
}
