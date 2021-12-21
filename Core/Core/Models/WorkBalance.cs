using System;
using System.Collections.Generic;

namespace Core.Models
{
    public class WorkBalance : BaseModel
    {
        Guid employeeId;
        Employee employee;
        List<Work> works;
        double totalPaid;

        public Employee Employee
        {
            get => employee;
            set => SetProperty(ref employee, value);
        }

        public Guid EmployeeId
        {
            get => employeeId;
            set => SetProperty(ref employeeId, value);
        }

        public List<Work> Works
        {
            get => works;
            set => SetProperty(ref works, value);
        }

        public double TotalPaid
        {
            get => totalPaid;
            set => SetProperty(ref totalPaid, value);
        }
    }
}
