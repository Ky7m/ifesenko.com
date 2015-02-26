using System.Configuration;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.Security.Application;
using PersonalHomePage.Models;
using SendGrid;

namespace PersonalHomePage.Services
{
    public sealed class EmailService
    {
        public static async Task SendEmailAsync(EmailMessageModel message)
        {
            var myMessage = new SendGridMessage();
            myMessage.AddTo(ConfigurationManager.AppSettings["emailService:EmailTo"]);

            var emailFrom = Sanitizer.GetSafeHtmlFragment(message.Email);
            var body = Sanitizer.GetSafeHtmlFragment(message.Message);

            myMessage.From = new MailAddress(emailFrom);
            myMessage.Subject = "Email from personal site";
            
            myMessage.Text = body;
            myMessage.Html = body;

            var credentials = new NetworkCredential(ConfigurationManager.AppSettings["emailService:Account"],
                                                    ConfigurationManager.AppSettings["emailService:Password"]);

            // Create a Web transport for sending email.
            var transportWeb = new Web(credentials);
            await transportWeb.DeliverAsync(myMessage);
        }
    }
}