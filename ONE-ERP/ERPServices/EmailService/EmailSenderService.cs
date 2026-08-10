using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ONEERP.Data;
using System.Net.Mail;
using System.Net;
using ONEERP.ERPServices.EmailService.Interfaces;

namespace ONEERP.ERPServices.EmailService
{
    public class EmailSenderService: IEmailSenderService
    {
        private readonly IConfiguration _configuration;
        private readonly ERPDbContext _context;

        public EmailSenderService(IConfiguration configuration, ERPDbContext context)
        {
            _configuration = configuration;
            _context = context;
        }

        public async Task SendEmail(string mailTo, string subject, string message)
        {
            string userName = _configuration["Email:Email"];
            string password = _configuration["Email:Password"];
            string host = _configuration["Email:Host"];
            int port = int.Parse(_configuration["Email:Port"]);
            string mailFrom = _configuration["Email:Email"];
            using (var client = new SmtpClient())
            {
                var credential = new NetworkCredential
                {
                    UserName = userName,
                    Password = password
                };

                client.Credentials = credential;
                client.Host = host;
                client.Port = port;
                client.EnableSsl = true;

                using (var emailMessage = new MailMessage())
                {
                    emailMessage.To.Add(new MailAddress(mailTo));
                    emailMessage.From = new MailAddress(mailFrom);
                    emailMessage.Subject = subject;
                    emailMessage.Body = message;
                    emailMessage.IsBodyHtml = true;
                    client.Send(emailMessage);
                }
            }
            await Task.CompletedTask;
        }


        public async Task SendEmailWithFrom(string mailTo, string name, string subject, string message)
        {
            try
            {
                string userName = _configuration["Email:Email"];
                string password = _configuration["Email:Password"];
                string host = _configuration["Email:Host"];
                int port = int.Parse(_configuration["Email:Port"]);
                string mailFrom = _configuration["Email:Email"];
                using (var client = new SmtpClient())
                {
                    var credential = new NetworkCredential
                    {
                        UserName = userName,
                        Password = password
                    };

                    client.Credentials = credential;
                    client.Host = host;
                    client.Port = port;
                    client.EnableSsl = true;

                    using (var emailMessage = new MailMessage())
                    {
                        emailMessage.To.Add(new MailAddress(mailTo));
                        emailMessage.From = new MailAddress(mailFrom, name);
                        emailMessage.Subject = subject;
                        emailMessage.Body = message;
                        emailMessage.IsBodyHtml = true;
                        client.Send(emailMessage);
                    }
                }
                await Task.CompletedTask;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            
        }


        public async Task SendEmailNew()
        {
            try
            {
                string smtpServer = "smtp.oneictltd.com"; // Replace with your SMTP server
                int smtpPort = 465; // Replace with your SMTP port
                string email = "info@oneictltd.com"; // Replace with your email address
                string password = "Admin.1ict"; // Replace with your email password
                string toEmail = "tonoy300oneict@gmail.com"; // Replace with recipient email address
                string subject = "Test Email";
                string body = "This is a test email sent from dotnet core project.";

                var smtpClient = new SmtpClient(smtpServer, smtpPort)
                {
                    Credentials = new NetworkCredential(email, password),
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Timeout = 20000
                };

                var message = new MailMessage(email, toEmail, subject, body);
                smtpClient.Send(message);

                await Task.CompletedTask;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public async Task SendEmailViaAppPass(string mailTo, string subject, string message)
        {

            try
            {
                string userName = _configuration["Email:Email"];
                string host = _configuration["Email:Host"];
                int port = int.Parse(_configuration["Email:Port"]);
                string mailFrom = _configuration["Email:Email"];
                string appPassword = _configuration["Email:AppPassword"];
                var emailMessage = new MailMessage();
                emailMessage.To.Add(new MailAddress(mailTo));
                emailMessage.From = new MailAddress(mailFrom, "ONE ICT");
                emailMessage.Subject = subject;
                emailMessage.Body = message;
                emailMessage.IsBodyHtml = true;
                var credential = new NetworkCredential
                {
                    UserName = userName,
                    Password = appPassword
                };

                SmtpClient SmtpServer = new SmtpClient(host, port);
                SmtpServer.DeliveryMethod = SmtpDeliveryMethod.Network;
                SmtpServer.Timeout = 5000;
                SmtpServer.EnableSsl = true;
                SmtpServer.UseDefaultCredentials = false;
                SmtpServer.Credentials = new NetworkCredential(mailFrom, appPassword);
                SmtpServer.Send(emailMessage);

                await Task.CompletedTask;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            
        }

    }
}
