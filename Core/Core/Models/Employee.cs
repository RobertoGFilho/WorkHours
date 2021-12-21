namespace Core.Models
{
    public class Employee : BaseModel
    {
        string name;
        WorkBalance workBalance;

        public string Name
        {
            get => name;
            set => SetProperty(ref name, value);
        }

        public WorkBalance WorkBalance
        {
            get => workBalance;
            set => SetProperty(ref workBalance, value);
        }
    }
}
