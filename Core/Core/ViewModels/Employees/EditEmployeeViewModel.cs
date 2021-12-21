using Core.Business;
using Core.Databases;
using Core.Models;

namespace Core.ViewModels
{
    public class EditEmployeeViewModel : BaseEditItemViewModel<Employee, EmployeeBusiness, EmployeesManager>
    { }
}
