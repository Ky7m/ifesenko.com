using System.Threading.Tasks;
using ifesenko.com.Models;

namespace ifesenko.com.Services.Interfaces
{
    public interface IEmailService
    {
        Task SendEmailAsync(EmailMessageModel message);
    }
}