namespace RingoMedia.Tasks.Application.Services.EmailServices
{
    using RingoMedia.Tasks.Domain.DbEntities;
    using System.Threading.Tasks;

    public interface IEmailService
    {
        Task<bool> SendAsync(string toEmail, string subject, string body);
        Task SendReminderToEmailAsync(Reminder reminder);
    }
}
