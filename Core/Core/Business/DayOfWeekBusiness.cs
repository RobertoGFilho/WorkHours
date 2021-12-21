using Core.Models;
using Core.Validators;
using MvvmHelpers;

namespace Core.Business
{
    public class DayOfWeekBusiness : BaseBusiness<DayOfWeek>
    {
        public ObservableRangeCollection<Payment> Payments { get; }
        public DayOfWeekBusiness()
        {
            Validator = new DayOfWeekValidator();
            Payments = new ObservableRangeCollection<Payment>();
        }

    }
}
