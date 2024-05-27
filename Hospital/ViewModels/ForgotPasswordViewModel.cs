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
                mail.Body = mailBody();
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
        private string mailBody()
        {
            var text = "<div style='font-family: Arial, sans-serif;'>" +
                       "<h2 style='color: #2F4F4F;'>Password Reset Request</h2>" +
                       $"<p>Dear <span style='color: #4682B4;'>{MailText}</span>,</p>" +
                       "<p>We received a request to reset your password. Here is your security code:</p>" +
                       $"<p style='font-size: 20px; color: #008B8B;'><b>{result[0]}{result[1]}{result[3]}{result[4]}</b></p>" +
                       "<p>Please use this code to access your account and change your password.</p>" +
                       "<p>If you did not request a password reset, please ignore this email or contact our support team.</p>" +
                       "<p style='color: #2F4F4F;'>Best Regards,</p>" +
                       "<p style='color: #2F4F4F;'><b>Kolding syghus</b></p>" +
                       "<img src='https://www.nordichorse.dk/cdn/shop/articles/spiseroersforstoppelse_hest_895x.progressive.jpg?v=1667830689' alt='Kolding syghus' style='width:100%;height:auto;'>" +
                       "</div>";

            return text;
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
