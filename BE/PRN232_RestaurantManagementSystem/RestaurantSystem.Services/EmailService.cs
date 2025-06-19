using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantSystem.Services
{
    public interface IEmailService
    {
        Task SendEmailAsync(string toEmail, string subject, string body, bool isHtml);
    }
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;
        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendEmailAsync(string toEmail, string subject, string body, bool isHtml)
        {
            var smtpHost = _configuration["EmailConfig:SmtpHost"];
            var smtpPort = int.Parse(_configuration["EmailConfig:SmtpPort"]);
            var smtpUser = _configuration["EmailConfig:SmtpUser"];
            var smtpPass = _configuration["EmailConfig:SmtpPass"];
            var fromEmail = _configuration["EmailConfig:From"];

            var mailMessage = new MailMessage
            {
                From = new MailAddress(fromEmail),
                Subject = subject,
                Body = body,
                IsBodyHtml = isHtml
            };

            mailMessage.To.Add(toEmail);

            using var client = new SmtpClient(smtpHost, smtpPort)
            {
                Credentials = new NetworkCredential(smtpUser, smtpPass),
                EnableSsl = true
            };

            await client.SendMailAsync(mailMessage);
        }
    }
}
