using Core.Models;
using Core.Validators;
using System.Linq;

namespace Core.Business
{
    public class WorkBusiness : BaseBusiness<Work>
    {
        public string DayOfWeekValidation => Erros?.Where(s => s.PropertyName == nameof(Model.DayOfWeek)).FirstOrDefault()?.ErrorMessage;
        public string StartHourValidation => Erros?.Where(s => s.PropertyName == nameof(Model.StartHour)).FirstOrDefault()?.ErrorMessage;
        public string FinishHourValidation => Erros?.Where(s => s.PropertyName == nameof(Model.FinishHour)).FirstOrDefault()?.ErrorMessage;

        public WorkBusiness()
        {
            Validator = new WorkValidator();
        }

        public override void Validate()
        {
            base.Validate();
            OnPropertyChanged(nameof(DayOfWeekValidation));
            OnPropertyChanged(nameof(StartHourValidation));
            OnPropertyChanged(nameof(FinishHourValidation));
        }

    }
}
