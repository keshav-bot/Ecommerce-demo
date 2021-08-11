using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using KKTraders.Models;
using Microsoft.Extensions.Options;
using System.Net.Mail;
using System.Net;
using System.Text;

namespace KKTraders.Services
{
    public class EmailService : IEmailService
    {
        private const string tampletPath = @"EmailTemplet/{0}.html";

        private readonly SMTPConfigModel _smtpConfig;

        public async Task SendTestEmail(UserEmailOption userEmailOptions)
        {
            userEmailOptions.Subject = updatePlaceHolder(" This is the test email from my kkTraders Application", userEmailOptions.Placeholder);
            userEmailOptions.Body = updatePlaceHolder(GetEmailBody("TestEmail"),userEmailOptions.Placeholder);
            await SendEmail(userEmailOptions);
        }  
        public async Task SendConfirmationMessage(UserEmailOption userEmailOptions)
        {
            userEmailOptions.Subject = updatePlaceHolder("Email ConfirmationMessage", userEmailOptions.Placeholder);
            userEmailOptions.Body = updatePlaceHolder(GetEmailBody("UserEmailConfirmationMessage"), userEmailOptions.Placeholder);
            await SendEmail(userEmailOptions);
        } 

        //sending message to user stating that their request has be accept .
        public async Task SendMessageAboutStatusAccepted(OrderConfirmEmail orderConfirmEmail)
        {
            orderConfirmEmail.Subject ="Order ConfirmationMessage";
            orderConfirmEmail.Body = GetEmailBody("ProductLedger");
            await sendOrderConfirmationEmail(orderConfirmEmail);
        }
        public EmailService(IOptions<SMTPConfigModel> smptConfig)
        {
            _smtpConfig = smptConfig.Value;
        }
        public async Task sendOrderConfirmationEmail(OrderConfirmEmail orderConfirmEmail)
        {
            MailMessage mail = new MailMessage()
            {
                Subject = orderConfirmEmail.Subject,
                Body = orderConfirmEmail.Body,
                From = new MailAddress(_smtpConfig.SenderAddress, _smtpConfig.SenderDisplayName),
                IsBodyHtml = _smtpConfig.IsBodyHTMl,
                
            };
            mail.To.Add(orderConfirmEmail.ToEmail);
            NetworkCredential networkCredential = new NetworkCredential(_smtpConfig.UserName, _smtpConfig.Password);


            SmtpClient smtpClient = new SmtpClient()
            {
                Host = _smtpConfig.Host,
                Port = _smtpConfig.Port,
                EnableSsl = _smtpConfig.EnableSSL,
                Credentials = networkCredential,
            };
            mail.BodyEncoding = Encoding.Default;
            await smtpClient.SendMailAsync(mail);
        }

        public async Task SendEmail(UserEmailOption userEmailOption)
        {
            MailMessage mail = new MailMessage()
            {
                Subject = userEmailOption.Subject,
                Body = userEmailOption.Body,
                From = new MailAddress(_smtpConfig.SenderAddress, _smtpConfig.SenderDisplayName),
                IsBodyHtml = _smtpConfig.IsBodyHTMl,

            };
            foreach (var item in userEmailOption.ToEmails)
            {
                mail.To.Add(item);
            }

            NetworkCredential networkCredential = new NetworkCredential(_smtpConfig.UserName, _smtpConfig.Password);


            SmtpClient smtpClient = new SmtpClient()
            {
                Host = _smtpConfig.Host,
                Port = _smtpConfig.Port,
                EnableSsl = _smtpConfig.EnableSSL,
                Credentials = networkCredential,
            };
            mail.BodyEncoding = Encoding.Default;
            await smtpClient.SendMailAsync(mail);
        }

        private string GetEmailBody(String tampletName)
        {
            var body = File.ReadAllText(String.Format(tampletPath, tampletName));
            return body;
        }
        private string  updatePlaceHolder(string text, List<KeyValuePair<string ,string >> keyValuePairs)
        {
            if(!String.IsNullOrEmpty(text) && keyValuePairs != null)
            {
                foreach(var placeholder in keyValuePairs)
                {
                    if(text.Contains(placeholder.Key))
                    {
                        text=text.Replace(placeholder.Key, placeholder.Value);
                    }
                }
                
            }
            return text;
        }


    }
}
