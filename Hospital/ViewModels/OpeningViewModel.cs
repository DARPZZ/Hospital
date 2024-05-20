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
        [RelayCommand]
        private void OnLogoutClicked()
        {
            Debug.WriteLine("logout clicked");
            SecureStorage.Default.Remove("email");
            SecureStorage.Default.Remove("password");

        }
    }
}