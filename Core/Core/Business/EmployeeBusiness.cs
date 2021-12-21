using Core.Models;
using Core.Validators;
using System.Linq;

namespace Core.Business
{
    public class EmployeeBusiness : BaseBusiness<Employee>
    {
        public string NameValidation => Erros?.Where(s => s.PropertyName == nameof(Model.Name)).FirstOrDefault()?.ErrorMessage;

        public EmployeeBusiness()
        {
            Validator = new EmployeeValidator();
        }

        public override void Validate()
        {
            base.Validate();
            OnPropertyChanged(nameof(NameValidation));
        }

    }
}
