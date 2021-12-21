using Core.Business;
using Core.Databases;
using Core.Models;
using Microsoft.EntityFrameworkCore;
using MvvmHelpers;
using MvvmHelpers.Commands;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Core.ViewModels
{
    public class EmployeeDetailsViewModel : BaseItemDetailsViewModel<Employee, EmployeeBusiness, EmployeesManager>
    {
        WorkBusiness selectedWork;
        PaymentsManager paymentsManager;
        WorksManager worksManager;
        DaysOfWeekManager daysOfWeekManager;

        public WorkBusiness SelectedWork
        {
            get => selectedWork;
            set => SetProperty(ref selectedWork, value);
        }
        public ObservableRangeCollection<WorkBusiness> Works { get; }
        public ICommand NewWorkCommand { get; }

        public EmployeeDetailsViewModel()
        {
            Works = new ObservableRangeCollection<WorkBusiness>();
            paymentsManager = new PaymentsManager();
            worksManager = new WorksManager();
            daysOfWeekManager = new DaysOfWeekManager();
            NewWorkCommand = new AsyncCommand(OnNewWork);
        }

        protected override async void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName == nameof(Business))
                await OnLoadWorks();
        }

        private async Task OnNewWork()
        {
            try
            {
                if (IsBusy)
                    return;
                
                IsBusy = true;
                await Task.Delay(100);

                var dayOfWeek = daysOfWeekManager.GetToday();
                
                SelectedWork = new WorkBusiness()
                {
                    Model = new Work()
                    {
                        DayOfWeek = dayOfWeek,
                        StartHour = new TimeSpan(DateTime.Now.Hour, 0, 0),
                        FinishHour = new TimeSpan(DateTime.Now.Hour + 1, 0, 0),
                    }
                };

                Works.Add(SelectedWork);

                var itemViewModel = await Navigation.GoToAsync<EditWorkViewModel>();
                itemViewModel.Business = SelectedWork;
                itemViewModel.SaveCommand = new AsyncCommand(OnSave);
                itemViewModel.CancelCommand = new AsyncCommand(OnCancel);

                IsBusy = false;
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception);
                IsBusy = false;
            }
        }
        private async Task OnSave()
        {
            try
            {
                if (IsBusy)
                    return;

                IsBusy = true;
                await Task.Delay(100);

                SelectedWork.Validate();

                if (!SelectedWork.IsValid)
                {
                    IsBusy = false;
                    return;
                }

                var workBalance = Business.Model.WorkBalance;
                var work = SelectedWork.Model;

                if (workBalance == null)
                {
                    workBalance = new WorkBalance() { Employee = Business.Model };
                    workBalance.Works = new List<Work>();
                    Business.Model.WorkBalance = workBalance;
                }

                work.WorkBalance = workBalance;
                SetAmounPaid(work);
                workBalance.TotalPaid += work.AmounPaid;
                DataManager.SaveBalanceAndWork(workBalance, work);
                await Navigation.GoToBackAsync();

                IsBusy = false;
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception);
                IsBusy = false;
            }
        }

        private void SetAmounPaid(Work model)
        {
            var start = model.StartHour;
            var finish = (model.FinishHour.Hours == 0) ? new TimeSpan(24, 0, 0) : model.FinishHour;
            
            while (start < finish)
            {
                var payment = paymentsManager.GetPaymentFromTime(start.Add(new TimeSpan(0, 1, 0)), model.DayOfWeek);

                if (payment != null)
                {
                    model.AmounPaid += payment.AmounPaid;
                }

                start = start.Add(new TimeSpan(1, 0, 0));
            }
        }

        private async Task OnCancel()
        {
            try
            {
                if (IsBusy)
                    return;

                IsBusy = true;
                await Task.Delay(100);

                switch (worksManager.GetState(SelectedWork.Model))
                {
                    case EntityState.Detached:
                    case EntityState.Deleted:
                        Works.Remove(SelectedWork);
                        break;

                    default:
                        worksManager.Reload(SelectedWork.Model);
                        SelectedWork.NotifyPropertyChanged(nameof(SelectedWork.Model));
                        break;
                }

                await Navigation.GoToBackAsync();

                IsBusy = false;
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception);
                IsBusy = false;
            }
        }
        public async Task OnWorkRemove(WorkBusiness workBusiness)
        {
            try
            {
                if (IsBusy)
                    return;

                IsBusy = true;
                await Task.Delay(100);
                
                var employee = Business.Model;
                var workBalance = Business.Model.WorkBalance;
                var work = workBusiness.Model;
                
                workBalance.TotalPaid -= work.AmounPaid;
                DataManager.RemoveWorkFromEmployee(employee, work);
                Works.Remove(workBusiness);
                IsBusy = false;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                IsBusy = false;
            }
        }

        private async Task OnLoadWorks()
        {
            try
            {
                if (IsBusy)
                    return;

                IsBusy = true;
                await Task.Delay(100);

                var workModels = Business.Model?.WorkBalance?.Works;

                if (workModels!= null && workModels.Any())
                {
                    var newItems = new List<WorkBusiness>();

                    foreach (var model in workModels)
                        newItems.Add(new WorkBusiness { Model = model });

                    Works.ReplaceRange(newItems);
                }

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
