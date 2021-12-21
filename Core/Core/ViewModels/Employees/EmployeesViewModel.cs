using Core.Business;
using Core.Databases;
using Core.Models;

namespace Core.ViewModels
{
    public class EmployeesViewModel : BaseEditDetailsCollectionViewModel<Employee, EmployeeBusiness, EditEmployeeViewModel, EmployeeDetailsViewModel, EmployeesManager>
    { }
}
