using Aztu_Events.Core.Utilities.Results.Abstract;
using Aztu_Events.Core.Utilities.Results.Concrete.ErrorResults;
using Aztu_Events.Core.Utilities.Results.Concrete.SuccessResults;
using Aztu_Events.DataAccess.Abstarct;
using Aztu_Events.DataAccess.Concrete.SQLServer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aztu_Events.DataAccess.Concrete
{
    public class EFAlertDAL : IAlertDAL
    {
        private readonly AppDbContext _context;

        public EFAlertDAL(AppDbContext context)
        {
            _context = context;
        }

        public IResult DeleteAlert(string alertId, string CurrentUserId)
        {
            try
            {
                var alert =CurrentUserId is null? _context.Alerts
                    .Include(x => x.AlertLaunguages)
                    .FirstOrDefault(x => x.Id.ToString() == alertId)
                    :
                    _context.Alerts
                    .Include(x => x.AlertLaunguages)
                    .FirstOrDefault(x => x.Id.ToString() == alertId && x.UserId == CurrentUserId);
                if (alert == null)  return new ErrorResult(message: "Alert is NotFound!");
                _context.AlertLaunguages.RemoveRange(alert.AlertLaunguages);
                _context.Alerts.Remove(alert);
                _context.SaveChanges();
                return new SuccessResult();

            }
            catch (Exception ex)
            {

                return new ErrorResult(message: ex.Message);
            }
        }

        public IResult DeleteAllAlert(string CurrentUserId)
        {
            try
            {
                var alert = CurrentUserId is not null ? _context.Alerts
                    .Include(x => x.AlertLaunguages)
                    .Where(x => x.UserId==CurrentUserId)
                    :
                    _context.Alerts
                    .Include(x => x.AlertLaunguages)
                    .Where(x =>x.UserId == null);
                if (alert == null) return new ErrorResult(message: "Alert is NotFound!");
             
             
                _context.Alerts.RemoveRange(alert);
                _context.SaveChanges();
                return new SuccessResult();

            }
            catch (Exception ex)
            {

                return new ErrorResult(message: ex.Message);
            }
        }
    }
}
