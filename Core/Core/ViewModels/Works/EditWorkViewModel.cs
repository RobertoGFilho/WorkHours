using Core.Business;
using Core.Databases;
using Core.Models;
using MvvmHelpers;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using DayOfWeek = Core.Models.DayOfWeek;

namespace Core.ViewModels
{
    public class EditWorkViewModel : BaseEditItemViewModel<Work, WorkBusiness, WorksManager>
    {
        readonly DaysOfWeekManager daysOfWeekManager;
        public ObservableRangeCollection<DayOfWeek> DaysOfWeek { get; }
        public EditWorkViewModel()
        {
            daysOfWeekManager = new DaysOfWeekManager();
            DaysOfWeek = new ObservableRangeCollection<DayOfWeek>();
        }

        public override async void OnAppearing()
        {
            base.OnAppearing();

            if (!DaysOfWeek.Any())
                await OnLoadDaysOfWeek();
        }

        public async Task OnLoadDaysOfWeek()
        {
            try
            {
                if (IsBusy)
                    return;

                IsBusy = true;
                await Task.Delay(100);

                var models = daysOfWeekManager.GetAll();
                DaysOfWeek.ReplaceRange(models);

                IsBusy = false;

            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                IsBusy = false;
            }
        }
    }
}
