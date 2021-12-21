# Work Hours
Xamarin Forms Application with design pattern <b>MVVM</b> and <b>CLEAN CODE</b> principles. Available on Android, iOS and Windows platforms

![Screenshot 2021-12-20 222701](https://user-images.githubusercontent.com/68563526/146860030-6d4033da-af3a-481d-b776-e25d88299283.png)

<h2>IDE and Project</h2>

* Microsoft Visual Studio 2019;
* Xamarin Forms Project;


<h2>Libraries</h2>

* Microsoft.EntityFrameworkCore.Sqlite : database abstraction layer;
* Microsoft.EntityFrameworkCore.Tools : used for data migration;
* Refractored.MvvmHelpers : used for data binding and synchronous and asynchronous commands;
* Xamarin.Essentials : internet connection verification;
* Shell : page browsing;
* FluentValidation: validation when editing data

<h2>Languages</h2>

<a href="https://developer.microsoft.com/en-us/windows/downloads/multilingual-app-toolkit/">Multilingual App Toolkit</a> extension was used to generate the translation files available in \Resources folder for languages:
* English;
* Brazilian portuguese;
<p></p>

	<Label Text="{extensions:Translate pokemonTypes}" FontSize="Caption"/>

<h2>Models</h2>
Three main classes <b>Payment, Work and WorkBalance</b>
<p></p>
<p></p>

![ModelsDiagram](https://user-images.githubusercontent.com/68563526/146860226-eedef8e9-7a8b-4cf1-a37a-9cd31c2c4f2f.png)

<h2>Business</h2>
Layer between model and viewModel responsible for data validation and new business rules;
<p></p>

    public class BaseBusiness<TModel> : ObservableObject where TModel : BaseModel, new()
    {
        ...
        public IList<ValidationFailure> Erros { get; set; }
        public AbstractValidator<TModel> Validator { get; set; }
        ...
    }

<h2>View Model</h2>
In this layer, inheritance and generics techniques <b>combined</b> were used, to define behavior patterns and maximum code reuse.
<p></p>

    public abstract class BaseCollectionViewModel<TModel, TBusiness, TDataManager> : 
	BaseDataViewModel<TModel, TBusiness, TDataManager> where TModel : BaseModel, new() where 
	TBusiness : BaseBusiness<TModel>, new() where 
	TDataManager : BaseManager<TModel>, new()
    { ... }

![BaseViewModelsDiagram - Copy](https://user-images.githubusercontent.com/68563526/146860614-d5f922a6-8772-45b9-9442-dd299f76ce3e.png)

<h2>View</h2>
Layer representing the page and injecting the viewModel.
<p></p>

	public partial class BasePage<TViewModel> : ContentPage where TViewModel : ViewModels.BaseViewModel, new()
	{ ... }
    
<h2>Database</h2>
The <b>Sqlite</b> and Entity Framework were used as data caching strategy.  

<h2>Migrations</h2>
Whenever model structures are changed, adding or removing fields or new models, the migration will be performed at startup restructuring the database;
<p></p>

    public class Database : DbContext
    {
        ...
        public void Initialize()
        {
            //Database.EnsureDeleted();
            Database.Migrate();
        }
    }

<h2>Set Amout Paid</h2>
<p></p>

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

<h2>Image Font</h2>

Strategy used to use icons, in the action bar, from true type fonts.
* icofont.ttf;
* material.ttf;
 
<h2>Navigation</h2>

Injecting the "views" page navigation service through view model navigation

    public partial class App : Application
    {
        ...   
        public App(string dbPath)
        {
            ...
            DependencyService.Register<Interfaces.INavigation, Navigation>();
        }   
     }
     
     public class Navigation : Interfaces.INavigation
     {
        public async Task<TViewModel> GoToAsync<TViewModel>() where TViewModel : BaseViewModel
        {
            await Shell.Current.GoToAsync(typeof(TViewModel).Name);
            return Shell.Current.CurrentPage.BindingContext as TViewModel;
        }

        public Task GoToBackAsync()
        {
            return Shell.Current.GoToAsync("..");

        }
     }
     
    public abstract class BaseViewModel : MvvmHelpers.BaseViewModel
    {
        ...
        public Interfaces.INavigation Navigation => 
        DependencyService.Get<Interfaces.INavigation>(DependencyFetchTarget.GlobalInstance);
    }

<h2>Conclusion</h2>

Focus on best programming practices in Xamarin Forms applications.
