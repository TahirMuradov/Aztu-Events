using Aztu_Events.Core.Utilities.Results.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aztu_Events.DataAccess.Abstarct
{
    public interface IAlertDAL
    {
        public IResult DeleteAlert(string alertId,string CurrentUserId);
        public IResult DeleteAllAlert(string CurrentUserId);
    }
}
