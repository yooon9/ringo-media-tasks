namespace RingoMedia.Tasks.Application.Services.ReminderServices
{
    using RingoMedia.Tasks.Domain.DbEntities;
    using System.Threading.Tasks;

    public interface IScheduleReminderService
    {
        Task AddReminderAsync(Reminder reminder);
    }
}
