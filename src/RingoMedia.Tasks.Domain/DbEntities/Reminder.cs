namespace RingoMedia.Tasks.Domain.DbEntities
{
    using RingoMedia.Tasks.Domain.DbEntities.Enums;
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Data;

    public class Reminder
    {
        public Reminder(string email, string title, DateTime dateTime)
        {
            Title = title;
            Email = email;
            DateTime = dateTime;
            Status = ReminderStatus.Pending;
        }

        public int Id { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public DateTime DateTime { get; set; }
        [Required]
        public ReminderStatus Status { get; set; }

        public void SetStatus(ReminderStatus status)
        {
            Status = status;
        }
    }
}
