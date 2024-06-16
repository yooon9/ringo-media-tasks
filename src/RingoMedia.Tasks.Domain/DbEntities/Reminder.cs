namespace RingoMedia.Tasks.Domain.DbEntities
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Reminder
    {
        public Reminder(string email, string title, DateTime dateTime)
        {
            Title = title;
            Email = email;
            DateTime = dateTime;
        }

        public int Id { get; set; }
        [Required]
        public string Email { get; set; }

        [Required]
        public string Title { get; set; }
        [Required]
        public DateTime DateTime { get; set; }
    }
}
