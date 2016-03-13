using System.Threading.Tasks;
using IfesenkoDotCom.Models;

namespace IfesenkoDotCom.Services.Interfaces
{
    public interface IEmailService
    {
        Task SendEmailAsync(EmailMessageModel message);
    }
}