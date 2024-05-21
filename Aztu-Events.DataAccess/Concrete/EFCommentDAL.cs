using Aztu_Events.Core.Utilities.Results.Abstract;
using Aztu_Events.Core.Utilities.Results.Concrete.ErrorResults;
using Aztu_Events.Core.Utilities.Results.Concrete.SuccessResults;
using Aztu_Events.DataAccess.Abstarct;
using Aztu_Events.DataAccess.Concrete.SQLServer;
using Aztu_Events.Entities.Concrete;
using Aztu_Events.Entities.DTOs.CommentDTOs;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Crypto.Prng;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aztu_Events.DataAccess.Concrete
{
   
    public class EFCommentDAL : ICommentDAL
    {
        private readonly AppDbContext _Context;

        public EFCommentDAL(AppDbContext context)
        {
            _Context = context;
        }
     
        public IResult AddComment(AddCommentDTO addCommentDTO)
        {
            try
            {
                var Userchecked = _Context.Users.FirstOrDefault(x => x.Id == addCommentDTO.UserId);
                if (Userchecked is null) return new ErrorResult("User is NotFound!");
                var ConferenceChecked = _Context.Confrans.FirstOrDefault(x => x.Id.ToString() == addCommentDTO.ConferenceId);
                if (ConferenceChecked is null) return new ErrorResult("Conference is NotFound!");
                _Context.Comments.Add(new Comment
                {
                    UserId = Userchecked.Id,
                    ConfransId = ConferenceChecked.Id,
                    Content = addCommentDTO.Content,
                    CreatedDate = DateTime.Now,
                   
                

                });
                _Context.SaveChanges();

                return new SuccessResult();

            }
            catch (Exception ex)
            {

                return new ErrorResult(message: ex.Message);
            }
        }

        public IResult AlertSeen()
        {
            try
            {
                var comments = _Context.Comments.Where(x => !x.AlertSeen);
                if (comments is null)
                    return new SuccessResult();
                foreach (var comment in comments)
                {
                    comment.AlertSeen= true;
                _Context.Comments.Update(comment);
                }
                _Context.SaveChanges();
                return new SuccessResult();
            }
            catch (Exception ex)
            {

                return new ErrorResult(message: ex.Message);
            }
        }

        public IResult ApporiveComment(string Id)
        {
            try
            {
                var comment = _Context.Comments.FirstOrDefault(x => x.Id.ToString() == Id);

                if (comment == null) return new ErrorResult(message: "Comment Is NotFound!");
                switch (comment.IsSafe)
                {
                    case true:
                        comment.IsSafe = false;
                        break;
                    case false:
                        comment.IsSafe = true;
                        break;

                }
                comment.AlertSeen = false;
              _Context.Comments.Update(comment);
                _Context.SaveChanges();
                return new SuccessResult();
            }
            catch (Exception ex)
            {

                return new ErrorResult(message: ex.Message);
            }
        }

        public IResult DeleteComment(string Id)
        {
            try
            {
              
                var CommentChecked = _Context.Comments.FirstOrDefault(x => x.Id.ToString() == Id);
                if (CommentChecked is null) return new ErrorResult("Comment is NotFound!");
                _Context.Comments.Remove(CommentChecked);
                _Context.SaveChanges();
                return new SuccessResult();
            }
            catch (Exception ex)
            {
                return new ErrorResult(ex.Message);
            }
        }

        public IDataResult<IQueryable<GetCommentForAdminDTO>> GetAllCommentsForAmin(string langCode)
        {
            try
            {
                var data=_Context.Comments.AsNoTracking().AsSplitQuery().AsQueryable();
                return new SuccessDataResult<IQueryable<GetCommentForAdminDTO>>(data: data.Select(x => new GetCommentForAdminDTO
                {
                    CommentId = x.Id.ToString(),
                    ConferenceId = x.ConfransId.ToString(),
                    UserId = x.UserId,
                    IsSafe = x.IsSafe,
                    UserFullName = x.User.FirstName + " " + x.User.LastName,
                    ConferenceName = x.Confrans.ConfranceLaunguages.FirstOrDefault(y => y.LangCode == langCode).ConfransName,
                    Content = x.Content,
                    CreatedDate = x.CreatedDate,
                    UpdateDate = x.UpdateDate,
                    AlertSeen=x.AlertSeen


                }));
            }
            catch (Exception ex)
            {

               return new ErrorDataResult<IQueryable<GetCommentForAdminDTO>>(message: ex.Message);
            }
        }

        public IDataResult<List<GetCommentForAdminDTO>> GetCommentsForAmin(string ConfransId,string langCode)
        {
            try
            {
                var ChecekedConfrans = _Context.Confrans.Include(x=>x.ConfranceLaunguages).Include(x => x.Comments).ThenInclude(x => x.User).FirstOrDefault(x => x.Id.ToString() == ConfransId);
                if (ChecekedConfrans is null) return new ErrorDataResult<List<GetCommentForAdminDTO>>(message: "Confrans is NotFound!");
                return new SuccessDataResult<List<GetCommentForAdminDTO>>(data: ChecekedConfrans.Comments.Select(x => new GetCommentForAdminDTO()
                {
                    CommentId = x.Id.ToString(),
                    Content = x.Content,
                    CreatedDate
                    = x.CreatedDate,
                    IsSafe = x.IsSafe,
                    UpdateDate = x.UpdateDate,
                    UserFullName = x.User.FirstName + " " + x.User.LastName,
                    UserId = x.UserId,
                    ConferenceId = x.ConfransId.ToString(),
                    ConferenceName=x.Confrans.ConfranceLaunguages.FirstOrDefault(x=>x.LangCode==langCode).ConfransName

                }).ToList());
            }
            catch (Exception ex)
            {

              return   new ErrorDataResult<List<GetCommentForAdminDTO>>(message:ex.Message);
            }
          
           
        }

        public IDataResult<List<GetCommentForUIDTO>> GetCommentsForUI(string ConfransId)
        {
            try
            {
                var CheckedConfransId = _Context.Confrans.Include(x => x.Comments).ThenInclude(x=>x.User).FirstOrDefault(x => x.Id.ToString() == ConfransId);
                if (CheckedConfransId == null) return new ErrorDataResult<List< GetCommentForUIDTO>>(message:"Confrans NotFound!");
                return new SuccessDataResult<List<GetCommentForUIDTO>>(data:CheckedConfransId.Comments.Select(x=>new GetCommentForUIDTO
                {
                    CommentId=x.Id.ToString(),
                    Content=x.Content,
                    CreatedDate=x.CreatedDate,  
                    UpdateDate=x.UpdateDate,
                    UserFullName=x.User.FirstName+" "+x.User.LastName,
                    UserId = x.UserId,
                    
                }).ToList());
            }
            catch (Exception ex)
            {

                return new ErrorDataResult<List<GetCommentForUIDTO>>(message: ex.Message);
            }
        }

        public IResult UpdateComment(UpdateCommentDTO updateCommentDTO)
        {
            try
            {
                var comment = _Context.Comments.FirstOrDefault(x => x.Id.ToString() == updateCommentDTO.CommentId && x.UserId == updateCommentDTO.UserId);
                if (comment == null) return new ErrorResult(message: "Comment Is NotFound!");
                comment.Content = updateCommentDTO.NewContent;
                comment.UpdateDate = DateTime.Now;
                comment.IsSafe = false;
                comment.AlertSeen = false;
                _Context.Comments.Update(comment);
                _Context.SaveChanges();
                return new SuccessResult();
            }
            catch (Exception ex)
            {

                return new ErrorResult(message: ex.Message);
            }
        }
    }
}
