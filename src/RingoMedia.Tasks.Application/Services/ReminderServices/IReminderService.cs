namespace RingoMedia.Tasks.Application.Services.ReminderServices
{
    using RingoMedia.Tasks.Application.Models;
    using RingoMedia.Tasks.Domain.DbEntities;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IReminderService
    {
        Task<int> CreateReminderAsync(CreateReminderModel ReminderModel);
        Task<List<Reminder>> GetRemindersAsync();
    }
}
