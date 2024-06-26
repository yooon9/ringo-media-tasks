﻿namespace RingoMedia.Tasks.Application.Models
{

    using System.ComponentModel.DataAnnotations;

    public sealed class CreateReminderModel
    {
        [Required][EmailAddress] public string Email { get; set; }

        [Required] public string Title { get; set; }

        [Display(Name = "Reminder date time")][Required] public DateTime DateTime { get; set; }
    }
}
