using System.Collections.Generic;

namespace Core.Models
{
    public class DayOfWeek : BaseModel
    {
        string name;
        int number;
        List<Payment> payments;

        public string Name
        {
            get => name;
            set => SetProperty(ref name, value);
        }

        public int Number
        {
            get => number;
            set => SetProperty(ref number, value);
        }

        public List<Payment> Payments
        {
            get => payments;
            set => SetProperty(ref payments, value);
        }
    }
}
