namespace RingoMedia.Tasks.Application.Services.EmailServices
{
    using System.Threading.Tasks;

    public interface IEmailService
    {
        Task SendAsync(string toEmail, string subject, string body);
    }
}
