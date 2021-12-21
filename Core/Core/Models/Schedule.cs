using System;
using System.Runtime.CompilerServices;

namespace Core.Models
{
    public class Schedule : BaseModel
    {
        Guid dayOfWeekId;
        DayOfWeek dayOfWeek;
        TimeSpan startHour;
        TimeSpan finishHour;
        double amounPaid;

        public DayOfWeek DayOfWeek
        {
            get => dayOfWeek;
            set => SetProperty(ref dayOfWeek, value);
        }

        public Guid DayOfWeekId
        {
            get => dayOfWeekId;
            set => SetProperty(ref dayOfWeekId, value);
        }

        public TimeSpan StartHour
        {
            get => startHour;
            set => SetProperty(ref startHour, value);
        }

        public TimeSpan FinishHour
        {
            get => finishHour;
            set => SetProperty(ref finishHour, value);
        }

        public double AmounPaid
        {
            get => amounPaid;
            set => SetProperty(ref amounPaid, value);
        }

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName == nameof(StartHour) && (StartHour.Minutes > 0 || StartHour.Seconds > 0))
                StartHour = new TimeSpan(StartHour.Hours, 0, 0);

            if (propertyName == nameof(FinishHour) && (FinishHour.Minutes > 0 || FinishHour.Seconds > 0))
                FinishHour = new TimeSpan(FinishHour.Hours, 0, 0);
        }
    }
}
