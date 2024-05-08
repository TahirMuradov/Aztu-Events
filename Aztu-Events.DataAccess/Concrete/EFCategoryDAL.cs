using Aztu_Events.Core.Utilities.Results.Abstract;
using Aztu_Events.Core.Utilities.Results.Concrete.ErrorResults;
using Aztu_Events.Core.Utilities.Results.Concrete.SuccessResults;
using Aztu_Events.DataAccess.Abstarct;
using Aztu_Events.DataAccess.Concrete.SQLServer;
using Aztu_Events.Entities.Concrete;
using Aztu_Events.Entities.DTOs.CategoryDTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aztu_Events.DataAccess.Concrete
{
    public class EFCategoryDAL : ICategoryDAL
    {
        private readonly AppDbContext _context;

        public EFCategoryDAL(AppDbContext context)
        {
            _context = context;
        }

        public IResult AddCategory(CategoryAddDTO categoryAddDTO)
        {
            try
            {
                Category category = new Category();
                _context.Categories.Add(category);
                for (int i = 0; i < categoryAddDTO.LangCode.Count; i++)
                {
                    CategoryLaunguage categoryLaunguage =new CategoryLaunguage()
                    {
                        LangCode = categoryAddDTO.LangCode[i],
                        CategoryName = categoryAddDTO.Content[i],
                        CategoryId=category.Id
                        
                    };
                    _context.CategoryLaunguages.Add(categoryLaunguage);
                }
                _context.SaveChanges();
                return new SuccessResult();
            }
            catch (Exception ex)
            {

                return new ErrorResult(message: ex.Message);
            }
        }

        public IDataResult<CategoryUpdateDTO> CategoryGetForUpdate(string Id)
        {
            try
            {
                var data = _context.Categories.Include(x => x.CategoryLaunguages).FirstOrDefault(x => x.Id.ToString() == Id);
                if (data == null) return new ErrorDataResult<CategoryUpdateDTO>(message:"Data is NotFound!");
                return new SuccessDataResult<CategoryUpdateDTO>(data:new CategoryUpdateDTO()
                {
                    CategoryId = Id,
                    Content=data.CategoryLaunguages.Select(x=>x.CategoryName).ToList(),
                    LangCode=data.CategoryLaunguages.Select(x=>x.LangCode).ToList()
                    
                });


            }
            catch (Exception ex)
            {

               return new ErrorDataResult<CategoryUpdateDTO>(message:ex.Message);
            }
        }

        public IDataResult<List<CategoryGetDTO>> GetAllCategory(string langCode)
        {
            try
            {var catigories=_context.Categories.Include(x=>x.CategoryLaunguages).Select(x=>new CategoryGetDTO
            {
                Id=x.Id.ToString(),
                Name=x.CategoryLaunguages.FirstOrDefault(y=>y.LangCode==langCode).CategoryName
            }).ToList();
                return new SuccessDataResult<List<CategoryGetDTO>>(data:catigories);
            }
            catch (Exception ex)
            {

                return new ErrorDataResult<List<CategoryGetDTO>>(message: ex.Message);
            }
        }

        public IDataResult<CategoryGetDTO> GetCategory(string id, string langCode)
        {
            try
            {
                var catigories = _context.Categories.Include(x => x.CategoryLaunguages).Select(x => new CategoryGetDTO
                {
                    Id = x.Id.ToString(),
                    Name = x.CategoryLaunguages.FirstOrDefault(y => y.LangCode == langCode).CategoryName
                }).FirstOrDefault(x=>x.Id==id);
                if (catigories == null) return new SuccessDataResult<CategoryGetDTO>(message: "Data is NotFound!");
                return new SuccessDataResult<CategoryGetDTO>(data: catigories);
            }
            catch (Exception ex)
            {

                return new ErrorDataResult<CategoryGetDTO>(message: ex.Message);
            }
        }

        public IResult RemoveCategory(string Id)
        {
            try
            {
                var data = _context.Categories.Include(x => x.CategoryLaunguages).FirstOrDefault(x => x.Id.ToString() == Id);
                if (data == null) return new ErrorResult(message: "Data Is NotFound");
                _context.CategoryLaunguages.RemoveRange(data.CategoryLaunguages);
                _context.Categories.Remove(data);
                _context.SaveChanges();
                return new SuccessResult();
            }
            catch (Exception ex)
            {

                return new ErrorResult(message: ex.Message);
            }
        }

        public IResult UpdateCategory(CategoryUpdateDTO categoryUpdateDTO)
        {
            try
            {
                var category=_context.Categories.Include(x=>x.CategoryLaunguages).FirstOrDefault(x=>x.Id.ToString()==categoryUpdateDTO.CategoryId);
                if (category == null) return new ErrorResult(message: "Data Is NotFound");
                for (int i = 0; i < categoryUpdateDTO.LangCode.Count; i++)
                {
                    var categoryLaunguage = category.CategoryLaunguages.FirstOrDefault(x => x.LangCode == categoryUpdateDTO.LangCode[i]);
                    if (categoryLaunguage == null) continue;
                    categoryLaunguage.CategoryName = categoryUpdateDTO.Content[i];
                    _context.CategoryLaunguages.Update(categoryLaunguage);
                }
                _context.SaveChanges();
                return new SuccessResult();
            }
            catch (Exception ex)
            {

                return new ErrorResult(message: ex.Message);
            }
        }
    }
}
