using CleanArchitecture.Application.Contracts.Infraestructure;
using CleanArchitecture.Application.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Net;

namespace CleanArchitecture.Infraestructure.Email
{
    public class EmailService : IEmailService
    {
        public EmailSettings EmailSettings { get; }
        private readonly ILogger<EmailService> _logger;

        public EmailService(IOptions<EmailSettings> emailSettings, ILogger<EmailService> logger)
        {
            EmailSettings = emailSettings.Value;
            _logger = logger;
        }

        public async Task<bool> SendEmail(Application.Models.Email email)
        {
            var client = new SendGridClient(EmailSettings.ApiKey);

            var subject = email.Subject;
            var to = new EmailAddress(email.To);
            var body = email.Body;

            var from = new EmailAddress
            {
                Email = EmailSettings.FromAddress,
                Name = EmailSettings.FromName
            };

            var sendGridMessage = MailHelper.CreateSingleEmail(from, to, subject, body, body);

            var response = await client.SendEmailAsync(sendGridMessage);

            var aceptedStatus = new List<HttpStatusCode> { HttpStatusCode.Accepted, HttpStatusCode.OK };

            if (aceptedStatus.Any(s => s == response.StatusCode))
            {
                return true;
            }

            _logger.LogError($"El email no pudo enviarse correctamente");
            return false;
        }
    }
}
