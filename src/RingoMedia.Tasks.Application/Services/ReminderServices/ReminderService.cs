namespace RingoMedia.Tasks.Application.Services.ReminderServices
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using RingoMedia.Tasks.Application.Models;
    using RingoMedia.Tasks.Application.Services.EmailServices;
    using RingoMedia.Tasks.Domain.Context;
    using RingoMedia.Tasks.Domain.DbEntities;
    using RingoMedia.Tasks.Domain.DbEntities.Enums;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class ReminderService : IReminderService
    {
        private readonly RingoMediaDbContext _context;
        private readonly IEmailService _emailService;

        public ReminderService(RingoMediaDbContext context, IEmailService emailService)
        {
            _context = context;
            _emailService = emailService;
        }

        public async Task<List<Reminder>> GetRemindersAsync()
        {
            return await _context.Reminders.OrderByDescending(a => a.DateTime).ToListAsync();
        }

        public async Task<int> CreateReminderAsync(CreateReminderModel ReminderModel)
        {
            var Reminder = new Reminder(ReminderModel.Email, ReminderModel.Title, ReminderModel.DateTime);
            _context.Add(Reminder);
            await _context.SaveChangesAsync();
            ScheduleReminderAsync(Reminder).ConfigureAwait(false);
            return Reminder.Id;
        }

        public async Task<Reminder> UpdateReminderAsync(Reminder reminder)
        {
            _context.Update(reminder);
            await _context.SaveChangesAsync();
            return reminder;
        }

        #region Schedule remider functions
        private async Task ScheduleReminderAsync(Reminder reminder)
        {
            var delay = reminder.DateTime - DateTime.Now;
            if (delay.TotalMilliseconds <= 0)
            {
                await _emailService.SendReminderToEmailAsync(reminder);
                return;
            }
            await Task.Delay(delay).ContinueWith(_ => _emailService.SendReminderToEmailAsync(reminder));
        }
        #endregion
    }
}
