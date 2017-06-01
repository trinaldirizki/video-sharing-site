using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;

namespace VideoSharing
{
    public class MyMail
    {
        public static void Send(ViewModels.UserSignUp signUpForm)
        {

            MailMessage ePosta = new MailMessage();
            ePosta.From = new MailAddress("YourTubeUserTeam@gmail.com");
            
            ePosta.To.Add(signUpForm.Email);
            
            ePosta.Subject = "Hoş Geldin";
            ePosta.IsBodyHtml = true;
            
            ePosta.Body = "Selam " + signUpForm.Name + signUpForm.Surname + ",\nGüzel ve eğlenceli vakit geçirmen dileklerimizle.\nŞifren:" + signUpForm.Password;
            
            SmtpClient smtp = new SmtpClient();
            

            smtp.Port = 587;

            if (signUpForm.Email.IndexOf("gmail.com") > -1)
                smtp.Host = "smtp.gmail.com";
            else if (signUpForm.Email.IndexOf("hotmail.com") > -1 || signUpForm.Email.IndexOf("windowslive.com") > -1 || signUpForm.Email.IndexOf("outlook.com") > -1)
                smtp.Host = "smtp.live.com";
            else if (signUpForm.Email.IndexOf("yahoo.com") > -1)
                smtp.Host = "smtp.mail.yahoo.com";

            smtp.EnableSsl = true;
            smtp.Credentials = new System.Net.NetworkCredential("yourtubeuserteam@gmail.com", "aaaaaaaa1");

            try
            {
                smtp.Send(ePosta);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
        }
    }
}