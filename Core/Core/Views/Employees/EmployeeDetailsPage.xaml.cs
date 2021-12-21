using Core.Business;
using Core.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Core.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EmployeeDetailsPage : EmployeeDetailsXaml
    {
        public EmployeeDetailsPage()
        {
            Initialize();
        }

        protected override void Initialize()
        {
            InitializeComponent();
            base.Initialize();
        }

        private async void OnWorkRemove(object sender, System.EventArgs e)
        {
            await ViewModel.OnWorkRemove((sender as Element).BindingContext as WorkBusiness);
        }
    }

    public class EmployeeDetailsXaml : BasePage<EmployeeDetailsViewModel> { }
}