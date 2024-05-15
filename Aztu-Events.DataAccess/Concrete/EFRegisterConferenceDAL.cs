using Aztu_Events.Core.Utilities.Results.Abstract;
using Aztu_Events.Core.Utilities.Results.Concrete.ErrorResults;
using Aztu_Events.Core.Utilities.Results.Concrete.SuccessResults;
using Aztu_Events.DataAccess.Abstarct;
using Aztu_Events.DataAccess.Concrete.SQLServer;
using Aztu_Events.Entities.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aztu_Events.DataAccess.Concrete
{
    public class EFRegisterConferenceDAL : IRegisterConferenceDAL
    {
		private readonly UserManager<User> _userManager;
		private readonly AppDbContext _Context;

        public EFRegisterConferenceDAL(UserManager<User> userManager, AppDbContext context)
        {
            _userManager = userManager;
            _Context = context;
        }

        public async Task<IResult> RegisterConferenceAsync(string ConferenceId, string UserId)
        {
			try
			{
                var checkedUser =await _userManager.FindByIdAsync(UserId);
                if (checkedUser == null) return new ErrorResult("User Is NotFound");
                var checkedConference = await _Context.Confrans.Include(x=>x.Audutorium).Include(x=>x.SpecialGuests).Include(x=>x.userConfrances).FirstOrDefaultAsync(x => x.Id.ToString() == ConferenceId);
                if (checkedConference is null) return new ErrorResult("Conference Is NotFound");
                var ChechkedRepeatRegister = await _Context.UserConfrances.FirstOrDefaultAsync(x => x.ConfransId.ToString() == ConferenceId && x.UserId == UserId);
                if (ChechkedRepeatRegister is not null) return new ErrorResult(message: "You are already registered!");
                if (checkedConference.Audutorium.AuditoryCapacity-checkedConference.userConfrances.Count - checkedConference.SpecialGuests.Count - 1 < 0)
                    return new ErrorResult(message:"Auditorim Capacity Full!");
               
                UserConfrance userConfrance =new UserConfrance()
                {
                    ConfransId=checkedConference.Id,
                    UserId=checkedUser.Id,
                };
             
              await  _Context.UserConfrances.AddAsync(userConfrance);
             await   _Context.SaveChangesAsync();
                return new SuccessResult();

            }
			catch (Exception ex)
			{

				return new SuccessResult(message: ex.Message);
			}
        }
    }
}
