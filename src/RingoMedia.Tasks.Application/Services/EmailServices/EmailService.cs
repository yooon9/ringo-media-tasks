namespace RingoMedia.Tasks.Application.Services.EmailServices
{
    using Microsoft.Extensions.DependencyInjection;
    using RingoMedia.Tasks.Application.Models;
    using RingoMedia.Tasks.Domain.Context;
    using RingoMedia.Tasks.Domain.DbEntities.Enums;
    using RingoMedia.Tasks.Domain.DbEntities;
    using System;
    using System.Net;
    using System.Net.Mail;
    using System.Threading.Tasks;
    using RingoMedia.Tasks.Application.Services.ReminderServices;

    public class EmailService : IEmailService
    {
        private readonly EmailSettings _settings;
        private readonly IServiceProvider _serviceProvider;

        //Just run this smtp client when the settings is correct
        //private readonly SmtpClient _smtpClient;

        public EmailService(EmailSettings settings, IServiceProvider serviceProvider)
        {
            _settings = settings;
            _serviceProvider = serviceProvider;

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

        public async Task<bool> SendAsync(string toEmail, string subject, string body)
        {
            try
            {
                //update reminder status is sent
                //var mail = new MailMessage(_settings.SenderName, toEmail)
                //{
                //    Subject = subject,
                //    Body = body
                //};
                //_smtpClient.Send(mail);
                return true;
            }
            catch (Exception e)
            {
                // console exception log here
            }

            return false;
        }

        public async Task SendReminderToEmailAsync(Reminder reminder)
        {
            if (await SendAsync(reminder.Email, reminder.Title, "Reminder"))
                reminder.SetStatus(ReminderStatus.Sent);
            else
                reminder.SetStatus(ReminderStatus.Error);

            using (var scope = _serviceProvider.CreateScope())
            {
                var reminderService = scope.ServiceProvider.GetRequiredService<IReminderService>();
                await reminderService.UpdateReminderAsync(reminder);
            }
        }
    }
}
