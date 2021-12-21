using Core.Business;
using Core.Databases;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using DayOfWeek = Core.Models.DayOfWeek;

namespace Core.ViewModels
{
    public class DaysOfWeekViewModel : BaseCollectionViewModel<DayOfWeek, DayOfWeekBusiness, DaysOfWeekManager>
    {
        protected override async Task OnLoad()
        {
            try
            {
                if (IsBusy)
                    return;

                IsBusy = true;
                await Task.Delay(100);

                if (Items.Count == 0)
                {
                    var models = DataManager.GetAll();

                    if (models.Any())
                    {
                        var newItems = new List<DayOfWeekBusiness>();

                        foreach (var model in models)
                        {
                            var newDayOfWeekBusiness = new DayOfWeekBusiness { Model = model };
                            newDayOfWeekBusiness.Payments.AddRange(model.Payments);
                            newItems.Add(newDayOfWeekBusiness);
                        }

                        Items.ReplaceRange(newItems);
                    }
                }
                else
                {
                    Items.Clear();
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
