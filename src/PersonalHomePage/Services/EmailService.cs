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

            myMessage.AddTo("igor.aka.ky7m@gmail.com");
            myMessage.From = new MailAddress(message.Email);
            myMessage.Subject = "Contact from personal site";
            myMessage.Text = message.YourMessage;
            myMessage.Html = message.YourMessage;

            var credentials = new NetworkCredential(ConfigurationManager.AppSettings["emailService:Account"],
                                                    ConfigurationManager.AppSettings["emailService:Password"]);

            // Create a Web transport for sending email.
            var transportWeb = new Web(credentials);
            await transportWeb.DeliverAsync(myMessage);
        }
    }
}