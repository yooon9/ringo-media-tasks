namespace RingoMedia.Tasks.Application.Models
{
    using System.ComponentModel.DataAnnotations;

    public sealed class CreateDepartmentModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string LogoUrl { get; set; }

        //Parent department Id
        public int? ParentDepartmentId { get; set; }
    }
}
