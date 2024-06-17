namespace RingoMedia.Tasks.Application.Services.ReminderServices
{
    using RingoMedia.Tasks.Application.Services.EmailServices;
    using RingoMedia.Tasks.Domain.DbEntities;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class ScheduleReminderService : IScheduleReminderService
    {
        private readonly List<Reminder> _reminders = new();
        private readonly IEmailService _emailService;

        public ScheduleReminderService(IEmailService emailService)
        {
            _emailService = emailService;
        }

        public async Task AddReminderAsync(Reminder reminder)
        {
            _reminders.Add(reminder);
            ScheduleReminderAsync(reminder).ConfigureAwait(false);
        }

        private async Task ScheduleReminderAsync(Reminder reminder)
        {
            var delay = reminder.DateTime - DateTime.Now;
            if (delay.TotalMilliseconds <= 0)
            {
                SendReminderAsync(reminder).ConfigureAwait(false);
                return;
            }

            Task.Delay(delay).ContinueWith(_ => SendReminderAsync(reminder)).ConfigureAwait(false);
        }

        private async Task SendReminderAsync(Reminder reminder)
        {
            _emailService.SendAsync(reminder.Email, "Reminder", reminder.Title).ConfigureAwait(false);
        }
    }
}
