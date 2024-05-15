using Aztu_Events.Core.Utilities.Results.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aztu_Events.DataAccess.Abstarct
{
    public interface IRegisterConferenceDAL
    {
       public Task< IResult> RegisterConferenceAsync(string ConferenceId, string UserId);
    }
}
