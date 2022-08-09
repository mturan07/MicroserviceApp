using FlowardApp.Services.EmailService.Models;
using FlowardApp.Services.EmailService.Services;
using FlowardApp.Shared.Messages;
using MassTransit;
using System.Threading.Tasks;

namespace FlowardApp.Services.EmailService.Consumer
{
    public class SendEmailCommandConsumer : IConsumer<SendEmailCommand>
    {
        private readonly IMailService _mailService;
        public SendEmailCommandConsumer(IMailService mailService)
        {
            _mailService = mailService;
        }
        public async Task Consume(ConsumeContext<SendEmailCommand> context)
        {
            MailRequest request = new MailRequest();
            request.Subject = "New Product Created";
            request.Body = $"Message: {context.Message.ProductId} {context.Message.ProductName}";
            request.ToEmail = context.Message.EmailAddress;

            try
            {
                await _mailService.SendEmailAsync(request);
            }
            catch (System.Exception ex)
            {
                // Log exception
            }
        }
    }
}