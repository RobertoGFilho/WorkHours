using Core.ViewModels;
using Xamarin.Forms.Xaml;

namespace Core.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditEmployeePage : EditEmployeeXaml
    {
        public EditEmployeePage()
        {
            Initialize();
        }

        protected override void Initialize()
        {
            InitializeComponent();
            base.Initialize();
        }
    }

    public class EditEmployeeXaml : BasePage<EditEmployeeViewModel> { }
}