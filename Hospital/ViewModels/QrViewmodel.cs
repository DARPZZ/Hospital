using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hospital.Services;

namespace Hospital.ViewModels
{
    public partial class QrViewmodel :BaseViewModel
    {
        Feedback feedback = new Feedback();
        private string scannedText;
        public string ScannedText
        {
            get => scannedText;
            set
            {
                SetProperty(ref scannedText, value);
                OnScannedTextChanged();
            }
        }

        [RelayCommand]
        public void OnPrintTextClicked()
        {
            Debug.WriteLine("Hej");
            Debug.WriteLine(ScannedText);
        }

        private void OnScannedTextChanged()
        {
            if (!string.IsNullOrEmpty(ScannedText))
            {
                
                GoToOpening();
            }
        }

        public void GoToOpening()
        {
      
            feedback.StartHaptiskFeedback();

            var navigationParameters = new Dictionary<string, object>
            {
                { "scanText", ScannedText },
            };
            Shell.Current.GoToAsync("///" + nameof(OpeningPage),false,navigationParameters);
        }
    }
}
