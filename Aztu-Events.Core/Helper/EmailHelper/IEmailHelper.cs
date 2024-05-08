
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
        Task<IResult> ApproveConfransSendEmail(string userEmail, string name,string dateTime,string AuditoriumNumber,bool UpdateDate=false);
        Task<IResult> DeclineConfransEmailAsync(string userEmail, string name, string responseMessage);
        Task<IResult> ApproveConfransSendEmailForGuest(string userEmail, string name,string dateTime, string AuditoriumNumber, string confransDetailUrl, bool UpdateDate = false,bool SendEmailGuest=false);
    }
}   
