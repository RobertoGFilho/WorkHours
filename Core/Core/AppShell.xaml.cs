using Core.ViewModels;
using Core.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Core
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(DaysOfWeekViewModel), typeof(DaysOfWeekPage));

            Routing.RegisterRoute(nameof(EmployeesViewModel), typeof(EmployeesPage));
            Routing.RegisterRoute(nameof(EditEmployeeViewModel), typeof(EditEmployeePage));
            Routing.RegisterRoute(nameof(EmployeeDetailsViewModel), typeof(EmployeeDetailsPage));

            Routing.RegisterRoute(nameof(PaymentsViewModel), typeof(PaymentsPage));

            Routing.RegisterRoute(nameof(EditWorkViewModel), typeof(EditWorkPage));

        }
    }
}