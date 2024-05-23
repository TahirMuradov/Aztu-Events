using Aztu_Events.Core.Utilities.Results.Abstract;
using Aztu_Events.Entities.DTOs.AlertDTOs;
using Aztu_Events.Entities.DTOs.CommentDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aztu_Events.Business.Abstarct
{
    public interface ICommentService
    {
        public IDataResult<IQueryable<GetAlertDTO>> GetAlertsForComment(string langCode);

        public IResult ApporiveComment(string Id);
        public IResult AddComment(AddCommentDTO addCommentDTO);
        public IResult DeleteComment(string Id);
        public IResult UpdateComment(UpdateCommentDTO updateCommentDTO);
        public IDataResult<List<GetCommentForUIDTO>> GetCommentsForUI(string ConfransId);
        public IDataResult<List<GetCommentForAdminDTO>> GetCommentsForAmin(string ConfransId, string langCode);
        public IDataResult<IQueryable<GetCommentForAdminDTO>> GetAllCommentsForAmin(string langCode);

    }
}
