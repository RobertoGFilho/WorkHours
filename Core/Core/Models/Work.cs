using System;

namespace Core.Models
{
    public class Work : Schedule
    {
        Guid workBalanceId;
        WorkBalance workBalance;

        public WorkBalance WorkBalance
        {
            get => workBalance;
            set => SetProperty(ref workBalance, value);
        }

        public Guid WorkBalanceId
        {
            get => workBalanceId;
            set => SetProperty(ref workBalanceId, value);
        }

    }
}
