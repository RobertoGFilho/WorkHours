using Core.Models;
using Core.Validators;

namespace Core.Business
{
    public class PaymentBusiness : BaseBusiness<Payment>
    {
        public PaymentBusiness()
        {
            Validator = new PaymentValidator();
        }

    }
}
