using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using ifesenko.com.Models;
using ifesenko.com.Services.Interfaces;
using ifesenko.com.Settings;
using Microsoft.Extensions.OptionsModel;
using Microsoft.Security.Application;
using SendGrid;

namespace ifesenko.com.Services.Implementation
{
    public sealed class EmailService : IEmailService
    {
        private readonly EmailServiceSettings _emailServiceSettings;

        public EmailService(IOptions<EmailServiceSettings> emailServiceSettings)
        {
            _emailServiceSettings = emailServiceSettings.Value;
        }

        public async Task SendEmailAsync(EmailMessageModel message)
        {
            var myMessage = new SendGridMessage();
            myMessage.AddTo(_emailServiceSettings.EmailTo);

            var emailFrom = Sanitizer.GetSafeHtmlFragment(message.Email);
            var body = Sanitizer.GetSafeHtmlFragment(message.Message);

            myMessage.From = new MailAddress(emailFrom);
            myMessage.Subject = "Email from personal site";
            
            myMessage.Text = body;
            myMessage.Html = body;

            var credentials = new NetworkCredential(_emailServiceSettings.Account, _emailServiceSettings.Password);

            // Create a Web transport for sending email.
            var transportWeb = new Web(credentials);
            await transportWeb.DeliverAsync(myMessage);
        }
    }
}