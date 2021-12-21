using Core.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Core.Databases
{
    public class WorksManager : BaseManager<Work>
    {
        public override IQueryable<Work> GetIncludes(IQueryable<Work> entities)
        {
            return entities
                .Include(work => work.DayOfWeek)
                .OrderBy(work => work.DayOfWeek.Number);
        }
    }
}
