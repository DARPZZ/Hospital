namespace Hospital.ViewModels
{
    public partial class OpeningViewModel : BaseViewModel,IQueryAttributable
    {
        [ObservableProperty]
        public string scanText;
        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            ScanText = query["scanText"] as string;
        }
    }
}