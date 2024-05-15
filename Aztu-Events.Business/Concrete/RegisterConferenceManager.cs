using Aztu_Events.Business.Abstarct;
using Aztu_Events.Core.Utilities.Results.Abstract;
using Aztu_Events.DataAccess.Abstarct;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aztu_Events.Business.Concrete
{
    public class RegisterConferenceManager : IRegisterConferenceService
    {
        private readonly IRegisterConferenceDAL _registerConferenceDAL;

        public RegisterConferenceManager(IRegisterConferenceDAL registerConferenceDAL)
        {
            _registerConferenceDAL = registerConferenceDAL;
        }

        public Task<IResult> RegisterConferenceAsync(string ConferenceId, string UserId)
        {
           return _registerConferenceDAL.RegisterConferenceAsync(ConferenceId, UserId);
        }

       
        
    }
}
