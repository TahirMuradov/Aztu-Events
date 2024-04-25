
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aztu_Events.Core.Utilities.Results.Abstract;

namespace Aztu_Events.Core.Helper.EmailHelper
{
    public interface IEmailHelper
    {
        Task<IResult> SendEmailAsync(string userEmail, string confirmationLink, string UserName);
    }
}
