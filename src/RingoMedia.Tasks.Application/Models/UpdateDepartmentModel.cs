namespace RingoMedia.Tasks.Application.Models
{
    using System.ComponentModel.DataAnnotations;

    public sealed class UpdateDepartmentModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string LogoUrl { get; set; }

        public int? ParentDepartmentId { get; set; }
    }
}
