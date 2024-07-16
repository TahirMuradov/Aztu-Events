using Aztu_Events.Core.Utilities.Results.Abstract;

namespace Aztu_Events.DataAccess.Abstarct
{
    public interface IAlertDAL
    {
        public IResult DeleteAlert(string alertId,string CurrentUserId);
        public IResult DeleteAllAlert(string CurrentUserId);
    }
}
