using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.ViewModels
{
    public partial class ForgotPasswordViewModel :BaseViewModel
    {

        List<int> result = new List<int>();

        [ObservableProperty]
        private string specialCode;

        [ObservableProperty]
        private string mailText;

        [ObservableProperty]
        private double beforeOpa;

        [ObservableProperty]
        private double afterOpa;

        public ForgotPasswordViewModel()
        {
            GenerateRandomNumber();
           
            BeforeOpa = 1;
            AfterOpa = 0;
        }

        [RelayCommand]
        private void OnConfirmClicked()
        {
            if (checkIfEqual())
            {
                Debug.WriteLine("They are equal");
            }
            else
            {
                Debug.WriteLine("Unable to set new password");
            }
        }

        [RelayCommand]
        private void OnSendEmailClicked()
        {

            AfterOpa = 1;
            Sendmail();
        }
        private void Sendmail()
        {
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient smtpClient = new SmtpClient("smtp.gmail.com");
                mail.From = new MailAddress("rasmushermansen490@gmail.com");
                mail.To.Add(MailText.ToString());
                mail.Subject = "Password reset";
                mail.Body = "You have requested a new password for youre account<br />" +
                   $"Youre one time password is<br /> <b>{result[0]}{result[1]}{result[3]}{result[4]}</b>";
                mail.IsBodyHtml = true;
                smtpClient.Port = 587;
                smtpClient.Credentials = new System.Net.NetworkCredential("rasmushermansen490@gmail.com", "");
                smtpClient.EnableSsl = true;

                smtpClient.Send(mail);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
        }
         
        private List<int> GenerateRandomNumber()
        {
            
            var random = new Random();
            for (int i = 0; i < 5; i++)
            {
               var num = random.Next(0, 9);
               result.Add(num);
            }
           
            return result;
        }

        private bool checkIfEqual()
        {
            char[] cArray = SpecialCode.ToCharArray();
            List<int> cList = result.Select(c => (int)c).ToList();
            bool areEqual = Enumerable.SequenceEqual(result, cList);
            if (areEqual)
            {
                return true;
            }
            else
            {
                return false;
            }


        }

    }
}
