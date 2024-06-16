namespace RingoMedia.Tasks.Application.Services.ReminderServices
{
    using Microsoft.EntityFrameworkCore;
    using RingoMedia.Tasks.Application.Models;
    using RingoMedia.Tasks.Domain.Context;
    using RingoMedia.Tasks.Domain.DbEntities;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class ReminderService : IReminderService
    {
        private readonly RingoMediaDbContext _context;
        private readonly IScheduleReminderService _scheduleReminderService;

        public ReminderService(RingoMediaDbContext context, IScheduleReminderService scheduleReminderService)
        {
            _context = context;
            _scheduleReminderService = scheduleReminderService;
        }

        public async Task<List<Reminder>> GetRemindersAsync()
        {
            return await _context.Reminders.ToListAsync();
        }

        public async Task<int> CreateReminderAsync(CreateReminderModel ReminderModel)
        {
            var Reminder = new Reminder(ReminderModel.Email, ReminderModel.Title, ReminderModel.DateTime);
            _context.Add(Reminder);
            await _context.SaveChangesAsync();
            _scheduleReminderService.AddReminderAsync(Reminder).ConfigureAwait(false);
            return Reminder.Id;
        }
    }
}
