using Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using DayOfWeek = Core.Models.DayOfWeek;

namespace Core.Databases
{
    public class PaymentsManager : BaseManager<Payment>
    {
        public override IQueryable<Payment> GetIncludes(IQueryable<Payment> entities)
        {
            return entities
                .Include(payment => payment.DayOfWeek)
                .OrderBy(paiment => paiment.DayOfWeek.Number);
        }

        public Payment GetPaymentFromTime(TimeSpan time, DayOfWeek dayOfWeek)
        {
            var result = Database.Payments
                .ToList()
                .Where(payment => payment.DayOfWeek == dayOfWeek && payment.StartHour <= time && (payment.FinishHour >= time | payment.FinishHour.Hours == 0))
                .FirstOrDefault();

            return result;
        }
    }
}
