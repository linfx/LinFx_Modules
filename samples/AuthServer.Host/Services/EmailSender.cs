using LinFx.Extensions.Email;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace AuthServer.Host.Services
{
    public class EmailSender : IEmailSender
    {
        private readonly ILogger<EmailSender> _logger;

        public EmailSender(ILogger<EmailSender> logger)
        {
            _logger = logger;
        }

        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            _logger.LogInformation($"Email: {email}, subject: {subject}, message: {htmlMessage}");

            return Task.FromResult(0);
        }

        public Task SendEmailAsync(string email, string subject, string message, bool isHtml = false)
        {
            throw new System.NotImplementedException();
        }
    }
}
