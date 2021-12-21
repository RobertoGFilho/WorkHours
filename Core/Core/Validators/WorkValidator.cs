using Core.Models;
using FluentValidation;

namespace Core.Validators
{
    public class WorkValidator : AbstractValidator<Work>
    {
        public WorkValidator()
        {
            RuleFor(x => x.DayOfWeek).NotEmpty().WithMessage("day of week is required.");
            RuleFor(x => x.StartHour).NotEmpty().WithMessage("start hour is required.");
            
            RuleFor(x => x.FinishHour)
                .NotEmpty()
                .WithMessage("finish hour is required.")
                .GreaterThan(x=> x.StartHour)
                .WithMessage("finish won't be less than start.")
                .NotEqual(x => x.StartHour)
                .WithMessage("finish won't be equal start.");
        }
    }
}
