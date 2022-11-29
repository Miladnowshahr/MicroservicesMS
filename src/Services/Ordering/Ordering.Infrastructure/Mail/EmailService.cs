using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Ordering.Application.Contracts.Infrastructure;
using Ordering.Application.Models;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Infrastructure.Mail
{
    public class EmailService : IEmailService
    {
        public EmailSettings _emailSetting { get; }
        public ILogger<EmailService> _logger { get; }

        public EmailService(IOptions<EmailSettings> options,ILogger<EmailService> logger) 
        {
            _emailSetting= options.Value;
            _logger= logger;
        }

        public async Task<bool> SendEmailAsync(Email email)
        {
            var client = new SendGridClient(_emailSetting.ApiKey);
            var subject = email.Subject;
            var body = email.Body;
            var to = new EmailAddress(email.To);

            var from = new EmailAddress
            {
                Email = _emailSetting.FromAddress,
                Name = _emailSetting.FromName
            };

            var sendGridMessage = MailHelper.CreateSingleEmail(from, to, subject, body, body);
            var response =await client.SendEmailAsync(sendGridMessage);
            _logger.LogInformation("Email Sent");

            if (response.StatusCode == System.Net.HttpStatusCode.Accepted || response.StatusCode == System.Net.HttpStatusCode.OK)
                return true;

            _logger.LogError("Email Sending failed");
            return false;
        }
    }
}
