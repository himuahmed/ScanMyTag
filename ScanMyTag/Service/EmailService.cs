using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis.Options;
using Microsoft.Extensions.Options;
using ScanMyTag.Models;

namespace ScanMyTag.Service
{
    public class EmailService : IEmailService
    {
        private const string templatePath = @"EmailTemplates/{0}.html";
        private readonly SMTPModel _smtpOptions;
        public EmailService(IOptions<SMTPModel> smptOptions)
        {
            _smtpOptions = smptOptions.Value;
        }

        public async Task SendTestEmail(EmailOptions emailOptions)
        {
            emailOptions.Subject = UpdatePlaceHolders("This is a test email for {{username}}", emailOptions.PlaceHolders );
            emailOptions.Body = UpdatePlaceHolders(GetEmailBody("Email"), emailOptions.PlaceHolders);

            await SendEmail(emailOptions);
        }

        public async Task SendEmailVerificationEmail(EmailOptions emailOptions)
        {
            emailOptions.Subject = UpdatePlaceHolders("Please verify your email.", emailOptions.PlaceHolders);
            emailOptions.Body = UpdatePlaceHolders(GetEmailBody("EmailVerification"), emailOptions.PlaceHolders);

            await SendEmail(emailOptions);
        }

        private async Task SendEmail(EmailOptions emailOptions)
        {
            MailMessage mail = new MailMessage()
            {
                Subject = emailOptions.Subject,
                Body = emailOptions.Body,
                From = new MailAddress(_smtpOptions.SenderAddress,_smtpOptions.SenderDisplayName),
                IsBodyHtml = _smtpOptions.IsBodyHtml
            };

            foreach (var email in emailOptions.EmailReceivers)
            {
                mail.To.Add(email);
            }

            NetworkCredential networkCredential = new NetworkCredential(_smtpOptions.UserName, _smtpOptions.Password);

            SmtpClient smtpClient = new SmtpClient
            {
                Host = _smtpOptions.Host,
                Port = _smtpOptions.Port,
                EnableSsl = _smtpOptions.EnableSSL,
                UseDefaultCredentials = _smtpOptions.UseDefaultCredential,
                Credentials =  networkCredential
            };
            mail.BodyEncoding = Encoding.Default;

            await smtpClient.SendMailAsync(mail);

        }

        private string GetEmailBody(string emailTemplate)
        {
            var body = File.ReadAllText(string.Format(templatePath, emailTemplate));
            return body;
        }

        private string UpdatePlaceHolders(string text, List<KeyValuePair<string, string>> keyValuePairs)
        {
            if (!string.IsNullOrEmpty(text) && keyValuePairs != null)
            {
                foreach (var placeHolder in keyValuePairs)
                {
                    if (text.Contains(placeHolder.Key))
                    {
                        text = text.Replace(placeHolder.Key, placeHolder.Value);
                    }
                }
            }

            return text;
        }
    }
}
