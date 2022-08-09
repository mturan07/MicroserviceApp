using FlowardApp.Services.EmailService.Models;
using System.Threading.Tasks;

namespace FlowardApp.Services.EmailService.Services
{
    public interface IMailService
    {
        Task SendEmailAsync(MailRequest mailRequest);
    }
}
