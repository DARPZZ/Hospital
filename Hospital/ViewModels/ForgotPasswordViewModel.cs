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
        private string mailText;
        [RelayCommand]
        private void OnSendEmailClicked()
        {
           GenerateRandomNumber();
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
                mail.Body = "You have requested a new password for youre account\n" +
                   $"Youre one time password is {result[0]} {result[1]} {result[3]}";
                smtpClient.Port = 587;
                smtpClient.Credentials = new System.Net.NetworkCredential("rasmushermansen490@gmail.com", "");
                smtpClient.EnableSsl = true;

                smtpClient.Send(mail);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
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
        private void check()
        {

        }

    }
}
