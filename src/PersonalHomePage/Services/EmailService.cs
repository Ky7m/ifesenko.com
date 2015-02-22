using System.Configuration;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using PersonalHomePage.Models;
using SendGrid;

namespace PersonalHomePage.Services
{
    public class EmailService
    {
        // Use NuGet to install SendGrid (Basic C# client lib) 
        private async Task SendEmailAsync(EmailMessageModel message)
        {
            var myMessage = new SendGridMessage();

            myMessage.AddTo(message.Destination);
            myMessage.From = new MailAddress("noreply@ifesenko.com", "No reply service");
            myMessage.Subject = message.Subject;
            myMessage.Text = message.Body;
            myMessage.Html = message.Body;

            var credentials = new NetworkCredential(ConfigurationManager.AppSettings["emailService:Account"],
                                                    ConfigurationManager.AppSettings["emailService:Password"]);

            // Create a Web transport for sending email.
            var transportWeb = new Web(credentials);
            await transportWeb.DeliverAsync(myMessage);

        }
    }
}