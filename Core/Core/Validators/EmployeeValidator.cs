using Core.Models;
using FluentValidation;

namespace Core.Validators
{
    public class EmployeeValidator : AbstractValidator<Employee>
    {
        public EmployeeValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("name is required.");
        }
    }
}
