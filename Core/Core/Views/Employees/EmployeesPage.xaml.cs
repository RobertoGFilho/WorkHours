using Core.Business;
using Core.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Core.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EmployeesPage : EmployeesXaml
    {
        public EmployeesPage()
        {
            Initialize();
        }

        protected override void Initialize()
        {
            InitializeComponent();
            base.Initialize();
        }

        private async void OnRemove(object sender, System.EventArgs e)
        {
            await ViewModel.OnRemove((sender as Element).BindingContext as EmployeeBusiness);
        }
    }

    public class EmployeesXaml : BasePage<EmployeesViewModel> { }
}