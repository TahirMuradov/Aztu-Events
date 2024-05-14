using Aztu_Events.Business.Abstarct;
using Aztu_Events.Core.Utilities.Results.Abstract;
using Aztu_Events.DataAccess.Abstarct;
using Aztu_Events.Entities.DTOs.CommentDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aztu_Events.Business.Concrete
{
    public class CommentManager : ICommentService
    {
        private readonly ICommentDAL _commentDAL;

        public CommentManager(ICommentDAL commentDAL)
        {
            _commentDAL = commentDAL;
        }

        public IResult AddComment(AddCommentDTO addCommentDTO)
        {
           return _commentDAL.AddComment(addCommentDTO);
        }

        public IResult AlertSeen()
        {
           return _commentDAL.AlertSeen();
        }

        public IResult ApporiveComment(string Id)
        {
       return _commentDAL.ApporiveComment(Id);
        }

        public IResult DeleteComment(string Id)
        {
           return _commentDAL.DeleteComment(Id);
        }

        public IDataResult<IQueryable<GetCommentForAdminDTO>> GetAllCommentsForAmin(string langCode)
        {
            return _commentDAL.GetAllCommentsForAmin(langCode);
        }

        public IDataResult<List<GetCommentForAdminDTO>> GetCommentsForAmin(string ConfransId, string langCode)
        {
            return _commentDAL.GetCommentsForAmin(ConfransId,langCode);
        }

        public IDataResult<List<GetCommentForUIDTO>> GetCommentsForUI(string ConfransId)
        {
           return _commentDAL.GetCommentsForUI(ConfransId);
        }

        public IResult UpdateComment(UpdateCommentDTO updateCommentDTO)
        {
            return _commentDAL.UpdateComment(updateCommentDTO);
        }
    }
}
