using Core.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Core.Databases
{
    public class EmployeesManager : BaseManager<Employee>
    {
        public override IQueryable<Employee> GetIncludes(IQueryable<Employee> entities)
        {
            return entities
                .Include(employee => employee.WorkBalance)
                    .ThenInclude(workBalance => workBalance.Works)
                        .ThenInclude(work => work.DayOfWeek);
        }

        public void SaveBalanceAndWork(WorkBalance workBalance, Work work)
        {
            switch (Database.Entry(workBalance).State)
            {
                case EntityState.Detached: Database.WorkBalances.Add(workBalance); break;
                case EntityState.Modified: Database.WorkBalances.Update(workBalance); break;
            }

            switch (Database.Entry(work).State)
            {
                case EntityState.Detached: Database.Works.Add(work); break;
                case EntityState.Modified: Database.Works.Update(work); break;
            }

            Database.SaveChanges();
        }

        public void RemoveWorkFromEmployee(Employee entity, Work work)
        {
            if (entity.WorkBalance.Works.Contains(work))
            {
                Database.Works.Remove(work);
                Database.SaveChanges();
            }
        }

    }
}
