using Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using DayOfWeek = Core.Models.DayOfWeek;

namespace Core.Databases
{
    public class DaysOfWeekManager : BaseManager<DayOfWeek>
    {

        public override IQueryable<DayOfWeek> GetIncludes(IQueryable<DayOfWeek> entities)
        {
            return entities
                .Include(day => day.Payments)
                .OrderBy(day => day.Number);
        }

        public DayOfWeek GetToday()
        {
            var number = (int)DateTime.Now.DayOfWeek + 1;
            return Database.DayOfWeeks.FirstOrDefault(day => day.Number == number);
        }

    }
}
