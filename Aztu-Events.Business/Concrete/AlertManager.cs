using Aztu_Events.Business.Abstarct;
using Aztu_Events.Core.Utilities.Results.Abstract;
using Aztu_Events.DataAccess.Abstarct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aztu_Events.Business.Concrete
{
    public class AlertManager : IAlertService
    {
        private readonly IAlertDAL _alertDAL;

        public AlertManager(IAlertDAL alertDAL)
        {
            _alertDAL = alertDAL;
        }

        public IResult DeleteAlert(string alertId, string CurrentUserId)
        {
         return _alertDAL.DeleteAlert(alertId, CurrentUserId);
        }

        public IResult DeleteAllAlert(string CurrentUserId)
        {
           return _alertDAL.DeleteAllAlert(CurrentUserId);
        }
    }
}
