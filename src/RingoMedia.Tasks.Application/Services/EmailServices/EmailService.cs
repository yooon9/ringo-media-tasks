namespace RingoMedia.Tasks.Application.Services.EmailServices
{
    using RingoMedia.Tasks.Application.Models;
    using System;
    using System.Net;
    using System.Net.Mail;
    using System.Threading.Tasks;

    public class EmailService : IEmailService
    {
        private readonly EmailSettings _settings;
        //Just run this smtp client when the settings is correct
        //private readonly SmtpClient _smtpClient;

        public EmailService(EmailSettings settings)
        {
            _settings = settings;
            //_smtpClient = new SmtpClient
            //{
            //    Host = settings.Host,
            //    Port = settings.Port,
            //    EnableSsl = settings.SSL,
            //    DeliveryMethod = SmtpDeliveryMethod.Network,
            //    UseDefaultCredentials = false,
            //    Credentials = new NetworkCredential(settings.UserName, settings.Password)
            //};
        }

        public async Task SendAsync(string toEmail, string subject, string body, bool isBodyHtml = true)
        {
            try
            {
                //var mail = new MailMessage(_settings.SenderName, toEmail)
                //{
                //    Subject = subject,
                //    Body = body,
                //    IsBodyHtml = isBodyHtml
                //};
                //_smtpClient.Send(mail);
            }
            catch (Exception e)
            {
            }

            await Task.CompletedTask;
        }
    }
}
